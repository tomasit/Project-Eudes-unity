using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class EndGameView : AView 
{
    [SerializeField] private Volume _volume;

    public override void Show()
    {
        _volume.enabled = true;
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
        _volume.enabled = false;
    }
}