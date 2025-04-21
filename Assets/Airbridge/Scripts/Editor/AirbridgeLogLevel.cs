public class AirbridgeLogLevel
{
    /**
     * +------------------+---------------+---------------+
     * | Unity Log Level  | Andoid Level  | iOS Level     |
     * +------------------+---------------+---------------+
     * | Fault            | Assert        | Crash         |
     * | Error            | Error         | Critical      |
     * | Warning          | Warn          | Warning       |
     * | Info             | Info          | Info          |
     * | Debug            | Verbose       | Debug         |
     * +------------------+---------------+---------------+
     */
    public static readonly string[] LogLevel = { "Debug", "Info", "Warning", "Error", "Fault" };

    public static readonly int Default = 2; // Default is "Warning"


    public static string GetAndroidLogLevel(int index)
    {
        switch (index)
        {
            // Unity Log Level: Debug   [0]
            // Andoid Level:    VERBOSE [2]
            case 0: return "2";
            // Unity Log Level: Info    [1]
            // Andoid Level:    INFO    [4]
            case 1: return "4";
            // Unity Log Level: Warning [2]
            // Andoid Level:    WARN    [5]
            case 2: return "5";
            // Unity Log Level: Error   [3]
            // Andoid Level:    ERROR   [6]
            case 3: return "6";
            // Unity Log Level: Fault   [4]
            // Andoid Level:    Assert  [7]
            default: return "7";
        }
    }

    // https://github.com/ab180/airbridge-ios-sdk/blob/archive/version1/AirBridge/Log/ABLogLevel.h
    public static string GetIOSLogLevel(int index)
    {
        switch (index)
        {
            // Unity Log Level: Debug           [0]
            // iOS Level:       AB_LOG_DEBUG    [5]
            case 0: return "5";
            // Unity Log Level: Info            [1]
            // iOS Level:       AB_LOG_INFO     [4]
            case 1: return "4";
            // Unity Log Level: Warning         [2]
            // iOS Level:       AB_LOG_WARNING  [3]
            case 2: return "3";
            // Unity Log Level: Error           [3]
            // iOS Level:       AB_LOG_CRITICAL [2]
            case 3: return "2";
            // Unity Log Level: Fault           [4]
            // iOS Level:       AB_LOG_CRASH    [1]
            default: return "1";
        }
    }
}