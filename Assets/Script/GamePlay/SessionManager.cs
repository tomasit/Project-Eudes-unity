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

    public bool tkt = false;

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
        if (tkt)
        {
            StartSession();
            tkt = false;
        }

        if (!CanRun())
            return;

        if (_timer >= SaveManager.DataInstance.GetParameters()._sessionDuration * 60f)
        {
            StopSession();
            // Pause();
            SaveManager.DataInstance.SaveGraph();
            // Debug.Log("End of session");
            SceneManager.LoadScene("MainMenu");
        }

        _timer += Time.deltaTime;
    }
}