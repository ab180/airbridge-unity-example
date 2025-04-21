using System;

public enum AirbridgeInAppPurchaseEnvironment
{
    Production,
    Sandbox
}

public static class AirbridgeInAppPurchaseEnvironmentExtension
{
    public static readonly string[] Environments =
    {
        AirbridgeInAppPurchaseEnvironment.Production.GetStringValue(), 
        AirbridgeInAppPurchaseEnvironment.Sandbox.GetStringValue()
    };
        
    public static string GetStringValue(this Enum environment)
    {
        switch (environment)
        {
            case AirbridgeInAppPurchaseEnvironment.Production:
                return "production";
            case AirbridgeInAppPurchaseEnvironment.Sandbox:
                return "sandbox";
            default:
                throw new InvalidOperationException();
        }
    }
}