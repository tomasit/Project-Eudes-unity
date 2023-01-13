using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO : clamp position into playing area
// move position each time going on the main loop
// connect with Input
public class Pedals : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _pedals;
    [SerializeField] private GameObject _target;
    [SerializeField] private bool _startSession = false;
    private PlayingArea _playingArea = null;
    private float _counter = 0.0f;
    private List<float> _reactionTime;
    private float _reactionTimeCounter;
    private List<float> _accuracy;
    private float _waitTime = 0.0f;
    private bool _calculReactionTime = false;

    private float _test = 0.0f;

    private void Start()
    {
        _playingArea = FindObjectOfType<PlayingArea>();
        _accuracy = new List<float>();
        _reactionTime = new List<float>();
        StartSession();
    }

    public void StartSession()
    {
        _startSession = true;
        _counter = 0.0f;
        _waitTime = GetWaitTime();
        _accuracy.Clear();
        _reactionTime.Clear();
        _reactionTimeCounter = 0.0f;
        _calculReactionTime = true;
    }

    private void InputInteraction()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (_calculReactionTime)
            {
                _reactionTime.Add(_reactionTimeCounter);
                _reactionTimeCounter = 0.0f;
                _calculReactionTime = false;
            }
            transform.Translate(-SaveManager.DataInstance.GetParameters()._pedalSpeed * Time.deltaTime, 0.0f, 0.0f);
            transform.position =
                new Vector3(Mathf.Clamp(transform.position.x,
                _playingArea.GetAreaPosition().x - _playingArea.GetAreaBounds().x * 0.5f + _pedals.bounds.size.x * 0.5f, _playingArea.GetAreaPosition().x + _playingArea.GetAreaBounds().x * 0.5f - _pedals.bounds.size.x * 0.5f),
                transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (_calculReactionTime)
            {
                _reactionTime.Add(_reactionTimeCounter);
                _reactionTimeCounter = 0.0f;
                _calculReactionTime = false;
            }
            transform.Translate(SaveManager.DataInstance.GetParameters()._pedalSpeed * Time.deltaTime, 0.0f, 0.0f);
            transform.position =
                new Vector3(Mathf.Clamp(transform.position.x,
                _playingArea.GetAreaPosition().x - _playingArea.GetAreaBounds().x * 0.5f + _pedals.bounds.size.x * 0.5f, _playingArea.GetAreaPosition().x + _playingArea.GetAreaBounds().x * 0.5f - _pedals.bounds.size.x * 0.5f),
                transform.position.y, transform.position.z);
        }
    }

    private float GetWaitTime()
    {
        return 2.0f;
    }

    public void StopSession()
    {
        _startSession = false;
        Debug.Log("Accuracy :");
        foreach (var item in _accuracy)
        {
            Debug.Log(item);
        }

        Debug.Log("Reaction time :");
        foreach (var item in _reactionTime)
        {
            Debug.Log(item);
        }
    }

    private void Update()
    {
        // // erase after debug
        // if (_test >= 20.0f && _startSession)
        //     StopSession();

        if (!_startSession)
            return;

        if (_calculReactionTime)
            _reactionTimeCounter += Time.deltaTime;

        InputInteraction();

        if (_counter >= _waitTime)
        {
            float distance = transform.position.x > _target.transform.position.x ? transform.position.x - _target.transform.position.x : _target.transform.position.x - transform.position.x;
            _accuracy.Add(distance);

            if (_calculReactionTime)
            {
                _reactionTime.Add(_reactionTimeCounter);
                _reactionTimeCounter = 0.0f;
            }
            _calculReactionTime = true;

            transform.position = new Vector3(
                Random.Range(_playingArea.GetAreaPosition().x - _playingArea.GetAreaBounds().x * 0.5f + _pedals.bounds.size.x * 0.5f,
                _playingArea.GetAreaPosition().x + _playingArea.GetAreaBounds().x * 0.5f - _pedals.bounds.size.x * 0.5f),
                transform.position.y, transform.position.z);

            _waitTime = 2.0f;
            _counter = 0.0f;
        }

        _test += Time.deltaTime;
        _counter += Time.deltaTime;
    }
}