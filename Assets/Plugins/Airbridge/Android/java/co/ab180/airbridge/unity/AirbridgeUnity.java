package co.ab180.airbridge.unity;

import android.content.Intent;
import android.net.Uri;
import android.util.Log;

import com.unity3d.player.UnityPlayer;

import org.jetbrains.annotations.NotNull;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.Map;

import co.ab180.airbridge.Airbridge;
import co.ab180.airbridge.AirbridgeCallback;
import co.ab180.airbridge.event.Event;
import co.ab180.airbridge.throwable.AirbridgeSDKNotInitializedException;

public class AirbridgeUnity {

    private static final String TAG = "AirbridgeUnity";

    private static String startDeeplink;
    private static String deeplinkCallbackObjectName;

    private static String receivedAttributionResult;
    private static String attributionResultCallbackObjectName;

    static AirbridgeCallbackWithReturn inAppPurchaseCallback = null;

    public static boolean isSDKEnabled() {
        try { return Airbridge.isSDKEnabled(); }
        catch (AirbridgeSDKNotInitializedException e) {
            Log.e(TAG, "Error occurs while calling {isSDKEnabled}", e);
        }
        return false;
    }

    public static void startTracking() {
        try { Airbridge.startTracking(); }
        catch (AirbridgeSDKNotInitializedException e) {
            Log.e(TAG, "Error occurs while calling {startTracking}", e);
        }
    }

    public static void stopTracking() {
        try { Airbridge.stopTracking(); }
        catch (AirbridgeSDKNotInitializedException e) {
            Log.e(TAG, "Error occurs while calling {stopTracking}", e);
        }
    }

    public static void setUserId(String id) {
        try { Airbridge.getCurrentUser().setId(id); }
        catch (AirbridgeSDKNotInitializedException e) {
            Log.e(TAG, "Error occurs while calling {setUserId}", e);
        }
    }

    public static void setUserEmail(String email) {
        try { Airbridge.getCurrentUser().setEmail(email); }
        catch (AirbridgeSDKNotInitializedException e) {
            Log.e(TAG, "Error occurs while calling {setUserEmail}", e);
        }
    }

    public static void setUserPhone(String phone) {
        try { Airbridge.getCurrentUser().setPhone(phone); }
        catch (AirbridgeSDKNotInitializedException e) {
            Log.e(TAG, "Error occurs while calling {setUserPhone}", e);
        }
    }

    public static void setUserAlias(String key, String value) {
        if (key == null || value == null) return;
        try { Airbridge.getCurrentUser().setAlias(key, value); }
        catch (AirbridgeSDKNotInitializedException e) {
            Log.e(TAG, "Error occurs while calling {setUserAlias}", e);
        }
    }

    public static void setUserAttribute(String key, int value) {
        if (key == null) return;
        try { Airbridge.getCurrentUser().setAttribute(key, value); }
        catch (AirbridgeSDKNotInitializedException e) {
            Log.e(TAG, "Error occurs while calling {setUserAttribute}", e);
        }
    }

    public static void setUserAttribute(String key, long value) {
        if (key == null) return;
        try { Airbridge.getCurrentUser().setAttribute(key, value); }
        catch (AirbridgeSDKNotInitializedException e) {
            Log.e(TAG, "Error occurs while calling {setUserAttribute}", e);
        }
    }

    public static void setUserAttribute(String key, float value) {
        if (key == null) return;
        try { Airbridge.getCurrentUser().setAttribute(key, value); }
        catch (AirbridgeSDKNotInitializedException e) {
            Log.e(TAG, "Error occurs while calling {setUserAttribute}", e);
        }
    }

    public static void setUserAttribute(String key, boolean value) {
        if (key == null) return;
        try { Airbridge.getCurrentUser().setAttribute(key, value); }
        catch (AirbridgeSDKNotInitializedException e) {
            Log.e(TAG, "Error occurs while calling {setUserAttribute}", e);
        }
    }

    public static void setUserAttribute(String key, String value) {
        if (key == null || value == null) return;
        try { Airbridge.getCurrentUser().setAttribute(key, value); }
        catch (AirbridgeSDKNotInitializedException e) {
            Log.e(TAG, "Error occurs while calling {setUserAttribute}", e);
        }
    }

    public static void clearUserAttributes() {
        try { Airbridge.getCurrentUser().clearAttributes(); }
        catch (AirbridgeSDKNotInitializedException e) {
            Log.e(TAG, "Error occurs while calling {clearUserAttributes}", e);
        }
    }

    public static void expireUser() {
        try { Airbridge.expireUser(); }
        catch (AirbridgeSDKNotInitializedException e) {
            Log.e(TAG, "Error occurs while calling {expireUser}", e);
        }
    }

    public static void clickTrackingLink(String trackingLink) {
        if (trackingLink == null) return;
        try { Airbridge.click(trackingLink, null); }
        catch (AirbridgeSDKNotInitializedException e) {
            Log.e(TAG, "Error occurs while calling {clickTrackingLink}", e);
        }
    }

    public static void impressionTrackingLink(String trackingLink) {
        if (trackingLink == null) return;
        try { Airbridge.impression(trackingLink); }
        catch (AirbridgeSDKNotInitializedException e) {
            Log.e(TAG, "Error occurs while calling {impressionTrackingLink}", e);
        }
    }

    public static void trackEvent(String jsonString) {
        try {
            JSONObject object = new JSONObject(jsonString);
            Event event = AirbridgeEventParser.from(object);
            Airbridge.trackEvent(event);
        }
        catch (AirbridgeSDKNotInitializedException e) {
            Log.e(TAG, "Error occurs while calling {trackEvent}", e);
        }
        catch (Exception e) {
            Log.e(TAG, "Error occurs while parsing data json string", e);
        }
    }

    public static void setDeeplinkCallback(String objectName) {
        deeplinkCallbackObjectName = objectName;
        if (startDeeplink != null && !startDeeplink.isEmpty()) {
            UnityPlayer.UnitySendMessage(deeplinkCallbackObjectName, "OnTrackingLinkResponse", startDeeplink);
            startDeeplink = null;
        }
    }

    @SuppressWarnings("WeakerAccess")
    public static void processDeeplinkData(Intent intent) {
        try {
            Airbridge.getDeeplink(intent, new AirbridgeCallback.SimpleCallback<Uri>() {

                @Override
                public void onSuccess(Uri uri) {
                    if (deeplinkCallbackObjectName != null && !deeplinkCallbackObjectName.isEmpty()) {
                        UnityPlayer.UnitySendMessage(deeplinkCallbackObjectName, "OnTrackingLinkResponse", uri.toString());
                        startDeeplink = null;
                    }
                    else {
                        startDeeplink = uri.toString();
                    }
                }

                @Override
                public void onFailure(@NotNull Throwable throwable) {

                }
            });
        } catch (AirbridgeSDKNotInitializedException e) {
            Log.e(TAG, "Error occurs while calling {processDeeplinkData}", e);
        }
    }

    public static void setAttributionResultCallback(String objectName) {
        attributionResultCallbackObjectName = objectName;
        if (receivedAttributionResult != null && !receivedAttributionResult.isEmpty()) {
            UnityPlayer.UnitySendMessage(attributionResultCallbackObjectName, "OnAttributionResultReceived", receivedAttributionResult);
            receivedAttributionResult = null;
        }
    }

    public static void processAttributionData(Map<String, String> result) {
        if (result == null) return;

        JSONObject jsonObject = new JSONObject();
        try {
            for (Map.Entry<String, String> entry : result.entrySet()) {
                jsonObject.put(entry.getKey(), entry.getValue());
            }
            receivedAttributionResult = jsonObject.toString();

            if (attributionResultCallbackObjectName != null && !attributionResultCallbackObjectName.isEmpty()) {
                UnityPlayer.UnitySendMessage(attributionResultCallbackObjectName, "OnAttributionResultReceived", receivedAttributionResult);
                receivedAttributionResult = null;
            }
        } catch (JSONException e) {
            Log.e(TAG, "Error occurs while parsing attribution data to json string", e);
        }
    }

    public static void setDeviceAlias(String key, String value)
    {
        if (key == null || value == null) return;
        try { Airbridge.setDeviceAlias(key, value); }
        catch (AirbridgeSDKNotInitializedException e) {
            Log.e(TAG, "Error occurs while calling {setDeviceAlias}", e);
        }
    }

    public static void removeDeviceAlias(String key)
    {
        if (key == null) return;
        try { Airbridge.removeDeviceAlias(key); }
        catch (AirbridgeSDKNotInitializedException e) {
            Log.e(TAG, "Error occurs while calling {removeDeviceAlias}", e);
        }
    }

    public static void clearDeviceAlias()
    {
        try { Airbridge.clearDeviceAlias(); }
        catch (AirbridgeSDKNotInitializedException e) {
            Log.e(TAG, "Error occurs while calling {clearDeviceAlias}", e);
        }
    }

    public static void registerPushToken(String token)
    {
        if (token == null) return;
        try { Airbridge.registerPushToken(token); }
        catch (AirbridgeSDKNotInitializedException e) {
            Log.e(TAG, "Error occurs while calling {registerPushToken}", e);
        }
    }

    public static boolean fetchAirbridgeGeneratedUUID(@NotNull String objectName)
    {
        try {
            return Airbridge.fetchAirbridgeGeneratedUUID(uuid ->
                UnityPlayer.UnitySendMessage(objectName, "OnFetchAirbridgeGeneratedUUID", uuid));
        }
        catch (AirbridgeSDKNotInitializedException e) {
            Log.e(TAG, "Error occurs while calling {fetchAirbridgeGeneratedUUID}", e);
        }
        return false;
    }
    
    public static void fetchDeviceUUID(@NotNull co.ab180.airbridge.unity.AirbridgeCallback onSuccess) {
        try {
            Airbridge.getDeviceInfo().getUUID(new co.ab180.airbridge.AirbridgeCallback.SimpleCallback<String>() {
                @Override
                public void onSuccess(String result) {
                    onSuccess.Invoke(result);
                }
    
                @Override
                public void onFailure(@NotNull Throwable throwable) {
                }
            });
        } catch (AirbridgeSDKNotInitializedException e) {
            Log.e("AirbridgeUnity", "Error occurs while calling {fetchDeviceUUID}", e);
        }
    }

    public static void startInAppPurchaseTracking() {
        try { Airbridge.startInAppPurchaseTracking(); }
        catch (AirbridgeSDKNotInitializedException e) {
            Log.e(TAG, "Error occurs while calling {startInAppPurchaseTracking}", e);
        }
    }
    
    public static void stopInAppPurchaseTracking() {
        try { Airbridge.stopInAppPurchaseTracking(); }
        catch (AirbridgeSDKNotInitializedException e) {
            Log.e(TAG, "Error occurs while calling {stopInAppPurchaseTracking}", e);
        }
    }
    
    public static boolean isInAppPurchaseTrackingEnabled() {
        try { return Airbridge.isInAppPurchaseTrackingEnabled(); }
        catch (AirbridgeSDKNotInitializedException e) {
            Log.e(TAG, "Error occurs while calling {isInAppPurchaseTrackingEnabled}", e);
        }
        return false;
    }

    public static void setOnInAppPurchaseReceived(AirbridgeCallbackWithReturn onInAppPurchaseReceived) {
        if (inAppPurchaseCallback == null) {
            inAppPurchaseCallback = onInAppPurchaseReceived;
        } else {
            Log.w(TAG, "Already called {setOnInAppPurchaseReceived}");
        }
    }
}