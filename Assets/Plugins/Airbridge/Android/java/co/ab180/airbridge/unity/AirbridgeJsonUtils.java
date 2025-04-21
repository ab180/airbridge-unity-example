package co.ab180.airbridge.unity;

import java.util.List;
import java.util.Map;

class AirbridgeJsonUtils {

    @SuppressWarnings("unchecked")
    public static Map<String, Object> safeConvertToMap(Object obj) {
        if (obj == null) {
            return null;
        }
        if (obj instanceof Map) {
            Map<?, ?> rawMap = (Map<?, ?>) obj;
            for (Map.Entry<?, ?> entry : rawMap.entrySet()) {
                if (!(entry.getKey() instanceof String)) {
                    return null;
                }
            }
            return (Map<String, Object>) rawMap;
        }
        return null;
    }

    @SuppressWarnings("unchecked")
    public static List<Object> safeConvertToList(Object obj) {
        if (obj == null) {
            return null;
        }
        if (obj instanceof List) {
            List<?> rawList = (List<?>) obj;
            return (List<Object>) rawList;
        }
        return null;
    }
}