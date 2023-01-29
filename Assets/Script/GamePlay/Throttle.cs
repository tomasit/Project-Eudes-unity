using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Throttle : ASessionObject
{
    private Rigidbody2D _rgbd;
    private float _timeToWait;
    private float _counter = 0.0f;
    private SpriteRenderer _renderer;
    private ConstructGazBar _gazBar;
    private float _accuracy;
    private float _speed = 0.0f;
    private Vector2 _oldVelocity;

    public void Awake()
    {
        _accuracy = 0.0f;
        _gazBar = FindObjectOfType<ConstructGazBar>();
        _renderer = GetComponent<SpriteRenderer>();
        _rgbd = GetComponent<Rigidbody2D>();
    }

    public override void StartSession()
    {
        _oldVelocity = Vector2.zero;
        _speed = 0.0f;
        _started = true;
        _accuracy = 0.0f;
        _gazBar.ResetArrowPosition();
        _rgbd.velocity = Vector2.zero;
        AutoAddForce(true);
    }

    public override void StopSession()
    {
        _started = false;
        _inPause = false;
        SaveSessionData();
    }

    public override bool Pause()
    {
        _oldVelocity = _rgbd.velocity;
        _rgbd.velocity = Vector2.zero;
        return base.Pause();
    }

    public override void Resume()
    {
        base.Resume();
        _rgbd.velocity = _oldVelocity;
    }

    // This function calcul statistique from session datas.
    // Balancing is done here too. If you change values from here
    // you are going to unbalance the graph, which doesn't care
    // about balancing changes.
    public override void SaveSessionData()
    {
        SaveManager.DataInstance.GetDict()[StatistiqueGraph.StatistiqueType.GAZ_ACCURACY].Add(
            _accuracy / (SaveManager.DataInstance.GetParameters()._sessionDuration * 60f));
    }

    public void MoveThrottle(InputAction.CallbackContext value)
    {
        if (value.ReadValue<float>() != 0.0f)
            _speed = value.ReadValue<float>();
    }

    private void GetAccuracy()
    {
        if (transform.localPosition.y <= _gazBar.GetToleranceGO().transform.localPosition.y + _gazBar.GetToleranceGO().GetComponent<SpriteRenderer>().bounds.size.y * 0.5f
        && transform.localPosition.y >= _gazBar.GetToleranceGO().transform.localPosition.y - _gazBar.GetToleranceGO().GetComponent<SpriteRenderer>().bounds.size.y * 0.5f)
        {
            _accuracy += Time.deltaTime;
        }
    }

    private void AutoAddForce(bool randomizeDirection = false)
    {
        if (randomizeDirection)
        {
            int seed = Random.Range(0, 1);
            
            _rgbd.AddForce(Vector2.up * Random.Range(seed == 0 ? SaveManager.DataInstance.GetParameters()._gazAutoForceMin : -SaveManager.DataInstance.GetParameters()._gazAutoForceMin, seed == 0 ? SaveManager.DataInstance.GetParameters()._gazAutoForceMax : -SaveManager.DataInstance.GetParameters()._gazAutoForceMax), ForceMode2D.Impulse);
        }
        else
        {
            if (_rgbd.velocity.y >= 0.0f)
                _rgbd.AddForce(Vector2.up * Random.Range(-SaveManager.DataInstance.GetParameters()._gazAutoForceMin, -SaveManager.DataInstance.GetParameters()._gazAutoForceMax), ForceMode2D.Impulse);
            else
                _rgbd.AddForce(Vector2.up * Random.Range(SaveManager.DataInstance.GetParameters()._gazAutoForceMin, SaveManager.DataInstance.GetParameters()._gazAutoForceMax), ForceMode2D.Impulse);
        }
        _counter = 0.0f;
        _timeToWait = Random.Range(SaveManager.DataInstance.GetParameters()._gazTimeRangeMin, SaveManager.DataInstance.GetParameters()._gazTimeRangeMax);
    }

    private void AddForceByThrottle()
    {
        // Debug.Log("Speed : " + ((SaveManager.DataInstance.GetParameters()._throttleSpeed * -_speed) * Time.deltaTime));
        _rgbd.AddForce(Vector2.up * ((SaveManager.DataInstance.GetParameters()._throttleSpeed * -_speed) * Time.deltaTime), ForceMode2D.Force);
    }

    private void MoveObject()
    {
        if (_counter >= _timeToWait)
        {
            AutoAddForce();
        }

        AddForceByThrottle();

        transform.localPosition = new Vector3(transform.localPosition.x,
        Mathf.Clamp(transform.localPosition.y, -_gazBar.GetScaleY() * 0.5f + _renderer.bounds.size.y * 0.5f + _gazBar._sideSize, _gazBar.GetScaleY() * 0.5f - _renderer.bounds.size.y * 0.5f - _gazBar._sideSize),
        transform.localPosition.z);
    }

    private void Update()
    {
        if (_demoObject)
        {
            MoveObject();
        }

        if (!CanRun())
            return;

        MoveObject();

        _counter += Time.deltaTime;
        GetAccuracy();
    }
}