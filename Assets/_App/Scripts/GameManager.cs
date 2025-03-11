using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject valueInputDialog;
    [SerializeField] private GameObject entryInputDialog;
    [SerializeField] private GameObject typedEntryInputDialog;

    [SerializeField] private Text isSDKEnabledValue;
    [SerializeField] private Text isInAppPurchaseTrackingEnabledValue;
    [SerializeField] private Text isTrackingEnabledValue;

    [SerializeField] private Text airbridgeGeneratedUUIDValue;
    [SerializeField] private Text deviceUUIDValue;
    [SerializeField] private Text attributionTouchPointValue;

    public int ProductIndex { get; set; }
    private List<Dictionary<string, object>> beverages = new List<Dictionary<string, object>>();

    private bool CloseDialog()
    {
        var isOpen = valueInputDialog.activeSelf || entryInputDialog.activeSelf || typedEntryInputDialog.activeSelf;
        if (isOpen)
        {
            valueInputDialog.SetActive(false);
            entryInputDialog.SetActive(false);
            typedEntryInputDialog.SetActive(false);
        }

        return isOpen;
    }

    private void Awake()
    {
        CloseDialog();

#if UNITY_IOS && !UNITY_EDITOR
        AppTrackingTransparency.OnAuthorizationStatusReceived += OnAuthorizationStatusReceived;
        AppTrackingTransparency.AuthorizationStatus status = AppTrackingTransparency.TrackingAuthorizationStatus();
        if (status == AppTrackingTransparency.AuthorizationStatus.NotDetermined)
        {
            AppTrackingTransparency.RequestTrackingAuthorization();
        }
        else
        {
            Init();
        }
#else
        Init();
#endif
    }

#if UNITY_IOS && !UNITY_EDITOR
    void OnAuthorizationStatusReceived(AppTrackingTransparency.AuthorizationStatus status)
    {
        AppTrackingTransparency.OnAuthorizationStatusReceived -= OnAuthorizationStatusReceived;
        Init();
    }
#endif

    private void Init()
    {
        UpdateAirbridgeStatus();

        Airbridge.SetOnDeeplinkReceived(url =>
        {
            // Process deeplink data
            ToastMessage.Show(url);
        });

        InitBeverages();
        InitDataFetching();
    }

    #region Core

    private void UpdateAirbridgeStatus()
    {
        isSDKEnabledValue.text = $"Status : {(Airbridge.IsSDKEnabled() ? "Enabled" : "Disabled")}";
        // isInAppPurchaseTrackingEnabledValue = $"Status : {(Airbridge.IsInAppPurchaseTrackingEnabled() ? "Started" : "Stopped")}";
        isTrackingEnabledValue.text = $"Status : {(Airbridge.IsTrackingEnabled() ? "Started" : "Stopped")}";
    }

    public void EnableSDK()
    {
        Airbridge.EnableSDK();
        UpdateAirbridgeStatus();
    }

    public void DisableSDK()
    {
        Airbridge.DisableSDK();
        UpdateAirbridgeStatus();
    }

    // public void StartInAppPurchaseTracking()
    // {
    //     Airbridge.StartInAppPurchaseTracking(); 
    //     UpdateAirbridgeStatus();
    // }
    //
    // public void StopInAppPurchaseTracking()
    // {
    //     Airbridge.StopInAppPurchaseTracking();
    //     UpdateAirbridgeStatus();
    // }

    public void StartTracking()
    {
        Airbridge.StartTracking();
        UpdateAirbridgeStatus();
    }

    public void StopTracking()
    {
        Airbridge.StopTracking();
        UpdateAirbridgeStatus();
    }

    #endregion

    #region Data Collection

    public void SetUserID()
    {
        valueInputDialog.GetComponent<ValueInputDialog>().Show(
            ok: "SAVE",
            onOkClicked: Airbridge.SetUserID
        );
    }

    public void ClearUserID()
    {
        Airbridge.ClearUserID();
        ToastMessage.Show("user id cleared!");
    }

    public void SetUserEmail()
    {
        valueInputDialog.GetComponent<ValueInputDialog>().Show(
            ok: "SAVE",
            onOkClicked: Airbridge.SetUserEmail
        );
    }

    public void ClearUserEmail()
    {
        Airbridge.ClearUserEmail();
        ToastMessage.Show("user email cleared!");
    }

    public void SetUserPhone()
    {
        valueInputDialog.GetComponent<ValueInputDialog>().Show(
            ok: "SAVE",
            onOkClicked: Airbridge.SetUserPhone
        );
    }

    public void ClearUserPhone()
    {
        Airbridge.ClearUserPhone();
        ToastMessage.Show("user phone cleared!");
    }

    public void SetUserAttribute()
    {
        typedEntryInputDialog.GetComponent<TypedEntryInputDialog>().Show(
            ok: "SAVE",
            onOkClicked: (key, value, type) =>
            {
                switch (type)
                {
                    case AirbridgePropertyType.Int:
                        if (int.TryParse(value, out int intValue))
                        {
                            Airbridge.SetUserAttribute(key, intValue);
                        }
                        else ToastMessage.Show("Invalid 'Integer' string provided");

                        break;
                    case AirbridgePropertyType.Long:
                        if (long.TryParse(value, out long longValue))
                        {
                            Airbridge.SetUserAttribute(key, longValue);
                        }
                        else ToastMessage.Show("Invalid 'Long' string provided");

                        break;
                    case AirbridgePropertyType.Float:
                        if (float.TryParse(value, out float floatValue))
                        {
                            Airbridge.SetUserAttribute(key, floatValue);
                        }
                        else ToastMessage.Show("Invalid 'Float' string provided");

                        break;
                    case AirbridgePropertyType.Double:
                        if (double.TryParse(value, out double doubleValue))
                        {
                            Airbridge.SetUserAttribute(key, doubleValue);
                        }
                        else ToastMessage.Show("Invalid 'Double' string provided");

                        break;
                    case AirbridgePropertyType.Boolean:
                        if (bool.TryParse(value, out bool boolValue))
                        {
                            Airbridge.SetUserAttribute(key, boolValue);
                        }
                        else ToastMessage.Show("Invalid 'Boolean' string provided");

                        break;
                    case AirbridgePropertyType.String:
                        Airbridge.SetUserAttribute(key, value);
                        break;
                }
            }
        );
    }

    public void RemoveUserAttribute()
    {
        valueInputDialog.GetComponent<ValueInputDialog>().Show(
            ok: "SAVE",
            onOkClicked: Airbridge.RemoveUserAttribute
        );
    }

    public void ClearUserAttributes()
    {
        Airbridge.ClearUserAttributes();
    }

    public void SetUserAlias()
    {
        entryInputDialog.GetComponent<EntryInputDialog>().Show(
            ok: "SAVE",
            onOkClicked: Airbridge.SetUserAlias
        );
    }

    public void RemoveUserAlias()
    {
        valueInputDialog.GetComponent<ValueInputDialog>().Show(
            ok: "SAVE",
            onOkClicked: Airbridge.RemoveUserAlias
        );
    }

    public void ClearUserAlias()
    {
        Airbridge.ClearUserAlias();
    }

    public void ClearUser()
    {
        Airbridge.ClearUser();
    }

    public void SetDeviceAlias()
    {
        entryInputDialog.GetComponent<EntryInputDialog>().Show(
            ok: "SAVE",
            onOkClicked: Airbridge.SetDeviceAlias
        );
    }

    public void RemoveDeviceAlias()
    {
        valueInputDialog.GetComponent<ValueInputDialog>().Show(
            ok: "SAVE",
            onOkClicked: Airbridge.RemoveDeviceAlias
        );
    }

    public void ClearDeviceAlias()
    {
        Airbridge.ClearDeviceAlias();
    }

    #endregion

    #region Event

    // Create beverages
    private void InitBeverages()
    {
        Dictionary<string, object> cocacola = new Dictionary<string, object>()
        {
            [AirbridgeAttribute.PRODUCT_ID] = "beverage_1",
            [AirbridgeAttribute.PRODUCT_NAME] = "Coca Cola",
            [AirbridgeAttribute.PRODUCT_PRICE] = 1.25f,
            [AirbridgeAttribute.PRODUCT_CURRENCY] = "USD",
            [AirbridgeAttribute.PRODUCT_QUANTITY] = 1,
            [AirbridgeAttribute.PRODUCT_POSITION] = 0
        };
        beverages.Add(cocacola);

        Dictionary<string, object> cocacolaPack = new Dictionary<string, object>()
        {
            [AirbridgeAttribute.PRODUCT_ID] = "beverage_2",
            [AirbridgeAttribute.PRODUCT_NAME] = "Coca Cola 12 Pack",
            [AirbridgeAttribute.PRODUCT_PRICE] = 12.99f,
            [AirbridgeAttribute.PRODUCT_CURRENCY] = "USD",
            [AirbridgeAttribute.PRODUCT_QUANTITY] = 1,
            [AirbridgeAttribute.PRODUCT_POSITION] = 1
        };
        beverages.Add(cocacolaPack);

        Dictionary<string, object> fanta = new Dictionary<string, object>()
        {
            [AirbridgeAttribute.PRODUCT_ID] = "beverage_3",
            [AirbridgeAttribute.PRODUCT_NAME] = "Fanta",
            [AirbridgeAttribute.PRODUCT_PRICE] = 1500f,
            [AirbridgeAttribute.PRODUCT_CURRENCY] = "KRW",
            [AirbridgeAttribute.PRODUCT_QUANTITY] = 1,
            [AirbridgeAttribute.PRODUCT_POSITION] = 2
        };
        beverages.Add(fanta);

        Dictionary<string, object> fantaWithCocacola = new Dictionary<string, object>()
        {
            [AirbridgeAttribute.PRODUCT_ID] = "beverage_4",
            [AirbridgeAttribute.PRODUCT_NAME] = "Fanta & Coca cola",
            [AirbridgeAttribute.PRODUCT_PRICE] = 2500f,
            [AirbridgeAttribute.PRODUCT_CURRENCY] = "KRW",
            [AirbridgeAttribute.PRODUCT_QUANTITY] = 1,
            [AirbridgeAttribute.PRODUCT_POSITION] = 3
        };
        beverages.Add(fantaWithCocacola);
    }

    public void SignUp()
    {
        Airbridge.TrackEvent(AirbridgeCategory.SIGN_UP);
    }

    public void SignIn()
    {
        Airbridge.TrackEvent(AirbridgeCategory.SIGN_IN);
    }

    public void SignOut()
    {
        Airbridge.TrackEvent(AirbridgeCategory.SIGN_OUT);
    }

    public void HomeViewed()
    {
        Airbridge.TrackEvent(AirbridgeCategory.HOME_VIEWED);
    }

    public void SearchResultViewed()
    {
        Airbridge.TrackEvent(
            category: AirbridgeCategory.SEARCH_RESULTS_VIEWED,
            semanticAttributes: new Dictionary<string, object>()
            {
                [AirbridgeAttribute.QUERY] = "SELECT * FROM beverages",
                [AirbridgeAttribute.PRODUCTS] = beverages
            }
        );
    }

    public void ProductListViewed()
    {
        Airbridge.TrackEvent(
            category: AirbridgeCategory.PRODUCT_LIST_VIEWED,
            semanticAttributes: new Dictionary<string, object>()
            {
                [AirbridgeAttribute.PRODUCT_LIST_ID] = "beverage_list_0",
                [AirbridgeAttribute.PRODUCTS] = beverages
            }
        );
    }

    public void ProductViewed()
    {
        Airbridge.TrackEvent(
            category: AirbridgeCategory.PRODUCT_VIEWED,
            semanticAttributes: new Dictionary<string, object>()
            {
                [AirbridgeAttribute.PRODUCTS] = beverages[ProductIndex]
            }
        );
    }

    public void AddedToCart()
    {
        Airbridge.TrackEvent(
            category: AirbridgeCategory.ADDED_TO_CART,
            semanticAttributes: new Dictionary<string, object>()
            {
                [AirbridgeAttribute.VALUE] = beverages[ProductIndex][AirbridgeAttribute.PRODUCT_PRICE],
                [AirbridgeAttribute.CART_ID] = "cart_1",
                [AirbridgeAttribute.PRODUCTS] = beverages[ProductIndex],
                [AirbridgeAttribute.CURRENCY] = beverages[ProductIndex][AirbridgeAttribute.PRODUCT_CURRENCY]
            }
        );
    }

    public void OrderCompleted()
    {
        Airbridge.TrackEvent(
            category: AirbridgeCategory.ORDER_COMPLETED,
            semanticAttributes: new Dictionary<string, object>()
            {
                [AirbridgeAttribute.VALUE] = beverages[ProductIndex][AirbridgeAttribute.PRODUCT_PRICE],
                [AirbridgeAttribute.TRANSACTION_ID] = "transaction_123",
                [AirbridgeAttribute.PRODUCTS] = beverages[ProductIndex],
                [AirbridgeAttribute.IN_APP_PURCHASED] = true,
                [AirbridgeAttribute.CURRENCY] = beverages[ProductIndex][AirbridgeAttribute.PRODUCT_CURRENCY]
            }
        );
    }

    public void CustomEvent()
    {
        Airbridge.TrackEvent(
            category: "custom category",
            semanticAttributes: new Dictionary<string, object>()
            {
                [AirbridgeAttribute.ACTION] = "custom action",
                [AirbridgeAttribute.LABEL] = "custom label",
                [AirbridgeAttribute.VALUE] = 12345,
                [AirbridgeAttribute.QUERY] = "query123",
                [AirbridgeAttribute.CURRENCY] = "usd",
                [AirbridgeAttribute.TOTAL_QUANTITY] = 10,
                [AirbridgeAttribute.CART_ID] = "cart_1234"
            },
            customAttributes: new Dictionary<string, object>()
            {
                ["custom_int"] = 123,
                ["custom_string"] = "text123",
                ["custom_double"] = 123.456,
                ["custom_map"] = new Dictionary<string, string> { { "map_key", "test123" } },
                ["custom_list"] = new List<string>() { "value1", "value2", "value3" }
            }
        );
    }

    #endregion

    #region Data Fetching

    public void InitDataFetching()
    {
        airbridgeGeneratedUUIDValue.text = default;
        deviceUUIDValue.text = default;
        attributionTouchPointValue.text = default;

        Airbridge.SetOnAttributionReceived(attributionResult =>
        {
            attributionTouchPointValue.text = MiniJson.Json.Serialize(attributionResult);
        });

        Airbridge.FetchAirbridgeGeneratedUUID(uuid => { airbridgeGeneratedUUIDValue.text = uuid; });
        Airbridge.FetchDeviceUUID(uuid => { deviceUUIDValue.text = uuid; });
    }

    #endregion

    public void DocumentTrackEventExample()
    {
        Airbridge.TrackEvent(
            category: AirbridgeCategory.ORDER_COMPLETED,
            semanticAttributes: new Dictionary<string, object>()
            {
                [AirbridgeAttribute.PRODUCTS] = new List<Dictionary<string, object>>()
                {
                    new Dictionary<string, object>()
                    {
                        [AirbridgeAttribute.PRODUCT_ID] = "coca_1",
                        [AirbridgeAttribute.PRODUCT_NAME] = "Coca Cola",
                        [AirbridgeAttribute.PRODUCT_CURRENCY] = "usd",
                        [AirbridgeAttribute.PRODUCT_PRICE] = 1.99f,
                        [AirbridgeAttribute.PRODUCT_QUANTITY] = 10,
                        [AirbridgeAttribute.PRODUCT_POSITION] = 1
                    }
                },
                [AirbridgeAttribute.IN_APP_PURCHASED] = true,
                [AirbridgeAttribute.TRANSACTION_ID] = "transaction_1",
                [AirbridgeAttribute.TOTAL_QUANTITY] = 1000,
                [AirbridgeAttribute.QUERY] = "SELECT * FROM beverages"
            },
            customAttributes: new Dictionary<string, object>()
            {
                ["custom_attribute_key"] = "custom_attribute_value"
            }
        );
    }
}