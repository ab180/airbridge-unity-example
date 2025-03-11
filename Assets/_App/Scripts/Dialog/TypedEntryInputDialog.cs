using System;
using UnityEngine;
using UnityEngine.UI;

public enum AirbridgePropertyType { Int, Long, Float, Double, Boolean, String }

public class TypedEntryInputDialog : IEntryInputDialog
{
    [SerializeField] private Dropdown typeDropdown;
    
    public void Show
    (
        string title                        = "Typed Entry", 
        string key                          = "KEY",
        string value                        = "VALUE",
        string ok                           = "OK",
        string cancel                       = "CANCEL",
        AirbridgePropertyType defaultType   = AirbridgePropertyType.String,
        int[] disableOptions                = null,
        Action<string, string, AirbridgePropertyType> onOkClicked = null
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
        typeDropdown.value = (int)defaultType;
        
        // Set disable option
        if (disableOptions != null)
        {
            foreach (var disableOption in disableOptions)
            {
                typeDropdown.GetComponent<DropDownController>().EnableOption(disableOption, false);
            }
        }
        
        // Set "OK Button"
        okButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = ok;
        okButton.onClick.AddListener(() =>
        {
            try
            {
                onOkClicked?.Invoke(keyInput.text, valueInput.text, (AirbridgePropertyType)typeDropdown.value);
            }
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