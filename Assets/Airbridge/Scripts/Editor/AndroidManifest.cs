using System.Xml;
using System.Collections.Generic;

// Exclude a class from the document
/// @cond HIDDEN_SYMBOLS
class AndroidManifest
{
    private const string androidUri = "http://schemas.android.com/apk/res/android";

    private const string manifestTag = "manifest";
    private const string permissionTag = "uses-permission";
    private const string metadataTag = "meta-data";
    private const string contentProviderTag = "provider";
    private const string applicationTag = "application";
    private const string activityTag = "activity";
    private const string intentFilterTag = "intent-filter";

    private const string packageAttrKey = "package";
    private const string themeAttrKey = "theme";
    private const string nameAttrKey = "name";
    private const string valueAttrKey = "value";
    private const string authoritiesAttrKey = "authorities";
    private const string exportedAttrKey = "exported";

    private XmlDocument document = new XmlDocument();

    private XmlElement manifest;
    private List<XmlElement> permissions = new List<XmlElement>();
    private XmlElement application;
    private List<XmlElement> unityActivities = new List<XmlElement>();

    public AndroidManifest(string filename)
    {
        document.Load(filename);

        // Finding element named "manifest"
        if (document.DocumentElement.Name == manifestTag)
        {
            manifest = document.DocumentElement;
        }
        else
        {
            return;
        }

        // Finding element named "uses-permission"
        foreach (XmlNode child in manifest.ChildNodes)
        {
            if (child.Name == permissionTag)
            {
                permissions.Add(child as XmlElement);
            }
        }

        // Finding element named "application"
        foreach (XmlNode child in manifest.ChildNodes)
        {
            if (child.Name == applicationTag)
            {
                application = child as XmlElement;
                break;
            }
        }

        // Finding element named "activity"
        if (application != null)
        {
            foreach (XmlNode child in application.ChildNodes)
            {
                if (child.Name == activityTag)
                {
                    XmlElement metadataElement = FindMetadataXmlElementInChildNodes(child, "unityplayer.UnityActivity");
                    if (metadataElement != null)
                    {
                        string value = metadataElement.GetAttribute(valueAttrKey, androidUri);
                        if (bool.Parse(value))
                        {
                            unityActivities.Add(child as XmlElement);
                        }
                    }
                }
            }
        }
    }

    public void SetPackageName(string packageName)
    {
        manifest.SetAttribute(packageAttrKey, packageName);
    }

    public void SetPermission(string name)
    {
        foreach (XmlElement permission in permissions)
        {
            if (permission.GetAttribute(nameAttrKey, androidUri) == name)
            {
                return;
            }
        }

        XmlElement element = document.CreateElement(permissionTag);
        element.SetAttribute(nameAttrKey, androidUri, name);
        manifest.AppendChild(element);
    }

    public void SetAppMetadata(string name, string value)
    {
        foreach (XmlNode child in application.ChildNodes)
        {
            if (child.Name == metadataTag)
            {
                XmlElement metadata = child as XmlElement;
                if (metadata.GetAttribute(nameAttrKey, androidUri) == name)
                {
                    metadata.SetAttribute(valueAttrKey, androidUri, value);
                    return;
                }
            }
        }

        XmlElement element = document.CreateElement(metadataTag);
        element.SetAttribute(nameAttrKey, androidUri, name);
        element.SetAttribute(valueAttrKey, androidUri, value);
        application.AppendChild(element);
    }

    public void SetContentProvider(string name, string authorities, string exported)
    {
        foreach (XmlNode child in application.ChildNodes)
        {
            if (child.Name == contentProviderTag)
            {
                XmlElement provider = child as XmlElement;
                if (provider.GetAttribute(nameAttrKey, androidUri) == name)
                {
                    provider.SetAttribute(authoritiesAttrKey, androidUri, authorities);
                    provider.SetAttribute(exportedAttrKey, androidUri, exported);
                    return;
                }
            }
        }

        XmlElement element = document.CreateElement(contentProviderTag);
        element.SetAttribute(nameAttrKey, androidUri, name);
        element.SetAttribute(authoritiesAttrKey, androidUri, authorities);
        element.SetAttribute(exportedAttrKey, androidUri, exported);
        application.AppendChild(element);
    }

    public void ReplaceApplicationTheme(List<string> themes, string newTheme)
    {
        if (themes.Contains(application.GetAttribute(themeAttrKey, androidUri)))
        {
            application.SetAttribute(themeAttrKey, androidUri, newTheme);
        }
    }
    
    public void ReplaceActivityName(List<string> names, string newName)
    {
        foreach (XmlElement unityActivity in unityActivities)
        {
            if (names.Contains(unityActivity.GetAttribute(nameAttrKey, androidUri)))
            {
                unityActivity.SetAttribute(nameAttrKey, androidUri, newName);
            }  
        }
    }
    
    public void SetUnityActivityIntentFilter(bool autoVerify, string action, string[] categories, string scheme, string host = "", string path = "")
    {
        if (scheme == null || scheme.Equals(""))
        {
            return;
        }

        foreach (XmlElement unityActivity in unityActivities)
        {
            bool didSetData = false;
            
            foreach (XmlNode child in unityActivity.ChildNodes)
            {
                if (child.Name == intentFilterTag)
                {
                    IntentFilter filter = new IntentFilter(document, child as XmlElement);
                    if (filter.HasData(scheme, host, path))
                    {
                        filter.SetAutoVerify(autoVerify);
                        filter.SetAction(action);
                        foreach (string category in categories)
                        {
                            filter.SetCategory(category);
                        }

                        filter.SetData(scheme, host, path);
                        didSetData = true;
                        break;
                    }
                }
            }

            if (!didSetData)
            {
                XmlElement element = document.CreateElement(intentFilterTag);
                unityActivity.AppendChild(element);

                IntentFilter newFilter = new IntentFilter(document, element);
                newFilter.SetAutoVerify(autoVerify);
                newFilter.SetAction(action);
                foreach (string category in categories)
                {
                    newFilter.SetCategory(category);
                }

                newFilter.SetData(scheme, host, path);
            }
        }
    }

    public void SetUnityActivityAppLinksIntentFilter(string host)
    {
        foreach (var httpScheme in new string[] { "http", "https" })
        {
            SetUnityActivityIntentFilter(
                true,
                "android.intent.action.VIEW",
                new string[] { "android.intent.category.DEFAULT", "android.intent.category.BROWSABLE" },
                httpScheme,
                host
            );
        }
    }

    public void RemoveUnityActivityIntentFilter(string scheme, string host = "", string path = "")
    {
        if (scheme == null || scheme.Equals(""))
        {
            return;
        }

        foreach (XmlElement unityActivity in unityActivities)
        {
            foreach (XmlNode child in unityActivity.ChildNodes)
            {
                if (child.Name == intentFilterTag)
                {
                    IntentFilter filter = new IntentFilter(document, child as XmlElement);
                    if (filter.HasData(scheme, host, path))
                    {
                        filter.Remove();
                    }
                }
            }
        }
    }

    public void Save(string filename)
    {
        document.Save(filename);
    }

    private XmlElement FindMetadataXmlElementInChildNodes(XmlNode node, string name)
    {
        foreach (XmlNode child in node.ChildNodes)
        {
            if (child.Name == metadataTag)
            {
                XmlElement element = child as XmlElement;
                if (element.GetAttribute(nameAttrKey, androidUri) == name)
                {
                    return element;
                }
            }
        }

        return null;
    }

    private class IntentFilter
    {
        private const string actionTag = "action";
        private const string categoryTag = "category";
        private const string dataTag = "data";

        private const string autoVerifyAttrKey = "autoVerify";
        private const string hostAttrKey = "host";
        private const string schemeAttrKey = "scheme";
        private const string pathAttrKey = "path";

        private XmlDocument document;
        private XmlElement root;
        private XmlElement action;
        private List<XmlElement> data = new List<XmlElement>();
        private List<XmlElement> categories = new List<XmlElement>();
        
        public IntentFilter(XmlDocument document, XmlElement element)
        {
            this.document = document;
            root = element;

            // Finding element named "action"
            foreach (XmlNode child in element.ChildNodes)
            {
                if (child.Name == actionTag)
                {
                    action = child as XmlElement;
                    break;
                }
            }

            // Finding element named "category"
            foreach (XmlNode child in element.ChildNodes)
            {
                if (child.Name == categoryTag)
                {
                    categories.Add(child as XmlElement);
                }
            }

            // Finding element named "data"
            foreach (XmlNode child in element.ChildNodes)
            {
                if (child.Name == dataTag)
                {
                    data.Add(child as XmlElement);
                }
            }
        }

        public void SetAutoVerify(bool autoVerify)
        {
            root.SetAttribute(autoVerifyAttrKey, androidUri, autoVerify.ToString().ToLower());
        }

        public void SetAction(string name)
        {
            if (action == null)
            {
                action = document.CreateElement(actionTag);
                root.AppendChild(action);
            }

            action.SetAttribute(nameAttrKey, androidUri, name);
        }

        public bool HasCategory(string name)
        {
            foreach (XmlElement element in categories)
            {
                if (element.GetAttribute(nameAttrKey, androidUri) == name)
                {
                    return true;
                }
            }

            return false;
        }

        public void SetCategory(string name)
        {
            if (HasCategory(name))
            {
                return;
            }

            XmlElement element = document.CreateElement(categoryTag);
            element.SetAttribute(nameAttrKey, androidUri, name);
            root.AppendChild(element);
            categories.Add(element);
        }

        public bool HasData(string scheme, string host = "", string path = "")
        {
            return GetData(scheme, host, path) != null;
        }

        public void SetData(string scheme, string host = "", string path = "")
        {
            XmlElement element = GetData(scheme, host, path);
            if (element == null)
            {
                element = document.CreateElement(dataTag);
                root.AppendChild(element);
                data.Add(element);
            }

            element.SetAttribute(schemeAttrKey, androidUri, scheme);
            if (!string.IsNullOrEmpty(host))
            {
                element.SetAttribute(hostAttrKey, androidUri, host);
            }

            if (!string.IsNullOrEmpty(path))
            {
                element.SetAttribute(pathAttrKey, androidUri, path);
            }
        }

        private XmlElement GetData(string scheme, string host = "", string path = "")
        {
            foreach (XmlElement element in data)
            {
                if (element.GetAttribute(schemeAttrKey, androidUri) != scheme)
                {
                    continue;
                }

                if (!string.IsNullOrEmpty(host))
                {
                    string matchingAirbridgeAppLinksHost = "";
                    foreach (var airbridgeAppLinksHost in new string[] { "abr.ge", "airbridge.io", "deeplink.page" })
                    {
                        if (host.EndsWith(airbridgeAppLinksHost) && host.Split('.').Length == 3)
                        {
                            matchingAirbridgeAppLinksHost = airbridgeAppLinksHost;
                        }
                    }

                    if (string.IsNullOrEmpty(matchingAirbridgeAppLinksHost))
                    {
                        if (element.GetAttribute(hostAttrKey, androidUri) != host)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (!element.GetAttribute(hostAttrKey, androidUri).EndsWith(matchingAirbridgeAppLinksHost))
                        {
                            continue;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(path))
                {
                    if (element.GetAttribute(pathAttrKey, androidUri) != path)
                    {
                        continue;
                    }
                }

                return element;
            }

            return null;
        }
        
        public void Remove()
        {
            if (root.ParentNode != null)
            {
                root.ParentNode.RemoveChild(root);
            }
        }
    }
}
// ReSharper disable once InvalidXmlDocComment
/// @endcond