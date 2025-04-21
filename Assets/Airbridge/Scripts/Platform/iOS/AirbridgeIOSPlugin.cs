#if UNITY_IOS || UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
[SuppressMessage("ReSharper", "CheckNamespace")]
internal class AirbridgeIOSPlugin : IAirbridgePlugin
{
    #region Core
    [DllImport("__Internal")] private static extern void native_enableSDK();
    public void EnableSDK()
    {
        native_enableSDK();
    }

    [DllImport("__Internal")] private static extern void native_disableSDK();
    public void DisableSDK()
    {
        native_disableSDK();
    }

    [DllImport("__Internal")] private static extern bool native_isSDKEnabled();
    public bool IsSDKEnabled()
    {
        return native_isSDKEnabled();
    }
    #endregion

    #region Privacy
    [DllImport("__Internal")] private static extern void native_startTracking();
    public void StartTracking()
    {
        native_startTracking();
    }
    
    [DllImport("__Internal")] private static extern void native_stopTracking();
    public void StopTracking()
    {
        native_stopTracking();
    }

    [DllImport("__Internal")] private static extern bool native_isTrackingEnabled();
    public bool IsTrackingEnabled()
    {
        return native_isTrackingEnabled();
    }
    #endregion
    
    #region Deeplink 
    [DllImport("__Internal")] private static extern void native_setOnDeeplinkReceived(Action<string> onReceived);

    private static Action<string> _onReceivedDeeplinkCallback;
    
    [MonoPInvokeCallback(typeof(Action<string>))]
    private static void SetOnDeeplinkReceivedCallback(string onReceivedString)
    {
        _onReceivedDeeplinkCallback?.Invoke(onReceivedString);
    }
    public void SetOnDeeplinkReceived(Action<string> onReceived)
    {
        _onReceivedDeeplinkCallback = onReceived;
        native_setOnDeeplinkReceived(SetOnDeeplinkReceivedCallback);
    }
    #endregion

    #region User
    [DllImport("__Internal")] private static extern void native_setUserID(string id);
    public void SetUserID(string id)
    {
        native_setUserID(id);
    }

    [DllImport("__Internal")] private static extern void native_clearUserID();
    public void ClearUserID()
    {
        native_clearUserID();
    }

    [DllImport("__Internal")] private static extern void native_setUserEmail(string email);
    public void SetUserEmail(string email)
    {
        native_setUserEmail(email);
    }

    [DllImport("__Internal")] private static extern void native_clearUserEmail();
    public void ClearUserEmail()
    {
        native_clearUserEmail();
    }

    [DllImport("__Internal")] private static extern void native_setUserPhone(string phone);
    public void SetUserPhone(string phone)
    {
        native_setUserPhone(phone);
    }

    [DllImport("__Internal")] private static extern void native_clearUserPhone();
    public void ClearUserPhone()
    {
        native_clearUserPhone();
    }

    [DllImport("__Internal")] private static extern void native_setUserAttributeWithInt(string key, int value);
    [DllImport("__Internal")] private static extern void native_setUserAttributeWithLong(string key, long value);
    [DllImport("__Internal")] private static extern void native_setUserAttributeWithFloat(string key, float value);
    [DllImport("__Internal")] private static extern void native_setUserAttributeWithDouble(string key, double value);
    [DllImport("__Internal")] private static extern void native_setUserAttributeWithBOOL(string key, bool value);
    [DllImport("__Internal")] private static extern void native_setUserAttributeWithString(string key, string value);
    public void SetUserAttribute(string key, object value)
    {
        switch (value)
        {
            case int intValue:
                native_setUserAttributeWithInt(key, intValue);
                return;
            case long longValue:
                native_setUserAttributeWithLong(key, longValue);
                return;
            case float floatValue:
                native_setUserAttributeWithFloat(key, floatValue);
                return;
            case double doubleValue:
                native_setUserAttributeWithDouble(key, doubleValue);
                return;
            case bool boolValue:
                native_setUserAttributeWithBOOL(key, boolValue);
                return;
            case string stringValue:
                native_setUserAttributeWithString(key, stringValue);
                return;
            default:
                Debug.LogWarning("Invalid data type received for 'user-attribute'. The value will be ignored.");
                return;
        }
    }

    [DllImport("__Internal")] private static extern void native_removeUserAttribute(string key);
    public void RemoveUserAttribute(string key)
    {
        native_removeUserAttribute(key);
    }

    [DllImport("__Internal")] private static extern void native_clearUserAttributes();
    public void ClearUserAttributes()
    {
        native_clearUserAttributes();
    }

    [DllImport("__Internal")] private static extern void native_setUserAlias(string key, string value);
    public void SetUserAlias(string key, string value)
    {
        native_setUserAlias(key, value);
    }

    [DllImport("__Internal")] private static extern void native_removeUserAlias(string key);
    public void RemoveUserAlias(string key)
    {
        native_removeUserAlias(key);
    }

    [DllImport("__Internal")] private static extern void native_clearUserAlias();
    public void ClearUserAlias()
    {
        native_clearUserAlias();
    }

    [DllImport("__Internal")] private static extern void native_clearUser();
    public void ClearUser()
    {
        native_clearUser();
    }

    [DllImport("__Internal")] private static extern void native_setDeviceAlias(string key, string value);
    public void SetDeviceAlias(string key, string value)
    {
        native_setDeviceAlias(key, value);
    }

    [DllImport("__Internal")] private static extern void native_removeDeviceAlias(string key);
    public void RemoveDeviceAlias(string key)
    {
        native_removeDeviceAlias(key);
    }

    [DllImport("__Internal")] private static extern void native_clearDeviceAlias();
    public void ClearDeviceAlias()
    {
        native_clearDeviceAlias();
    }

    [DllImport("__Internal")] private static extern void native_registerPushToken(string token);
    public void RegisterPushToken(string token)
    {
        native_registerPushToken(token);
    }
    #endregion

    #region Event
    [DllImport("__Internal")] private static extern void native_trackEvent(
        string category,
        string semanticJson,
        string customJson
    ); 
    public void TrackEvent(
        string category,
        Dictionary<string, object> semanticAttributes,
        Dictionary<string, object> customAttributes
    )
    {
        native_trackEvent(
            category,
            AirbridgeJson.Serialize(semanticAttributes),
            AirbridgeJson.Serialize(customAttributes)
        );
    }
    #endregion

    #region Interface
    [DllImport("__Internal")] private static extern string native_CreateWebInterfaceScript(string webToken, string postMessageScript);
    public string CreateWebInterfaceScript(string webToken, string postMessageScript)
    {
       return native_CreateWebInterfaceScript(webToken, postMessageScript);
    }
    
    [DllImport("__Internal")] private static extern void native_HandleWebInterfaceCommand(string command);
    public void HandleWebInterfaceCommand(string command)
    {
        native_HandleWebInterfaceCommand(command);
    }
    #endregion
    
    #region Placement
    [DllImport("__Internal")] private static extern void native_click(
        string trackingLink,
        Action onSuccess,
        Action<string> onFailure
    );

    private static Action _onSuccessClickCallback;
    [MonoPInvokeCallback(typeof(Action))] 
    private static void SetOnSuccessClickCallback()
    {
        _onSuccessClickCallback?.Invoke();
    }
    
    private static Action<string> _onFailureClickCallback;
    [MonoPInvokeCallback(typeof(Action<string>))] 
    private static void SetOnFailureClickCallback(string exceptionString)
    {
        _onFailureClickCallback?.Invoke(exceptionString);
    }
    
    public void Click(
        string trackingLink,
        Action onSuccess,
        Action<Exception> onFailure
    )
    {
        _onSuccessClickCallback = onSuccess;
        _onFailureClickCallback = (exceptionString) =>
        {
            onFailure(new Exception(exceptionString));
        };
        native_click(trackingLink, SetOnSuccessClickCallback, SetOnFailureClickCallback);
    }
    
    private static Action _onSuccessImpressionCallback;
    [MonoPInvokeCallback(typeof(Action))] 
    private static void SetOnSuccessImpressionCallback()
    {
        _onSuccessImpressionCallback?.Invoke();
    }
    
    private static Action<string> _onFailureImpressionCallback;
    [MonoPInvokeCallback(typeof(Action<string>))] 
    private static void SetOnFailureImpressionCallback(string exceptionString)
    {
        _onFailureImpressionCallback?.Invoke(exceptionString);
    }
    
    [DllImport("__Internal")] private static extern void native_impression(
        string trackingLink,
        Action onSuccess,
        Action<string> onFailure
    );
    public void Impression(
        string trackingLink,
        Action onSuccess,
        Action<Exception> onFailure
    )
    {
        _onSuccessImpressionCallback = onSuccess;
        _onFailureImpressionCallback = (exceptionString) =>
        {
            onFailure(new Exception(exceptionString));
        };
        native_impression(trackingLink, SetOnSuccessImpressionCallback, SetOnFailureImpressionCallback);
    }
    #endregion

    #region Information
    [DllImport("__Internal")] private static extern void native_fetchDeviceUUID(
        Action<string> onSuccess,
        Action<string> onFailure
    );
    
    private static Action<string> _onSuccessFetchDeviceUUIDCallback;
    [MonoPInvokeCallback(typeof(Action<string>))] 
    private static void SetSuccessFetchDeviceUUIDCallback(string deviceUUID)
    {
        _onSuccessFetchDeviceUUIDCallback?.Invoke(deviceUUID);
    }
    
    private static Action<string> _onFailureFetchDeviceUUIDCallback;
    [MonoPInvokeCallback(typeof(Action<string>))] 
    private static void SetFailureFetchDeviceUUIDCallback(string exceptionString)
    {
        _onFailureFetchDeviceUUIDCallback?.Invoke(exceptionString);
    }
    
    public void FetchDeviceUUID(Action<string> onSuccess, /* CanBeNull */ Action<Exception> onFailure)
    {
        _onSuccessFetchDeviceUUIDCallback = onSuccess;
        _onFailureFetchDeviceUUIDCallback = (exceptionString) =>
        {
            onFailure(new Exception(exceptionString));
        };
        native_fetchDeviceUUID(SetSuccessFetchDeviceUUIDCallback, SetFailureFetchDeviceUUIDCallback);
    }
    
    [DllImport("__Internal")] private static extern void native_fetchAirbridgeGeneratedUUID(
        Action<string> onSuccess,
        Action<string> onFailure
    );
    
    private static Action<string> _onSuccessFetchAirbridgeGeneratedUUIDCallback;
    [MonoPInvokeCallback(typeof(Action<string>))] 
    private static void SetSuccessFetchAirbridgeGeneratedUUIDCallback(string deviceUUID)
    {
        _onSuccessFetchAirbridgeGeneratedUUIDCallback?.Invoke(deviceUUID);
    }
    
    private static Action<string> _onFailureFetchAirbridgeGeneratedUUIDCallback;
    [MonoPInvokeCallback(typeof(Action<string>))] 
    private static void SetFailureFetchAirbridgeGeneratedUUIDCallback(string exceptionString)
    {
        _onFailureFetchAirbridgeGeneratedUUIDCallback?.Invoke(exceptionString);
    }
    
    public void FetchAirbridgeGeneratedUUID(Action<string> onSuccess, Action<Exception> onFailure)
    {
        _onSuccessFetchAirbridgeGeneratedUUIDCallback = onSuccess;
        _onFailureFetchAirbridgeGeneratedUUIDCallback = (exceptionString) =>
        {
            onFailure(new Exception(exceptionString));
        };
        native_fetchAirbridgeGeneratedUUID(
            SetSuccessFetchAirbridgeGeneratedUUIDCallback,
            SetFailureFetchAirbridgeGeneratedUUIDCallback
        );
    }

    [DllImport("__Internal")] private static extern void native_setOnAttributionReceived(
        Action<string> onReceived
    );
    
    private static Action<Dictionary<string, object>> _onReceivedAttributionCallback;
    [MonoPInvokeCallback(typeof(Action<Dictionary<string, object>>))]
    private static void SetOnAttributionReceivedCallback(string onReceivedString)
    {
        if (string.IsNullOrWhiteSpace(onReceivedString)) return;
        
        _onReceivedAttributionCallback?.Invoke(
            AirbridgeJson.Deserialize(onReceivedString) as Dictionary<string, object>
        );
    }
    
    public void SetOnAttributionReceived(Action<Dictionary<string, object>> onReceived)
    {
        _onReceivedAttributionCallback = onReceived;
        native_setOnAttributionReceived(SetOnAttributionReceivedCallback);
    }
    
    [DllImport("__Internal")] private static extern void native_createTrackingLink(
        string channel,
        string option,
        Action<string, string> onSuccess,
        Action<string> onFailure
    );

    // Create Tracking Link
    private static Action<string, string> _onSuccessTrackingLinkCallback;
    [MonoPInvokeCallback(typeof(Action<string, string>))]
    private static void SetOnSuccessTrackingLinkCallback(string shortURLString, string qrcodeURLString)
    {
        if (string.IsNullOrWhiteSpace(shortURLString)) return;
        if (string.IsNullOrWhiteSpace(qrcodeURLString)) return;
        
        _onSuccessTrackingLinkCallback?.Invoke(
            shortURLString, qrcodeURLString
        );
    }

    private static Action<string> _onFailureTrackingLinkCallback;
    [MonoPInvokeCallback(typeof(Action<string>))]
    private static void SetOnFailureTrackingLinkCallback(string exceptionString)
    {
        _onFailureTrackingLinkCallback?.Invoke(exceptionString);
    }

    public void CreateTrackingLink(string channel, Dictionary<string, object> option, Action<AirbridgeTrackingLink> onSuccess, Action<Exception> onFailure)
    {
        _onSuccessTrackingLinkCallback = (shortURLString, qrcodeURLString) => 
        {
            onSuccess(new AirbridgeTrackingLink(shortURLString, qrcodeURLString));
        };
        _onFailureTrackingLinkCallback =(exceptionString) =>
        {
            onFailure(new Exception(exceptionString));
        };
        native_createTrackingLink(
            channel,
            AirbridgeJson.Serialize(option),
            SetOnSuccessTrackingLinkCallback,
            SetOnFailureTrackingLinkCallback
        ); 
    }
    #endregion
    
    [DllImport("__Internal")] private static extern void native_startInAppPurchaseTracking();
    public void StartInAppPurchaseTracking()
    {
        native_startInAppPurchaseTracking();
    }

    [DllImport("__Internal")] private static extern void native_stopInAppPurchaseTracking();
    public void StopInAppPurchaseTracking()
    {
        native_stopInAppPurchaseTracking();
    }
    
    [DllImport("__Internal")] private static extern bool native_isInAppPurchaseTrackingEnabled();
    public bool IsInAppPurchaseTrackingEnabled()
    {
        return native_isInAppPurchaseTrackingEnabled();
    }
    
    [DllImport("__Internal")] private static extern void native_setOnInAppPurchaseReceived(
        Func<string, string> onReceived
    );
    
    private static OnAirbridgeInAppPurchaseReceiveListener _setOnInAppPurchaseReceived;
    [MonoPInvokeCallback(typeof(Action<Dictionary<string, object>>))]
    private static string SetOnInAppPurchaseReceived(string onReceivedString)
    {
        if (string.IsNullOrWhiteSpace(onReceivedString)) return null;

        var inAppPurchase = new AirbridgeInAppPurchase(
            AirbridgeJson.Deserialize(onReceivedString) as Dictionary<string, object>
        );
            
        _setOnInAppPurchaseReceived?.Invoke(ref inAppPurchase);
        return AirbridgeJson.Serialize(inAppPurchase.ToDictionary());
    }

    public void SetOnInAppPurchaseReceived(OnAirbridgeInAppPurchaseReceiveListener onReceived)
    {
        _setOnInAppPurchaseReceived = onReceived;
        native_setOnInAppPurchaseReceived(SetOnInAppPurchaseReceived);
    }
}

#endif