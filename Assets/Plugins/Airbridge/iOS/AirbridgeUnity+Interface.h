//
//  AirbridgeUnity+Interface.h
//  UnityFramework
//
//  Created by mjgu on 8/12/24.
//

#import "AirbridgeUnity.h"

NS_ASSUME_NONNULL_BEGIN

@interface AirbridgeUnity (Interface)

@end

void native_HandleWebInterfaceCommand(const char* command);
const char * native_CreateWebInterfaceScript(const char* webToken, const char* postMessageScript);

NS_ASSUME_NONNULL_END
