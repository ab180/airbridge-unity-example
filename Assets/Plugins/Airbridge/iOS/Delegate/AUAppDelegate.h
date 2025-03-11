//
//  AUAppDelegate.h
//  AirbridgeUnity
//
//  Created by WOF on 2019/12/10.
//  Copyright © 2019 ab180. All rights reserved.
//

#import <Foundation/Foundation.h>

#import "AppDelegateListener.h"

NS_ASSUME_NONNULL_BEGIN

@interface AUAppDelegate : NSObject <AppDelegateListener>

+ (AUAppDelegate *) instance;

- (BOOL)application:(UIApplication *)app openURL:(NSURL *)url options:(NSDictionary<UIApplicationOpenURLOptionsKey,id> *)options;
- (BOOL)application:(UIApplication*)application continueUserActivity:(NSUserActivity*)userActivity restorationHandler:(void (^)(NSArray<id<UIUserActivityRestoring>>* _Nullable))restorationHandler;

@end

NS_ASSUME_NONNULL_END
