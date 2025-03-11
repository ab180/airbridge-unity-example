//
//  AUAppDelegate.mm
//  AirbridgeUnity
//
//  Created by WOF on 2019/12/10.
//  Copyright © 2019 ab180. All rights reserved.
//

#import "AUAppDelegate.h"

#import <Airbridge/Airbridge.h>

#import "AirbridgeUnity.h"
#import "Libraries/Plugins/iOS/Airbridge/AUAppSetting.h"

@implementation AUAppDelegate

static AUAppDelegate* shared = AUAppDelegate.instance;
static BOOL isInitialized = NO;

+ (void)initialize {
    if (shared == nil) {
        shared = AUAppDelegate.instance;
    }
}

+ (AUAppDelegate *)instance {
    if (shared == nil) {
        shared = [[AUAppDelegate alloc] init];
    }

    return shared;
}

- (instancetype) init {
    self = [super init];
    if (!self) {
        return nil;
    }

    UnityRegisterAppDelegateListener(self);

    return self;
}

- (void)didFinishLaunching:(NSNotification *)notification {
    if (appToken == nil || [appToken isEqualToString:@""]) { return; }
    if (appName == nil || [appName isEqualToString:@""]) { return; }
    
    [AirbridgeUnity.sharedInstance initializeSDK];
    isInitialized = YES;
}

- (BOOL)application:(UIApplication *)app openURL:(NSURL *)url options:(NSDictionary<UIApplicationOpenURLOptionsKey,id> *)options {
    if (!isInitialized) { return NO; }
    [Airbridge trackDeeplinkWithUrl:url];
    BOOL isHandled = [Airbridge handleDeeplinkWithUrl:url onSuccess:^(NSURL * _Nonnull url) {
         if (AirbridgeUnity.sharedInstance.deeplinkOnReceived == nil) {
              AirbridgeUnity.sharedInstance.initializeBeforeDeeplinkString = url.absoluteString;
              return;
          }
        AirbridgeUnity.sharedInstance.deeplinkOnReceived(url.absoluteString);
    }];
    
    if (isHandled || isHandleAirbridgeDeeplinkOnly) { return YES; }
    
    if (AirbridgeUnity.sharedInstance.deeplinkOnReceived == nil) {
        AirbridgeUnity.sharedInstance.initializeBeforeDeeplinkString = url.absoluteString;
    } else {
        AirbridgeUnity.sharedInstance.deeplinkOnReceived(url.absoluteString);
    }
    
    return YES;
}

- (BOOL)application:(UIApplication*)application continueUserActivity:(NSUserActivity*)userActivity restorationHandler:(void (^)(NSArray<id<UIUserActivityRestoring>>* _Nullable))restorationHandler {
    if (!isInitialized) { return NO; }
    [Airbridge trackDeeplinkWithUserActivity:userActivity];
    BOOL isHandled = [Airbridge handleDeeplinkWithUserActivity:userActivity onSuccess:^(NSURL * _Nonnull url) {
        if (AirbridgeUnity.sharedInstance.deeplinkOnReceived == nil) {
            AirbridgeUnity.sharedInstance.initializeBeforeDeeplinkString = url.absoluteString;
            return;
        }
        AirbridgeUnity.sharedInstance.deeplinkOnReceived(url.absoluteString);
    }];
    
    if (isHandled || isHandleAirbridgeDeeplinkOnly) { return YES; }
    
    if (AirbridgeUnity.sharedInstance.deeplinkOnReceived == nil) {
        AirbridgeUnity.sharedInstance.initializeBeforeDeeplinkString = userActivity.webpageURL.absoluteString;
    } else {
        AirbridgeUnity.sharedInstance.deeplinkOnReceived(userActivity.webpageURL.absoluteString);
    }
    
    return YES;
}

@end
