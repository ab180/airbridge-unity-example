using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

/// <summary>
/// %Airbridge Unity SDK
/// </summary>
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
[SuppressMessage("ReSharper", "CheckNamespace")]
public class Airbridge
{
    private static readonly Lazy<IAirbridgePlugin> Lazy = new Lazy<IAirbridgePlugin>(() =>
    {
        IAirbridgePlugin pluginInterface = new AirbridgeUnsupportedPlugin();
#if UNITY_ANDROID && !UNITY_EDITOR
            pluginInterface = new AirbridgeAndroidPlugin();
#elif UNITY_IOS && !UNITY_EDITOR 
            pluginInterface = new AirbridgeIOSPlugin();
#endif
            
        return pluginInterface;
    });
    
   private static IAirbridgePlugin AirbridgePlugin => Lazy.Value;
   
    #region Core

    /// <summary>
    /// Enables the SDK.
    /// </summary>
    public static void EnableSDK()
    {
        AirbridgePlugin.EnableSDK();
    }

    /// <summary>
    /// Disables the SDK.
    /// </summary>
    public static void DisableSDK()
    {
        AirbridgePlugin.DisableSDK();
    }

    /// <summary>
    /// Checks whether the SDK is currently enabled.
    /// </summary>
    /// <returns> `true` if the SDK is enabled, `false` otherwise.</returns>
    public static bool IsSDKEnabled()
    {
        return AirbridgePlugin.IsSDKEnabled();
    }
    
    #endregion

    #region Privacy

    /// <summary>
    /// Start collecting and transferring events.
    /// </summary>
    public static void StartTracking()
    {
        AirbridgePlugin.StartTracking();
    }

    /// <summary>
    /// Stop collecting and transferring events.
    /// </summary>
    public static void StopTracking()
    {
        AirbridgePlugin.StopTracking();
    }

    /// <summary>
    /// Checks whether the SDK is currently enabled for tracking.
    /// </summary>
    /// <returns> `true` if the SDK is enabled for tracking, `false` otherwise.</returns>
    public static bool IsTrackingEnabled()
    {
        return AirbridgePlugin.IsTrackingEnabled();
    }

    #endregion

    #region Deeplink

    /// <summary>
    /// Handles deeplink and deferred-deeplink.
    /// </summary>
    /// <param name="onDeeplinkReceived"> A callback listener that receives a delivered deeplink URL.</param>
    /// <example>
    /// **Example usage**
    /// @code
    /// Airbridge.SetOnDeeplinkReceived((string url)  =>
    /// {
    ///     /* Process deeplink data */
    /// });
    /// @endcode
    /// </example>
    public static void SetOnDeeplinkReceived(Action<string> onDeeplinkReceived)
    {
        AirbridgeNullCheck.CallMethodWithNullCheck(() =>
        {
            AirbridgePlugin.SetOnDeeplinkReceived(AirbridgeNullCheck.RequireNonNull(onDeeplinkReceived));
        });
    }

    #endregion

    #region Data Collection

    /// <summary>
    /// Sets the user ID.
    /// </summary>
    /// <param name="id"> The user ID.</param>
    public static void SetUserID(string id)
    {
        AirbridgeNullCheck.CallMethodWithNullCheck(() =>
        {
            AirbridgePlugin.SetUserID(AirbridgeNullCheck.RequireNonNull(id));
        });
    }

    /// <summary>
    /// Clears the user ID.
    /// </summary>
    public static void ClearUserID()
    {
        AirbridgePlugin.ClearUserID();
    }

    /// <summary>
    /// Sets the user email.
    /// </summary>
    /// <param name="email"> The user email.</param>
    public static void SetUserEmail(string email)
    {
        AirbridgeNullCheck.CallMethodWithNullCheck(() =>
        {
            AirbridgePlugin.SetUserEmail(AirbridgeNullCheck.RequireNonNull(email));
        });
    }

    /// <summary>
    /// Clears the user email.
    /// </summary>
    public static void ClearUserEmail()
    {
        AirbridgePlugin.ClearUserEmail();
    }

    /// <summary>
    /// Sets the user phone number.
    /// </summary>
    /// <param name="phone"> The user phone number.</param>
    public static void SetUserPhone(string phone)
    {
        AirbridgeNullCheck.CallMethodWithNullCheck(() =>
        {
            AirbridgePlugin.SetUserPhone(AirbridgeNullCheck.RequireNonNull(phone));
        });
    }

    /// <summary>
    /// Clears the user phone number.
    /// </summary>
    public static void ClearUserPhone()
    {
        AirbridgePlugin.ClearUserPhone();
    }

    /// <summary>
    /// Sets the key, value pair to the user attribute.
    /// </summary>
    /// <param name="key"> The key that uniquely identifies the user attribute.</param>
    /// <param name="value"> The value to set for the user attribute.</param>
    public static void SetUserAttribute(string key, object value)
    {
        AirbridgeNullCheck.CallMethodWithNullCheck(() =>
        {
            AirbridgePlugin.SetUserAttribute(AirbridgeNullCheck.RequireNonNull(key), AirbridgeNullCheck.RequireNonNull(value));
        });
    }

    /// <summary>
    /// Removes the user attribute with the given key.
    /// </summary>
    /// <param name="key"> The key that uniquely identifies the user attribute.</param>
    public static void RemoveUserAttribute(string key)
    {
        AirbridgeNullCheck.CallMethodWithNullCheck(() =>
        {
            AirbridgePlugin.RemoveUserAttribute(AirbridgeNullCheck.RequireNonNull(key));
        });
    }

    /// <summary>
    /// Clears all user attributes.
    /// </summary>
    public static void ClearUserAttributes()
    {
        AirbridgePlugin.ClearUserAttributes();
    }

    /// <summary>
    /// Sets the key, value pair to the user alias.
    /// </summary>
    /// <param name="key"> The key that uniquely identifies the user alias.</param>
    /// <param name="value"> The value to set for the user alias.</param>
    public static void SetUserAlias(string key, string value)
    {
        AirbridgeNullCheck.CallMethodWithNullCheck(() =>
        {
            AirbridgePlugin.SetUserAlias(AirbridgeNullCheck.RequireNonNull(key), AirbridgeNullCheck.RequireNonNull(value));
        });
    }

    /// <summary>
    /// Removes the user alias with the given key.
    /// </summary>
    /// <param name="key"> The key that uniquely identifies the user alias.</param>
    public static void RemoveUserAlias(string key)
    {
        AirbridgeNullCheck.CallMethodWithNullCheck(() =>
        {
            AirbridgePlugin.RemoveUserAlias(AirbridgeNullCheck.RequireNonNull(key));
        });
    }

    /// <summary>
    /// Clears all user aliases.
    /// </summary>
    public static void ClearUserAlias()
    {
        AirbridgePlugin.ClearUserAlias();
    }

    /// <summary>
    /// Clears all user information.
    /// </summary>
    public static void ClearUser()
    {
        AirbridgePlugin.ClearUser();
    }


    /// <summary>
    /// Sets the key, value pair to the device alias.
    /// </summary>
    /// <param name="key"> The key that uniquely identifies the device alias.</param>
    /// <param name="value"> The value to set for the device alias.</param>
    public static void SetDeviceAlias(string key, string value)
    {
        AirbridgeNullCheck.CallMethodWithNullCheck(() =>
        {
            AirbridgePlugin.SetDeviceAlias(AirbridgeNullCheck.RequireNonNull(key), AirbridgeNullCheck.RequireNonNull(value));
        });
    }

    /// <summary>
    /// Removes the device alias with the given key.
    /// </summary>
    /// <param name="key"> The key that uniquely identifies the device alias.</param>
    public static void RemoveDeviceAlias(string key)
    {
        AirbridgeNullCheck.CallMethodWithNullCheck(() =>
        {
            AirbridgePlugin.RemoveDeviceAlias(AirbridgeNullCheck.RequireNonNull(key));
        }); 
    }

    /// <summary>
    /// Clears all device aliases.
    /// </summary>
    public static void ClearDeviceAlias()
    {
        AirbridgePlugin.ClearDeviceAlias();
    }


    /// <summary>
    /// Registers a push notification token to track app uninstalls.
    /// </summary>
    /// <param name="token"> The push notification token, typically obtained from the device’s notification service provider,
    /// such as APNs for Apple devices or FCM registration token for Android devices.</param>
    public static void RegisterPushToken(string token)
    {
        AirbridgeNullCheck.CallMethodWithNullCheck(() =>
        {
            AirbridgePlugin.RegisterPushToken(AirbridgeNullCheck.RequireNonNull(token));
        });
    }

    #endregion

    #region Event

    /// <summary>
    /// Track events.
    /// </summary>
    /// <param name="category"> The category of the event.</param>
    /// <param name="semanticAttributes"> The semantic attributes of the event.</param>
    /// <param name="customAttributes"> The custom attributes of the event.</param>
    public static void TrackEvent(
        string category,
        Dictionary<string, object> semanticAttributes = null,
        Dictionary<string, object> customAttributes = null
    )
    {
        AirbridgeNullCheck.CallMethodWithNullCheck(() =>
        {
            AirbridgePlugin.TrackEvent(AirbridgeNullCheck.RequireNonNull(category), semanticAttributes, customAttributes);
        });
    }

    #endregion

    #region Placement

    /// <summary>
    /// Queries the tracking link to the server to get the deeplink information and then moves according to that information if a deeplink is set up.
    /// It also adds a click event for the tracking link.
    /// </summary>
    /// <param name="trackingLink"> The URL to be queried for deeplink information and tracked for clicks.</param>
    /// <param name="onSuccess"> An optional callback action to be invoked if the click action is successful.</param>
    /// <param name="onFailure"> An optional callback action to be invoked if an error occurs during the click action.</param>
    /// <example>
    /// **Example usage**
    /// @code
    /// Airbridge.Click(
    ///     trackingLink: "https://abr.ge/~~~",
    ///     onSuccess: () => { /* Handle on success */ },
    ///     onFailure: (Exception exception) => { /* Handle on failure */ }
    /// );
    /// @endcode
    /// </example>
    public static void Click(string trackingLink, Action onSuccess = null, Action<Exception> onFailure = null)
    {
        AirbridgeNullCheck.CallMethodWithNullCheck(() =>
        {
            AirbridgePlugin.Click(AirbridgeNullCheck.RequireNonNull(trackingLink), onSuccess, onFailure);
        });
    }

    /// <summary>
    /// Adds an impression event for the tracking link.
    /// </summary>
    /// <param name="trackingLink"> The URL to be tracked for impressions.</param>
    /// <param name="onSuccess"> An optional callback action to be invoked if the impression action is successful.</param>
    /// <param name="onFailure"> An optional callback action to be invoked if an error occurs during the impression action.</param>
    /// <example>
    /// **Example usage**
    /// @code
    /// Airbridge.Impression(
    ///     trackingLink: "https://abr.ge/~~~",
    ///     onSuccess: () => { /* Handle on success */ },
    ///     onFailure: (Exception exception) => { /* Handle on failure */ }
    /// );
    /// @endcode
    /// </example>
    public static void Impression(string trackingLink, Action onSuccess = null, Action<Exception> onFailure = null)
    {
        AirbridgeNullCheck.CallMethodWithNullCheck(() =>
        {
            AirbridgePlugin.Impression(AirbridgeNullCheck.RequireNonNull(trackingLink), onSuccess, onFailure);
        });
    }

    #endregion

    #region Publication

    /// <summary>
    /// Request the Device UUID and get the response.
    /// </summary>
    /// <param name="onSuccess"> A callback action that takes a string parameter representing the Device UUID.</param>
    /// <param name="onFailure"> An optional callback action to be invoked if an error occurs.</param>
    /// <example>
    /// **Example usage**
    /// @code
    /// Airbridge.FetchDeviceUUID(
    ///     onSuccess: (string uuid) => { /* Process UUID */ },
    ///     onFailure: (Exception exception) => { /* Handle on failure */ }
    /// );
    /// @endcode
    /// </example>
    public static void FetchDeviceUUID(Action<string> onSuccess, Action<Exception> onFailure = null)
    {
        AirbridgeNullCheck.CallMethodWithNullCheck(() =>
        {
            AirbridgePlugin.FetchDeviceUUID(AirbridgeNullCheck.RequireNonNull(onSuccess), onFailure);
        });
    }

    /// <summary>
    /// Request the AirbridgeGeneratedUUID and get the response.
    /// </summary>
    /// <param name="onSuccess"> A callback action that takes a string parameter representing the %Airbridge Generated UUID.</param>
    /// <param name="onFailure"> An optional callback action to be invoked if an error occurs.</param>
    /// <example>
    /// **Example usage**
    /// @code
    /// Airbridge.FetchAirbridgeGeneratedUUID(
    ///     onSuccess: (string uuid) => { /* Process UUID */ },
    ///     onFailure: (Exception exception) => { /* Handle on failure */ }
    /// );
    /// @endcode
    /// </example>
    public static void FetchAirbridgeGeneratedUUID(Action<string> onSuccess, Action<Exception> onFailure = null)
    {
        AirbridgeNullCheck.CallMethodWithNullCheck(() =>
        {
            AirbridgePlugin.FetchAirbridgeGeneratedUUID(AirbridgeNullCheck.RequireNonNull(onSuccess), onFailure);
        });
    }

    /// <summary>
    /// Sets a listener for receiving attribution of install event.
    /// </summary>
    /// <param name="onAttributionReceived"> A callback listener that receives a Dictionary&lt;string, object&gt; parameter representing the attribution result.</param>
    /// <example>
    /// **Example usage**
    /// @code
    /// Airbridge.SetOnAttributionReceived((Dictionary&lt;string, object&gt; attributionResult) =>
    /// {
    ///     /* Process attribution data */
    /// });
    /// @endcode
    /// </example>
    public static void SetOnAttributionReceived(Action<Dictionary<string, object>> onAttributionReceived)
    {
        AirbridgeNullCheck.CallMethodWithNullCheck(() =>
        {
            AirbridgePlugin.SetOnAttributionReceived(AirbridgeNullCheck.RequireNonNull(onAttributionReceived));
        });
    }
    
    /// <summary>
    /// Creates a tracking-link using airbridge-server that move user to specific page of app and track click-event.
    /// </summary>
    /// <param name="channel"> The channel of tracking-link.</param>
    /// <param name="option"> The option to create tracking-link.</param>
    /// <param name="onSuccess"> A callback action that takes an AirbridgeTrackingLink parameter representing the tracking-link created.</param>
    /// <param name="onFailure"> An optional callback action to be invoked if an error occurs.</param>
    public static void CreateTrackingLink(
        string channel, 
        Dictionary<string, object> option, 
        Action<AirbridgeTrackingLink> onSuccess,
        Action<Exception> onFailure = null
    )
    {
        AirbridgeNullCheck.CallMethodWithNullCheck(() =>
        {
            AirbridgePlugin.CreateTrackingLink(
                AirbridgeNullCheck.RequireNonNull(channel),
                AirbridgeNullCheck.RequireNonNull(option),
                AirbridgeNullCheck.RequireNonNull(onSuccess),
                onFailure
            );
        });
    }

    #endregion

    #region Hybrid

    /// <summary>
    /// Creates a script that initialize the web interface to implement web interface manually.
    /// </summary>
    /// <param name="webToken"> The token to initialize %Airbridge Web SDK.</param>
    /// <param name="postMessageScript"> The JavaScript code to post commands from web to app.
    /// (The parameter name used to send messages from web to app is `payload`.) </param>
    /// <returns> The web interface script.</returns>
    public static string CreateWebInterfaceScript(string webToken, string postMessageScript)
    {
        return AirbridgeNullCheck.CallMethodWithNullCheck(
            () => AirbridgePlugin.CreateWebInterfaceScript(AirbridgeNullCheck.RequireNonNull(webToken),
                AirbridgeNullCheck.RequireNonNull(postMessageScript)),
            defaultValue: ""
        );
    }

    /// <summary>
    /// Handles commands from the web interface to implement web interface manually.
    /// </summary>
    /// <param name="command"> The command to handle.</param>
    public static void HandleWebInterfaceCommand(string command)
    {
        AirbridgeNullCheck.CallMethodWithNullCheck(() =>
        {
            AirbridgePlugin.HandleWebInterfaceCommand(AirbridgeNullCheck.RequireNonNull(command));
        });
    }

    #endregion

    /// <summary>
    /// Indicates whether notification was sent by %Airbridge to track uninstall of app.
    /// </summary>
    /// <param name="data"> The notification data to check.</param>
    /// <returns> `true` if notification was sent by %Airbridge, `false` otherwise.</returns>
    public static bool IsUninstallTrackingNotification(IDictionary<string, string> data)
    {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        return data.ContainsKey("airbridge-uninstall-tracking");
#else
        Debug.Log("Airbridge is not implemented this method on this platform.");
        return false;
#endif
    }
}