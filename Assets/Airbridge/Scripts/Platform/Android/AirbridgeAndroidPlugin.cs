#if UNITY_ANDROID || UNITY_EDITOR

using System;
using System.Collections.Generic;
using UnityEngine;

class AirbridgeAndroidPlugin : IAirbridgePlugin
{
    private static AndroidJavaObject airbridge = new AndroidJavaObject("co.ab180.airbridge.unity.AirbridgeUnity");

    #region Core

    public void EnableSDK()
    {
        airbridge.CallStatic("enableSDK");
    }

    public void DisableSDK()
    {
        airbridge.CallStatic("disableSDK");
    }

    public bool IsSDKEnabled()
    {
        return airbridge.CallStatic<bool>("isSDKEnabled");
    }
    
    #endregion

    #region Privacy

    public void StartTracking()
    {
        airbridge.CallStatic("startTracking");
    }

    public void StopTracking()
    {
        airbridge.CallStatic("stopTracking");
    }

    public bool IsTrackingEnabled()
    {
        return airbridge.CallStatic<bool>("isTrackingEnabled");
    }

    #endregion

    #region Deeplink

    public void SetOnDeeplinkReceived(Action<string> onDeeplinkReceived)
    {
        airbridge.CallStatic("handleDeeplink",
            new AirbridgeCallbackAndroidBridge(onDeeplinkReceived.Invoke)
        );
    }

    #endregion

    #region Data Collection

    public void SetUserID(string id)
    {
        airbridge.CallStatic("setUserID", id);
    }

    public void ClearUserID()
    {
        airbridge.CallStatic("clearUserID");
    }

    public void SetUserEmail(string email)
    {
        airbridge.CallStatic("setUserEmail", email);
    }

    public void ClearUserEmail()
    {
        airbridge.CallStatic("clearUserEmail");
    }

    public void SetUserPhone(string phone)
    {
        airbridge.CallStatic("setUserPhone", phone);
    }

    public void ClearUserPhone()
    {
        airbridge.CallStatic("clearUserPhone");
    }

    public void SetUserAttribute(string key, object value)
    {
        switch (value)
        {
            case int intValue:
                airbridge.CallStatic("setUserAttribute", key, intValue);
                break;
            case long longValue:
                airbridge.CallStatic("setUserAttribute", key, longValue);
                break;
            case float floatValue:
                airbridge.CallStatic("setUserAttribute", key, floatValue);
                break;
            case double doubleValue:
                airbridge.CallStatic("setUserAttribute", key, doubleValue);
                break;
            case bool boolValue:
                airbridge.CallStatic("setUserAttribute", key, boolValue);
                break;
            case string stringValue:
                airbridge.CallStatic("setUserAttribute", key, stringValue);
                break;
            default:
                Debug.LogWarning("Invalid data type received for 'user-attribute'. The value will be ignored.");
                break;
        }
    }

    public void RemoveUserAttribute(string key)
    {
        airbridge.CallStatic("removeUserAttribute", key);
    }

    public void ClearUserAttributes()
    {
        airbridge.CallStatic("clearUserAttributes");
    }

    public void SetUserAlias(string key, string value)
    {
        airbridge.CallStatic("setUserAlias", key, value);
    }

    public void RemoveUserAlias(string key)
    {
        airbridge.CallStatic("removeUserAlias", key);
    }

    public void ClearUserAlias()
    {
        airbridge.CallStatic("clearUserAlias");
    }

    public void ClearUser()
    {
        airbridge.CallStatic("clearUser");
    }


    public void SetDeviceAlias(string key, string value)
    {
        airbridge.CallStatic("setDeviceAlias", key, value);
    }

    public void RemoveDeviceAlias(string key)
    {
        airbridge.CallStatic("removeDeviceAlias", key);
    }

    public void ClearDeviceAlias()
    {
        airbridge.CallStatic("clearDeviceAlias");
    }


    public void RegisterPushToken(string token)
    {
        airbridge.CallStatic("registerPushToken", token);
    }

    #endregion

    #region Event

    public void TrackEvent(
        string category,
        /* CanBeNull */ Dictionary<string, object> semanticAttributes,
        /* CanBeNull */ Dictionary<string, object> customAttributes
    )
    {
        airbridge.CallStatic(
            methodName: "trackEvent",
            category,
            AirbridgeJson.Serialize(semanticAttributes),
            AirbridgeJson.Serialize(customAttributes)
        );
    }

    #endregion

    #region Placement

    public void Click(
        string trackingLink,
        /* CanBeNull */ Action onSuccess,
        /* CanBeNull */ Action<Exception> onFailure
    )
    {
        airbridge.CallStatic("click",
            trackingLink,
            (onSuccess == null) ? null : new AirbridgeCallbackAndroidBridge(_ => onSuccess.Invoke()),
            (onFailure == null)
                ? null
                : new AirbridgeCallbackAndroidBridge(error => onFailure.Invoke(new Exception(error)))
        );
    }

    public void Impression(
        string trackingLink,
        /* CanBeNull */ Action onSuccess,
        /* CanBeNull */ Action<Exception> onFailure
    )
    {
        airbridge.CallStatic("impression",
            trackingLink,
            (onSuccess == null) ? null : new AirbridgeCallbackAndroidBridge(_ => onSuccess.Invoke()),
            (onFailure == null)
                ? null
                : new AirbridgeCallbackAndroidBridge(error => onFailure.Invoke(new Exception(error)))
        );
    }

    #endregion

    #region Publication

    public void FetchAirbridgeGeneratedUUID(Action<string> onSuccess, /* CanBeNull */ Action<Exception> onFailure)
    {
        airbridge.CallStatic("fetchAirbridgeGeneratedUUID",
            new AirbridgeCallbackAndroidBridge(onSuccess.Invoke),
            (onFailure == null)
                ? null
                : new AirbridgeCallbackAndroidBridge(error => onFailure.Invoke(new Exception(error)))
        );
    }

    public void FetchDeviceUUID(Action<string> onSuccess, /* CanBeNull */ Action<Exception> onFailure)
    {
        airbridge.CallStatic("fetchDeviceUUID",
            new AirbridgeCallbackAndroidBridge(onSuccess.Invoke),
            (onFailure == null)
                ? null
                : new AirbridgeCallbackAndroidBridge(error => onFailure.Invoke(new Exception(error)))
        );
    }

    public void SetOnAttributionReceived(Action<Dictionary<string, object>> onAttributionReceived)
    {
        airbridge.CallStatic("setOnAttributionReceived",
            new AirbridgeCallbackAndroidBridge(data =>
            {
                if (string.IsNullOrWhiteSpace(data)) return;
                
                onAttributionReceived.Invoke(
                    AirbridgeJson.Deserialize(data) as Dictionary<string, object>
                );
            })
        );
    }

    public void CreateTrackingLink(string channel, Dictionary<string, object> option, Action<AirbridgeTrackingLink> onSuccess, Action<Exception> onFailure)
    {
        airbridge.CallStatic("createTrackingLink",
            channel,
            AirbridgeJson.Serialize(option),
            new AirbridgeCallbackAndroidBridge(data =>
            {
                if (string.IsNullOrWhiteSpace(data)) return;
                
                if (AirbridgeJson.Deserialize(data) is Dictionary<string, object> result)
                {
                    if (result.ContainsKey("shortURL") && result["shortURL"] is string shortURL &&
                        result.ContainsKey("qrcodeURL") && result["qrcodeURL"] is string qrcodeURL)
                    {
                        AirbridgeTrackingLink trackingLink = new AirbridgeTrackingLink(shortURL, qrcodeURL);
                        onSuccess.Invoke(trackingLink);
                    }
                }
            }),
            (onFailure == null)
                ? null
                : new AirbridgeCallbackAndroidBridge(error => onFailure.Invoke(new Exception(error)))
        );
    }

    #endregion

    #region Hybrid

    public string CreateWebInterfaceScript(string webToken, string postMessageScript)
    {
        return airbridge.CallStatic<string>("createWebInterfaceScript", webToken, postMessageScript);
    }

    public void HandleWebInterfaceCommand(string command)
    {
        airbridge.CallStatic("handleWebInterfaceCommand", command);
    }

    #endregion
    
    #region IAP
    
    public void StartInAppPurchaseTracking()
    {
        airbridge.CallStatic("startInAppPurchaseTracking");
    }

    public void StopInAppPurchaseTracking()
    {
        airbridge.CallStatic("stopInAppPurchaseTracking");
    }
    
    public bool IsInAppPurchaseTrackingEnabled()
    {
        return airbridge.CallStatic<bool>("isInAppPurchaseTrackingEnabled");
    }

    public void SetOnInAppPurchaseReceived(OnAirbridgeInAppPurchaseReceiveListener onReceived)
    {
        airbridge.CallStatic("setOnInAppPurchaseReceived", new AirbridgeCallbackWithReturnAndroidBridge(jsonString =>
        {
            try
            {
                AirbridgeInAppPurchase purchase =
                    new AirbridgeInAppPurchase(AirbridgeJson.Deserialize(jsonString) as Dictionary<string, object>);
                onReceived.Invoke(ref purchase);
                Dictionary<string, object> result = purchase.ToDictionary();
                return AirbridgeJson.Serialize(result);
            }
            catch (Exception e)
            {
                Debug.Log("[Airbridge][SetOnInAppPurchaseReceived] Exception:\n" + e.StackTrace);
            }
            return jsonString;
        }));
    }
    
    #endregion
}

#endif