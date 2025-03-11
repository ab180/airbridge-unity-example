//
//  AirbridgeUnity+Event.m
//  UnityFramework
//
//  Created by mjgu on 8/12/24.
//

#import "AirbridgeUnity+Event.h"

#import <Airbridge/Airbridge.h>

#import "AUConvert.h"

@implementation AirbridgeUnity (Event)

@end

void native_trackEvent(const char* category, const char* semanticJson, const char* customJson) {
    NSString *categoryString = [AUConvert stringFromChars:category];
    NSDictionary *semanticAttributes = [AUConvert dictionaryFromJSONChars:semanticJson];
    NSDictionary *customAttributes = [AUConvert dictionaryFromJSONChars:customJson];
    
    [Airbridge
     trackEventWithCategory:categoryString
     semanticAttributes:semanticAttributes
     customAttributes:customAttributes
    ];
}

