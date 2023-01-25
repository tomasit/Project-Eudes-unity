using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO : clamp position into playing area
// move position each time going on the main loop
// connect with Input
public class Pedals : ASessionObject
{
    [SerializeField] private SpriteRenderer _pedals;
    [SerializeField] private GameObject _target;
    private PlayingArea _playingArea = null;
    private float _counter = 0.0f;
    private List<float> _reactionTime;
    private float _reactionTimeCounter;
    private List<float> _accuracy;
    private float _waitTime = 0.0f;
    private bool _calculReactionTime = false;

    private void Start()
    {
        // Debug.Log(GameBalance.ComputeBalance(new List<float>(){1.2f, 1.3f, 2.3f, 0.7f}, new Vector2(0.8f, 2.0f)));
        _playingArea = FindObjectOfType<PlayingArea>();
        _accuracy = new List<float>();
        _reactionTime = new List<float>();
    }

    public override void StartSession()
    {
        _started = true;
        _counter = 0.0f;
        _waitTime = GetWaitTime();
        _accuracy.Clear();
        _reactionTime.Clear();
        _reactionTimeCounter = 0.0f;
        _calculReactionTime = true;
    }

    public override void StopSession()
    {
        _started = false;
        SaveSessionData();
    }

    // This function calcul statistique from session datas.
    // Balancing is done here too. If you change values from here
    // you are going to unbalance the graph, which doesn't care
    // about balancing changes.
    public override void SaveSessionData()
    {
        
    }

    private void MoveToCenter()
    {
        transform.position = new Vector3(
            Random.Range(_playingArea.GetAreaPosition().x - _playingArea.GetAreaBounds().x * 0.5f + _pedals.bounds.size.x * 0.5f,
            _playingArea.GetAreaPosition().x + _playingArea.GetAreaBounds().x * 0.5f - _pedals.bounds.size.x * 0.5f), transform.position.y, transform.position.z);
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

    private void Update()
    {
        if (_demoObject)
            InputInteraction();

        if (!CanRun())
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

            MoveToCenter();
            _calculReactionTime = true;

            _waitTime = 2.0f;
            _counter = 0.0f;
        }

        _counter += Time.deltaTime;
    }
}