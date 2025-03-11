package co.ab180.airbridge.unity;

import android.app.Activity;
import android.app.Application;
import android.content.ContentProvider;
import android.content.ContentValues;
import android.database.Cursor;
import android.net.Uri;
import android.util.Log;

import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

import java.util.HashMap;
import java.util.Objects;

import co.ab180.airbridge.Airbridge;
import co.ab180.airbridge.AirbridgeLifecycleIntegration;
import co.ab180.airbridge.AirbridgeLogLevel;
import co.ab180.airbridge.AirbridgeOptionBuilder;

public class AirbridgeContentProvider extends ContentProvider {

    @Override
    public boolean onCreate() {
        try {
            Application app = (Application) Objects.requireNonNull(getContext()).getApplicationContext();

            AirbridgeOptionBuilder builder = new AirbridgeOptionBuilder(AirbridgeSettings.appName, AirbridgeSettings.appToken);
            builder.setSDKDevelopmentPlatform("unity");

            if (isNotEmpty(AirbridgeSettings.sdkSignatureSecretID) && isNotEmpty(AirbridgeSettings.sdkSignatureSecret)) {
                builder.setSDKSignature(AirbridgeSettings.sdkSignatureSecretID, AirbridgeSettings.sdkSignatureSecret);
            }

            builder.setLogLevel(AirbridgeLogLevel.values()[AirbridgeSettings.logLevel]);

            String customDomain = AirbridgeSettings.customDomain;
            if (isNotEmpty(customDomain)) {
                builder.setCustomDomains(AirbridgeUtils.joinedStringToList(customDomain, " "));
            }

            builder
                    .setSessionTimeout(AirbridgeSettings.sessionTimeoutSeconds)
                    .setHashUserInformationEnabled(AirbridgeSettings.userInfoHashEnabled)
                    .setCollectLocationEnabled(AirbridgeSettings.locationCollectionEnabled)
                    .setTrackAirbridgeDeeplinkOnlyEnabled(AirbridgeSettings.trackAirbridgeLinkOnly)
                    .setAutoStartTrackingEnabled(AirbridgeSettings.autoStartTrackingEnabled)
                    .setTrackMetaDeferredAppLinkEnabled(AirbridgeSettings.facebookDeferredAppLinkEnabled)
                    .setOnAttributionReceived(AirbridgeUnity::processSetOnAttributionReceived)
                    .setTrackInSessionLifeCycleEventEnabled(AirbridgeSettings.trackInSessionLifeCycleEventEnabled)
                    .setPauseEventTransmitOnBackgroundEnabled(AirbridgeSettings.pauseEventTransmitOnBackgroundEnabled)
                    .setClearEventBufferOnInitializeEnabled(AirbridgeSettings.clearEventBufferOnInitializeEnabled)
                    .setSDKEnabled(AirbridgeSettings.sdkEnabled);
                   
            String appMarketIdentifier = AirbridgeSettings.appMarketIdentifier;
            if (isNotEmpty(appMarketIdentifier)) { 
                builder.setAppMarketIdentifier(appMarketIdentifier);
            }
                   
            builder
                    .setEventBufferCountLimit(AirbridgeSettings.eventBufferCountLimitInGibibyte)
                    .setEventBufferSizeLimit(AirbridgeSettings.eventBufferSizeLimitInGibibyte)
                    .setEventTransmitInterval(AirbridgeSettings.eventTransmitIntervalSeconds);

            String facebookAppId = AirbridgeSettings.facebookAppId;
            if (isNotEmpty(facebookAppId)) {
                builder.setMetaInstallReferrer(facebookAppId);
            }

            builder.setLifecycleIntegration(new AirbridgeLifecycleIntegration() {

                @Nullable
                @Override
                public String getDataString(@NotNull Activity activity) {
                    if (AirbridgeUnity.airbridgeLifecycleIntegration != null) {
                        return AirbridgeUnity.airbridgeLifecycleIntegration.getDataString(activity);
                    }
                    return null;
                }
            });
            
            HashMap<String, String> sdkAttributes = new HashMap<>();
            sdkAttributes.put("wrapperName", "airbridge-unity-sdk");
            sdkAttributes.put("wrapperVersion", "4.3.0");
            builder.setSDKAttributes(sdkAttributes);
            
            HashMap<String, Object> sdkWrapperOption = new HashMap<>();
            sdkWrapperOption.put("isHandleAirbridgeDeeplinkOnly", AirbridgeSettings.isHandleAirbridgeDeeplinkOnly);
            builder.setSDKWrapperOption(sdkWrapperOption);
            
            Airbridge.initializeSDK(app, builder.build());
        } catch (Throwable throwable) {
            Log.e(AirbridgeUnity.TAG, "Couldn't initialize SDK.", throwable);
        }
        return true;
    }

    @Override
    public Cursor query(@NotNull Uri uri, String[] projection, String selection, String[] selectionArgs, String sortOrder) {
        return null;
    }

    @Override
    public String getType(@NotNull Uri uri) {
        return null;
    }

    @Override
    public Uri insert(@NotNull Uri uri, ContentValues values) {
        return null;
    }

    @Override
    public int delete(@NotNull Uri uri, String selection, String[] selectionArgs) {
        return 0;
    }

    @Override
    public int update(@NotNull Uri uri, ContentValues values, String selection, String[] selectionArgs) {
        return 0;
    }

    private boolean isNotEmpty(String string) {
        if (string == null) { return false; }
        return !(string.isEmpty());
    }
}