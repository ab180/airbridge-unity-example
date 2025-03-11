//
//  AirbridgeUnity+Core.h
//  UnityFramework
//
//  Created by mjgu on 8/12/24.
//

#import "AirbridgeUnity.h"

NS_ASSUME_NONNULL_BEGIN

@interface AirbridgeUnity (Core)

@end

void native_enableSDK(void);
void native_disableSDK(void);
bool native_isSDKEnabled(void);

NS_ASSUME_NONNULL_END
