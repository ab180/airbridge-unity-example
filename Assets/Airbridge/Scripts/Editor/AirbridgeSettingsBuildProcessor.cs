#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public class AirbridgeSettingsBuildProcessor : IPreprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        Console.WriteLine("AirbridgeSettingsBuildProcessor.OnPreprocessBuild for target " + report.summary.platform);
        
        switch (report.summary.platform)
        {
            case BuildTarget.Android:
                AirbridgeSettingsWindow.UpdateAndroidNativeCode();
                AirbridgeSettingsWindow.UpdateAndroidAirbridgeSettings();
                break;
            case BuildTarget.iOS:
                AirbridgeSettingsWindow.UpdateiOSAppSetting();
                break;
            default:
                return;
        }
        
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}

#endif