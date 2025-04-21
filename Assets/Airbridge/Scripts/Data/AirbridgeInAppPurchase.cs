using System;
using System.Collections.Generic;

// ReSharper disable InconsistentNaming
// ReSharper disable once CheckNamespace

/// <summary>
/// Represents an %Airbridge in-app purchase event.
/// </summary>
public class AirbridgeInAppPurchase
{
    // Common
    private const string SEMANTIC_ATTRIBUTES_KEY    = "semanticAttributes";
    private const string CUSTOM_ATTRIBUTES_KEY      = "customAttributes";
    // iOS Only
    private const string TRANSACTION_KEY            = "transaction";
    // Android Only
    private const string PURCHASE_KEY               = "purchase";

    private readonly Dictionary<string, object> _data;

    internal AirbridgeInAppPurchase(Dictionary<string, object> data)
    {
#if UNITY_IOS
        _data = new Dictionary<string, object>()
        {
            { TRANSACTION_KEY, data }
        };
#elif UNITY_ANDROID
        _data = new Dictionary<string, object>()
        {
            { PURCHASE_KEY, data }
        };
#else
        _data = new Dictionary<string, object>();
#endif
    }
    
    /// <summary>
    /// Sets the semantic attributes for the in-app purchase event.
    /// </summary>
    /// <param name="attributes"> A dictionary of semantic attributes.</param>
    public void SetSemanticAttributes(Dictionary<string, object> attributes)
    {
        AddData(SEMANTIC_ATTRIBUTES_KEY, attributes);
    }
    
    /// <summary>
    /// Sets custom attributes for the in-app purchase event.
    /// </summary>
    /// <param name="attributes"> A dictionary of custom attributes.</param>
    public void SetCustomAttributes(Dictionary<string, object> attributes)
    {
        AddData(CUSTOM_ATTRIBUTES_KEY, attributes);
    }
    
    /// <summary>
    /// Gets the iOS-specific transaction data associated with the in-app purchase.
    /// @note
    /// This data is only present if the purchase occurred on an <b>iOS</b> device.
    /// </summary>
    public Dictionary<string, object> Transaction
    {
        get
        {
            try
            {
                if (_data.TryGetValue(TRANSACTION_KEY, out var value))
                {
                    return (Dictionary<string, object>)value;
                }
                
                return new Dictionary<string, object>();
            }
            catch (Exception)
            {
                return new Dictionary<string, object>();
            }
        }
    }
    
    /// <summary>
    /// Gets the Android-specific purchase data associated with the in-app purchase.
    /// @note
    /// This data is only present if the purchase occurred on an <b>Android</b> device.
    /// </summary>
    public Dictionary<string, object> Purchase
    {
        get
        {
            try
            {
                if (_data.TryGetValue(PURCHASE_KEY, out var value))
                {
                    return (Dictionary<string, object>)value;
                }
                
                return new Dictionary<string, object>();
            }
            catch (Exception)
            {
                return new Dictionary<string, object>();
            }
        }
    }
    
    internal Dictionary<string, object> ToDictionary()
    {
        return _data;
    }

    private void AddData(string key, object value)
    {
        _data[key] = value;
    }
}
