using System;
using UnityEngine;

// Exclude a class from the document
/// @cond HIDDEN_SYMBOLS
class AirbridgeCallbackAndroidBridge : AndroidJavaProxy
{
    private Action<string> Callback { get; }

    public AirbridgeCallbackAndroidBridge(Action<string> callback) : base("co.ab180.airbridge.unity.AirbridgeCallback")
    {
        Callback = callback;
    }

    public void Invoke(string arg)
    {
        Callback.Invoke(arg);
    }
}

class AirbridgeCallbackWithReturnAndroidBridge : AndroidJavaProxy
{
    private Func<string, string> Callback { get; }

    public AirbridgeCallbackWithReturnAndroidBridge(Func<string, string> callback) : base("co.ab180.airbridge.unity.AirbridgeCallbackWithReturn")
    {
        Callback = callback;
    }

    public string Invoke(string arg)
    {
        return Callback.Invoke(arg);
    }
}
// ReSharper disable once InvalidXmlDocComment
/// @endcond