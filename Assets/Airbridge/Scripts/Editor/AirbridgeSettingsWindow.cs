#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Linq;

class AirbridgeSettingsWindow : EditorWindow
{
    private readonly AirbridgeArrayDataHandler customDomainHandler = new AirbridgeArrayDataHandler("Custom Domain", "URL");
    
    [MenuItem("Airbridge/Airbridge Settings")]
    public static void ShowWindow()
    {
        GetWindow(typeof(AirbridgeSettingsWindow), true, "Airbridge Settings");
    }

    private void OnGUI()
    {
        EditorGUIUtility.labelWidth = 300;
        SerializedObject serializedAirbridgeData = new SerializedObject(AirbridgeData.GetInstance());

        SerializedProperty appNameProperty = serializedAirbridgeData.FindProperty("appName");
        EditorGUILayout.PropertyField(appNameProperty, new GUILayoutOption[] { });
        
        SerializedProperty appTokenProperty = serializedAirbridgeData.FindProperty("appToken");
        EditorGUILayout.PropertyField(appTokenProperty, new GUILayoutOption[] { });

        SerializedProperty logLevel = serializedAirbridgeData.FindProperty("logLevel");
        logLevel.intValue = EditorGUILayout.Popup("Log Level", logLevel.intValue, AirbridgeLogLevel.LogLevel);
        
        SerializedProperty iOSURISchemeProperty = serializedAirbridgeData.FindProperty("iOSURIScheme");
        EditorGUILayout.PropertyField(iOSURISchemeProperty, new GUIContent("iOS URI Scheme"), new GUILayoutOption[] { });

        SerializedProperty androidURISchemeProperty = serializedAirbridgeData.FindProperty("androidURIScheme");
        EditorGUILayout.PropertyField(androidURISchemeProperty, new GUILayoutOption[] { });

        SerializedProperty customDomainProperty = serializedAirbridgeData.FindProperty("customDomain");
        customDomainHandler.SetProperty(customDomainProperty);
        customDomainHandler.Draw();
        customDomainHandler.RemoveAction += RemoveCustomDomain;

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
        EditorGUILayout.PropertyField(iOSTrackingAuthorizeTimeoutSecondsProperty, new GUIContent("iOS Tracking Authorize Timeout Seconds"), new GUILayoutOption[] { });

        SerializedProperty sdkSignatureSecretIDProperty = serializedAirbridgeData.FindProperty("sdkSignatureSecretID");
        EditorGUILayout.PropertyField(sdkSignatureSecretIDProperty, new GUIContent("SDK Signature Secret ID"), new GUILayoutOption[] { });
        
        SerializedProperty sdkSignatureSecretProperty = serializedAirbridgeData.FindProperty("sdkSignatureSecret");
        EditorGUILayout.PropertyField(sdkSignatureSecretProperty, new GUIContent("SDK Signature Secret"), new GUILayoutOption[] { });
        
        SerializedProperty trackInSessionLifeCycleEventEnabledProperty = serializedAirbridgeData.FindProperty("trackInSessionLifeCycleEventEnabled");
        EditorGUILayout.PropertyField(trackInSessionLifeCycleEventEnabledProperty, new GUILayoutOption[] { });
        
        SerializedProperty pauseEventTransmitOnBackgroundEnabledProperty = serializedAirbridgeData.FindProperty("pauseEventTransmitOnBackgroundEnabled");
        EditorGUILayout.PropertyField(pauseEventTransmitOnBackgroundEnabledProperty, new GUILayoutOption[] { });
        
        SerializedProperty resetEventBufferEnabledProperty = serializedAirbridgeData.FindProperty("resetEventBufferEnabled");
        EditorGUILayout.PropertyField(resetEventBufferEnabledProperty, new GUIContent("Clear Event Buffer On Initialize Enabled"), new GUILayoutOption[] { });
  
        SerializedProperty sdkEnabledProperty = serializedAirbridgeData.FindProperty("sdkEnabled");
        EditorGUILayout.PropertyField(sdkEnabledProperty, new GUIContent("SDK Enabled"), new GUILayoutOption[] { });
        
        SerializedProperty appMarketIdentifierProperty = serializedAirbridgeData.FindProperty("appMarketIdentifier");
        EditorGUILayout.PropertyField(appMarketIdentifierProperty, new GUILayoutOption[] { });
        
        SerializedProperty eventMaximumBufferCountProperty = serializedAirbridgeData.FindProperty("eventMaximumBufferCount");
        EditorGUILayout.PropertyField(eventMaximumBufferCountProperty, new GUIContent("Event Buffer Count Limit"), new GUILayoutOption[] { });
        
        SerializedProperty eventMaximumBufferSizeProperty = serializedAirbridgeData.FindProperty("eventMaximumBufferSize");
        EditorGUILayout.PropertyField(eventMaximumBufferSizeProperty, new GUIContent("Event Buffer Size Limit In Gibibyte"), new GUILayoutOption[] { });
  
        SerializedProperty eventTransmitIntervalSecondsProperty = serializedAirbridgeData.FindProperty("eventTransmitIntervalSeconds");
        EditorGUILayout.PropertyField(eventTransmitIntervalSecondsProperty, new GUILayoutOption[] { });
        
        SerializedProperty facebookAppIdProperty = serializedAirbridgeData.FindProperty("facebookAppId");
        EditorGUILayout.PropertyField(facebookAppIdProperty, new GUIContent("Meta Install Referrer (Facebook App ID)"), new GUILayoutOption[] { });
        
        SerializedProperty isHandleAirbridgeDeeplinkOnlyProperty = serializedAirbridgeData.FindProperty("isHandleAirbridgeDeeplinkOnly");
        EditorGUILayout.PropertyField(isHandleAirbridgeDeeplinkOnlyProperty, new GUIContent("Is Handle Airbridge Deeplink Only"), new GUILayoutOption[] { });
        
        EditorGUILayout.Space();

        if (GUI.changed)
        {
            serializedAirbridgeData.ApplyModifiedProperties();
            EditorUtility.SetDirty(AirbridgeData.GetInstance());
            AssetDatabase.SaveAssets();
        }
    }

    private void RemoveCustomDomain(string host)
    {
        string manifestPath = Path.Combine(Application.dataPath, "Plugins/Android/AndroidManifest.xml");

        if (!File.Exists(manifestPath)) return;
        
        AndroidManifest manifest = new AndroidManifest(manifestPath);
        
        manifest.RemoveUnityActivityIntentFilter("http", host);
        manifest.RemoveUnityActivityIntentFilter("https", host);

        manifest.Save(manifestPath);
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
        string pluginPath = AirbridgeUtils.GetPluginPath(AirbridgeUtils.Platform.Android);
        if (pluginPath == null) { return; }
        
        try
        {
            string airbridgeActivityPath = Path.Combine(pluginPath, AirbridgeAndroidApplicationEntry.Activity.GetActivityFileName());
            string airbridgeGameActivityPath = Path.Combine(pluginPath, AirbridgeAndroidApplicationEntry.GameActivity.GetActivityFileName());
            
            // Reset
            if (File.Exists(airbridgeActivityPath)) { File.Delete(airbridgeActivityPath); }
            if (File.Exists(airbridgeGameActivityPath)) { File.Delete(airbridgeGameActivityPath); }
            
            if (entry.IsNone()) { return; }
            
            string defaultActivityPath = Path.Combine(
                AirbridgeUtils.GetUnityPackageAssetsPath(),
                "Plugins/Airbridge/Android/java/co/ab180/airbridge/unity",
                entry.GetActivityFileName() + ".template"
            );
            string activityPath = Path.Combine(pluginPath, entry.GetActivityFileName());
            
            // Copy
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
                string defaultManifestPath = Path.Combine(
                    AirbridgeUtils.GetUnityPackageAssetsPath(),
                    "Plugins/Airbridge/Android",
                    entry.GetManifestFileName()
                );
                
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
                manifest.SetUnityActivityAppLinksIntentFilter(string.Format("{0}.abr.ge", AirbridgeData.GetInstance().appName));
                manifest.SetUnityActivityAppLinksIntentFilter(string.Format("{0}.airbridge.io", AirbridgeData.GetInstance().appName));
                manifest.SetUnityActivityAppLinksIntentFilter(string.Format("{0}.deeplink.page", AirbridgeData.GetInstance().appName));
            }

            // URI Scheme of the deep link
            if (!string.IsNullOrEmpty(AirbridgeData.GetInstance().androidURIScheme))
            {
                manifest.SetUnityActivityIntentFilter(
                    false,
                    "android.intent.action.VIEW",
                    new string[] { "android.intent.category.DEFAULT", "android.intent.category.BROWSABLE" },
                    AirbridgeData.GetInstance().androidURIScheme
                );
            }

            // Custom Domain
            AirbridgeData.GetInstance().CustomDomains().ForEach(customDomain =>
            {
                manifest.SetUnityActivityAppLinksIntentFilter(customDomain);
            });
            
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
        string pluginPath = AirbridgeUtils.GetPluginPath(AirbridgeUtils.Platform.Android);
        if (pluginPath == null) { return; }
        
        try
        {
            string settingsPath = Path.Combine(pluginPath, "AirbridgeSettings.java");
            
            if (!File.Exists(settingsPath))
            {
                File.Create(settingsPath).Dispose();
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
                + "public static int logLevel = " + AirbridgeData.GetInstance().logLevel + ";\n"
                + "public static String customDomain = \"" + AirbridgeData.GetInstance().customDomain + "\";\n"
                + "public static int sessionTimeoutSeconds = " + AirbridgeData.GetInstance().sessionTimeoutSeconds + ";\n"
                + "public static boolean userInfoHashEnabled = " + AirbridgeData.GetInstance().userInfoHashEnabled.ToString().ToLower() + ";\n"
                + "public static boolean locationCollectionEnabled = " + AirbridgeData.GetInstance().locationCollectionEnabled.ToString().ToLower() + ";\n"
                + "public static boolean trackAirbridgeLinkOnly = " + AirbridgeData.GetInstance().trackAirbridgeLinkOnly.ToString().ToLower() + ";\n"
                + "public static boolean autoStartTrackingEnabled = " + AirbridgeData.GetInstance().autoStartTrackingEnabled.ToString().ToLower() + ";\n"
                + "public static boolean facebookDeferredAppLinkEnabled = " + AirbridgeData.GetInstance().facebookDeferredAppLinkEnabled.ToString().ToLower() + ";\n"
                + "public static boolean trackInSessionLifeCycleEventEnabled = " + AirbridgeData.GetInstance().trackInSessionLifeCycleEventEnabled.ToString().ToLower() + ";\n"
                + "public static boolean pauseEventTransmitOnBackgroundEnabled = " + AirbridgeData.GetInstance().pauseEventTransmitOnBackgroundEnabled.ToString().ToLower() + ";\n"
                + "public static boolean clearEventBufferOnInitializeEnabled = " + AirbridgeData.GetInstance().resetEventBufferEnabled.ToString().ToLower() + ";\n"
                + "public static boolean sdkEnabled = " + AirbridgeData.GetInstance().sdkEnabled.ToString().ToLower() + ";\n"
                + "public static String appMarketIdentifier = \"" + AirbridgeData.GetInstance().appMarketIdentifier + "\";\n"
                + "public static int eventBufferCountLimitInGibibyte = " + AirbridgeData.GetInstance().eventMaximumBufferCount + ";\n"
                + "public static double eventBufferSizeLimitInGibibyte = " + AirbridgeData.GetInstance().eventMaximumBufferSize + ";\n"
                + "public static long eventTransmitIntervalSeconds = " + AirbridgeData.GetInstance().eventTransmitIntervalSeconds + ";\n"
                + "public static String facebookAppId = \"" + AirbridgeData.GetInstance().facebookAppId + "\";\n"
                + "public static boolean isHandleAirbridgeDeeplinkOnly = " + AirbridgeData.GetInstance().isHandleAirbridgeDeeplinkOnly.ToString().ToLower() + ";\n"
                + "\n"
                + "}\n";

            File.WriteAllText(settingsPath, content);
            
            Debug.Log("Updated Android Airbridge settings (AirbridgeSettings.java)");
        }
        catch (Exception exception)
        {
            Debug.LogErrorFormat("Something broken while updating Android Airbridge settings file : {0}", exception);
        }
    }

    internal static void UpdateiOSAppSetting()
    {
        string pluginPath = AirbridgeUtils.GetPluginPath(AirbridgeUtils.Platform.iOS);
        if (pluginPath == null) { return; }
        
        try
        {
            string path = Path.Combine(pluginPath, "AUAppSetting.h");
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
                + "static NSUInteger logLevel = " + AirbridgeData.GetInstance().logLevel + ";\n"
                + "static NSString* appScheme = @\"" + AirbridgeData.GetInstance().iOSURIScheme + "\";\n"
                + "static NSString* customDomain = @\"" + AirbridgeData.GetInstance().customDomain + "\";\n"
                + "static NSInteger sessionTimeoutSeconds = " + AirbridgeData.GetInstance().sessionTimeoutSeconds + ";\n"
                + "static BOOL autoStartTrackingEnabled = " + AirbridgeData.GetInstance().autoStartTrackingEnabled.ToString().ToLower() + ";\n"
                + "static BOOL userInfoHashEnabled = " + AirbridgeData.GetInstance().userInfoHashEnabled.ToString().ToLower() + ";\n"
                + "static BOOL trackAirbridgeLinkOnly = " + AirbridgeData.GetInstance().trackAirbridgeLinkOnly.ToString().ToLower() + ";\n"
                + "static BOOL facebookDeferredAppLinkEnabled = " + AirbridgeData.GetInstance().facebookDeferredAppLinkEnabled.ToString().ToLower() + ";\n"
                + "static NSInteger trackingAuthorizeTimeoutSeconds = " + AirbridgeData.GetInstance().iOSTrackingAuthorizeTimeoutSeconds + ";\n"
                + "static BOOL trackInSessionLifeCycleEventEnabled = " + AirbridgeData.GetInstance().trackInSessionLifeCycleEventEnabled.ToString().ToLower() + ";\n"
                + "static BOOL pauseEventTransmitOnBackgroundEnabled = " + AirbridgeData.GetInstance().pauseEventTransmitOnBackgroundEnabled.ToString().ToLower() + ";\n"
                + "static BOOL clearEventBufferOnInitializeEnabled = " + AirbridgeData.GetInstance().resetEventBufferEnabled.ToString().ToLower() + ";\n"
                + "static BOOL sdkEnabled = " + AirbridgeData.GetInstance().sdkEnabled.ToString().ToLower() + ";\n"
                + "static NSInteger eventBufferCountLimitInGibibyte = " + AirbridgeData.GetInstance().eventMaximumBufferCount + ";\n"
                + "static NSInteger eventBufferSizeLimitInGibibyte = " + AirbridgeData.GetInstance().eventMaximumBufferSize + ";\n"
                + "static NSInteger eventTransmitIntervalSeconds = " + AirbridgeData.GetInstance().eventTransmitIntervalSeconds + ";\n"
                + "static BOOL isHandleAirbridgeDeeplinkOnly = " + AirbridgeData.GetInstance().isHandleAirbridgeDeeplinkOnly.ToString().ToLower() + ";\n"
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