//
//  AUHex.m
//  AirbridgeUnity
//

#import "AUHex.h"

@implementation AUHex

+ (nullable NSData *)dataFromHexString:(NSString *)string {
    // Convert the string to lowercase only once
    string = [string lowercaseString];
    NSUInteger length = [string length];
    
    // Reserve capacity for the mutable data to avoid frequent reallocation
    NSMutableData* data = [NSMutableData dataWithCapacity:length / 2];
    
    // Define constants for hex characters
    static const char hexCharacters[] = "0123456789abcdef";
    
    for (NSUInteger i = 0; i < length; i += 2) {
        // Get two characters at once for better performance
        char highCharacter = [string characterAtIndex:i];
        char lowCharacter = (i + 1 < length) ? [string characterAtIndex:i + 1] : 0;
        
        // Convert hex characters to bytes
        const char *highCharacterPointer = strchr(hexCharacters, highCharacter);
        const char *lowCharacterPointer = strchr(hexCharacters, lowCharacter);
        if (!highCharacterPointer || !lowCharacterPointer) {
            // catch invalid characters
            return nil;
        }
        
        // Calculate the byte from the hex characters
        uint8_t byte = ((highCharacterPointer - hexCharacters) << 4) | (lowCharacterPointer - hexCharacters);
        
        // Append the byte to the data
        [data appendBytes:&byte length:1];
    }
    
    return data;
}

@end
