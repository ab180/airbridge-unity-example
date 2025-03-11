//
//  AirbridgeUnity+Deeplink.h
//  UnityFramework
//
//  Created by mjgu on 8/12/24.
//

#import "AirbridgeUnity.h"

NS_ASSUME_NONNULL_BEGIN

@interface AirbridgeUnity (Deeplink)

- (void)setOnDeeplinkReceived:(DeeplinkOnReceived)onReceived;

@end

void native_setOnDeeplinkReceived(UnityDeeplinkReceived onReceived);

NS_ASSUME_NONNULL_END
