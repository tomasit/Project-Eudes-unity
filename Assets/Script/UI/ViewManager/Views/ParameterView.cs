using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI.Extensions;

public class ParameterView : AView 
{
    [SerializeField] private AssignInputValue[] _input;
    [HideInInspector] public ParametrableValues _tmpSettings;
    [SerializeField] private PlayingArea _playingArea;

    public override void Show()
    {
        _tmpSettings = new ParametrableValues(SaveManager.DataInstance.GetParameters());
        _playingArea.gameObject.SetActive(true);
        _playingArea.EnableArea(true);
        _playingArea.RestartSession();
        WriteDefault();
        base.Show();
    }

    public override void Hide()
    {
        _playingArea.EnableArea(false);
        _playingArea.gameObject.SetActive(false);
        base.Hide();
    }

    public void WriteDefault()
    {
        foreach (var input in _input)
        {
            input.WriteDefault();
        }
    }

    public void RestoreOldParameters()
    {
        // Debug.Log("Restore old parameters");
        SaveManager.DataInstance.SetParameters(_tmpSettings);
    }

    public void InteractInputField(bool interact)
    {
        foreach (var input in _input)
        {
            input.GetComponent<RangeSlider>().interactable = interact;
        }
    }
}