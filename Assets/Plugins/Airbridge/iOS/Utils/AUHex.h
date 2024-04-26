//
//  AUHex.h
//  AirbridgeUnity
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface AUHex : NSObject

+ (nullable NSData *)dataFromHexString:(NSString *)string;

@end

NS_ASSUME_NONNULL_END