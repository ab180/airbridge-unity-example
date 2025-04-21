//
//  AUSettingAPI.h
//  AirbridgeUnity
//
//  Created by WOF on 29/11/2019.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

typedef NSString * _Nullable (^InAppPurchaseOnReceived)(NSString *iapInformation);
typedef const char * _Nullable (*UnityInAppPurchaseOnReceived)(const char* iapInformation);


@interface AUSettingAPI : NSObject

+ (AUSettingAPI*) instance;

- (void) startTracking;
- (void) stopTracking;
- (void) registerPushToken:(NSData *)token;
- (void) setSessionTimeout:(uint64_t)timeout;
- (void) setDeeplinkFetchTimeout:(uint64_t)timeout;
- (void) setIsUserInfoHashed:(BOOL)enable;
- (void) setIsTrackAirbridgeDeeplinkOnly:(BOOL)enable;

@end

void native_startTracking(void);
void native_stopTracking(void);
void native_registerPushToken(const char* __nonnull token);
void native_startInAppPurchaseTracking(void);
void native_stopInAppPurchaseTracking(void);
void native_setInAppPurchaseEnvironment(const char *environmentString);
void native_setOnInAppPurchaseReceived(UnityInAppPurchaseOnReceived onReceived);

NS_ASSUME_NONNULL_END
