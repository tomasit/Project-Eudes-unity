using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionManager : MonoBehaviour
{
    [SerializeField] private ASessionObject[] _objectsToRecord;
    [SerializeField] private bool _isRunning = false;
    private bool _isPaused = false;
    private float _timer = 0.0f;

    public bool IsRunning()
    {
        return _isRunning;
    }

    public void StartSession()
    {
        foreach (var o in _objectsToRecord)
            o.StartSession();
        _timer = 0.0f;
        _isRunning = true;
        _isPaused = false;
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
        _isPaused = true;
    }

    public void Resume()
    {
        foreach (var o in _objectsToRecord)
            o.Resume();
        _isPaused = false;
    }

    private bool CanRun()
    {
        return !_isPaused && _isRunning;
    }

    private void Update()
    {
        if (!CanRun())
            return;

        if (_timer >= SaveManager.DataInstance.GetParameters()._sessionDuration * 60f)
        {
            FindObjectOfType<ShowView>().ShowEndGameView();
        }

        _timer += Time.deltaTime;
    }
}