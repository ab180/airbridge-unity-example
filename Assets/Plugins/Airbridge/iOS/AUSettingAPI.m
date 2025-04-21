//
//  AUSettingAPI.m
//  AirbridgeUnity
//
//  Created by WOF on 29/11/2019.
//

#import "AUSettingAPI.h"
#import "AirbridgeUnity.h"
#import "AUGet.h"
#import "AUConvert.h"
#import "AUHex.h"

#import "AirBridge/AirBridge.h"

@interface AUSettingAPI (Internal)

+ (void) setInstance:(AUSettingAPI*)input;

@end

@implementation AUSettingAPI

static AUSettingAPI* instance;

//
// singleton
//

+ (AUSettingAPI*) instance {
    if (instance == nil) {
        instance = [[AUSettingAPI alloc] init];
    }
    
    return instance;
}

+ (void) setInstance:(AUSettingAPI*)input {
    instance = input;
}

//
// method
//

- (bool) isSDKEnabled {
    return [AirbridgeUnity isSDKEnabled];
}

- (void) startTracking {
    [AirbridgeUnity startTracking];
}

- (void) stopTracking {
    [AirbridgeUnity stopTracking];
}

- (void) setSessionTimeout:(uint64_t)timeout {
    [AirbridgeUnity setSessionTimeout:timeout];
}

- (void) setDeeplinkFetchTimeout:(uint64_t)timeout {
    [AirbridgeUnity setDeeplinkFetchTimeout:timeout];
}

- (void) setIsUserInfoHashed:(BOOL)enable {
    [AirbridgeUnity setIsUserInfoHashed:enable];
}

- (void) setIsTrackAirbridgeDeeplinkOnly:(BOOL)enable {
    [AirbridgeUnity setIsTrackAirbridgeDeeplinkOnly:enable];
}

- (void) registerPushToken:(NSData *)token {
    [AirbridgeUnity registerPushToken:token];
}

- (void)setOnInAppPurchaseReceived:(InAppPurchaseOnReceived)onReceived {
    [[AirBridge setting] setOnInAppPurchaseReceived:^ABInAppPurchase * _Nonnull(ABInAppPurchase * _Nonnull inAppPurchase) {
        NSMutableDictionary *dictionary = [NSMutableDictionary new];
        if (inAppPurchase.action != nil) {
            [dictionary setValue:inAppPurchase.action forKey:@"action"];
        }
        if (inAppPurchase.label != nil) {
            [dictionary setValue:inAppPurchase.label forKey:@"label"];
        }
        if (inAppPurchase.semanticAttributes != nil) {
            [dictionary setValue:inAppPurchase.semanticAttributes forKey:@"semanticAttributes"];
        }
        if (inAppPurchase.customAttributes != nil) {
            [dictionary setValue:inAppPurchase.customAttributes forKey:@"customAttributes"];
        }
        
        NSString *result = onReceived([AUConvert stringFromDictionary:dictionary]);
        if (result == nil) { return inAppPurchase; }
        
        NSDictionary *resultDictionary = [AUConvert dictionaryFromString:result];
        if (resultDictionary == nil) { return inAppPurchase; }

        ABInAppPurchase *newInAppPurchase = [ABInAppPurchase new];
        
        NSString *action = [resultDictionary objectForKey:@"action"];
        if (action != nil) { newInAppPurchase.action = action; }
        
        NSString *label = [resultDictionary objectForKey:@"label"];
        if (label != nil) { newInAppPurchase.label = label; }

        NSDictionary *semanticAttributes = [resultDictionary objectForKey:@"semanticAttributes"];
        if (semanticAttributes != nil) { newInAppPurchase.semanticAttributes = semanticAttributes; }
        
        NSDictionary *customAttributes = [resultDictionary objectForKey:@"customAttributes"];
        if (customAttributes != nil) { newInAppPurchase.customAttributes = customAttributes; }
        
        return newInAppPurchase;
    }];
}

@end

//
// unity method
//

bool native_isSDKEnabled() {
    return [AUSettingAPI.instance isSDKEnabled];
}

void native_startTracking() {
    [AUSettingAPI.instance startTracking];
}

void native_stopTracking() {
    [AUSettingAPI.instance stopTracking];
}

void native_registerPushToken(const char* __nonnull token) {
    NSString *tokenString = [AUConvert stringFromChars:token];
    NSData *tokenData = [AUHex dataFromHexString:tokenString];
   
    if (tokenData == nil || tokenData.length == 0) { return; }
    [AirbridgeUnity registerPushToken:tokenData];
}

void native_startInAppPurchaseTracking() {
    [AirbridgeUnity startInAppPurchaseTracking];
}

void native_stopInAppPurchaseTracking() {
    [AirbridgeUnity stopInAppPurchaseTracking];
}

void native_setInAppPurchaseEnvironment(const char *environmentString) {
    [AirbridgeUnity setInAppPurchaseEnvironment:[AUConvert stringFromChars:environmentString]];
}

void native_setOnInAppPurchaseReceived(UnityInAppPurchaseOnReceived onReceived) {
    [AUSettingAPI.instance setOnInAppPurchaseReceived:^NSString *(NSString * _Nonnull iapInformation) {
        const char *iapInformationChar = [AUConvert charsFromString:iapInformation];
        const char *converted = onReceived(iapInformationChar);
        return [AUConvert stringFromChars:converted];
    }];
}
