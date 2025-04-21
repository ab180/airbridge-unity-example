//
//  AirbridgeUnity+Deeplink.m
//  UnityFramework
//
//  Created by mjgu on 8/12/24.
//

#import "AirbridgeUnity+Deeplink.h"
#import "Libraries/Plugins/iOS/Airbridge/AUAppSetting.h"

@implementation AirbridgeUnity (Deeplink)

- (void)setOnDeeplinkReceived:(DeeplinkOnReceived)onReceived {
    self.deeplinkOnReceived = onReceived;
    if (AirbridgeUnity.sharedInstance.initializeBeforeDeeplinkString != nil) {
        self.deeplinkOnReceived(AirbridgeUnity.sharedInstance.initializeBeforeDeeplinkString);
        AirbridgeUnity.sharedInstance.initializeBeforeDeeplinkString = nil;
    }
}

@end

void native_setOnDeeplinkReceived(UnityDeeplinkReceived onReceived) {
    [AirbridgeUnity.sharedInstance setOnDeeplinkReceived:^(NSString * urlString) {
        onReceived([urlString UTF8String]);
    }];
}

