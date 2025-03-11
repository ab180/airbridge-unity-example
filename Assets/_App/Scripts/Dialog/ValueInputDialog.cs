using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ValueInputDialog : MonoBehaviour
{
    [SerializeField] private GameObject dialog;
    
    [SerializeField] private Text titleLabel;
    [SerializeField] private Text valueLabel;
    
    [SerializeField] private Button okButton;
    [SerializeField] private Button cancelButton;
    
    [SerializeField] private InputField valueInput;
    
    public void OnClick_ValueLabel()
    {
        EventSystem.current.SetSelectedGameObject(valueInput.gameObject, null);
        valueInput.OnPointerClick(new PointerEventData(EventSystem.current));
    }
    
    public void Show
    (
        string title    = "Value",
        string value    = "VALUE",
        string ok       = "OK",
        string cancel   = "CANCEL",
        Action<string> onOkClicked = null
    ) {
        dialog.SetActive(true);
        
        titleLabel.text = title;
        valueLabel.text = value;
        
        // Reset
        valueInput.text = "";
        okButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
        
        // Set "OK Button"
        okButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = ok;
        okButton.onClick.AddListener(() =>
        {
            try { onOkClicked?.Invoke(valueInput.text); }
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
