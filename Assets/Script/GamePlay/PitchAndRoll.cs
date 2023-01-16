using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PitchAndRoll : ASessionObject
{
    [SerializeField] private float _rotationSpeed; // parametrable value
    [SerializeField] private float _movementSpeed; // parametrable value
    [SerializeField] private float _clampToRotation;
    [SerializeField] private SpriteRenderer _sprite;

    private PlayerInput _input;
    private Vector2 _movement;
    private PlayingArea _playingArea = null;
    private List<float> _reactionTime;
    private List<float> _accuracy;

    public void Start()
    {
        _playingArea = FindObjectOfType<PlayingArea>();
        PlaceInMiddle();
    }

    private void PlaceInMiddle()
    {
        transform.position = new Vector3(
            Random.Range(_playingArea.GetAreaPosition().x - _playingArea.GetAreaBounds().x * 0.5f + _sprite.bounds.size.x * 0.5f,
            _playingArea.GetAreaPosition().x + _playingArea.GetAreaBounds().x * 0.5f - _sprite.bounds.size.x * 0.5f),
            transform.position.y, transform.position.z);
    }

    public override void StartSession()
    {
        _hasStarted = true;

    }

    public override void StopSession()
    {
        _hasStarted = false;
        _inPause = false;
    }

    public void Move(InputAction.CallbackContext value)
    {
        _movement = value.ReadValue<Vector2>();
    }

    public void MoveObject()
    {
        float newRotation = (_rotationSpeed * Time.deltaTime * _movement.x);
        transform.Rotate(0.0f, 0.0f, -newRotation, Space.World);
        var rotation = transform.eulerAngles;
        rotation.z = Mathf.Clamp(rotation.z, rotation.z > 150.0f ? 360.0f - _clampToRotation : 0, rotation.z > 150.0f ? 360.0f : _clampToRotation);
        transform.eulerAngles = rotation;

        transform.Translate(transform.up * (_movementSpeed * Time.deltaTime) * _movement.y);
    }

    private void Update()
    {
        if (_isDemo)
            MoveObject();

        if (!CanRun())
            return;
        
        MoveObject();

    }
}