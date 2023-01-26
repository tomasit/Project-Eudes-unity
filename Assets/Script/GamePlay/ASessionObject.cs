using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASessionObject : MonoBehaviour
{
    [SerializeField] protected bool _inPause = false;
    [SerializeField] protected bool _started = false;
    [SerializeField] protected bool _demoObject = false;
    public abstract void StartSession();
    public abstract void StopSession();
    public abstract void SaveSessionData();
    public bool CanRun() { return !_inPause && _started; }
    public virtual bool Pause() { return _started ? _inPause = true : false; }
    public virtual void Resume() { _inPause = false; }
}