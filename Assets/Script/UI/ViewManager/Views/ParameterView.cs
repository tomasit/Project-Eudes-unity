using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ParameterView : AView 
{
    [SerializeField] private AssignInputValue[] _input;
    
    public override void Show()
    {
        // GetComponent<SpawnPopup>().DeletePopup();
        WriteDefault();
        base.Show();
    }

    public void WriteDefault()
    {
        foreach (var input in _input)
        {
            input.WriteDefault();
        }
    }

    public void ValidateInput()
    {
        foreach (var input in _input)
        {
            input.AssignValue();
        }
    }

    public void InteractInputField(bool interact)
    {
        foreach (var input in _input)
        {
            input.GetComponent<TMP_InputField>().interactable = interact;
        }
    }
}