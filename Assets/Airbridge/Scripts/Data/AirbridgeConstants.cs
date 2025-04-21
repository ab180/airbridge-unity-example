/// <summary>
/// Contains category of standard events.
/// </summary>
public static class AirbridgeCategory
{
    public const string SIGN_UP                     = "airbridge.user.signup";
    public const string SIGN_IN                     = "airbridge.user.signin";
    public const string SIGN_OUT                    = "airbridge.user.signout";

    public const string HOME_VIEWED                 = "airbridge.ecommerce.home.viewed";
    public const string SEARCH_RESULTS_VIEWED       = "airbridge.ecommerce.searchResults.viewed";
    public const string PRODUCT_LIST_VIEWED         = "airbridge.ecommerce.productList.viewed";
    public const string PRODUCT_VIEWED              = "airbridge.ecommerce.product.viewed";

    public const string ADDED_TO_CART               = "airbridge.ecommerce.product.addedToCart";
    public const string ORDER_COMPLETED             = "airbridge.ecommerce.order.completed";

    public const string ADD_PAYMENT_INFO            = "airbridge.addPaymentInfo";
    public const string ADD_TO_WISHLIST             = "airbridge.addToWishlist";
    public const string INITIATE_CHECKOUT           = "airbridge.initiateCheckout";
    public const string ORDER_CANCELED              = "airbridge.ecommerce.order.canceled";
    public const string START_TRIAL                 = "airbridge.startTrial";
    public const string SUBSCRIBE                   = "airbridge.subscribe";
    public const string UNSUBSCRIBE                 = "airbridge.unsubscribe";
    public const string AD_IMPRESSION               = "airbridge.adImpression";
    public const string AD_CLICK                    = "airbridge.adClick";
    public const string COMPLETE_TUTORIAL           = "airbridge.completeTutorial";
    public const string ACHIEVE_LEVEL               = "airbridge.achieveLevel";
    public const string UNLOCK_ACHIEVEMENT          = "airbridge.unlockAchievement";
    public const string RATE                        = "airbridge.rate";
    public const string SHARE                       = "airbridge.share";
    public const string SCHEDULE                    = "airbridge.schedule";
    public const string SPEND_CREDITS               = "airbridge.spendCredits";
}

/// <summary>
/// Contains key of semantic attributes.
/// </summary>
public static class AirbridgeAttribute
{
    public const string ACTION  = "action";
    public const string LABEL   = "label";
    public const string VALUE   = "value";

    public const string PRODUCT_LIST_ID         = "productListID";
    public const string CART_ID                 = "cartID";
    public const string TRANSACTION_ID          = "transactionID";
    public const string IN_APP_PURCHASED        = "inAppPurchased";
    public const string PRODUCTS                = "products";
    public const string CURRENCY                = "currency";
    public const string TOTAL_VALUE             = "totalValue";
    public const string TOTAL_QUANTITY          = "totalQuantity";
    public const string QUERY                   = "query";

    public const string PERIOD                              = "period";
    public const string IS_RENEWAL                          = "isRenewal";
    public const string RENEWAL_COUNT                       = "renewalCount";
    public const string TRANSACTION_TYPE                    = "transactionType";
    public const string TRANSACTION_PAIRED_EVENT_CATEGORY   = "transactionPairedEventCategory";
    public const string TRANSACTION_PAIRED_EVENT_TIMESTAMP  = "transactionPairedEventTimestamp";
    public const string CONTRIBUTION_MARGIN                 = "contributionMargin";
    public const string LIST_ID                             = "listID";
    public const string RATE_ID                             = "rateID";
    public const string RATE                                = "rate";
    public const string MAX_RATE                            = "maxRate";
    public const string ACHIEVEMENT_ID                      = "achievementID";
    public const string SHARED_CHANNEL                      = "sharedChannel";
    public const string DATE_TIME                           = "datetime";
    public const string DESCRIPTION                         = "description";
    public const string IS_REVENUE                          = "isRevenue";
    public const string PLACE                               = "place";
    public const string SCHEDULE_ID                         = "scheduleID";
    public const string TYPE                                = "type";
    public const string LEVEL                               = "level";
    public const string SCORE                               = "score";
    public const string AD_PARTNERS                         = "adPartners";
    public const string IS_FIRST_PER_USER                   = "isFirstPerUser";

    public const string PRODUCT_ID              = "productID";
    public const string PRODUCT_NAME            = "name";
    public const string PRODUCT_PRICE           = "price";
    public const string PRODUCT_QUANTITY        = "quantity";
    public const string PRODUCT_CURRENCY        = "currency";
    public const string PRODUCT_POSITION        = "position";
    public const string PRODUCT_CATEGORY_ID     = "categoryID";
    public const string PRODUCT_CATEGORY_NAME   = "categoryName";
    public const string PRODUCT_BRAND_ID        = "brandID";
    public const string PRODUCT_BRAND_NAME      = "brandName";
}

/// <summary>
/// Contains key of tracking-link option.
/// </summary>
public static class AirbridgeTrackingLinkOption
{
    /// A campaign parameter. (String)
    public const string CAMPAIGN = "campaign";

    /// A campaign parameter. (String)
    public const string AD_GROUP = "ad_group";

    /// A campaign parameter. (String)
    public const string AD_CREATIVE = "ad_creative";

    /// A campaign parameter. (String)
    public const string CONTENT = "content";

    /// A campaign parameter. (String)
    public const string TERM = "term";

    /// A campaign parameter. (String)
    public const string SUB_ID = "sub_id";

    /// A campaign parameter. (String)
    public const string SUB_ID_1 = "sub_id_1";

    /// A campaign parameter. (String)
    public const string SUB_ID_2 = "sub_id_2";

    /// A campaign parameter. (String)
    public const string SUB_ID_3 = "sub_id_3";

    /// A url of deeplink open the app. (Custom Scheme URL String)
    public const string DEEPLINK_URL = "deeplink_url";

    /// A option enable stop-over-feature of deeplink. (Boolean)
    public const string DEEPLINK_STOPOVER = "deeplink_stopover";

    /// A url of fallback for ios if app is not installed. ("store" | Web URL String)
    public const string FALLBACK_IOS = "fallback_ios";

    /// A url of fallback for android if app is not installed. ("store" | Web URL String)
    public const string FALLBACK_ANDROID = "fallback_android";

    /// A url of fallback for desktop if app is not installed. (Web URL String)
    public const string FALLBACK_DESKTOP = "fallback_desktop";

    /// A custom id of store for ios used when user move to store. (String)
    public const string FALLBACK_IOS_STORE_PPID = "fallback_ios_store_ppid";

    /// A custom id of store for android used when user move to store. (String)
    public const string FALLBACK_ANDROID_STORE_LISTING = "fallback_android_store_listing";

    /// A title of link-preview. (String)
    public const string OGTAG_TITLE = "ogtag_title";

    /// A description of link-preview. (String)
    public const string OGTAG_DESCRIPTION = "ogtag_description";

    /// A image-url of link-preview. (Web URL String)
    public const string OGTAG_IMAGE_URL = "ogtag_image_url";

    /// A customization of link-preview. ("desktop")
    public const string OGTAG_WEBSITE_CRAWL = "ogtag_website_crawl";

    /// A custom-short-id of tracking-link. (String)
    public const string CUSTOM_SHORT_ID = "custom_short_id";

    /// A option enable re-engagement-feature of attribution. ("off" | "on_true" | "on_false")
    public const string IS_REENGAGEMENT = "is_reengagement";
}