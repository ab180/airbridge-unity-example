//
//  AirbridgeUnity+Information.m
//  UnityFramework
//
//  Created by mjgu on 8/12/24.
//

#import "AirbridgeUnity+Information.h"

#import <Airbridge/Airbridge.h>

#import "AUConvert.h"

@implementation AirbridgeUnity (Information)

- (void)fetchDeviceUUID:(OnSuccessStringHandler)onSuccess OnFailureHandler:(OnFailureHandler)onFailure {
    [Airbridge
     fetchDeviceUUIDOnSuccess:onSuccess
     onFailure:^(NSError * _Nonnull error) {
        onFailure(error.localizedDescription);
    }];
}

- (void)fetchAirbridgeGeneratedUUID:(OnSuccessStringHandler)onSuccess OnFailureHandler:(OnFailureHandler)onFailure {
    [Airbridge
     fetchAirbridgeGeneratedUUIDOnSuccess:onSuccess
     onFailure:^(NSError * _Nonnull error) {
        onFailure(error.localizedDescription);
    }];
}

- (void)setOnAttributionReceived:(AttributionOnReceived)onReceived {
    AirbridgeUnity.sharedInstance.attributionOnReceived = onReceived;
}

@end

void native_fetchDeviceUUID(UnityOnSuccessStringHandler onSuccess, UnityOnFailureHandler onFailure) {
    [AirbridgeUnity.sharedInstance 
     fetchDeviceUUID:^(NSString * _Nonnull string) {
        onSuccess([AUConvert charsFromString:string]);
    }
     OnFailureHandler:^(NSString * _Nonnull error) {
        onFailure([AUConvert charsFromString:error]);
    }];
}

void native_fetchAirbridgeGeneratedUUID(UnityOnSuccessStringHandler onSuccess, UnityOnFailureHandler onFailure) {
    [AirbridgeUnity.sharedInstance
     fetchAirbridgeGeneratedUUID:^(NSString * _Nonnull string) {
        onSuccess([AUConvert charsFromString:string]);
    }
     OnFailureHandler:^(NSString * _Nonnull error) {
        onFailure([AUConvert charsFromString:error]);
    }];
}

void native_setOnAttributionReceived(UnityAttributionOnReceived onReceived) {
    [AirbridgeUnity.sharedInstance setOnAttributionReceived:^(NSString * _Nonnull attributionString) {
        onReceived([AUConvert charsFromString:attributionString]);
    }];
}
