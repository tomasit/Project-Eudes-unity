using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    [SerializeField] private ASessionObject[] _objectsToRecord;
    [SerializeField] private bool _isRunning = false;
    private float _timer = 0.0f;

    public bool tkt = false;

    public void StartSession()
    {
        foreach (var o in _objectsToRecord)
            o.StartSession();
        _timer = 0.0f;
        _isRunning = true;
    }

    public void StopSession()
    {
        foreach (var o in _objectsToRecord)
            o.StopSession();

        _isRunning = false;
    }

    public void Pause()
    {
        foreach (var o in _objectsToRecord)
            o.Pause();
        _isRunning = false;
    }

    public void Resume()
    {
        foreach (var o in _objectsToRecord)
            o.Resume();
        _isRunning = true;
    }

    private void Update()
    {
        if (tkt)
        {
            StartSession();
            tkt = false;
        }

        if (!_isRunning)
            return;

        if (_timer >= SaveManager.DataInstance.GetParameters()._sessionDuration * 60f)
        {
            StopSession();
            // Pause();
            SaveManager.DataInstance.SaveGraph();
        }

        _timer += Time.deltaTime;
    }
}