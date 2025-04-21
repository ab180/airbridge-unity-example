package co.ab180.airbridge.unity;

import android.app.Application;
import android.content.ContentProvider;
import android.content.ContentValues;
import android.database.Cursor;
import android.net.Uri;
import android.util.Log;

import org.jetbrains.annotations.NotNull;

import java.lang.reflect.Method;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Objects;

import co.ab180.airbridge.Airbridge;
import co.ab180.airbridge.AirbridgeConfig;
import co.ab180.airbridge.AirbridgeInAppPurchase;
import co.ab180.airbridge.AirbridgeInAppPurchaseEnvironment;
import co.ab180.airbridge.OnAttributionResultReceiveListener;
import co.ab180.airbridge.OnInAppPurchaseReceiveListener;

public class AirbridgeContentProvider extends ContentProvider {

    @Override
    public boolean onCreate() {
        try {
            Application app = (Application) Objects.requireNonNull(getContext()).getApplicationContext();

            AirbridgeConfig.Builder builder = new AirbridgeConfig.Builder(AirbridgeSettings.appName, AirbridgeSettings.appToken);
            builder.setPlatform("unity");

            if (isNotEmpty(AirbridgeSettings.sdkSignatureSecretID) && isNotEmpty(AirbridgeSettings.sdkSignatureSecret)) {
                builder.setSDKSignatureSecret(AirbridgeSettings.sdkSignatureSecretID, AirbridgeSettings.sdkSignatureSecret);
            }

            builder.setLogLevel(AirbridgeSettings.logLevel);

            String customDomain = AirbridgeSettings.customDomain;
            if (isNotEmpty(customDomain)) {
                List<String> customDomains = new ArrayList<>();
                customDomains.add(customDomain);
                builder.setCustomDomains(customDomains);
            }

            builder.setSessionTimeoutSeconds(AirbridgeSettings.sessionTimeoutSeconds);
            builder.setUserInfoHashEnabled(AirbridgeSettings.userInfoHashEnabled);
            builder.setLocationCollectionEnabled(AirbridgeSettings.locationCollectionEnabled);
            builder.setTrackAirbridgeLinkOnly(AirbridgeSettings.trackAirbridgeLinkOnly);
            builder.setAutoStartTrackingEnabled(AirbridgeSettings.autoStartTrackingEnabled);
            builder.setFacebookDeferredAppLinkEnabled(AirbridgeSettings.facebookDeferredAppLinkEnabled);

            builder.setOnAttributionResultReceiveListener(new OnAttributionResultReceiveListener() {
                @Override
                public void onAttributionResultReceived(Map<String, String> result) {
                    AirbridgeUnity.processAttributionData(result);
                }
            });

            String facebookAppId = AirbridgeSettings.facebookAppId;
            if (isNotEmpty(facebookAppId)) {
                builder.setMetaInstallReferrer(facebookAppId);
            }

            builder.setInAppPurchaseEnvironment(
                    AirbridgeSettings.inAppPurchaseEnvironment.equals(AirbridgeInAppPurchaseEnvironment.SANDBOX.getValue$airbridge_release()) ?
                            AirbridgeInAppPurchaseEnvironment.SANDBOX :
                            AirbridgeInAppPurchaseEnvironment.PRODUCTION
            );
            builder.setOnInAppPurchaseReceived(new OnInAppPurchaseReceiveListener() {
                @Override
                public void onInAppPurchaseReceived(@NotNull AirbridgeInAppPurchase airbridgeInAppPurchase) {
                    if (AirbridgeUnity.inAppPurchaseCallback == null) return;

                    // Use reflection so that it can be used even if the billing client library is not implemented.
                    String originalJson = "{}";
                    try {
                        Class<?> airbridgeInAppPurchaseClass = Class.forName("co.ab180.airbridge.AirbridgeInAppPurchase");
                        Method getPurchaseMethod = airbridgeInAppPurchaseClass.getMethod("getPurchase");
                        Object purchase = getPurchaseMethod.invoke(airbridgeInAppPurchase);

                        Class<?> purchaseClass = Class.forName("com.android.billingclient.api.Purchase");
                        Method getOriginalJsonMethod = purchaseClass.getMethod("getOriginalJson");
                        originalJson = (String) getOriginalJsonMethod.invoke(purchase);
                    } catch (Throwable throwable) {
                        Log.e("AirbridgeUnity", "Error occurs while getting purchase using reflection", throwable);
                    }

                    try {
                        String result = AirbridgeUnity.inAppPurchaseCallback.Invoke(originalJson);
                        HashMap<String, Object> resultMap = new HashMap<>(Objects.requireNonNull(AirbridgeJsonParser.from(result)));

                        // Set Action
                        if (resultMap.containsKey(AirbridgeConstants.Param.ACTION) &&
                                resultMap.get(AirbridgeConstants.Param.ACTION) instanceof String) {
                            airbridgeInAppPurchase.setAction((String) resultMap.get(AirbridgeConstants.Param.ACTION));
                        }
                        // Set Label
                        if (resultMap.containsKey(AirbridgeConstants.Param.LABEL) &&
                                resultMap.get(AirbridgeConstants.Param.LABEL) instanceof String) {
                            airbridgeInAppPurchase.setLabel((String) resultMap.get(AirbridgeConstants.Param.LABEL));
                        }
                        // Set Custom Attributes
                        if (resultMap.containsKey(AirbridgeConstants.Param.CUSTOM_ATTRIBUTES) &&
                                (resultMap.get(AirbridgeConstants.Param.CUSTOM_ATTRIBUTES) instanceof Map)) {
                            airbridgeInAppPurchase.setCustomAttributes(
                                    ((Map<String, Object>) resultMap.get(AirbridgeConstants.Param.CUSTOM_ATTRIBUTES))
                            );
                        }
                        // Set Semantic Attributes
                        if (resultMap.containsKey(AirbridgeConstants.Param.SEMANTIC_ATTRIBUTES) &&
                                (resultMap.get(AirbridgeConstants.Param.SEMANTIC_ATTRIBUTES) instanceof Map)) {
                            airbridgeInAppPurchase.setSemanticAttributes(
                                    ((Map<String, Object>) resultMap.get(AirbridgeConstants.Param.SEMANTIC_ATTRIBUTES))
                            );
                        }
                    } catch (Throwable throwable) {
                        Log.e("AirbridgeUnity", "Error occurs while calling {onInAppPurchaseReceived}", throwable);
                    }
                }
            });

            Airbridge.init(app, builder.build());
        } catch (Throwable throwable) {
            // Enable error logs to be collected to the server
            Log.e("Airbridge", "[Airbridge Unity] Couldn't initialize SDK.", throwable);
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