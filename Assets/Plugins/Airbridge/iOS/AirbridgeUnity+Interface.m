//
//  AirbridgeUnity+Interface.m
//  UnityFramework
//
//  Created by mjgu on 8/12/24.
//

#import "AirbridgeUnity+Interface.h"

#import <Airbridge/Airbridge.h>

#import "AUConvert.h"

@implementation AirbridgeUnity (Interface)

@end

void native_HandleWebInterfaceCommand(const char* command) {
    [Airbridge handleWebInterfaceCommand:[AUConvert stringFromChars:command]];
}

const char * native_CreateWebInterfaceScript(const char* webToken, const char* postMessageScript) {
    NSString *script = [Airbridge
     createWebInterfaceScriptWithWebToken:[AUConvert stringFromChars:webToken]
     postMessageScript:[AUConvert stringFromChars:postMessageScript]
    ];
    
    return [AUConvert charsFromString:script];
}
