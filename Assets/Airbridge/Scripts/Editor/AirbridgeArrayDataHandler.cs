#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

class AirbridgeArrayDataHandler
{
    private string input = default;
    
    private readonly string label;          // Label
    private readonly string elementLabel;   // Add {ElementLabel} | Remove {ElementLabel}
    private readonly char separator;

    public Action<string> AddAction;
    public Action<string> RemoveAction;
    
    private SerializedProperty property;
    
    // Since the IMGUI is a stateless mode,
    // you must cache the ScrollView's scroll position to preserve the scrolling state.
    private Vector2 scrollPosition;
    
    public AirbridgeArrayDataHandler(string label, string elementLabel, char separator = ' ') {
        this.label = label;
        this.elementLabel = elementLabel;
        this.separator = separator;
    }

    private HashSet<string> GetHashSet()
    {
        if (property == null) return null;
        return new HashSet<string>(
            property.stringValue.Split(separator)
                .Where(value => !string.IsNullOrWhiteSpace(value))
                .ToList()
        );
    }

    public void SetProperty(SerializedProperty property)
    {
        this.property = property;
    }

    public void Draw()
    {
        if (property == null)
        {
            Debug.LogError($"Before calling the Draw() function, set the property through calling the SetProperty() function. (at <{label}>)");
            return;
        }
        
        using (new EditorGUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField(label, GUILayout.Width(300));
            input = GUILayout.TextField(input, GUILayout.ExpandWidth(true));
            if (GUILayout.Button($"Add {elementLabel}", EditorStyles.toolbarButton, GUILayout.Width(120)))
            {
                if (!string.IsNullOrWhiteSpace(input)) { Add(input); }
                input = default;    // reset text field
            }
        }

        DrawList();
    }
    
    private void DrawList()
    {
        var set = GetHashSet();
        if (set != null)
        {
            int i = 0;
            if (set.Count > 5)
            {
                // Drawing the scroll view
                using (var scrollViewScope = new EditorGUILayout.ScrollViewScope(scrollPosition, GUILayout.MaxHeight(100)))
                {
                    scrollPosition = scrollViewScope.scrollPosition;
                    foreach (var element in set) { DrawListItem(i++, element); }
                }
            }
            else
            {
                foreach (var element in set) { DrawListItem(i++, element); }
            }
        }
    }

    private void DrawListItem(int i, string element)
    {
        using (new EditorGUILayout.HorizontalScope())
        {
            // Add padding space
            GUILayout.Space(300);
            
            GUILayout.Label((i + 1).ToString(), GUILayout.Width(20));
            GUILayout.Label(element);
            GUILayout.FlexibleSpace();
            
            if (GUILayout.Button($"Remove {elementLabel}")) { Remove(element); }
        }
    }
    
    private void Add(string element)
    {
        var set = GetHashSet();
        if (set != null)
        {
            set.Add(element);
            property.stringValue = string.Join(separator.ToString(), set);

            if (AddAction != null)
            {
                AddAction(element);
            }
        }
    }

    private void Remove(string element)
    {
        var set = GetHashSet();
        if (set != null)
        {
            set.Remove(element);
            property.stringValue = string.Join(separator.ToString(), set);

            if (RemoveAction != null)
            {
                RemoveAction(element);
            }
        }
    }
}
#endif