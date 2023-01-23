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
    private const float _minimumPosY = 0.07f;

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
        transform.eulerAngles = new Vector3(0, 0, (Random.Range(0, 2) == 1 ?
            Random.Range(360.0f - _clampToRotation, 355.0f) : Random.Range(5.0f, _clampToRotation)));

        float newPos = (Random.Range(0, 2) == 1) ?
            Random.Range(_playingArea.GetAreaBounds().y * _minimumPosY, _playingArea.GetAreaBounds().y * 0.35f) :
            Random.Range(_playingArea.GetAreaBounds().y * -_minimumPosY, _playingArea.GetAreaBounds().y * -0.35f);

        var position = new Vector3(
            transform.position.x, _playingArea.GetAreaPosition().y + newPos, transform.position.z);

        transform.position = position;
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
        SaveManager.DataInstance.GetDict()[StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_REACTION_TIME].Add(1 -
            GameBalance.ComputeBalance(_reactionTime, SaveManager.DataInstance.GetBalance(StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_REACTION_TIME)));
        SaveManager.DataInstance.GetDict()[StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_ACCURACY].Add(1 -
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

    private int CheckTop(Vector3[] corners, int i)
    {
        bool farestCorner = (corners[0].x > corners[1].x) ? true : false;
 
        if (farestCorner && corners[0].x > _playingArea.GetAreaPosition().x + _playingArea.GetAreaBounds().x * 0.5f)
        {
            var p = transform.position;
            p.x -= corners[0].x - (_playingArea.GetAreaPosition().x + _playingArea.GetAreaBounds().x * 0.5f);
            transform.position = p;
            i |= (1 << 1);
        }
        else if (corners[1].x > _playingArea.GetAreaPosition().x + _playingArea.GetAreaBounds().x * 0.5f)
        {
            var p = transform.position;
            p.x -= corners[1].x - (_playingArea.GetAreaPosition().x + _playingArea.GetAreaBounds().x * 0.5f);
            transform.position = p;
            i |= (1 << 2);
        }

        bool farestCornerY = (Mathf.Abs(corners[0].y) > Mathf.Abs(corners[1].y)) ? true : false;
        if (farestCornerY && corners[0].y > _playingArea.GetAreaPosition().y + _playingArea.GetAreaBounds().y * 0.5f)
        {
            var p = transform.position;
            p.y -= corners[0].y - (_playingArea.GetAreaPosition().y + _playingArea.GetAreaBounds().y * 0.5f);
            transform.position = p;
            i |= (1 << 3);
        }
        else if (corners[1].y < _playingArea.GetAreaPosition().y - _playingArea.GetAreaBounds().y * 0.5f)
        {
            var p = transform.position;
            p.y += Mathf.Abs(corners[1].y) - Mathf.Abs(_playingArea.GetAreaPosition().y - _playingArea.GetAreaBounds().y * 0.5f);
            transform.position = p;
            i |= (1 << 4);
        }

        return i;
    }

    private int CheckBot(Vector3[] corners, int i)
    {
        bool farestCorner = (corners[2].x < corners[3].x) ? true : false;

        if (farestCorner && corners[2].x < _playingArea.GetAreaPosition().x - _playingArea.GetAreaBounds().x * 0.5f)
        {
            var p = transform.position;
            p.x += Mathf.Abs(corners[2].x) - Mathf.Abs(_playingArea.GetAreaPosition().x - _playingArea.GetAreaBounds().x * 0.5f);
            transform.position = p;
            i |= (1 << 5);
        }
        else if (corners[3].x < _playingArea.GetAreaPosition().x - _playingArea.GetAreaBounds().x * 0.5f)
        {
            var p = transform.position;
            p.x += Mathf.Abs(corners[3].x) - Mathf.Abs(_playingArea.GetAreaPosition().x - _playingArea.GetAreaBounds().x * 0.5f);
            transform.position = p;
            i |= (1 << 6);
        }

        bool farestCornerY = (Mathf.Abs(corners[2].y) > Mathf.Abs(corners[3].y)) ? true : false;
        if (farestCornerY && corners[2].y < _playingArea.GetAreaPosition().y - _playingArea.GetAreaBounds().y * 0.5f)
        {
            var p = transform.position;
            p.y += Mathf.Abs(corners[2].y) - Mathf.Abs(_playingArea.GetAreaPosition().y - _playingArea.GetAreaBounds().y * 0.5f);
            transform.position = p;
            i |= (1 << 7);
        }
        else if (corners[3].y > _playingArea.GetAreaPosition().y + _playingArea.GetAreaBounds().y * 0.5f)
        {
            var p = transform.position;
            p.y -= corners[3].y - (_playingArea.GetAreaPosition().y + _playingArea.GetAreaBounds().y * 0.5f);
            transform.position = p;
            i |= (1 << 8);
        }

        return i;
    }

    private int CheckCorners()
    {
        var corners = GetSpriteCorners(_sprite);

        int i = 0;
        i = CheckTop(corners, i);
        i = CheckBot(corners, i);
        return i;
    }

    public void MoveObject()
    {
        Debug.Log("Move pitch and roll");
        var tempPosition = transform.position;
        var tempRotation = transform.eulerAngles;

        transform.Translate(Vector3.up * ((SaveManager.DataInstance.GetParameters()._PR_MoveSpeed * Time.deltaTime) * _movement.y), Space.World);
        
        Debug.Log(SaveManager.DataInstance.GetParameters()._PR_MoveSpeed);

        float newRotation = (SaveManager.DataInstance.GetParameters()._PR_RotationSpeed * Time.deltaTime * _movement.x);
        transform.Rotate(0.0f, 0.0f, -newRotation, Space.World);
        var rotation = transform.eulerAngles;
        rotation.z = Mathf.Clamp(rotation.z, rotation.z > 150.0f ? 360.0f - _clampToRotation : 0, rotation.z > 150.0f ? 360.0f : _clampToRotation);
        transform.eulerAngles = rotation;

        int flags = CheckCorners();

        if (((flags >> 3) & 1) == 1 && ((flags >> 7) & 1) == 1)
        {
            transform.position = new Vector3(transform.position.x, tempPosition.y, tempPosition.z);
            transform.eulerAngles = tempRotation;
        }
        if (((flags >> 4) & 1) == 1 && ((flags >> 8) & 1) == 1)
        {
            transform.position = new Vector3(transform.position.x, tempPosition.y, tempPosition.z);
            transform.eulerAngles = tempRotation;
        }
        if (((flags >> 1) & 1) == 1 && ((flags >> 5) & 1) == 1)
        {
            transform.position = new Vector3(tempPosition.x, transform.position.y, tempPosition.z);
            transform.eulerAngles = tempRotation;
        }
        if (((flags >> 2) & 1) == 1 && ((flags >> 6) & 1) == 1)
        {
            transform.position = new Vector3(tempPosition.x, transform.position.y, tempPosition.z);
            transform.eulerAngles = tempRotation;
        }
    }

    private int GetDir(Vector3 target)
    {
        int dir;
        if (transform.position.y > target.y)
            dir = 1;
        else if (transform.position.y < target.y)
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

        // clamp between balance values
        float r = Mathf.Clamp(_reactionTimeCounter, SaveManager.DataInstance.GetBalance(StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_REACTION_TIME).x, SaveManager.DataInstance.GetBalance(StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_REACTION_TIME).y);
        _reactionTime.Add(r);
        _reactionTimeCoroutine = null;
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
            MoveToRandomPosition();
            StartClock();

            _timer = 0.0f;
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }
}