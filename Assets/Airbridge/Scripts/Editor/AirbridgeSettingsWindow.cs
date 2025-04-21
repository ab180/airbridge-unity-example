#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

[SuppressMessage("ReSharper", "CheckNamespace")]
public class AirbridgeSettingsWindow : EditorWindow
{
    [MenuItem("AB180/Airbridge Settings")]
    public static void ShowWindow()
    {
        GetWindow(typeof(AirbridgeSettingsWindow), true, "Airbridge Settings");
    }

    private void OnGUI()
    {
        EditorGUIUtility.labelWidth = 240;
        SerializedObject serializedAirbridgeData = new SerializedObject(AirbridgeData.GetInstance());

        SerializedProperty appNameProperty = serializedAirbridgeData.FindProperty("appName");
        EditorGUILayout.PropertyField(appNameProperty, new GUILayoutOption[] { });
        
        SerializedProperty appTokenProperty = serializedAirbridgeData.FindProperty("appToken");
        EditorGUILayout.PropertyField(appTokenProperty, new GUILayoutOption[] { });

        SerializedProperty logLevel = serializedAirbridgeData.FindProperty("logLevel");
        logLevel.intValue = EditorGUILayout.Popup("Log Level", logLevel.intValue, AirbridgeLogLevel.LogLevel);
        
        SerializedProperty iosUriSchemeProperty = serializedAirbridgeData.FindProperty("iosUriScheme");
        EditorGUILayout.PropertyField(iosUriSchemeProperty, new GUIContent("iOS URI Scheme"), new GUILayoutOption[] { });

        SerializedProperty androidUriSchemeProperty = serializedAirbridgeData.FindProperty("androidURIScheme");
        EditorGUILayout.PropertyField(androidUriSchemeProperty, new GUILayoutOption[] { });

        SerializedProperty customDomainProperty = serializedAirbridgeData.FindProperty("customDomain");
        EditorGUILayout.PropertyField(customDomainProperty, new GUILayoutOption[] { });

        SerializedProperty sessionTimeoutSecondsProperty = serializedAirbridgeData.FindProperty("sessionTimeoutSeconds");
        EditorGUILayout.PropertyField(sessionTimeoutSecondsProperty, new GUILayoutOption[] { });

        SerializedProperty userInfoHashEnabledProperty = serializedAirbridgeData.FindProperty("userInfoHashEnabled");
        EditorGUILayout.PropertyField(userInfoHashEnabledProperty, new GUILayoutOption[] { });

        SerializedProperty locationCollectionEnabledProperty = serializedAirbridgeData.FindProperty("locationCollectionEnabled");
        EditorGUILayout.PropertyField(locationCollectionEnabledProperty, new GUILayoutOption[] { });

        SerializedProperty trackAirbridgeLinkOnlyProperty = serializedAirbridgeData.FindProperty("trackAirbridgeLinkOnly");
        EditorGUILayout.PropertyField(trackAirbridgeLinkOnlyProperty, new GUILayoutOption[] { });

        SerializedProperty autoStartTrackingEnabledProperty = serializedAirbridgeData.FindProperty("autoStartTrackingEnabled");
        EditorGUILayout.PropertyField(autoStartTrackingEnabledProperty, new GUILayoutOption[] { });

        SerializedProperty facebookDeferredAppLinkEnabledProperty = serializedAirbridgeData.FindProperty("facebookDeferredAppLinkEnabled");
        EditorGUILayout.PropertyField(facebookDeferredAppLinkEnabledProperty, new GUILayoutOption[] { });

        SerializedProperty iOSTrackingAuthorizeTimeoutSecondsProperty = serializedAirbridgeData.FindProperty("iOSTrackingAuthorizeTimeoutSeconds");
        EditorGUILayout.PropertyField(iOSTrackingAuthorizeTimeoutSecondsProperty, new GUILayoutOption[] { });

        SerializedProperty sdkSignatureSecretIDProperty = serializedAirbridgeData.FindProperty("sdkSignatureSecretID");
        EditorGUILayout.PropertyField(sdkSignatureSecretIDProperty, new GUILayoutOption[] { });
        
        SerializedProperty sdkSignatureSecretProperty = serializedAirbridgeData.FindProperty("sdkSignatureSecret");
        EditorGUILayout.PropertyField(sdkSignatureSecretProperty, new GUILayoutOption[] { });
        
        SerializedProperty facebookAppIdProperty = serializedAirbridgeData.FindProperty("facebookAppId");
        EditorGUILayout.PropertyField(facebookAppIdProperty, new GUIContent("Meta Install Referrer (Facebook App ID)"), new GUILayoutOption[] { });
        
        SerializedProperty inAppPurchaseEnvironment = serializedAirbridgeData.FindProperty("inAppPurchaseEnvironment");
        inAppPurchaseEnvironment.intValue = EditorGUILayout.Popup(
            "In-App Purchase Environment",
            inAppPurchaseEnvironment.intValue,
            AirbridgeInAppPurchaseEnvironmentExtension.Environments
        );
        
        GUILayout.FlexibleSpace();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Update iOS Settings", new GUILayoutOption[] { GUILayout.Height(30) }))
        {
            UpdateiOSAppSetting();
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        if (GUILayout.Button("Update Android Settings", new GUILayoutOption[] { GUILayout.Height(30) }))
        {
            UpdateAndroidNativeCode();
            UpdateAndroidAirbridgeSettings();
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        if (GUI.changed)
        {
            serializedAirbridgeData.ApplyModifiedProperties();
            EditorUtility.SetDirty(AirbridgeData.GetInstance());
            AssetDatabase.SaveAssets();
        }
    }

    internal static void UpdateAndroidNativeCode()
    {
        AirbridgeAndroidApplicationEntry entry = AirbridgeAndroidApplicationEntry.None;
#if UNITY_2023_1_OR_NEWER
                entry = PlayerSettings.Android.applicationEntry.ConvertToAirbridgeType();
#else
        entry = AirbridgeAndroidApplicationEntry.Activity;
#endif
        
        UpdateAndroidActivity(entry);
        UpdateAndroidManifest(entry);
    }
    
    private static void UpdateAndroidActivity(AirbridgeAndroidApplicationEntry entry)
    {
        try
        {
            string activityDirPath = Path.Combine(Application.dataPath, "Plugins/Android");
            
            string airbridgeActivityPath = Path.Combine(activityDirPath, AirbridgeAndroidApplicationEntry.Activity.GetActivityFileName());
            string airbridgeGameActivityPath = Path.Combine(activityDirPath, AirbridgeAndroidApplicationEntry.GameActivity.GetActivityFileName());
            
            // Reset
            if (File.Exists(airbridgeActivityPath)) { File.Delete(airbridgeActivityPath); }
            if (File.Exists(airbridgeGameActivityPath)) { File.Delete(airbridgeGameActivityPath); }
            
            if (entry.IsNone()) { return; }

            string defaultActivityPath = Path.Combine(Application.dataPath,
                "Plugins/Airbridge/Android/java/co/ab180/airbridge/unity",
                entry.GetActivityFileName() + ".template"
            );
            string activityPath = Path.Combine(activityDirPath, entry.GetActivityFileName());
            
            // Create
            if (!Directory.Exists(activityDirPath))
            {
                Directory.CreateDirectory(activityDirPath);
                Debug.LogFormat("Create Android Airbridge Activity directiory : {0}", activityDirPath);
            }
            File.Copy(defaultActivityPath, activityPath);
            Debug.LogFormat("Copied default Android Airbridge Activity file from \'{0}\'", defaultActivityPath);
        }
        catch (Exception exception)
        {
            Debug.LogErrorFormat("Something broken while updating Android Airbridge Activity file : {0}", exception);
        }
    }
    
    private static void UpdateAndroidManifest(AirbridgeAndroidApplicationEntry entry)
    {
        try
        {
            string manifestDirPath = Path.Combine(Application.dataPath, "Plugins/Android");
            string manifestPath = Path.Combine(manifestDirPath, "AndroidManifest.xml");

            if (!entry.IsNone())
            {
                string defaultManifestPath = Path.Combine(Application.dataPath, "Plugins/Airbridge/Android", entry.GetManifestFileName());
                
                if (!File.Exists(manifestPath))
                {
                    Debug.Log("Couldn't find any Android App Manifest file");

                    if (!Directory.Exists(manifestDirPath))
                    {
                        Directory.CreateDirectory(manifestDirPath);
                        Debug.LogFormat("Create Android App Manifest directiory : {0}", manifestDirPath);
                    }

                    File.Copy(defaultManifestPath, manifestPath);
                    Debug.LogFormat("Copied default Android App Manifest file from \'{0}\'", defaultManifestPath);
                }
            }
            
            AndroidManifest manifest = new AndroidManifest(manifestPath);
            manifest.SetPackageName(Application.identifier);
            manifest.SetPermission("android.permission.INTERNET");
            manifest.SetPermission("android.permission.ACCESS_NETWORK_STATE");

            manifest.SetContentProvider("co.ab180.airbridge.unity.AirbridgeContentProvider",
                "${applicationId}.co.ab180.airbridge.unity.AirbridgeContentProvider", "false");

            if (!entry.IsNone())
            {
                manifest.ReplaceApplicationTheme(
                    AirbridgeAndroidApplicationEntryExtension.ApplicationThemes.ToList(),
                    entry.GetApplicationTheme()
                );
                manifest.ReplaceActivityName(
                    AirbridgeAndroidApplicationEntryExtension.ActivityNames.ToList(), 
                    entry.GetActivityName()
                );
            }
            
            // Airbridge App Links
            if (!string.IsNullOrEmpty(AirbridgeData.GetInstance().appName))
            {
                manifest.SetUnityActivityAppLinksIntentFilter($"{AirbridgeData.GetInstance().appName}.abr.ge");
                manifest.SetUnityActivityAppLinksIntentFilter($"{AirbridgeData.GetInstance().appName}.airbridge.io");
                manifest.SetUnityActivityAppLinksIntentFilter($"{AirbridgeData.GetInstance().appName}.deeplink.page");
            }

            // URI Scheme of the deep link
            if (!string.IsNullOrEmpty(AirbridgeData.GetInstance().androidURIScheme))
            {
                manifest.SetUnityActivityIntentFilter(
                    false,
                    "android.intent.action.VIEW",
                    new[] { "android.intent.category.DEFAULT", "android.intent.category.BROWSABLE" },
                    AirbridgeData.GetInstance().androidURIScheme
                );
            }

            // Custom Domain
            if (!string.IsNullOrEmpty(AirbridgeData.GetInstance().customDomain))
            {
                manifest.SetUnityActivityAppLinksIntentFilter(AirbridgeData.GetInstance().customDomain);
            }

            manifest.Save(manifestPath);
            
            Debug.Log("Updated Android App Manifest (AndroidManifest.xml)");
        }
        catch (Exception exception)
        {
            Debug.LogErrorFormat("Something broken while updating Android App Manifest file : {0}", exception);
        }
    }

    internal static void UpdateAndroidAirbridgeSettings()
    {
        try {
            string path = Path.Combine(Application.dataPath, "Plugins/Airbridge/Android/java/co/ab180/airbridge/unity/AirbridgeSettings.java");
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }

            string content = 
                "package co.ab180.airbridge.unity;\n"
                + "\n"
                + "public class AirbridgeSettings {\n"
                + "\n"
                + "public static String appName = \"" + AirbridgeData.GetInstance().appName + "\";\n"
                + "public static String appToken = \"" + AirbridgeData.GetInstance().appToken + "\";\n"
                + "public static String sdkSignatureSecretID = \"" + AirbridgeData.GetInstance().sdkSignatureSecretID + "\";\n"
                + "public static String sdkSignatureSecret = \"" + AirbridgeData.GetInstance().sdkSignatureSecret + "\";\n"
                + "public static int logLevel = " + AirbridgeLogLevel.GetAndroidLogLevel(AirbridgeData.GetInstance().logLevel) + ";\n"
                + "public static String customDomain = \"" + AirbridgeData.GetInstance().customDomain + "\";\n"
                + "public static int sessionTimeoutSeconds = " + AirbridgeData.GetInstance().sessionTimeoutSeconds + ";\n"
                + "public static boolean userInfoHashEnabled = " + AirbridgeData.GetInstance().userInfoHashEnabled.ToString().ToLower() + ";\n"
                + "public static boolean locationCollectionEnabled = " + AirbridgeData.GetInstance().locationCollectionEnabled.ToString().ToLower() + ";\n"
                + "public static boolean trackAirbridgeLinkOnly = " + AirbridgeData.GetInstance().trackAirbridgeLinkOnly.ToString().ToLower() + ";\n"
                + "public static boolean autoStartTrackingEnabled = " + AirbridgeData.GetInstance().autoStartTrackingEnabled.ToString().ToLower() + ";\n"
                + "public static boolean facebookDeferredAppLinkEnabled = " + AirbridgeData.GetInstance().facebookDeferredAppLinkEnabled.ToString().ToLower() + ";\n"
                + "public static String facebookAppId = \"" + AirbridgeData.GetInstance().facebookAppId + "\";\n"
                + "public static String inAppPurchaseEnvironment = \"" + AirbridgeData.GetInstance().inAppPurchaseEnvironment.GetStringValue() + "\";\n"
                + "\n"
                + "}\n";

            File.WriteAllText(path, content);
            
            Debug.Log("Updated Android Airbridge settings (AirbridgeSettings.java)");
        }
        catch (Exception exception)
        {
            Debug.LogErrorFormat("Something broken while updating Android Airbridge settings file : {0}", exception);
        }
    }

    internal static void UpdateiOSAppSetting()
    {
        try
        {
            string path = Path.Combine(Application.dataPath, "Plugins/Airbridge/iOS/Delegate/AUAppSetting.h");
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }

            string content = 
                "#ifndef AUAppSetting_h\n"
                + "#define AUAppSetting_h\n"
                + "\n"
                + "static NSString* appName = @\"" + AirbridgeData.GetInstance().appName + "\";\n"
                + "static NSString* appToken = @\"" + AirbridgeData.GetInstance().appToken + "\";\n"
                + "static NSString* sdkSignatureSecretID = @\"" + AirbridgeData.GetInstance().sdkSignatureSecretID + "\";\n"
                + "static NSString* sdkSignatureSecret = @\"" + AirbridgeData.GetInstance().sdkSignatureSecret + "\";\n"
                + "static NSUInteger logLevel = " + AirbridgeLogLevel.GetIOSLogLevel(AirbridgeData.GetInstance().logLevel) + ";\n"
                + "static NSString* appScheme = @\"" + AirbridgeData.GetInstance().iosUriScheme + "\";\n"
                + "static NSInteger sessionTimeoutSeconds = " + AirbridgeData.GetInstance().sessionTimeoutSeconds + ";\n"
                + "static BOOL autoStartTrackingEnabled = " + AirbridgeData.GetInstance().autoStartTrackingEnabled.ToString().ToLower() + ";\n"
                + "static BOOL userInfoHashEnabled = " + AirbridgeData.GetInstance().userInfoHashEnabled.ToString().ToLower() + ";\n"
                + "static BOOL trackAirbridgeLinkOnly = " + AirbridgeData.GetInstance().trackAirbridgeLinkOnly.ToString().ToLower() + ";\n"
                + "static BOOL facebookDeferredAppLinkEnabled = " + AirbridgeData.GetInstance().facebookDeferredAppLinkEnabled.ToString().ToLower() + ";\n"
                + "static NSInteger trackingAuthorizeTimeoutSeconds = " + AirbridgeData.GetInstance().iOSTrackingAuthorizeTimeoutSeconds + ";\n"
                + "static NSString* inAppPurchaseEnvironment = @\"" + AirbridgeData.GetInstance().inAppPurchaseEnvironment.GetStringValue() + "\";\n"
                + "\n"
                + "#endif\n";

            File.WriteAllText(path, content);
            
            Debug.Log("Updated iOS Airbridge settings (AUAppSetting.h)");
        }
        catch (Exception exception)
        {
            Debug.LogErrorFormat("Something broken while updating iOS Airbridge settings file : {0}", exception);
        }
    }
}

#endif