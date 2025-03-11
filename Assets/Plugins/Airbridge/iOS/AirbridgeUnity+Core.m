//
//  AirbridgeUnity+Core.m
//  UnityFramework
//
//  Created by mjgu on 8/12/24.
//

#import "AirbridgeUnity+Core.h"

#import <Airbridge/Airbridge.h>

@implementation AirbridgeUnity (Core)

@end

void native_enableSDK(void) {
    [Airbridge enableSDK];
}

void native_disableSDK(void) {
    [Airbridge disableSDK];
}

bool native_isSDKEnabled(void) {
    return Airbridge.isSDKEnabled;
}
