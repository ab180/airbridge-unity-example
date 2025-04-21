//
//  AirbridgeUnity+Interface.m
//  UnityFramework
//
//  Created by mjgu on 8/12/24.
//

#import "AirbridgeUnity+Interface.h"

#import <Airbridge/Airbridge.h>
#import <StoreKit/StoreKit.h>

#import "AUConvert.h"

@implementation AirbridgeUnity (Interface)

- (void)setOnInAppPurchaseReceived:(InAppPurchaseOnReceived)onReceived {
    self.inAppPurchaseOnReceived = onReceived;
}

@end

void native_HandleWebInterfaceCommand(const char* command) {
    [Airbridge handleWebInterfaceCommand:[AUConvert stringFromChars:command]];
}

const char * native_CreateWebInterfaceScript(const char* webToken, const char* postMessageScript) {
    NSString *script = [Airbridge
        createWebInterfaceScriptWithWebToken:[AUConvert stringFromChars:webToken]
        postMessageScript:[AUConvert stringFromChars:postMessageScript]
    ];
    
    return [AUConvert charsFromString:script];
}

void native_startInAppPurchaseTracking() {
    [Airbridge startInAppPurchaseTracking];
}

void native_stopInAppPurchaseTracking() {
    [Airbridge stopInAppPurchaseTracking];
}

bool native_isInAppPurchaseTrackingEnabled() {
    return Airbridge.isInAppPurchaseTrackingEnabled; 
}

void native_setOnInAppPurchaseReceived(UnityInAppPurchaseOnReceived onReceived) {
    [AirbridgeUnity.sharedInstance setOnInAppPurchaseReceived:^(AirbridgeInAppPurchase * _Nonnull inAppPurchase) {
        NSMutableDictionary *transactionDictionary = [NSMutableDictionary dictionary];
        SKPaymentTransaction *transaction = inAppPurchase.transaction;
        
        if (transaction.transactionIdentifier) {
            [transactionDictionary setObject:transaction.transactionIdentifier forKey:@"transactionIdentifier"];
        }
        
        if (transaction.transactionDate) {
            [transactionDictionary setObject:@([transaction.transactionDate timeIntervalSince1970]) forKey:@"transactionDate"];
        }
        
        if (transaction.payment) {
            [transactionDictionary setObject:transaction.payment.productIdentifier forKey:@"productIdentifier"];
            [transactionDictionary setObject:@(transaction.payment.quantity) forKey:@"quantity"];
        }
        
        const char *resultCString = onReceived([AUConvert charsFromDictionary:@{
            @"transaction": transactionDictionary
        }]);
        if (resultCString == nil) { return; }
        
        NSDictionary *resultDictionary = [AUConvert dictionaryFromJSONChars:resultCString];
        NSDictionary *semanticDictionary = [resultDictionary objectForKey:@"semanticAttributes"];
        if (semanticDictionary) {
            [inAppPurchase setSemanticAttributes:semanticDictionary];
        }
        
        NSDictionary *customDictionary = [resultDictionary objectForKey:@"customAttributes"];
        if (customDictionary) {
            [inAppPurchase setCustomAttributes:customDictionary];
        }
    }];
}
