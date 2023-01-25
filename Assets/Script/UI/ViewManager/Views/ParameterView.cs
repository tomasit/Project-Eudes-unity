using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI.Extensions;
using UnityEngine.UI;

public class ParameterView : AView 
{
    [SerializeField] private AssignRangedValue[] _input;
    [SerializeField] private AssignSliderValue[] _sliders;
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
        foreach (var slider in _sliders)
        {
            slider.WriteDefault();
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
        foreach (var slider in _sliders)
        {
            slider.GetComponent<Slider>().interactable = interact;
        }
    }
}