using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class GameView : AView 
{
    [SerializeField] private SessionManager _sessionManager;

    public override void Show()
    {
        base.Show();
        if (_sessionManager.IsRunning())
        {
            _sessionManager.Resume();
        }
        else
            _sessionManager.StartSession();
    }

    public override void Hide()
    {
        base.Hide();
        _sessionManager.Pause();
    }
}