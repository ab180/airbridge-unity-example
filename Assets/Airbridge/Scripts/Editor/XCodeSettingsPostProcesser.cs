#if UNITY_IOS && UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.iOS.Xcode;
using UnityEditor.Callbacks;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class AirbridgeXcodeSettingsProcesser
{
    [PostProcessBuild]
    public static void OnPostBuild(BuildTarget buildTarget, string pathToBuiltProject)
    {
        if (buildTarget != BuildTarget.iOS) return;
        
        List<string> universalLink = new List<string>();
        if (!string.IsNullOrEmpty(AirbridgeData.GetInstance().appName))
        {
            universalLink.Add(string.Format("{0}.abr.ge", AirbridgeData.GetInstance().appName));
            universalLink.Add(string.Format("{0}.airbridge.io", AirbridgeData.GetInstance().appName));
            universalLink.Add(string.Format("{0}.deeplink.page", AirbridgeData.GetInstance().appName));
        }
        
        AddUniversalLink(pathToBuiltProject, universalLink
            .Union(AirbridgeData.GetInstance().CustomDomains())
            .ToArray()
        );
        
        if (!string.IsNullOrEmpty(AirbridgeData.GetInstance().iOSURIScheme))
        {
            AddScheme(pathToBuiltProject, AirbridgeData.GetInstance().iOSURIScheme);   
        }
        
        AddCustomDomainResInPlist(pathToBuiltProject);
    }

    private static void AddUniversalLink(string pathProject, string[] hosts)
    {
        List<string> applinks = new List<string>();
        string pathPBXProject = Path.Combine(pathProject, "Unity-iPhone.xcodeproj/project.pbxproj");
        string target = "Unity-iPhone";
        string entitlements = "Unity-iPhone.entitlements";

        foreach (string host in hosts)
        {
            if (!string.IsNullOrEmpty(host))
            {
                applinks.Add(string.Format("applinks:{0}", host));
            }
        }

        ProjectCapabilityManager manager = new ProjectCapabilityManager(pathPBXProject, entitlements, target);
        manager.AddAssociatedDomains(applinks.ToArray());

        manager.WriteToFile();
    }

    private static void AddScheme(string pathProject, string scheme)
    {
        if (scheme == null || scheme.Equals(""))
        {
            return;
        }

        PlistDocument plist = new PlistDocument();
        string pathPlist = Path.Combine(pathProject, "Info.plist");
        plist.ReadFromString(File.ReadAllText(pathPlist));

        PlistElementDict root = plist.root;

        PlistElementArray urlTypes;
        if (!root.values.ContainsKey("CFBundleURLTypes"))
        {
            urlTypes = root.CreateArray("CFBundleURLTypes");
        }
        else
        {
            urlTypes = root.values["CFBundleURLTypes"].AsArray();
        }

        PlistElementDict urlType;
        if (urlTypes.values.Count == 0)
        {
            urlType = urlTypes.AddDict();
        }
        else
        {
            urlType = urlTypes.values[0].AsDict();
        }

        PlistElementArray schemes;
        if (!urlType.values.ContainsKey("CFBundleURLSchemes"))
        {
            schemes = urlType.CreateArray("CFBundleURLSchemes");
        }
        else
        {
            schemes = urlType.values["CFBundleURLSchemes"].AsArray();
        }

        foreach (PlistElement schemeElement in schemes.values)
        {
            if (schemeElement.AsString().Equals(scheme))
            {
                return;
            }
        }

        schemes.AddString(scheme);

        File.WriteAllText(pathPlist, plist.WriteToString());
    }

    private static void AddCustomDomainResInPlist(string pathProject)
    {
        const string customDomainsKey = "co.ab180.airbridge.trackingLink.customDomains";

        List<string> customDomainList = AirbridgeData.GetInstance().CustomDomains();
        if (customDomainList.Count == 0)
        {
            return;
        }
        
        PlistDocument plist = new PlistDocument();
        string pathPlist = Path.Combine(pathProject, "Info.plist");
        plist.ReadFromString(File.ReadAllText(pathPlist));

        PlistElementDict root = plist.root;
        PlistElementArray customDomains;
        if (root.values.ContainsKey(customDomainsKey))
        {
            root.values.Remove(customDomainsKey);
        }
        customDomains = root.CreateArray(customDomainsKey);
        
        customDomainList.ForEach(customDomain =>
        {
            customDomains.AddString(customDomain);
        });
        
        File.WriteAllText(pathPlist, plist.WriteToString());
    }

    private static void AddOSFrameworks(string pathProject, string[] frameworks)
    {
        string pathPBXProject = Path.Combine(pathProject, "Unity-iPhone.xcodeproj/project.pbxproj");

        PBXProject project = new PBXProject();
        project.ReadFromString(File.ReadAllText(pathPBXProject));
#if UNITY_2019_3_OR_NEWER
        string guidTarget = project.GetUnityMainTargetGuid();
#else
            string guidTarget = project.TargetGuidByName(PBXProject.GetUnityTargetName());
#endif
        foreach (string framework in frameworks)
        {
            project.AddFrameworkToProject(guidTarget, framework, true);
        }

        File.WriteAllText(pathPBXProject, project.WriteToString());
    }
}
#endif