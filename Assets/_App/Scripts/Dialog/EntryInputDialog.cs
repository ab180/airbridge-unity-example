using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IEntryInputDialog : MonoBehaviour
{
    [SerializeField] public GameObject dialog;
    
    [SerializeField] public Text titleLabel;
    [SerializeField] public Text keyLabel;
    [SerializeField] public Text valueLabel;
    
    [SerializeField] public Button okButton;
    [SerializeField] public Button cancelButton;
    
    [SerializeField] public InputField keyInput;
    [SerializeField] public InputField valueInput;

    public void OnClick_KeyLabel()
    {
        EventSystem.current.SetSelectedGameObject(keyInput.gameObject, null);
        keyInput.OnPointerClick(new PointerEventData(EventSystem.current));
    }
    
    public void OnClick_ValueLabel()
    {
        EventSystem.current.SetSelectedGameObject(valueInput.gameObject, null);
        valueInput.OnPointerClick(new PointerEventData(EventSystem.current));
    }
}

public class EntryInputDialog : IEntryInputDialog
{
    public void Show
    (
        string title    = "Entry", 
        string key      = "KEY",
        string value    = "VALUE",
        string ok       = "OK",
        string cancel   = "CANCEL",
        Action<string, string> onOkClicked = null
    ) {
        dialog.SetActive(true);
        
        titleLabel.text = title;
        keyLabel.text = key;
        valueLabel.text = value;
        
        // Reset
        keyInput.text = "";
        valueInput.text = "";
        okButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
    
        // Set "OK Button"
        okButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = ok;
        okButton.onClick.AddListener(() =>
        {
            try { onOkClicked?.Invoke(keyInput.text, valueInput.text); }
            catch (Exception exception) { /* ignored */ }
            dialog.SetActive(false);
        });
        
        // Set "Cancel Button"
        cancelButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = cancel;
        cancelButton.onClick.AddListener(() =>
        {
            dialog.SetActive(false);
        });
    }
}
