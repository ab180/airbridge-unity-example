package co.ab180.airbridge.unity;

import static co.ab180.airbridge.unity.AirbridgeUtils.getMessage;
import static co.ab180.airbridge.unity.AirbridgeUtils.isNotNull;

import android.content.Intent;
import android.net.Uri;
import android.util.Log;

import org.jetbrains.annotations.NotNull;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

import co.ab180.airbridge.Airbridge;
import co.ab180.airbridge.AirbridgeLifecycleIntegration;

public class AirbridgeUnity {

    public static final String TAG = AirbridgeUnity.class.getSimpleName();

    private static String deeplink = null;
    private static AirbridgeCallback deeplinkCallback = null;

    private static String receivedAttributionResult = null;
    private static AirbridgeCallback attributionResultCallback = null;

    static AirbridgeLifecycleIntegration airbridgeLifecycleIntegration = null;

    public static void enableSDK() {
        try {
            Airbridge.enableSDK();
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {enableSDK}", throwable);
        }
    }

    public static void disableSDK() {
        try {
            Airbridge.disableSDK();
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {disableSDK}", throwable);
        }
    }

    public static boolean isSDKEnabled() {
        try {
            return Airbridge.isSDKEnabled();
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {isSDKEnabled}", throwable);
        }
        return false;
    }

    public static void startTracking() {
        try {
            Airbridge.startTracking();
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {startTracking}", throwable);
        }
    }

    public static void stopTracking() {
        try {
            Airbridge.stopTracking();
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {stopTracking}", throwable);
        }
    }

    public static boolean isTrackingEnabled() {
        try {
            return Airbridge.isTrackingEnabled();
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {isTrackingEnabled}", throwable);
        }
        return false;
    }

    public static void setUserID(@NotNull String id) {
        try {
            Airbridge.setUserID(id);
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {setUserID}", throwable);
        }
    }

    public static void clearUserID() {
        try {
            Airbridge.clearUserID();
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {clearUserID}", throwable);
        }
    }

    public static void setUserEmail(@NotNull String email) {
        try {
            Airbridge.setUserEmail(email);
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {setUserEmail}", throwable);
        }
    }

    public static void clearUserEmail() {
        try {
            Airbridge.clearUserEmail();
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {clearUserEmail}", throwable);
        }
    }

    public static void setUserPhone(@NotNull String phone) {
        try {
            Airbridge.setUserPhone(phone);
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {setUserPhone}", throwable);
        }
    }

    public static void clearUserPhone() {
        try {
            Airbridge.clearUserPhone();
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {clearUserPhone}", throwable);
        }
    }

    public static void setUserAttribute(@NotNull String key, int value) {
        try {
            Airbridge.setUserAttribute(key, value);
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {setUserAttribute}", throwable);
        }
    }

    public static void setUserAttribute(@NotNull String key, long value) {
        try {
            Airbridge.setUserAttribute(key, value);
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {setUserAttribute}", throwable);
        }
    }

    public static void setUserAttribute(@NotNull String key, float value) {
        try {
            Airbridge.setUserAttribute(key, value);
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {setUserAttribute}", throwable);
        }
    }

    public static void setUserAttribute(@NotNull String key, double value) {
        try {
            Airbridge.setUserAttribute(key, value);
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {setUserAttribute}", throwable);
        }
    }

    public static void setUserAttribute(@NotNull String key, boolean value) {
        try {
            Airbridge.setUserAttribute(key, value);
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {setUserAttribute}", throwable);
        }
    }

    public static void setUserAttribute(@NotNull String key, @NotNull String value) {
        try {
            Airbridge.setUserAttribute(key, value);
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {setUserAttribute}", throwable);
        }
    }

    public static void removeUserAttribute(@NotNull String key) {
        try {
            Airbridge.removeUserAttribute(key);
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {removeUserAttribute}", throwable);
        }
    }

    public static void clearUserAttributes() {
        try {
            Airbridge.clearUserAttributes();
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {clearUserAttributes}", throwable);
        }
    }

    public static void setUserAlias(@NotNull String key, @NotNull String value) {
        try {
            Airbridge.setUserAlias(key, value);
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {setUserAlias}", throwable);
        }
    }

    public static void removeUserAlias(@NotNull String key) {
        try {
            Airbridge.removeUserAlias(key);
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {removeUserAlias}", throwable);
        }
    }

    public static void clearUserAlias() {
        try {
            Airbridge.clearUserAlias();
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {clearUserAlias}", throwable);
        }
    }

    public static void clearUser() {
        try {
            Airbridge.clearUser();
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {clearUser}", throwable);
        }
    }

    public static void setDeviceAlias(@NotNull String key, @NotNull String value) {
        try {
            Airbridge.setDeviceAlias(key, value);
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {setDeviceAlias}", throwable);
        }
    }

    public static void removeDeviceAlias(@NotNull String key) {
        try {
            Airbridge.removeDeviceAlias(key);
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {removeDeviceAlias}", throwable);
        }
    }

    public static void clearDeviceAlias() {
        try {
            Airbridge.clearDeviceAlias();
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {clearDeviceAlias}", throwable);
        }
    }

    public static void registerPushToken(@NotNull String token) {
        try {
            Airbridge.registerPushToken(token);
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {registerPushToken}", throwable);
        }
    }

    public static void trackEvent(
            @NotNull String category,
            String semanticAttributesJsonString,
            String customAttributesJsonString
    ) {
        try {
            Map<String, Object> semanticAttributes = AirbridgeJsonParser.from(semanticAttributesJsonString);
            Map<String, Object> customAttributes = AirbridgeJsonParser.from(customAttributesJsonString);
            Airbridge.trackEvent(category, semanticAttributes, customAttributes);
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {trackEvent}", throwable);
        }
    }

    public static void click(@NotNull String trackingLink, AirbridgeCallback onSuccess, AirbridgeCallback onFailure) {
        try {
            Airbridge.click(
                    Uri.parse(trackingLink),
                    unit -> {
                        if (onSuccess != null) onSuccess.Invoke("");
                    },
                    throwable -> {
                        if (onFailure != null) onFailure.Invoke(getMessage(throwable));
                    }
            );
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {click}", throwable);
        }
    }

    public static void impression(@NotNull String trackingLink, AirbridgeCallback onSuccess, AirbridgeCallback onFailure) {
        try {
            Airbridge.impression(
                    Uri.parse(trackingLink),
                    unit -> {
                        if (onSuccess != null) onSuccess.Invoke("");
                    },
                    throwable -> {
                        if (onFailure != null) onFailure.Invoke(getMessage(throwable));
                    }
            );
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {impression}", throwable);
        }
    }

    public static void fetchAirbridgeGeneratedUUID(@NotNull AirbridgeCallback onSuccess, AirbridgeCallback onFailure) {
        try {
            Airbridge.fetchAirbridgeGeneratedUUID(
                    onSuccess::Invoke,
                    throwable -> {
                        if (onFailure != null) onFailure.Invoke(getMessage(throwable));
                    }
            );
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {fetchAirbridgeGeneratedUUID}", throwable);
        }
    }

    public static void fetchDeviceUUID(@NotNull AirbridgeCallback onSuccess, AirbridgeCallback onFailure) {
        try {
            Airbridge.fetchDeviceUUID(
                    onSuccess::Invoke,
                    throwable -> {
                        if (onFailure != null) onFailure.Invoke(getMessage(throwable));
                    }
            );
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {fetchDeviceUUID}", throwable);
        }
    }

    public static String createWebInterfaceScript(@NotNull String webToken, @NotNull String postMessageScript) {
        try {
            return Airbridge.createWebInterfaceScript(webToken, postMessageScript);
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {createWebInterfaceScript}", throwable);
        }
        return "";
    }

    public static void handleWebInterfaceCommand(@NotNull String command) {
        try {
            Airbridge.handleWebInterfaceCommand(command);
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {handleWebInterfaceCommand}", throwable);
        }
    }

    public static void createTrackingLink(
            @NotNull String channel,
            String optionJsonString,
            @NotNull AirbridgeCallback onSuccess,
            AirbridgeCallback onFailure
    ) {
        try {
            Map<String, Object> option = AirbridgeJsonParser.from(optionJsonString);
            if (option == null) {
                option = new HashMap<>();
            }
            Airbridge.createTrackingLink(channel, option,
                    trackingLink -> {
                        try {
                            JSONObject result = new JSONObject();
                            result.put("shortURL", trackingLink.getShortURL().toString());
                            result.put("qrcodeURL", trackingLink.getQrcodeURL().toString());
                            onSuccess.Invoke(result.toString());
                        } catch (Throwable throwable) {
                            Log.e(TAG, "Error occurs while parsing tracking-link data to json string", throwable);
                        }
                    },
                    throwable -> {
                        if (onFailure != null) onFailure.Invoke(getMessage(throwable));
                    }
            );
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {createTrackingLink}", throwable);
        }
    }

    /* ========================== Handle Deeplink ========================== */

    public static void handleDeeplink(@NotNull AirbridgeCallback onDeeplinkReceived) {

        // Only the first callback is valid
        if (deeplinkCallback != null) return;
        deeplinkCallback = onDeeplinkReceived;

        // Success
        if (isNotNull(deeplink)) {
            handleDeeplinkOnSuccess(deeplink);
        }
        // Check deferred deeplink
        else {
            processHandleDeferredDeeplink();
        }
    }

    @SuppressWarnings("WeakerAccess")
    public static void processHandleDeeplink(Intent intent) {
        try {
            boolean handled = Airbridge.handleDeeplink(intent, /* NotNull */ uri -> {
                if (isNotNull(deeplinkCallback)) {
                    handleDeeplinkOnSuccess(uri.toString());
                } else {
                    deeplink = uri.toString();
                }
            });

            if (handled || AirbridgeSettings.isHandleAirbridgeDeeplinkOnly) { return; }
            if (isNotNull(deeplinkCallback)) {
                handleDeeplinkOnSuccess(intent.getDataString());
            } else {
                deeplink = intent.getDataString();
            }
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {handleDeeplink}", throwable);
        }
    }

    private static void processHandleDeferredDeeplink() {
        try {
            Airbridge.handleDeferredDeeplink(/* Nullable */ uri -> {
                if (isNotNull(uri) && isNotNull(deeplinkCallback)) {
                    handleDeeplinkOnSuccess(uri.toString());
                }
            });
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while calling {handleDeferredDeeplink}", throwable);
        }
    }

    private static void handleDeeplinkOnSuccess(String uri) {
        deeplinkCallback.Invoke(uri);
        deeplink = null;
    }

    /* ========================== Attribution Result Callback ========================== */

    public static void setOnAttributionReceived(@NotNull AirbridgeCallback onAttributionReceived) {

        // Only the first callback is valid
        if (attributionResultCallback != null) return;
        attributionResultCallback = onAttributionReceived;

        unitySendAttributionResult();
    }

    public static void processSetOnAttributionReceived(@NotNull Map<String, String> result) {
        try {
            receivedAttributionResult = AirbridgeJsonParser.to(result);
        } catch (Throwable throwable) {
            Log.e(TAG, "Error occurs while parsing attribution data to json string", throwable);
        }
        unitySendAttributionResult();
    }

    private static void unitySendAttributionResult() {
        if (isNotNull(attributionResultCallback) && isNotNull(receivedAttributionResult)) {
            attributionResultCallback.Invoke(receivedAttributionResult);
            receivedAttributionResult = null;
        }
    }

    /* ================================================================================= */

    public static void setLifecycleIntegration(AirbridgeLifecycleIntegration lifecycleIntegration) {
        if (airbridgeLifecycleIntegration != null) return;
        airbridgeLifecycleIntegration = lifecycleIntegration;
    }
}