using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    private Rigidbody2D _rgbd = null;
    private float _time;
    [SerializeField] private Vector2 _forceRange;
    private float _currentForce = 0.0f;
    [SerializeField] private float _maxTime = 1.0f;


    private void Start()
    {
        _rgbd = FindObjectOfType<AutoAddForce>(true).GetRigidbody();
    }

    private void ArrowShit()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            _time = 0.0f;
            _currentForce = 0.0f;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            _currentForce = _forceRange.x + (_forceRange.y - _forceRange.x) * (_time / _maxTime);
            _time += Time.deltaTime;
            _time = Mathf.Clamp(_time, 0, _maxTime);
            _rgbd.AddForce(Vector2.up * _currentForce, ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _currentForce = _forceRange.x + (_forceRange.y - _forceRange.x) * (_time / _maxTime);
            _currentForce *= -1;
            _time += Time.deltaTime;
            _time = Mathf.Clamp(_time, 0, _maxTime);
            _rgbd.AddForce(Vector2.up * _currentForce, ForceMode2D.Force);
        }
    }

    private void PitchAndRollShit()
    {
        
    }

    private void Update()
    {
        if (_rgbd == null)
            _rgbd = FindObjectOfType<AutoAddForce>(true).GetRigidbody();
        ArrowShit();
        PitchAndRollShit();
    }
}