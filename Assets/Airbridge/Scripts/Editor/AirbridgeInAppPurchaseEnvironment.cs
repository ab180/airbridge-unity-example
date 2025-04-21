using System;

/// <summary>
/// Enumeration of in-app purchase environment used in [AirbridgeData](@ref AirbridgeData#inAppPurchaseEnvironment).
/// </summary>
public enum AirbridgeInAppPurchaseEnvironment
{
    Production,
    Sandbox
}

// Exclude a class from the document
/// @cond HIDDEN_SYMBOLS
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
// ReSharper disable once InvalidXmlDocComment
/// @endcond