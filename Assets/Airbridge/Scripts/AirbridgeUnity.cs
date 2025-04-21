using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
#if UNITY_IOS && !UNITY_EDITOR
using System.Runtime.InteropServices;
using AOT;
#endif

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
[SuppressMessage("ReSharper", "CheckNamespace")]
public class AirbridgeUnity
{
#if UNITY_IOS && !UNITY_EDITOR

    #region DLL Imports, only iOS
    [DllImport("__Internal")]
    private static extern bool native_isSDKEnabled();
    [DllImport("__Internal")]
    private static extern void native_startTracking();
    [DllImport("__Internal")]
    private static extern void native_stopTracking();
    [DllImport ("__Internal")]
    private static extern void native_setUserID(string userID);
    [DllImport ("__Internal")]
    private static extern void native_setUserEmail(string userEmail);
    [DllImport ("__Internal")]
    private static extern void native_setUserPhone(string userPhone);
    [DllImport ("__Internal")]
    private static extern void native_addUserAlias(string key, string value);
    [DllImport("__Internal")]
    private static extern void native_addUserAttributesWithInt(string key, int value);
    [DllImport("__Internal")]
    private static extern void native_addUserAttributesWithLong(string key, long value);
    [DllImport("__Internal")]
    private static extern void native_addUserAttributesWithFloat(string key, float value);
    [DllImport("__Internal")]
    private static extern void native_addUserAttributesWithBOOL(string key, bool value);
    [DllImport("__Internal")]
    private static extern void native_addUserAttributesWithString(string key, string value);
    [DllImport("__Internal")]
    private static extern void native_clearUserAttributes();
    [DllImport("__Internal")]
    private static extern void native_expireUser();
    [DllImport("__Internal")]
    private static extern void native_click(string trackingLink);
    [DllImport("__Internal")]
    private static extern void native_impression(string trackingLink);
    [DllImport("__Internal")]
    private static extern void native_setDeeplinkCallback(string objectName);
    [DllImport("__Internal")]
    private static extern void native_setAttributionResultCallback(string objectName);
    [DllImport("__Internal")]
    private static extern void native_sendEvent(string json);
    [DllImport("__Internal")]
    private static extern void native_registerPushToken(string token);
    [DllImport("__Internal")]
    private static extern void native_setDeviceAliasWithKey(string key, string value);
    [DllImport("__Internal")]
    private static extern void native_removeDeviceAliasWithKey(string key);
    [DllImport("__Internal")]
    private static extern void native_clearDeviceAlias();
    [DllImport("__Internal")]
    private static extern int native_fetchAirbridgeGeneratedUUID(string objectName);
    [DllImport("__Internal")]
    private static extern void native_fetchDeviceUUID(Action<string> onSuccess);
    [DllImport("__Internal")]
    private static extern void native_startInAppPurchaseTracking();
    [DllImport("__Internal")]
    private static extern void native_stopInAppPurchaseTracking();
    [DllImport("__Internal")]
    private static extern void native_setOnInAppPurchaseReceived(Func<string, string> onReceived);
    #endregion

    #region iOS Bridge Interfaces
    public static bool IsSDKEnabled()
    {  
        return native_isSDKEnabled();
    }
    
    public static void StartTracking()
    {
        native_startTracking();
    }
    
    public static void StopTracking()
    {
        native_stopTracking();
    }
    
    public static void SetUser(AirbridgeUser user)
    {
        ExpireUser();

        native_setUserID(user.GetId());
        native_setUserEmail(user.GetEmail());
        native_setUserPhone(user.GetPhoneNumber());

        Dictionary<string, string> userAlias = user.GetAlias();
        foreach (string key in userAlias.Keys)
        {
            native_addUserAlias(key, userAlias[key]);
        }
        Dictionary<string, object> attrs = user.GetAttributes();
        foreach (string key in attrs.Keys)
        {
            object value = attrs[key];
            if (value is int)
            {
                native_addUserAttributesWithInt(key, (int)value);
            }
            else if (value is long)
            {
                native_addUserAttributesWithLong(key, (long)value);
            }
            else if (value is float)
            {
                native_addUserAttributesWithFloat(key, (float)value);
            }
            else if (value is bool)
            {
                native_addUserAttributesWithBOOL(key, (bool)value);
            }
            else if (value is string)
            {
                native_addUserAttributesWithString(key, (string)value);
            }
            else
            {
                Debug.LogWarning("Invalid 'user-attribute' value data type received. The value will ignored");
            }
        }
    }

    public static void ExpireUser()
    {
        native_expireUser();
    }


    public static void ClickTrackingLink(string trackingLink, string deeplink = null, string fallback = null)
    {
        native_click(trackingLink);
    }

    public static void ImpressionTrackingLink(string trackingLink)
    {
        native_impression(trackingLink);
    }

    public static void SetDeeplinkCallback(string callbackObjectName)
    {
        native_setDeeplinkCallback(callbackObjectName);
    }

    public static void SetOnAttributionReceived(string callbackObjectName)
    {
        native_setAttributionResultCallback(callbackObjectName);
    }

    public static void TrackEvent(AirbridgeEvent @event)
    {
        native_sendEvent(@event.ToJsonString());
    }
    
    public static void SetDeviceAlias(string key, string value)
    {
        native_setDeviceAliasWithKey(key, value);
    }

    public static void RemoveDeviceAlias(string key)
    {
        native_removeDeviceAliasWithKey(key);
    }

    public static void ClearDeviceAlias()
    {
        native_clearDeviceAlias();
    }
    
    public static void RegisterPushToken(string token)
    {
        native_registerPushToken(token);
    }
    
    public static AirbridgeWebInterface CreateWebInterface(string webToken, PostCommandFunction postCommandFunction)
    {
        return new AirbridgeWebInterfaceImpl(webToken, postCommandFunction);
    }
    
    public static bool FetchAirbridgeGeneratedUUID(string callbackObjectName)
    {
        return native_fetchAirbridgeGeneratedUUID(callbackObjectName) != 0;
    }

    private static Action<string> _onDeviceUUIDSuccessCallback;

    [MonoPInvokeCallback(typeof(Func<string, string>))]
    private static void FetchDeviceUUIDCallback(string deviceUUID)
    {
        _onDeviceUUIDSuccessCallback?.Invoke(deviceUUID);
    }
    
    public static void FetchDeviceUUID(Action<string> onSuccess)
    {
        _onDeviceUUIDSuccessCallback = onSuccess;
        native_fetchDeviceUUID(FetchDeviceUUIDCallback);
    }
    
    public static void StartInAppPurchaseTracking()
    {
        native_startInAppPurchaseTracking();
    }

    public static void StopInAppPurchaseTracking()
    {
        native_stopInAppPurchaseTracking();
    }
    
    private static Func<Dictionary<string, object>, Dictionary<string, object>> _onInAppPurchaseReceivedCallback;
    
    [MonoPInvokeCallback(typeof(Func<string, string>))]
    private static string SetOnInAppPurchaseReceivedCallback(string onReceivedString)
    {
        if (_onInAppPurchaseReceivedCallback == null) { return null; }

        try
        {
            if (string.IsNullOrEmpty(onReceivedString)) { return null; }
            
            var dictionary = AirbridgeJson.Deserialize(onReceivedString) as Dictionary<string, object>;
            var result = _onInAppPurchaseReceivedCallback.Invoke(
                dictionary ?? new Dictionary<string, object>()
            );

            return result == null ? null : AirbridgeJson.Serialize(result);
        }
        catch (Exception e)
        {
            Debug.Log("[Airbridge][SetOnInAppPurchaseReceivedCallback] Exception:\n" + e.StackTrace);
        }

        return onReceivedString;
    }

     public static void SetOnInAppPurchaseReceived(Func<Dictionary<string, object>, Dictionary<string, object>> onReceived)
    {
        _onInAppPurchaseReceivedCallback = onReceived;
        native_setOnInAppPurchaseReceived(SetOnInAppPurchaseReceivedCallback);
    }
    #endregion
    
#elif UNITY_ANDROID && !UNITY_EDITOR
    private static AndroidJavaObject airbridge = new AndroidJavaObject("co.ab180.airbridge.unity.AirbridgeUnity");

    public static bool IsSDKEnabled()
    {  
        return airbridge.CallStatic<bool>("isSDKEnabled");
    }
    
    public static void StartTracking()
    {
        airbridge.CallStatic("startTracking");
    }
    
    public static void StopTracking()
    {
        airbridge.CallStatic("stopTracking");
    }
    
    public static void SetUser(AirbridgeUser user)
    {
        ExpireUser();

        airbridge.CallStatic("setUserId", user.GetId());
        airbridge.CallStatic("setUserEmail", user.GetEmail());
        airbridge.CallStatic("setUserPhone", user.GetPhoneNumber());
        Dictionary<string, string> alias = user.GetAlias();
        foreach (string key in alias.Keys)
        {
            airbridge.CallStatic("setUserAlias", key, alias[key]);
        }
        Dictionary<string, object> attrs = user.GetAttributes();
        foreach (string key in attrs.Keys)
        {
            object value = attrs[key];
            if (value is int)
            {
                airbridge.CallStatic("setUserAttribute", key, (int)value);
            }
            else if (value is long)
            {
                airbridge.CallStatic("setUserAttribute", key, (long)value);
            }
            else if (value is float)
            {
                airbridge.CallStatic("setUserAttribute", key, (float)value);
            }
            else if (value is bool)
            {
                airbridge.CallStatic("setUserAttribute", key, (bool)value);
            }
            else if (value is string)
            {
                airbridge.CallStatic("setUserAttribute", key, (string)value);
            }
            else
            {
                Debug.LogWarning("Invalid 'user-attribute' value data type received. The value will ignored");
            }
        }
    }

    public static void ExpireUser()
    {
        airbridge.CallStatic("expireUser");
    }

    public static void ClickTrackingLink(string trackingLink, string deeplink = null, string fallback = null)
    {
        airbridge.CallStatic("clickTrackingLink", trackingLink);
    }

    public static void ImpressionTrackingLink(string trackingLink)
    {
        airbridge.CallStatic("impressionTrackingLink", trackingLink);
    }

    public static void SetDeeplinkCallback(string callbackObjectName)
    {
        airbridge.CallStatic("setDeeplinkCallback", callbackObjectName);
    }

    public static void SetOnAttributionReceived(string callbackObjectName)
    {
        airbridge.CallStatic("setAttributionResultCallback", callbackObjectName);
    }

    public static void TrackEvent(AirbridgeEvent @event)
    {
        string jsonString = @event.ToJsonString();
        airbridge.CallStatic("trackEvent", jsonString);
    }
    
    public static void SetDeviceAlias(string key, string value)
    {
        airbridge.CallStatic("setDeviceAlias", key, value);
    }

    public static void RemoveDeviceAlias(string key)
    {
        airbridge.CallStatic("removeDeviceAlias", key);
    }

    public static void ClearDeviceAlias()
    {
        airbridge.CallStatic("clearDeviceAlias");
    }
    
    public static void RegisterPushToken(string token)
    {
        airbridge.CallStatic("registerPushToken", token);
    }
    
    public static AirbridgeWebInterface CreateWebInterface(string webToken, PostCommandFunction postCommandFunction)
    {
        return new AirbridgeWebInterfaceImpl(webToken, postCommandFunction);
    }
    
    public static bool FetchAirbridgeGeneratedUUID(string callbackObjectName)
    {
        return airbridge.CallStatic<bool>("fetchAirbridgeGeneratedUUID", callbackObjectName);
    }

    public static void FetchDeviceUUID(Action<string> onSuccess)
    {
        airbridge.CallStatic("fetchDeviceUUID", new AirbridgeCallbackAndroidBridge(onSuccess.Invoke));
    }
    
    public static void StartInAppPurchaseTracking()
    {
        airbridge.CallStatic("startInAppPurchaseTracking");
    }
    
    public static void StopInAppPurchaseTracking()
    {
        airbridge.CallStatic("stopInAppPurchaseTracking");
    }

    public static void SetOnInAppPurchaseReceived(Func<Dictionary<string, object>, Dictionary<string, object>> onReceived)
    {
        airbridge.CallStatic("setOnInAppPurchaseReceived", new AirbridgeCallbackWithReturnAndroidBridge(jsonString =>
        {
            try
            {
                Dictionary<string, object> result =
                    onReceived.Invoke(AirbridgeJson.Deserialize(jsonString) as Dictionary<string, object>);
                return AirbridgeJson.Serialize(result);     
            }
            catch (Exception e)
            {
                Debug.Log("[Airbridge][SetOnInAppPurchaseReceived] Exception:\n" + e.StackTrace);
            }

            return jsonString;
        }));
    }

#else
    public static bool IsSDKEnabled()
    {  
        Debug.Log("Airbridge is not implemented this method on this platform");
        return false;
    }
    
    public static void StartTracking()
    {
        Debug.Log("Airbridge is not implemented this method on this platform");
    }

    public static void StopTracking()
    {
        Debug.Log("Airbridge is not implemented this method on this platform");
    }

    public static void SetUser(AirbridgeUser user)
    {
        Debug.Log("Airbridge is not implemented this method on this platform");
    }

    public static void ExpireUser()
    {
        Debug.Log("Airbridge is not implemented this method on this platform");
    }

    public static void ClickTrackingLink(string trackingLink, string deeplink = null, string fallback = null)
    {
        Debug.Log("Airbridge is not implemented this method on this platform");
    }

    public static void ImpressionTrackingLink(string trackingLink)
    {
        Debug.Log("Airbridge is not implemented this method on this platform");
    }

    // When you register your deeplink callback, 'Airbridge' will call "void OnTrackingLinkResponse(string url)" method
    public static void SetDeeplinkCallback(string callbackObjectName)
    {
        Debug.Log("Airbridge is not implemented this method on this platform");
    }
    
    // When you register your attribution result callback, 'Airbridge' will call "void OnAttributionResultReceived(string jsonString)" method
    public static void SetOnAttributionReceived(string callbackObjectName)
    {
        Debug.Log("Airbridge is not implemented this method on this platform");
    }

    public static void TrackEvent(AirbridgeEvent @event)
    {
        Debug.Log("Airbridge is not implemented this method on this platform");
    }

    public static void SetDeviceAlias(string key, string value)
    {
        Debug.Log("Airbridge is not implemented this method on this platform");
    }

    public static void RemoveDeviceAlias(string key)
    {
        Debug.Log("Airbridge is not implemented this method on this platform");
    }

    public static void ClearDeviceAlias()
    {
        Debug.Log("Airbridge is not implemented this method on this platform");
    }

    public static void RegisterPushToken(string token)
    {
        Debug.Log("Airbridge is not implemented this method on this platform");
    }
    
    public static AirbridgeWebInterface CreateWebInterface(string webToken, PostCommandFunction postCommandFunction)
    {
        Debug.Log("Airbridge is not implemented this method on this platform");
        return new AirbridgeWebInterfaceDefault();
    }
    
    public static bool FetchAirbridgeGeneratedUUID(string callbackObjectName)
    {
        Debug.Log("Airbridge is not implemented this method on this platform");
        return false;
    }
    
    public static void FetchDeviceUUID(Action<string> onSuccess)
    {
        Debug.Log("Airbridge is not implemented this method on this platform");
    }
    
    public static void StartInAppPurchaseTracking()
    {
        Debug.Log("Airbridge is not implemented this method on this platform");
    }
    
    public static void StopInAppPurchaseTracking()
    {
        Debug.Log("Airbridge is not implemented this method on this platform");
    }
    
    public static void SetOnInAppPurchaseReceived(Func<Dictionary<string, object>, Dictionary<string, object>> onReceived)
    {
        Debug.Log("Airbridge is not implemented this method on this platform");
    }
    
#endif
}
