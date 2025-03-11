//
//  AirbridgeUnity+Placement.m
//  UnityFramework
//
//  Created by mjgu on 8/12/24.
//

#import "AirbridgeUnity+Placement.h"

#import <Airbridge/Airbridge.h>

#import "AUConvert.h"

@implementation AirbridgeUnity (Placement)

- (void)click:(NSString *)trackingLink OnSuccessHandler:(OnSuccessHandler)onSuccess OnFailureHandler:(OnFailureHandler)onFailure {
    [Airbridge
     clickWithTrackingLink:[NSURL URLWithString:trackingLink]
     onSuccess:^{
        onSuccess();
    } onFailure:^(NSError * _Nonnull error) {
        onFailure(error.localizedDescription);
    }];
}

- (void)impression:(NSString *)trackingLink OnSuccessHandler:(OnSuccessHandler)onSuccess OnFailureHandler:(OnFailureHandler)onFailure {
    [Airbridge
     impressionWithTrackingLink:[NSURL URLWithString:trackingLink]
     onSuccess:^{
        onSuccess();
    } onFailure:^(NSError * _Nonnull error) {
        onFailure(error.localizedDescription);
    }];
}

- (void)createTrackingLink:(NSString *)channel Option:(NSDictionary *)option OnSuccessHandler:(OnSuccessTwoStringHandler)onSuccess OnFailureHandler: (OnFailureHandler)onFailure {
    [Airbridge
     createTrackingLinkWithChannel:channel
     option:option
     onSuccess:^(AirbridgeTrackingLink * _Nonnull trackingLink) {
        onSuccess(trackingLink.shortURL.absoluteString, trackingLink.qrcodeURL.absoluteString);
     }
     onFailure:^(NSError * _Nonnull error) {
        onFailure(error.localizedDescription);
    }];
}

@end

void native_click(const char* trackingLink, UnityOnSuccessHandler onSuccess, UnityOnFailureHandler onFailure) {
    [AirbridgeUnity.sharedInstance
     click:[AUConvert stringFromChars:trackingLink]
     OnSuccessHandler:^{
        onSuccess();
    }
     OnFailureHandler:^(NSString * _Nonnull error) {
        onFailure([AUConvert charsFromString:error]);
    }];
}

void native_impression(const char* trackingLink, UnityOnSuccessHandler onSuccess, UnityOnFailureHandler onFailure) {
    [AirbridgeUnity.sharedInstance
     impression:[AUConvert stringFromChars:trackingLink]
     OnSuccessHandler:^{
        onSuccess();
    }
     OnFailureHandler:^(NSString * _Nonnull error) {
        onFailure([AUConvert charsFromString:error]);
    }];
}

void native_createTrackingLink(const char *channel, const char *option, UnityOnSuccessTwoStringHandler onSuccess, UnityOnFailureHandler onFailure) {
    [AirbridgeUnity.sharedInstance
     createTrackingLink:[AUConvert stringFromChars:channel]
     Option:[AUConvert dictionaryFromJSONChars:option]
     OnSuccessHandler:^(NSString * _Nonnull string, NSString * _Nonnull string2) {
        onSuccess([AUConvert charsFromString:string], [AUConvert charsFromString:string2]);
    } OnFailureHandler:^(NSString * _Nonnull error) {
        onFailure([AUConvert charsFromString:error]);
    }];
}
