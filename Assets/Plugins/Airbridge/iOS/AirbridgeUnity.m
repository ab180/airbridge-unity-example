//
//  AirbridgeUnity.h
//  AirbridgeUnity
//
//  Created by WOF on 29/11/2019.
//

#import "AirbridgeUnity.h"

#import <Airbridge/Airbridge.h>

#import "AUConvert.h"
#import "Libraries/Plugins/iOS/Airbridge/AUAppSetting.h"

@implementation AirbridgeUnity

+ (instancetype)sharedInstance
{
    static AirbridgeUnity *sharedInstance = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        sharedInstance = [[self alloc] init];
    });
    return sharedInstance;
}

- (void)initializeSDK {
    AirbridgeOptionBuilder *builder = [[AirbridgeOptionBuilder alloc]
        initWithName:appName
        token:appToken
    ];
    
    NSArray *customDomains = [customDomain componentsSeparatedByString:@" "];
    
    builder = [builder setLogLevel:logLevel];
    builder = [builder setSDKDevelopmentPlatform:@"unity"];
    if (customDomains.count != 0) {
        builder = [builder setTrackingLinkCustomDomains:customDomains];
    }
    builder = [builder setSessionTimeoutWithSecond:sessionTimeoutSeconds];
    builder = [builder setAutoStartTrackingEnabled:autoStartTrackingEnabled];
    builder = [builder setHashUserInformationEnabled:userInfoHashEnabled];
    builder = [builder setTrackAirbridgeDeeplinkOnlyEnabled:trackAirbridgeLinkOnly];
    builder = [builder setAutoDetermineTrackingAuthorizationTimeoutWithSecond:trackingAuthorizeTimeoutSeconds];
    builder = [builder setTrackInSessionLifecycleEventEnabled:trackInSessionLifeCycleEventEnabled];
    builder = [builder setPauseEventTransmitOnBackgroundEnabled:pauseEventTransmitOnBackgroundEnabled];
    builder = [builder setClearEventBufferOnInitializeEnabled:clearEventBufferOnInitializeEnabled];
    builder = [builder setSDKEnabled:sdkEnabled];
    builder = [builder setEventBufferCountLimit:eventBufferCountLimitInGibibyte];
    builder = [builder setEventBufferSizeLimitWithGibibyte:eventBufferSizeLimitInGibibyte];
    builder = [builder setEventTransmitIntervalWithSecond:eventTransmitIntervalSeconds];
    builder = [builder setOnAttributionReceived:^(NSDictionary<NSString *,NSString *> * dictionary) {
    if (self.attributionOnReceived == nil) { return; }
        self.attributionOnReceived([AUConvert stringFromDictionary:dictionary]);
    }];
    builder = [builder
     setSDKSignatureWithId:sdkSignatureSecretID
     secret:sdkSignatureSecret
    ];
    
    builder = [builder setSDKAttributes: @{
        @"wrapperName": @"airbridge-unity-sdk",
        @"wrapperVersion": @"4.3.0"
    }];
    
    builder = [builder setSDKWrapperOption: @{
        @"isHandleAirbridgeDeeplinkOnly": @(isHandleAirbridgeDeeplinkOnly)
    }];
    
    [Airbridge initializeSDKWithOption:[builder build]];
}

@end

