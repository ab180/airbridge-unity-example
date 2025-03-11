using UnityEngine;

public class UISafeAreaHandler : MonoBehaviour
{
    private RectTransform _panel;
    
    // Start is called before the first frame update
    void Start()
    {
        _panel = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Rect area = Screen.safeArea;
        /* Pixel size in screen space of the whole screen */
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        /* Set anchors to percentages of the screen used. */
        _panel.anchorMin = area.position / screenSize;
        _panel.anchorMax = (area.position + area.size) / screenSize;
    }
}
