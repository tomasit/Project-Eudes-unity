using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class InGameStatView : AView 
{
    [SerializeField] private Volume _volume;
    [SerializeField] private SessionManager _sessionManager;

    public override void Show()
    {
        _sessionManager.StopSession();
        SaveManager.DataInstance.SaveGraph();

        _volume.enabled = true;
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
        _volume.enabled = false;
    }
}