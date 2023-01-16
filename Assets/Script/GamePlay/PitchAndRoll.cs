using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PitchAndRoll : ASessionObject
{
    [SerializeField] private float _clampToRotation;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private GameObject _target;
  
    private Vector2 _movement;
    private PlayingArea _playingArea = null;
    private List<float> _reactionTime;
    private List<float> _accuracy;
    private Coroutine _reactionTimeCoroutine = null;
    private float _reactionTimeCounter = 0.0f;
    private float _timer = 0.0f;

    public void Start()
    {
        _reactionTime = new List<float>();
        _accuracy = new List<float>();
        _playingArea = FindObjectOfType<PlayingArea>();
    }

    public Bounds GetSpriteBounds()
    {
        return _sprite.bounds;
    }

    private void MoveToRandomPosition()
    {
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3(
            Random.Range(_playingArea.GetAreaPosition().x - _playingArea.GetAreaBounds().x * 0.5f + _sprite.bounds.size.x * 0.5f,
                _playingArea.GetAreaPosition().x + _playingArea.GetAreaBounds().x * 0.5f - _sprite.bounds.size.x * 0.5f),
            Random.Range(_playingArea.GetAreaPosition().y - _playingArea.GetAreaBounds().y * 0.5f + _sprite.bounds.size.y * 0.5f,
                _playingArea.GetAreaPosition().y + _playingArea.GetAreaBounds().y * 0.5f - _sprite.bounds.size.y * 0.5f), transform.position.z);
    }

    public override void StartSession()
    {
        _started = true;
        _timer = 0.0f;
        MoveToRandomPosition();
        StartClock();
    }

    public override void StopSession()
    {
        _started = false;
        _inPause = false;
        SaveSessionData();
        _accuracy.Clear();
        _reactionTime.Clear();

        foreach (var tkt in SaveManager.DataInstance.GetDict()[StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_ACCURACY])
        {
            Debug.Log("Percentage of accuracy : " + tkt);
        }
        foreach (var tkt in SaveManager.DataInstance.GetDict()[StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_REACTION_TIME])
        {
            Debug.Log("Percentage of reactionTime : " + tkt);
        }
    }

    // This function calcul statistique from session datas.
    // Balancing is done here too. If you change values from here
    // you are going to unbalance the graph, which doesn't care
    // about balancing changes.
    public override void SaveSessionData()
    {
        SaveManager.DataInstance.GetDict()[StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_REACTION_TIME].Add(100.0f -
            GameBalance.ComputeBalance(_reactionTime, SaveManager.DataInstance.GetBalance(StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_REACTION_TIME)));
        SaveManager.DataInstance.GetDict()[StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_ACCURACY].Add(100.0f -
            GameBalance.ComputeBalance(_accuracy, SaveManager.DataInstance.GetBalance(StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_ACCURACY)));
    }

    public void Move(InputAction.CallbackContext value)
    {
        _movement = value.ReadValue<Vector2>();
    }

    public static Vector3[] GetSpriteCorners(SpriteRenderer renderer)
    {
        Vector3 topRight = renderer.transform.TransformPoint(renderer.sprite.bounds.max);
        Vector3 topLeft = renderer.transform.TransformPoint(new Vector3(renderer.sprite.bounds.max.x, renderer.sprite.bounds.min.y, 0));
        Vector3 botLeft = renderer.transform.TransformPoint(renderer.sprite.bounds.min);
        Vector3 botRight = renderer.transform.TransformPoint(new Vector3(renderer.sprite.bounds.min.x, renderer.sprite.bounds.max.y, 0));
        return new Vector3[] { topRight, topLeft, botLeft, botRight };
    }

    private bool CheckTop(Vector3[] corners)
    {
        bool ret = false;
        bool farestCorner = (corners[0].x > corners[1].x) ? true : false;
 
        if (farestCorner && corners[0].x > _playingArea.GetAreaPosition().x + _playingArea.GetAreaBounds().x * 0.5f)
        {
            var p = transform.position;
            p.x -= corners[0].x - (_playingArea.GetAreaPosition().x + _playingArea.GetAreaBounds().x * 0.5f);
            transform.position = p;
            ret = true;
        }
        else if (corners[1].x > _playingArea.GetAreaPosition().x + _playingArea.GetAreaBounds().x * 0.5f)
        {
            var p = transform.position;
            p.x -= corners[1].x - (_playingArea.GetAreaPosition().x + _playingArea.GetAreaBounds().x * 0.5f);
            transform.position = p;
            ret = true;
        }

        if (corners[0].y > _playingArea.GetAreaPosition().y + _playingArea.GetAreaBounds().y * 0.5f)
        {
            var p = transform.position;
            p.y -= corners[0].y - (_playingArea.GetAreaPosition().y + _playingArea.GetAreaBounds().y * 0.5f);
            transform.position = p;
            ret = true;
        }
        else if (corners[1].y < _playingArea.GetAreaPosition().y - _playingArea.GetAreaBounds().y * 0.5f)
        {
            var p = transform.position;
            p.y += Mathf.Abs(corners[1].y) - Mathf.Abs(_playingArea.GetAreaPosition().y - _playingArea.GetAreaBounds().y * 0.5f);
            transform.position = p;
            ret = true;
        }

        return ret;
    }

    private bool CheckBot(Vector3[] corners)
    {
        bool ret = false;
        bool farestCorner = (corners[2].x < corners[3].x) ? true : false;

        if (farestCorner && corners[2].x < _playingArea.GetAreaPosition().x - _playingArea.GetAreaBounds().x * 0.5f)
        {
            var p = transform.position;
            p.x += Mathf.Abs(corners[2].x) - Mathf.Abs(_playingArea.GetAreaPosition().x - _playingArea.GetAreaBounds().x * 0.5f);
            transform.position = p;
            ret = true;
        }
        else if (corners[3].x < _playingArea.GetAreaPosition().x - _playingArea.GetAreaBounds().x * 0.5f)
        {
            var p = transform.position;
            p.x += Mathf.Abs(corners[3].x) - Mathf.Abs(_playingArea.GetAreaPosition().x - _playingArea.GetAreaBounds().x * 0.5f);
            transform.position = p;
            ret = true;
        }

        if (corners[2].y < _playingArea.GetAreaPosition().y - _playingArea.GetAreaBounds().y * 0.5f)
        {
            var p = transform.position;
            p.y += Mathf.Abs(corners[2].y) - Mathf.Abs(_playingArea.GetAreaPosition().y - _playingArea.GetAreaBounds().y * 0.5f);
            transform.position = p;
            ret = true;
        }
        else if (corners[3].y > _playingArea.GetAreaPosition().y + _playingArea.GetAreaBounds().y * 0.5f)
        {
            var p = transform.position;
            p.y -= corners[3].y - (_playingArea.GetAreaPosition().y + _playingArea.GetAreaBounds().y * 0.5f);
            transform.position = p;
            ret = true;
        }

        return ret;
    }

    private int GetDir(Vector3 target)
    {
        int dir;
        if (transform.position.x > target.x)
            dir = 1;
        else if (transform.position.x < target.x)
            dir = -1;
        else
            dir = 0;

        return dir;
    }

    private IEnumerator ReactionTimeClock(float waitTime)
    {
        _reactionTimeCounter = 0.0f;
        int dir = -GetDir(_target.transform.position);
        
        Vector3 lastPos = transform.position;

        for (int nDir = 0; nDir != dir; nDir = GetDir(lastPos), _reactionTimeCounter += Time.deltaTime)
        {
            lastPos.x = transform.position.x;
            yield return null;
        }

        // this put a minimum of reaction time in case the player
        // input the good direction by default
        _reactionTime.Add(Mathf.Clamp(_reactionTimeCounter, SaveManager.DataInstance.GetBalance(StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_REACTION_TIME).x, SaveManager.DataInstance.GetBalance(StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_REACTION_TIME).y));
        _reactionTimeCoroutine = null;
    }

    private void CheckCorners()
    {
        var corners = GetSpriteCorners(_sprite);

        CheckTop(corners);
        CheckBot(corners);
    }

    public void MoveObject()
    {
        transform.Translate(transform.up * ((SaveManager.DataInstance.GetParameters()._PR_MoveSpeed * Time.deltaTime) * _movement.y), Space.World);
        
        float newRotation = (SaveManager.DataInstance.GetParameters()._PR_RotationSpeed * Time.deltaTime * _movement.x);
        transform.Rotate(0.0f, 0.0f, -newRotation, Space.World);
        var rotation = transform.eulerAngles;
        rotation.z = Mathf.Clamp(rotation.z, rotation.z > 150.0f ? 360.0f - _clampToRotation : 0, rotation.z > 150.0f ? 360.0f : _clampToRotation);
        transform.eulerAngles = rotation;

        CheckCorners();
    }

    private void GetAccuracy()
    {
        float distance = Vector3.Distance(transform.position, _target.transform.position);
        distance = Mathf.Clamp(distance, SaveManager.DataInstance.GetBalance(StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_ACCURACY).x, SaveManager.DataInstance.GetBalance(StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_ACCURACY).y);
        _accuracy.Add(distance);
    }

    private void StartClock()
    {
        if (_reactionTimeCoroutine != null)
        {
            StopCoroutine(_reactionTimeCoroutine);
            _reactionTimeCoroutine = null;
            _reactionTime.Add(Mathf.Clamp(_reactionTimeCounter, SaveManager.DataInstance.GetBalance(StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_REACTION_TIME).x, SaveManager.DataInstance.GetBalance(StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_REACTION_TIME).y));
        }
        _reactionTimeCoroutine = StartCoroutine(ReactionTimeClock(0.01f));
    }

    private void Update()
    {
        if (_demoObject)
            MoveObject();

        if (!CanRun())
            return;

        MoveObject();

        if (_timer >= 2.0f)
        {
            GetAccuracy();
            // MoveToRandomPosition();
            StartClock();

            _timer = 0.0f;
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }
}