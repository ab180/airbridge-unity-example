using System;

namespace Airbridge.Constants
{
    public static class CATEGORY
    {
        public const string SIGN_UP     = "airbridge.user.signup";
        public const string SIGN_IN     = "airbridge.user.signin";
        public const string SIGN_OUT    = "airbridge.user.signout";

        [Obsolete("The constant VIEW_HOME will be deprecated. Consider using HOME_VIEWED instead.", false)]
        public const string VIEW_HOME               = "airbridge.ecommerce.home.viewed";
        public const string HOME_VIEWED             = "airbridge.ecommerce.home.viewed";
        
        [Obsolete("The constant VIEW_SEARCH_RESULT will be deprecated. Consider using SEARCH_RESULTS_VIEWED instead.", false)]
        public const string VIEW_SEARCH_RESULT      = "airbridge.ecommerce.searchResults.viewed";
        public const string SEARCH_RESULTS_VIEWED   = "airbridge.ecommerce.searchResults.viewed";
        
        [Obsolete("The constant VIEW_PRODUCT_LIST will be deprecated. Consider using PRODUCT_LIST_VIEWED instead.", false)]
        public const string VIEW_PRODUCT_LIST       = "airbridge.ecommerce.productList.viewed";
        public const string PRODUCT_LIST_VIEWED     = "airbridge.ecommerce.productList.viewed";
        
        [Obsolete("The constant VIEW_PRODUCT_DETAILS will be deprecated. Consider using PRODUCT_VIEWED instead.", false)]
        public const string VIEW_PRODUCT_DETAILS    = "airbridge.ecommerce.product.viewed";
        public const string PRODUCT_VIEWED          = "airbridge.ecommerce.product.viewed";

        [Obsolete("The constant ADD_TO_CART will be deprecated. Consider using ADDED_TO_CART instead.", false)]
        public const string ADD_TO_CART     = "airbridge.ecommerce.product.addedToCart";
        public const string ADDED_TO_CART   = "airbridge.ecommerce.product.addedToCart";
        
        [Obsolete("The constant ORDER_COMPLETE will be deprecated. Consider using ORDER_COMPLETED instead.", false)]
        public const string ORDER_COMPLETE  = "airbridge.ecommerce.order.completed";
        public const string ORDER_COMPLETED = "airbridge.ecommerce.order.completed";

        public const string ADD_PAYMENT_INFO              = "airbridge.addPaymentInfo";
        public const string ADD_TO_WISHLIST               = "airbridge.addToWishlist";
        public const string INITIATE_CHECKOUT             = "airbridge.initiateCheckout";
        public const string ORDER_CANCELED                = "airbridge.ecommerce.order.canceled";
        public const string START_TRIAL                   = "airbridge.startTrial";
        public const string SUBSCRIBE                     = "airbridge.subscribe";
        public const string UNSUBSCRIBE                   = "airbridge.unsubscribe";
        public const string AD_IMPRESSION                 = "airbridge.adImpression";
        public const string AD_CLICK                      = "airbridge.adClick";
        public const string COMPLETE_TUTORIAL             = "airbridge.completeTutorial";
        public const string ACHIEVE_LEVEL                 = "airbridge.achieveLevel";
        public const string UNLOCK_ACHIEVEMENT            = "airbridge.unlockAchievement";
        public const string RATE                          = "airbridge.rate";
        public const string SHARE                         = "airbridge.share";
        public const string SCHEDULE                      = "airbridge.schedule";
        public const string SPEND_CREDITS                 = "airbridge.spendCredits";
    }
}