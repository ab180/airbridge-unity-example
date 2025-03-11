//
//  AirbridgeUnity+Placement.h
//  UnityFramework
//
//  Created by mjgu on 8/12/24.
//

#import "AirbridgeUnity.h"

NS_ASSUME_NONNULL_BEGIN

@interface AirbridgeUnity (Placement)

@end

void native_click(const char* trackingLink, UnityOnSuccessHandler onSuccess, UnityOnFailureHandler onFailure);
void native_impression(const char* trackingLink, UnityOnSuccessHandler onSuccess, UnityOnFailureHandler onFailure);
void native_createTrackingLink(const char *channel, const char *option, UnityOnSuccessTwoStringHandler onSuccess, UnityOnFailureHandler onFailure);

NS_ASSUME_NONNULL_END
