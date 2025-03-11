//
//  AirbridgeUnity+Collect.m
//  UnityFramework
//
//  Created by mjgu on 8/12/24.
//

#import "AirbridgeUnity+Collect.h"

#import <Airbridge/Airbridge.h>

#import "AUHex.h"
#import "AUConvert.h"

@implementation AirbridgeUnity (Collect)

@end

// MARK: - User ID
void native_setUserID(const char* __nullable ID) {
    [Airbridge setUserID:[AUConvert stringFromChars:ID]];
}

void native_clearUserID() {
    [Airbridge clearUserID];
}


// MARK: - User Email
void native_setUserEmail(const char* __nullable email) {
    [Airbridge setUserEmail:[AUConvert stringFromChars:email]];
}
void native_clearUserEmail() {
    [Airbridge clearUserEmail];
}


// MARK: - User Phone
void native_setUserPhone(const char* __nullable phone) {
    [Airbridge setUserPhone:[AUConvert stringFromChars:phone]];
}

void native_clearUserPhone() {
    [Airbridge clearUserPhone];
}

// MARK: - User Attribute
void native_setUserAttributeWithInt(const char* __nullable key, int value) {
    [Airbridge 
     setUserAttributeWithKey:[AUConvert stringFromChars:key]
     value:[NSNumber numberWithInt:value]
    ];
}

void native_setUserAttributeWithLong(const char* __nullable key, long long value) {
    [Airbridge
     setUserAttributeWithKey:[AUConvert stringFromChars:key]
     value:[NSNumber numberWithLongLong:value]
    ];
}

void native_setUserAttributeWithFloat(const char* __nullable key, float value) {
    [Airbridge
     setUserAttributeWithKey:[AUConvert stringFromChars:key]
     value:[NSNumber numberWithFloat:value]
    ];
}

void native_setUserAttributeWithDouble(const char* __nullable key, double value) {
    [Airbridge
     setUserAttributeWithKey:[AUConvert stringFromChars:key]
     value:[NSNumber numberWithDouble:value]
    ];
}

void native_setUserAttributeWithBOOL(const char* __nullable key, BOOL value) {
    [Airbridge
     setUserAttributeWithKey:[AUConvert stringFromChars:key]
     value:[NSNumber numberWithBool:value]
    ];
}

void native_setUserAttributeWithString(const char* __nullable key, const char* __nullable value) {
    [Airbridge
     setUserAttributeWithKey:[AUConvert stringFromChars:key]
     value:[AUConvert stringFromChars:value]
    ];
}

void native_removeUserAttribute(const char* __nullable key) {
    [Airbridge removeUserAttributeWithKey:[AUConvert stringFromChars:key]];
}
void native_clearUserAttributes(void) {
    [Airbridge clearUserAttributes];
}

// MARK: - User Alias
void native_setUserAlias(const char* __nullable key, const char* __nullable value) {
    [Airbridge
     setUserAliasWithKey:[AUConvert stringFromChars:key]
     value:[AUConvert stringFromChars:value]
    ];
}

void native_removeUserAlias(const char* __nullable key) {
    [Airbridge removeUserAliasWithKey:[AUConvert stringFromChars:key]];
}
void native_clearUserAlias(void) {
    [Airbridge clearUserAlias];
}

// MARK: - Clear User
void native_clearUser(void) {
    [Airbridge clearUser];
}


// MARK: - Device
void native_setDeviceAlias(const char* __nullable key, const char* __nullable value) {
    [Airbridge
     setDeviceAliasWithKey:[AUConvert stringFromChars:key]
     value:[AUConvert stringFromChars:value]
    ];
}

void native_removeDeviceAlias(const char* __nullable key) {
    [Airbridge removeDeviceAliasWithKey:[AUConvert stringFromChars:key]];
}

void native_clearDeviceAlias(void) {
    [Airbridge clearDeviceAlias];
}

// MARK: - Token
void native_registerPushToken(const char* __nonnull token) {
    NSString *tokenString = [AUConvert stringFromChars:token];
    NSData *tokenData = [AUHex dataFromHexString:tokenString];
    
    if (tokenData == nil || tokenData.length == 0) { return; }
    [Airbridge registerPushToken:tokenData];
}
