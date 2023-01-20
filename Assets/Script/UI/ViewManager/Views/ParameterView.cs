using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ParameterView : AView 
{
    [SerializeField] private AssignInputValue[] _input;
    [HideInInspector] public ParametrableValues _tmpSettings;
    public override void Show()
    {
        _tmpSettings = SaveManager.DataInstance.GetParameters();
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
        SaveManager.DataInstance.SetParameters(_tmpSettings);
    }

    public void InteractInputField(bool interact)
    {
        foreach (var input in _input)
        {
            input.GetComponent<TMP_InputField>().interactable = interact;
        }
    }
}