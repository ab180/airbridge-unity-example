# Airbridge Unity SDK Example

## Requirements
![Generic badge](https://img.shields.io/badge/Unity-2020.3.41f1-black.svg)

## <a id="plugin-build-for">Built-in Airbridge Unity SDK Version
![Generic badge](https://img.shields.io/badge/Airbridge_Unity_SDK-1.9.3-orange.svg)

## 사용하기 전에

해당 페이지는, Airbridge Unity SDK 샘플 앱 설정에 대한 가이드입니다.

###  [Developer Docs](https://developers.airbridge.io/docs/unity-sdk)

### Android

1. 유니티 상단의 `File > Build Settings...` 을 클릭하여 Platform 을 Android 로 변경해주세요.
2. External Dependency Manager 의 Auto-Resolution 기능이 동작하면서 자동으로 `Assets > Plugins > Android` 폴더에
   Dependency 라이브러리가 임포트됩니다.     
   (Auto-Resolution 가 정상 동작하지 않은 경우, `Assets > External Dependency Manager > Android Resolver > Force Resolve` 을 클릭하여 수동으로 라이브러리를 임포트해주세요.)
3. 유니티 상단의 `AB180 > Airbridge Settings` 을 클릭하여 [프로젝트 설정](https://developers.airbridge.io/docs/unity-sdk#%ED%94%84%EB%A1%9C%EC%A0%9D%ED%8A%B8-%EC%84%A4%EC%A0%95) 작업을 진행하여 주세요.
4. 필드 입력 완료 후 적용을 위해 `Update Android Manifest` 버튼을 눌러주세요.
5. Android 빌드를 진행해 주세요.

### iOS

1. 유니티 상단의 `File > Build Settings...` 을 클릭하여 Platform 을 iOS 로 변경해주세요.
2. 유니티 상단의 `AB180 > Airbridge Settings` 을 클릭하여 [프로젝트 설정](https://developers.airbridge.io/docs/unity-sdk#%ED%94%84%EB%A1%9C%EC%A0%9D%ED%8A%B8-%EC%84%A4%EC%A0%95) 작업을 진행하여 주세요.
3. 필드 입력 완료 후 적용을 위해 `Update iOS App Setting` 버튼을 눌러주세요.
4. iOS 빌드를 진행해 주세요.

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