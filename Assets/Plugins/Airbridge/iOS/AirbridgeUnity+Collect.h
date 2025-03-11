//
//  AirbridgeUnity+Collect.h
//  UnityFramework
//
//  Created by mjgu on 8/12/24.
//

#import "AirbridgeUnity.h"

NS_ASSUME_NONNULL_BEGIN

@interface AirbridgeUnity (Collect)

@end

void native_setUserID(const char* __nullable ID);
void native_clearUserID();

void native_setUserEmail(const char* __nullable email);
void native_clearUserEmail();

void native_setUserPhone(const char* __nullable phone);
void native_clearUserPhone();

void native_setUserAttributeWithInt(const char* __nullable key, int value);
void native_setUserAttributeWithLong(const char* __nullable key, long long value);
void native_setUserAttributeWithFloat(const char* __nullable key, float value);
void native_setUserAttributeWithDouble(const char* __nullable key, double value);
void native_setUserAttributeWithBOOL(const char* __nullable key, BOOL value);
void native_setUserAttributeWithString(const char* __nullable key, const char* __nullable value);
void native_removeUserAttribute(const char* __nullable key);
void native_clearUserAttributes(void);

void native_setUserAlias(const char* __nullable key, const char* __nullable value);
void native_removeUserAlias(const char* __nullable key);
void native_clearUserAlias(void);

void native_clearUser(void);

void native_setDeviceAlias(const char* __nullable key, const char* __nullable value);
void native_removeDeviceAlias(const char* __nullable key);
void native_clearDeviceAlias(void);

void native_registerPushToken(const char* __nonnull token);


NS_ASSUME_NONNULL_END
