# Airbridge Unity SDK Example

## Requirements
![Generic badge](https://img.shields.io/badge/Unity-2020.3.41f1-black.svg)

## <a id="plugin-build-for">Built-in Airbridge Unity SDK Version
![Generic badge](https://img.shields.io/badge/Airbridge_Unity_SDK-1.12.1-orange.svg)

## Before you start

This page is a guide to setting up the Airbridge Unity SDK example app.

###  [Developer Docs](https://developers.airbridge.io/v1.1-en/docs/unity-sdk)

### Android

1. Change the Platform to Android by clicking `File > Build Settings...` at the top of the Unity Editor.
2. The Auto-Resolution feature of the External Dependency Manager is activated and automatically imports the
   dependency library into the `Assets > Plugins > Android` folder.    
   (If Auto-Resolution is not working properly, please import the library manually by clicking `Assets > External Dependency Manager > Android Resolver > Force Resolve`.)
3. Click `AB180 > Airbridge Settings` at the top of the Unity Editor to [set up your project](https://developers.airbridge.io/v1.1-en/docs/unity-sdk#project-setup).
4. Press `Update Android Manifest` to apply the changes.
5. Proceed with the Android build.

### iOS

1. Change the Platform to iOS by clicking `File > Build Settings...` at the top of the Unity Editor.
2. Click `AB180 > Airbridge Settings` at the top of the Unity Editor to [set up your project](https://developers.airbridge.io/v1.1-en/docs/unity-sdk#project-setup).
3. Press `Update iOS App Setting` to apply the changes.
4. Proceed with the iOS build.

## Troubleshooting

### Android

### iOS

1. **You must rebuild it with bitcode enabled (Xcode setting ENABLE_BITCODE), obtain an updated library from the vendor, or disable bitcode for this target.**

    : Set `Unity-iPhone (PROJECT) > Build Settings (All | Combined) > Build Options > Enable Bitcode` to `NO`

2. **This app has crashed because it attempted to access privacy-sensitive data without a usage description.  The app's Info.plist must contain an NSUserTrackingUsageDescription key with a string value explaining to the user how the app uses this data.**
 
    : Edit info.plist

    ```xml
   <?xml version="1.0" encoding="UTF-8"?>
   <!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
   <plist version="1.0">
      <dict>
         <key>NSUserTrackingUsageDescription</key>
         <string>track in-app events</string>
         ...
    ```
