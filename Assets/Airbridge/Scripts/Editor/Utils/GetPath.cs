#if UNITY_EDITOR
using System;
using System.IO;
using UnityEngine;

partial class AirbridgeUtils
{
    internal static string GetUnityPackageAssetsPath()
    {
        string path = Application.dataPath;

        try
        {
            string packagePath = Path.GetFullPath("Packages/co.ab180.airbridge-unity-sdk/Assets");
            if (Directory.Exists(packagePath))
            {
                path = packagePath;
            }
        }
        catch (Exception) { /* ignored */ }
        
        return path;
    }
    
    internal static string GetPluginPath(Platform platform)
    {
        try
        {
            string airbridgeDirPath = Path.Combine(Application.dataPath, "Plugins", platform.GetStringValue(), "Airbridge");
            
            if (!Directory.Exists(airbridgeDirPath))
            {
                Directory.CreateDirectory(airbridgeDirPath);
                Debug.LogFormat(
                    "Create Airbridge {0} Plugin directiory : {1}",
                    platform.GetStringValue(),
                    airbridgeDirPath
                );
            }

            return airbridgeDirPath;
        }
        catch (Exception exception)
        {
            Debug.LogErrorFormat(
                "Something broken while getting Airbridge {0} Plugin directiory path : {1}",
                platform.GetStringValue(),
                exception
            );
            return null;
        }
    }
    
    internal enum Platform
    {
        Android, 
        iOS
    }
}

static class AirbridgeUtilsPlatformExtension
{
    internal static string GetStringValue(this AirbridgeUtils.Platform platform)
    {
        switch (platform)
        {
            case AirbridgeUtils.Platform.Android:
                return "Android";
            case AirbridgeUtils.Platform.iOS:
                return "iOS";
            default:
                throw new InvalidOperationException();
        }
    }
}

#endif