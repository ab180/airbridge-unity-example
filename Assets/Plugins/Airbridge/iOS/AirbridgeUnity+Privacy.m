//
//  AirbridgeUnity+Privacy.m
//  UnityFramework
//
//  Created by mjgu on 8/12/24.
//

#import "AirbridgeUnity+Privacy.h"

#import <Airbridge/Airbridge.h>

@implementation AirbridgeUnity (Privacy)

@end

void native_startTracking(void) {
    [Airbridge startTracking];
}

void native_stopTracking(void) {
    [Airbridge stopTracking];
}

bool native_isTrackingEnabled(void) {
    return [Airbridge isTrackingEnabled];
}
