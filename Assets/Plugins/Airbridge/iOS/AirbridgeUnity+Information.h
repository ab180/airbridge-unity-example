//
//  AirbridgeUnity+Information.h
//  UnityFramework
//
//  Created by mjgu on 8/12/24.
//

#import "AirbridgeUnity.h"

NS_ASSUME_NONNULL_BEGIN

@interface AirbridgeUnity (Information)

@end

void native_fetchDeviceUUID(UnityOnSuccessStringHandler onSuccess, UnityOnFailureHandler onFailure);
void native_fetchAirbridgeGeneratedUUID(UnityOnSuccessStringHandler onSuccess, UnityOnFailureHandler onFailure);
void native_setOnAttributionReceived(UnityAttributionOnReceived onReceived);

NS_ASSUME_NONNULL_END
