using System;

internal enum AirbridgeAndroidApplicationEntry
{
    None,
    Activity,
    GameActivity,
}

internal static class AirbridgeAndroidApplicationEntryExtension
{
/* ===================== AndroidApplicationEntry ===================== */

#if UNITY_EDITOR && UNITY_2023_1_OR_NEWER
    internal static AirbridgeAndroidApplicationEntry ConvertToAirbridgeType(this UnityEditor.AndroidApplicationEntry entry)
    {
        switch (entry)
        {
            case UnityEditor.AndroidApplicationEntry.Activity:
                return AirbridgeAndroidApplicationEntry.Activity;
            case UnityEditor.AndroidApplicationEntry.GameActivity:
                return AirbridgeAndroidApplicationEntry.GameActivity;
            default:
                return AirbridgeAndroidApplicationEntry.None;
        }
    }
#endif
    
/* ===================== AirbridgeAndroidApplicationEntry ===================== */

    internal static bool IsNone(this AirbridgeAndroidApplicationEntry entry)
    {
        return (entry == AirbridgeAndroidApplicationEntry.None);
    }
    
    internal static readonly string[] ApplicationThemes =
    {
        AirbridgeAndroidApplicationEntry.Activity.GetApplicationTheme(),
        AirbridgeAndroidApplicationEntry.GameActivity.GetApplicationTheme(),
    };
    
    internal static readonly string[] ActivityNames =
    {
        AirbridgeAndroidApplicationEntry.Activity.GetActivityName(),
        AirbridgeAndroidApplicationEntry.GameActivity.GetActivityName(),
    };
    
    internal static string GetActivityFileName(this AirbridgeAndroidApplicationEntry entry)
    {
        switch (entry)
        {
            case AirbridgeAndroidApplicationEntry.Activity:
                return "AirbridgeActivity.java";
            case AirbridgeAndroidApplicationEntry.GameActivity:
                return "AirbridgeGameActivity.java";
            default:
                throw new InvalidOperationException();
        }
    }

    internal static string GetManifestFileName(this AirbridgeAndroidApplicationEntry entry)
    {
        switch (entry)
        {
            case AirbridgeAndroidApplicationEntry.Activity:
                return "Activity_AndroidManifest.xml";
            case AirbridgeAndroidApplicationEntry.GameActivity:
                return "GameActivity_AndroidManifest.xml";
            default:
                throw new InvalidOperationException();
        }
    }

    internal static string GetApplicationTheme(this AirbridgeAndroidApplicationEntry entry)
    {
        switch (entry)
        {
            case AirbridgeAndroidApplicationEntry.Activity:
                return "@style/UnityThemeSelector";
            case AirbridgeAndroidApplicationEntry.GameActivity:
                return "@style/BaseUnityGameActivityTheme";
            default:
                throw new InvalidOperationException();
        }
    }

    internal static string GetActivityName(this AirbridgeAndroidApplicationEntry entry)
    {
        switch (entry)
        {
            case AirbridgeAndroidApplicationEntry.Activity:
                return "co.ab180.airbridge.unity.AirbridgeActivity";
            case AirbridgeAndroidApplicationEntry.GameActivity:
                return "co.ab180.airbridge.unity.AirbridgeGameActivity";
            default:
                throw new InvalidOperationException();
        }
    }
}