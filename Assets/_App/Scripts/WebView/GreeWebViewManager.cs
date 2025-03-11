using System;
using UnityEngine;

public class GreeWebViewManager : MonoBehaviour
{
    [SerializeField] private WebViewObject webViewObject;
    [SerializeField] private string url;
    [SerializeField] private string webToken;
   
    private string webInterfaceScript;

    void Start()
    {
        string PostMessageCommand(string arg) =>
            $@"
if (window && window.webkit && window.webkit.messageHandlers && window.webkit.messageHandlers.unityControl) {{
    window.webkit.messageHandlers.unityControl.postMessage({arg});
}} else {{
    var iframe = document.createElement('IFRAME');
    iframe.setAttribute('src', 'unity:' + {arg});
    document.documentElement.appendChild(iframe);
    iframe.parentNode.removeChild(iframe);
    iframe = null;
}}";
        webInterfaceScript = Airbridge.CreateWebInterfaceScript(webToken, PostMessageCommand("payload"));
        StartWebView();
    }
    
    private void Update()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        // Android "Back" Button
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                OnBackPressed();
            }
        }
#endif
    }
    
    private void OnBackPressed()
    {
        // Provides a webpage go back history feature.
        if (webViewObject.gameObject.activeInHierarchy && webViewObject.CanGoBack())
        {
            webViewObject.GoBack();
        }
    }
    
    private void StartWebView()
    {
        try
        {
            webViewObject.Init(
                cb: (msg) =>
                {
                    print($"WebView CallFromJS : {msg}");
                    Airbridge.HandleWebInterfaceCommand(msg);
                },
                err: (msg) => { Debug.Log($"WebView Error : {msg}"); },
                httpErr: (msg) => { Debug.Log($"WebView HttpError : {msg}"); },
                started: (msg) =>
                {
                    Debug.Log($"WebView Started : {msg}");
                        
// JavaScript must be injected before the website loads.
#if UNITY_IOS && !UNITY_EDITOR
                    webViewObject.EvaluateJS(webInterfaceScript);
#endif
                },
                hooked: (msg) => { Debug.Log($"WebView Hooked : {msg}"); },
                ld: (msg) =>
                {
                    Debug.Log($"WebView Loaded : {msg}");
                    webViewObject.EvaluateJS(webInterfaceScript);
                }
                , androidForceDarkMode: 1  // 0: follow system setting, 1: force dark off, 2: force dark on
 
#if UNITY_EDITOR
                , separated: true
#endif
            );
            
            webViewObject.LoadURL(url);
            webViewObject.SetVisibility(true);
            AdjustWebViewMargin();
        }
        catch (System.Exception e)
        {
            print($"WebView Error : {e}");
        }
    }
    
    void OnRectTransformDimensionsChange()
    {
        AdjustWebViewMargin();
    }
    
    private void AdjustWebViewMargin()
    {
        var safeArea = new
        {
            top = Screen.safeArea.yMax,
            bottom = Screen.safeArea.yMin,
            left = Screen.safeArea.xMin,
            right = Screen.safeArea.xMax
        };
        
        // Debug.Log($"[Screen] Width-[{Screen.width}] | Height-[{Screen.height}]");
        // Debug.Log(
        //     $"[Safe Area] Top-[{safeArea.top}] | Bottom-[{safeArea.bottom}]" + 
        //     $" | Left-[{safeArea.left}] | Right-[{safeArea.right}]"
        // );
        
        const double tolerance = 1e-6;
        
        float leftMargin = 0, topMargin = 0, rightMargin = 0, bottomMargin = 0;
        
        // Portrait
        if (Screen.width < Screen.height)
        {
            if (Math.Abs(Screen.height - safeArea.top) < tolerance && safeArea.bottom == 0)
            {
                leftMargin = 0;
                rightMargin = 0;
                topMargin = 300;
                bottomMargin = 0;
            }
            // Has notch area
            else
            {
                leftMargin = safeArea.left + 0;
                rightMargin = (Screen.width - safeArea.right) + 0;
                topMargin = (Screen.height - safeArea.top) + 300;
                bottomMargin = safeArea.bottom + 0;
            }
        }
        // Landscape
        else
        {
            if (Math.Abs(Screen.width - safeArea.right) < tolerance && safeArea.left == 0)
            {
                leftMargin = 0;
                rightMargin = 0;
                topMargin = 300;
                bottomMargin = 0;
            }
            // Has notch area
            else
            {
                leftMargin = safeArea.left + 0;
                rightMargin = (Screen.width - safeArea.right) + 0;
                topMargin = (Screen.height - safeArea.top) + 300;
                bottomMargin = safeArea.bottom + 0;
            }
        }
        
        webViewObject.SetMargins((int)leftMargin, (int)topMargin, (int)rightMargin, (int)bottomMargin);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        webViewObject.SetVisibility(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        webViewObject.SetVisibility(false);
    }
}