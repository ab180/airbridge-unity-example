using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[InitializeOnLoad]
#endif
// ReSharper disable once InvalidXmlDocComment
/// <summary>
/// Data used in %Airbridge Settings
/// </summary>
class AirbridgeData : ScriptableObject
{
    private const string assetPath = "Assets/Airbridge/Resources";
    private const string assetName = "AirbridgeData";
    private const string assetExtension = ".asset";
    
    /// <summary> App Name.</summary>
    public string appName;
    
    /// <summary> App SDK Token.</summary>
    public string appToken;
    
    /// <summary> Adjusts the log record level for %Airbridge.</summary>
    public int logLevel = AirbridgeLogLevel.defaultLogLevel;
    
    /// <summary> Protects against SDK spoofing. Both sdkSignatureSecretID and sdkSignatureSecret values must be applied.</summary>
    public string sdkSignatureSecretID;
    
    /// <summary> Protects against SDK spoofing. Both sdkSignatureSecretID and sdkSignatureSecret values must be applied.</summary>
    public string sdkSignatureSecret;
    
    /// <summary> URI Scheme of the deep link. (iOS Only)</summary>
    public string iOSURIScheme;
    
    /// <summary> URI Scheme of the deep link. (Android Only)</summary>
    public string androidURIScheme;
    
    /// <summary>
    /// Customized URLs. Such as `go.my_company.com/abcd` can also be used as tracking links to improve the branding and CTR (Click Through Rate).
    /// @attention
    /// Custom Domain should match the information in the %Airbridge dashboard.
    /// </summary>
    public string customDomain;
    
    /// <summary>
    /// An app open event will not be sent when the app is reopened within the designated period.
    /// Session timeout seconds must be between 0 second and 7 days (604800 seconds).
    /// </summary>
    public long sessionTimeoutSeconds = 300;
    
    /// <summary> When set to `false`, user email and user phone information are sent without being hashed.</summary>
    public bool userInfoHashEnabled = true;
    
    /// <summary>
    /// When set to `true`, location information is collected. (Android Only)
    /// @note
    /// Two permissions must be allowed in AndroidManifest.xml
    ///  * android.permission.ACCESS_FINE_LOCATION
    ///  * android.permission.ACCESS_COARSE_LOCATION
    /// </summary>
    public bool locationCollectionEnabled = false;
    
    /// <summary> When set to `true`, deep link events are sent only when app is opened with an %Airbridge deep link.</summary>
    public bool trackAirbridgeLinkOnly = false;
    
    /// <summary> When set to `false`, no events will be sent until Airbridge#StartTracking is called.</summary>
    public bool autoStartTrackingEnabled = true;
    
    /// <summary> When set to `true` and the Facebook SDK is installed, Facebook Deferred App Link data is collected.</summary>
    public bool facebookDeferredAppLinkEnabled = false;
    
    /// <summary>
    /// When timeout is set, Install event is delayed until Request tracking authorization alert is clicked (iOS only).
    /// iOS tracking authorize timeout seconds must be between 0 second and 1 hour (3600 seconds).
    /// </summary>
    public int iOSTrackingAuthorizeTimeoutSeconds = 30;
    
    /// <summary> When set to `true`, Open and Foreground events during the ongoing session is collected.</summary>
    public bool trackInSessionLifeCycleEventEnabled = false;
    
    /// <summary> When set to `true`, event transmission will be paused when the app goes to the background.</summary>
    public bool pauseEventTransmitOnBackgroundEnabled = false;
    
    /// <summary> When set to `true`, each time the app is opened, events stored in the device's internal database are cleared.</summary>
    public bool resetEventBufferEnabled = false;
    
    /// <summary> When set to `false`, %Airbridge SDK will be deactivated until Airbridge#EnableSDK is called.</summary>
    public bool sdkEnabled = true;
    
    /// <summary> App market identifier.</summary>
    /// @deprecated Deprecated and will be automatically collected.
    public string appMarketIdentifier;
    
    /// <summary>
    /// Adjusts the maximum event count.
    /// Event maximum buffer count must be between 0 and 2147483647.
    /// @note
    /// The %Airbridge SDK stores events as long as they do not exceed the maximum event count and maximum event size limitations.
    /// Any excess events are discarded.
    /// </summary>
    public int eventMaximumBufferCount = int.MaxValue;
    
    /// <summary>
    /// Adjusts the maximum event size in GiB (gibibytes).
    /// Event maximum buffer size must be between 0 byte and 1 tebibyte (1024 gibibytes).
    /// @note
    /// The %Airbridge SDK stores events as long as they do not exceed the maximum event count and maximum event size limitations.
    /// Any excess events are discarded.
    /// 
    /// </summary>
    public double eventMaximumBufferSize = 1024;
    
    /// <summary>
    /// Adjusts event transmission interval in seconds.
    /// Event transmit interval seconds must be between 0 second and 1 day (86400 seconds).
    /// </summary>
    public long eventTransmitIntervalSeconds = 0;
    
    /// <summary> Facebook App ID for meta install referrer collection setup.</summary>
    public string facebookAppId;
    
    /// <summary> When set to `true`, provide only %Airbridge deep links in the Airbridge#SetOnDeeplinkReceived callback</summary>
    public bool isHandleAirbridgeDeeplinkOnly = false;
    
    /// <summary> Sets an in-app purchase environment.</summary>
    public AirbridgeInAppPurchaseEnvironment inAppPurchaseEnvironment = AirbridgeInAppPurchaseEnvironment.Production;

    /// <summary> When set to `true`, TCF(Transparency & Consent Framework) data will be collected automatically.</summary>
    public bool collectTCFDataEnabled = false;
    
    private void OnValidate()
    {
        sessionTimeoutSeconds = Math.Max(0, Math.Min(sessionTimeoutSeconds, (long)TimeSpan.FromDays(7).TotalSeconds));
        eventMaximumBufferCount = Math.Max(0, Math.Min(eventMaximumBufferCount, int.MaxValue));
        eventMaximumBufferSize = Math.Max(0.0, Math.Min(eventMaximumBufferSize, 1024.0));
        eventTransmitIntervalSeconds = Math.Max(0, Math.Min(eventTransmitIntervalSeconds, (long)TimeSpan.FromDays(1).TotalSeconds));
        
        iOSTrackingAuthorizeTimeoutSeconds = Math.Max(0, Math.Min(iOSTrackingAuthorizeTimeoutSeconds, (int)TimeSpan.FromHours(1).TotalSeconds));
    }

    public List<string> CustomDomains()
    {
        List<string> toReturn = new List<string>();
        if (!string.IsNullOrEmpty(customDomain))
        {
            customDomain.Split(' ').ToList().ForEach(domain =>
            {
                if (!string.IsNullOrEmpty(domain))
                {
                    toReturn.Add(domain.Trim());
                }
            });
        }
        return toReturn;
    }
    
#if UNITY_EDITOR
    private static AirbridgeData instance;
    public static AirbridgeData GetInstance()
    {
        instance = Resources.Load(assetName) as AirbridgeData;

        if (instance == null)
        {
            instance = CreateInstance<AirbridgeData>();
            if (!Directory.Exists(assetPath))
            {
                Directory.CreateDirectory(assetPath);
                AssetDatabase.Refresh();
            }

            string fullPath = Path.Combine(assetPath, assetName + assetExtension);
            AssetDatabase.CreateAsset(instance, fullPath);
        }
        return instance;
    }
#endif
}