package co.ab180.airbridge.unity;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

class AirbridgeJsonParser {

    /**
     * Convert Map to Json String
     */
    static String to(Map<String, ?> map) throws JSONException {
        if (map == null) {
            return null;
        }

        JSONObject toReturn = new JSONObject();
        for (Map.Entry<String, ?> entry : map.entrySet()) {
            toReturn.put(entry.getKey(), entry.getValue());
        }
        return toReturn.toString();
    }

    /**
     * Convert Json String to Map
     */
    static Map<String, Object> from(String jsonString) throws JSONException {
        JSONObject object = toJSONObject(jsonString);
        if (object == null) {
            return null;
        } else {
            return toMap(object);
        }
    }

    private static JSONObject toJSONObject(String jsonString) throws JSONException {
        if (jsonString == null
                || jsonString.trim().equals("null")
                || jsonString.trim().isEmpty()
        ) {
            return null;
        }
        return new JSONObject(jsonString);
    }

    private static Map<String, Object> toMap(JSONObject object) throws JSONException {
        Map<String, Object> toReturn = new HashMap<>();
        Iterator<String> keys = object.keys();
        while (keys.hasNext()) {
            String key = keys.next();
            Object value = object.get(key);
            if (value instanceof JSONArray) {
                value = toList((JSONArray) value);
            } else if (value instanceof JSONObject) {
                value = toMap((JSONObject) value);
            } else if (value == JSONObject.NULL) {
                value = null;
            }
            toReturn.put(key, value);
        }
        return toReturn;
    }

    private static List<Object> toList(JSONArray array) throws JSONException {
        List<Object> toReturn = new ArrayList<>();
        for (int i = 0; i < array.length(); i++) {
            Object value = array.get(i);
            if (value instanceof JSONArray) {
                value = toList((JSONArray) value);
            } else if (value instanceof JSONObject) {
                value = toMap((JSONObject) value);
            } else if (value == JSONObject.NULL) {
                value = null;
            }
            toReturn.add(value);
        }
        return toReturn;
    }
}