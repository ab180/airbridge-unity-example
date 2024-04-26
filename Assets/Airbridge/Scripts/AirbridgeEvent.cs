using System.Collections.Generic;

public class AirbridgeEvent
{
    private const string categoryKey    = "category";
    private const string actionKey      = "action";
    private const string labelKey       = "label";
    private const string valueKey       = "value";

    private const string semanticAttributesKey = "semanticAttributes";
    
    private const string queryKey           = "query";
    private const string cartIdKey          = "cartID";
    private const string productListIdKey   = "productListID";
    private const string transactionIdKey   = "transactionID";
    private const string inAppPurchasedKey  = "inAppPurchased";
    private const string currencyKey        = "currency";
    private const string totalValueKey      = "totalValue";
    private const string totalQuantityKey   = "totalQuantity";
    private const string productsKey        = "products";

    private const string originalCurrencyKey                = "originalCurrency";
    private const string periodKey                          = "period";
    private const string isRenewalKey                       = "isRenewal";
    private const string renewalCountKey                    = "renewalCount";
    private const string transactionTypeKey                 = "transactionType";
    private const string transactionPairedEventCategoryKey  = "transactionPairedEventCategory";
    private const string transactionPairedEventTimestampKey = "transactionPairedEventTimestamp";
    private const string contributionMarginKey              = "contributionMargin";
    private const string originalContributionMarginKey      = "originalContributionMargin";
    private const string listIdKey                          = "listID";
    private const string rateIdKey                          = "rateID";
    private const string rateKey                            = "rate";
    private const string maxRateKey                         = "maxRate";
    private const string achievementIdKey                   = "achievementID";
    private const string sharedChannelKey                   = "sharedChannel";
    private const string datetimeKey                        = "datetime";
    private const string descriptionKey                     = "description";
    private const string isRevenueKey                       = "isRevenue";
    private const string placeKey                           = "place";
    private const string scheduleIdKey                      = "scheduleID";
    private const string typeKey                            = "type";
    private const string levelKey                           = "level";
    private const string scoreKey                           = "score";
    
    private const string customAttributesKey = "customAttributes";

    private Dictionary<string, object> data = new Dictionary<string, object>();
    private Dictionary<string, object> semanticAttributes = new Dictionary<string, object>();
    private Dictionary<string, object> customAttributes = new Dictionary<string, object>();

    public AirbridgeEvent(string category)
    {
        AddData(categoryKey, category);
        AddData(semanticAttributesKey, semanticAttributes);
        AddData(customAttributesKey, customAttributes);
    }

    #region default attributes

    public void SetAction(string action)
    {
        AddData(actionKey, action);
    }

    public void SetLabel(string label)
    {
        AddData(labelKey, label);
    }

    public void SetValue(double value)
    {
        AddData(valueKey, value);
    }

    #endregion

    #region semantic attributes

    public void SetQuery(string query)
    {
        AddSemanticAttribute(queryKey, query);
    }

    public void SetCartId(string cartId)
    {
        AddSemanticAttribute(cartIdKey, cartId);
    }

    public void SetTransactionId(string transactionId)
    {
        AddSemanticAttribute(transactionIdKey, transactionId);
    }

    public void SetProducts(params Airbridge.Ecommerce.Product[] products)
    {
        List<Dictionary<string, object>> serialized = new List<Dictionary<string, object>>();
        foreach (Airbridge.Ecommerce.Product product in products)
        {
            serialized.Add(product.ToDictionary());
        }
        AddSemanticAttribute(productsKey, serialized);
    }

    public void SetProductListId(string productListId)
    {
        AddSemanticAttribute(productListIdKey, productListId);
    }

    public void SetInAppPurchased(bool inAppPurchased)
    {
        AddSemanticAttribute(inAppPurchasedKey, inAppPurchased);
    }

    public void SetCurrency(string currency)
    {
        AddSemanticAttribute(currencyKey, currency);
    }

    public void SetTotalValue(double totalValue)
    {
        AddSemanticAttribute(totalValueKey, totalValue);
    }

    public void SetTotalQuantity(int totalQuantity)
    {
        AddSemanticAttribute(totalQuantityKey, totalQuantity);
    }

    public void SetOriginalCurrency(string originalCurrency)
    {
        AddSemanticAttribute(originalCurrencyKey, originalCurrency);
    }
    
    public void SetPeriod(string period)
    {
        AddSemanticAttribute(periodKey, period);
    }
   
    public void SetIsRenewal(bool isRenewal)
    {
        AddSemanticAttribute(isRenewalKey, isRenewal);
    }
    
    public void SetRenewalCount(int renewalCount)
    {
        AddSemanticAttribute(renewalCountKey, renewalCount);
    }
    
    public void SetTransactionType(string transactionType)
    {
        AddSemanticAttribute(transactionTypeKey, transactionType);
    }
    
    public void SetTransactionPairedEventCategory(string transactionPairedEventCategory)
    {
        AddSemanticAttribute(transactionPairedEventCategoryKey, transactionPairedEventCategory);
    }
    
    public void SetTransactionPairedEventTimestamp(int transactionPairedEventTimestamp)
    {
        AddSemanticAttribute(transactionPairedEventTimestampKey, transactionPairedEventTimestamp);
    }
    
    public void SetContributionMargin(double contributionMargin)
    {
        AddSemanticAttribute(contributionMarginKey, contributionMargin);
    }
    
    public void SetOriginalContributionMargin(double originalContributionMargin)
    {
        AddSemanticAttribute(originalContributionMarginKey, originalContributionMargin);
    }
    
    public void SetListId(string listId)
    {
        AddSemanticAttribute(listIdKey, listId);
    }
    
    public void SetRateId(string rateId)
    {
        AddSemanticAttribute(rateIdKey, rateId);
    }
    
    public void SetRate(double rate)
    {
        AddSemanticAttribute(rateKey, rate);
    }
    
    public void SetMaxRate(double maxRate)
    {
        AddSemanticAttribute(maxRateKey, maxRate);
    }
    
    public void SetAchievementId(string achievementId)
    {
        AddSemanticAttribute(achievementIdKey, achievementId);
    }
    
    public void SetSharedChannel(string sharedChannel)
    {
        AddSemanticAttribute(sharedChannelKey, sharedChannel);
    }
    
    public void SetDatetime(string datetime)
    {
        AddSemanticAttribute(datetimeKey, datetime);
    }
    
    public void SetDescription(string description)
    {
        AddSemanticAttribute(descriptionKey, description);
    }
    
    public void SetIsRevenue(bool isRevenue)
    {
        AddSemanticAttribute(isRevenueKey, isRevenue);
    }
    
    public void SetPlace(string place)
    {
        AddSemanticAttribute(placeKey, place);
    }
    
    public void SetScheduleId(string scheduleId)
    {
        AddSemanticAttribute(scheduleIdKey, scheduleId);
    }
    
    public void SetType(string type)
    {
        AddSemanticAttribute(typeKey, type);
    }
    
    public void SetLevel(string level)
    {
        AddSemanticAttribute(levelKey, level);
    }
    
    public void SetScore(double score)
    {
        AddSemanticAttribute(scoreKey, score);
    }

    public void AddSemanticAttribute(string key, object value)
    {
        if (!semanticAttributes.ContainsKey(key))
        {
            semanticAttributes.Add(key, value);
        }
        else
        {
            semanticAttributes[key] = value;
        }
    }

    #endregion

    #region custom attributes

    public void AddCustomAttribute(string key, object value)
    {
        if (customAttributes.ContainsKey(key))
        {
            customAttributes[key] = value;
        }
        else
        {
            customAttributes.Add(key, value);
        }
    }

    #endregion

    public string ToJsonString()
    {
        return AirbridgeJson.Serialize(data);
    }

    private void AddData(string key, object value)
    {
        if (!data.ContainsKey(key))
        {
            data.Add(key, value);
        }
        else
        {
            data[key] = value;
        }
    }
}