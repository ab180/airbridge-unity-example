using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// Exclude a class from the document
/// @cond HIDDEN_SYMBOLS
[SuppressMessage("ReSharper", "CheckNamespace")]
internal interface IAirbridgePlugin
{
    #region Core

    void EnableSDK();
    void DisableSDK();
    bool IsSDKEnabled();
    
    #endregion

    #region Privacy

    void StartTracking();
    void StopTracking();
    bool IsTrackingEnabled();

    #endregion

    #region Deeplink

    void SetOnDeeplinkReceived(Action<string> onDeeplinkReceived);

    #endregion

    #region Data Collection

    void SetUserID(string id);
    void ClearUserID();
    void SetUserEmail(string email);
    void ClearUserEmail();
    void SetUserPhone(string phone);
    void ClearUserPhone();
    void SetUserAttribute(string key, object value);
    void RemoveUserAttribute(string key);
    void ClearUserAttributes();
    void SetUserAlias(string key, string value);
    void RemoveUserAlias(string key);
    void ClearUserAlias();
    void ClearUser();

    void SetDeviceAlias(string key, string value);
    void RemoveDeviceAlias(string key);
    void ClearDeviceAlias();

    void RegisterPushToken(string token);

    #endregion

    #region Event

    void TrackEvent(
        string category,
        /* CanBeNull */ Dictionary<string, object> semanticAttributes,
        /* CanBeNull */ Dictionary<string, object> customAttributes
    );

    #endregion

    #region Placement

    void Click(string trackingLink, /* CanBeNull */ Action onSuccess, /* CanBeNull */ Action<Exception> onFailure);
    void Impression(string trackingLink, /* CanBeNull */ Action onSuccess, /* CanBeNull */ Action<Exception> onFailure);

    #endregion

    #region Publication

    void FetchDeviceUUID(Action<string> onSuccess, /* CanBeNull */ Action<Exception> onFailure);
    void FetchAirbridgeGeneratedUUID(Action<string> onSuccess, /* CanBeNull */ Action<Exception> onFailure);
    void SetOnAttributionReceived(Action<Dictionary<string, object>> onAttributionReceived);
    void CreateTrackingLink(
        string channel,
        Dictionary<string, object> option,
        Action<AirbridgeTrackingLink> onSuccess,
        /* CanBeNull */ Action<Exception> onFailure
    );
    
    #endregion

    #region Hybrid

    string CreateWebInterfaceScript(string webToken, string postMessageScript);
    void HandleWebInterfaceCommand(string command);

    #endregion
}
// ReSharper disable once InvalidXmlDocComment
/// @endcond