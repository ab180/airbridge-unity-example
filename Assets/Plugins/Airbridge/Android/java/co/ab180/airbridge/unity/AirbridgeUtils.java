package co.ab180.airbridge.unity;

import org.jetbrains.annotations.NotNull;

import java.util.ArrayList;
import java.util.List;

class AirbridgeUtils {

    static List<String> joinedStringToList(String joinedString, String delimiter) {
        String[] array = joinedString.split(delimiter);
        List<String> ret = new ArrayList<>();
        for (String element : array) {
            // Remove white space
            if (!element.trim().isEmpty()) {
                ret.add(element);
            }
        }
        return ret;
    }

    static boolean isNotNull(Object obj) {
        return (obj != null);
    }

    static String getMessage(@NotNull Throwable throwable) {
        String message = throwable.getMessage();
        return (message == null) ? "" : message;
    }
}
