using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAddForce : MonoBehaviour
{
    private Rigidbody2D _rgbd;
    private float _timeToWait;
    private float _counter = 0.0f;
    private bool _init = false;
    private SpriteRenderer _renderer;
    private ConstructGazBar _gazBar;
    private float _accuracy;
    [SerializeField] private bool _sessionStart = false;

    private void Start()
    {
        if (!_init)
            Initialize();
    }

    public void StartSession()
    {
        _sessionStart = true;
        transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
        _accuracy = 0.0f;
        _timeToWait = Random.Range(SaveManager.DataInstance.GetParameters()._gazTimeRangeMin, SaveManager.DataInstance.GetParameters()._gazTimeRangeMax);
        int seed = Random.Range(0, 1);
        _rgbd.AddForce(Vector2.up * Random.Range(seed == 0 ? SaveManager.DataInstance.GetParameters()._gazAutoForceMin : -SaveManager.DataInstance.GetParameters()._gazAutoForceMin, seed == 0 ? SaveManager.DataInstance.GetParameters()._gazAutoForceMax : -SaveManager.DataInstance.GetParameters()._gazAutoForceMax), ForceMode2D.Impulse);
        _counter = 0.0f;
    }

    public Rigidbody2D GetRigidbody()
    {
        return _rgbd;
    }

    private void Initialize()
    {
        _accuracy = 0.0f;
        _gazBar = FindObjectOfType<ConstructGazBar>();
        _renderer = GetComponent<SpriteRenderer>();
        _rgbd = GetComponent<Rigidbody2D>();
        _timeToWait = Random.Range(SaveManager.DataInstance.GetParameters()._gazTimeRangeMin, SaveManager.DataInstance.GetParameters()._gazTimeRangeMax);
        int seed = Random.Range(0, 1);
        
        _rgbd.AddForce(Vector2.up * Random.Range(seed == 0 ? SaveManager.DataInstance.GetParameters()._gazAutoForceMin : -SaveManager.DataInstance.GetParameters()._gazAutoForceMin, seed == 0 ? SaveManager.DataInstance.GetParameters()._gazAutoForceMax : -SaveManager.DataInstance.GetParameters()._gazAutoForceMax), ForceMode2D.Impulse);
        _init = true;
    }

    private void GetAccuracy()
    {
        if (transform.localPosition.y <= _gazBar.GetToleranceGO().transform.localPosition.y + _gazBar.GetToleranceGO().GetComponent<SpriteRenderer>().bounds.size.y * 0.5f
        && transform.localPosition.y >= _gazBar.GetToleranceGO().transform.localPosition.y - _gazBar.GetToleranceGO().GetComponent<SpriteRenderer>().bounds.size.y * 0.5f)
        {
            _accuracy += Time.deltaTime;
        }
    }

    private void Update()
    {
        if (!_sessionStart)
            return;

        if (_counter >= _timeToWait)
        {
            if (_rgbd.velocity.y >= 0.0f)
                _rgbd.AddForce(Vector2.up * Random.Range(-SaveManager.DataInstance.GetParameters()._gazAutoForceMin, -SaveManager.DataInstance.GetParameters()._gazAutoForceMax), ForceMode2D.Impulse);
            else
                _rgbd.AddForce(Vector2.up * Random.Range(SaveManager.DataInstance.GetParameters()._gazAutoForceMin, SaveManager.DataInstance.GetParameters()._gazAutoForceMax), ForceMode2D.Impulse);
            _timeToWait = Random.Range(SaveManager.DataInstance.GetParameters()._gazTimeRangeMin, SaveManager.DataInstance.GetParameters()._gazTimeRangeMax);
            _counter = 0.0f;
        }
        transform.localPosition = new Vector3(transform.localPosition.x,
        Mathf.Clamp(transform.localPosition.y, -ScaleWithScreen.GetScreenToWorldHeight * 0.5f + _renderer.bounds.size.y * 0.5f + _gazBar._sideSize, ScaleWithScreen.GetScreenToWorldHeight * 0.5f - _renderer.bounds.size.y * 0.5f - _gazBar._sideSize),
        transform.localPosition.z);
        _counter += Time.deltaTime;
        GetAccuracy();
    }
}