//
//  AirbridgeUnity.h
//  AirbridgeUnity
//
//  Created by WOF on 29/11/2019.
//

NS_ASSUME_NONNULL_BEGIN

typedef void (^DeeplinkOnReceived)(NSString *urlString);
typedef void (*UnityDeeplinkReceived)(const char* url);

typedef void (^AttributionOnReceived)(NSString *attributionString);
typedef void (*UnityAttributionOnReceived)(const char* attributionJson);

typedef void (^OnSuccessHandler)(void);
typedef void (*UnityOnSuccessHandler)(void);

typedef void (^OnSuccessTwoStringHandler)(NSString *string, NSString *string2);
typedef void (*UnityOnSuccessTwoStringHandler)(const char *string, const char *string2);

typedef void (^OnSuccessStringHandler)(NSString *string);
typedef void (*UnityOnSuccessStringHandler)(const char *string);

typedef void (^OnFailureHandler)(NSString *error);
typedef void (*UnityOnFailureHandler)(const char* string);

@interface AirbridgeUnity : NSObject

@property (nonatomic, strong, nullable) NSString *initializeBeforeDeeplinkString;
@property (nonatomic, strong) DeeplinkOnReceived deeplinkOnReceived;
@property (nonatomic, strong) AttributionOnReceived attributionOnReceived;

+ (instancetype)sharedInstance;

- (void)initializeSDK;

@end

NS_ASSUME_NONNULL_END
