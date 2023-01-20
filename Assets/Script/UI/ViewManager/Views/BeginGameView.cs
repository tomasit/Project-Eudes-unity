using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class BeginGameView : AView 
{
    [SerializeField] private Volume _volume;
    [SerializeField] private WriteTimer _timerText;
    [SerializeField] private LightManager _lightManager;

    public override void Show()
    {
        _volume.enabled = true;
        _timerText.WriteSessionDuration();
        _lightManager.PreSessionCalculs();
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
        _volume.enabled = false;
    }
}