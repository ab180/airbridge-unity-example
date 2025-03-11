using System;
using System.Collections.Generic;
using UnityEngine;

// Exclude a class from the document
/// @cond HIDDEN_SYMBOLS
class AirbridgeUnsupportedPlugin: IAirbridgePlugin
{
    public void EnableSDK() { UnsupportedPlatform(); }

    public void DisableSDK() { UnsupportedPlatform(); }

    public bool IsSDKEnabled() { return UnsupportedPlatform(false); }

    public void StartTracking() { UnsupportedPlatform(); }

    public void StopTracking() { UnsupportedPlatform(); }

    public bool IsTrackingEnabled() { return UnsupportedPlatform(false); }

    public void SetOnDeeplinkReceived(Action<string> onDeeplinkReceived) { UnsupportedPlatform(); }

    public void SetUserID(string id) { UnsupportedPlatform(); }

    public void ClearUserID() { UnsupportedPlatform(); }

    public void SetUserEmail(string email) { UnsupportedPlatform(); }

    public void ClearUserEmail() { UnsupportedPlatform(); }
    
    public void SetUserPhone(string phone) { UnsupportedPlatform(); }

    public void ClearUserPhone() { UnsupportedPlatform(); }
    
    public void SetUserAttribute(string key, object value) { UnsupportedPlatform(); }

    public void RemoveUserAttribute(string key) { UnsupportedPlatform(); }

    public void ClearUserAttributes() { UnsupportedPlatform(); }

    public void SetUserAlias(string key, string value) { UnsupportedPlatform(); }

    public void RemoveUserAlias(string key) { UnsupportedPlatform(); }

    public void ClearUserAlias() { UnsupportedPlatform(); }

    public void ClearUser() { UnsupportedPlatform(); }
    
    public void SetDeviceAlias(string key, string value) { UnsupportedPlatform(); }

    public void RemoveDeviceAlias(string key) { UnsupportedPlatform(); }

    public void ClearDeviceAlias() { UnsupportedPlatform(); }

    public void RegisterPushToken(string token) { UnsupportedPlatform(); }

    public void TrackEvent(string category, Dictionary<string, object> semanticAttributes, Dictionary<string, object> customAttributes) { UnsupportedPlatform(); }

    public void Click(string trackingLink, Action onSuccess, Action<Exception> onFailure) { UnsupportedPlatform(); }

    public void Impression(string trackingLink, Action onSuccess, Action<Exception> onFailure) { UnsupportedPlatform(); }

    public void FetchDeviceUUID(Action<string> onSuccess, Action<Exception> onFailure) { UnsupportedPlatform(); }

    public void FetchAirbridgeGeneratedUUID(Action<string> onSuccess, Action<Exception> onFailure) { UnsupportedPlatform(); }

    public void SetOnAttributionReceived(Action<Dictionary<string, object>> onAttributionReceived) { UnsupportedPlatform(); }
    
    public void CreateTrackingLink(string channel, Dictionary<string, object> option, Action<AirbridgeTrackingLink> onSuccess, Action<Exception> onFailure)
    {
        UnsupportedPlatform();
    }

    public string CreateWebInterfaceScript(string webToken, string postMessageScript) { return UnsupportedPlatform(""); }

    public void HandleWebInterfaceCommand(string command) { UnsupportedPlatform(); }

    private void UnsupportedPlatform()
    {
        Debug.Log("Airbridge is not implemented this method on this platform.");
    }
    
    private T UnsupportedPlatform<T>(T toReturn)
    {
        Debug.Log("Airbridge is not implemented this method on this platform.");
        return toReturn;
    }
}
// ReSharper disable once InvalidXmlDocComment
/// @endcond