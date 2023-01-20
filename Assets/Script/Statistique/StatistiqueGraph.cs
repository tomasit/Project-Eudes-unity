using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class StatistiqueGraph : MonoBehaviour
{
    [System.Serializable]
    public enum StatistiqueType
    {
        PITCH_AND_ROLL_ACCURACY,
        PITCH_AND_ROLL_REACTION_TIME,
        RUDDER_ACCURACY,
        RUDDER_REACTION_TIME,
        GAZ_ACCURACY,
        NUMBER_MEMORY,
        LIGHT_MEMORY
    }

    [SerializeField] private float _minOffset;
    [SerializeField] private int _nbShownSession;
    [SerializeField] private GameObject _valueLayout;
    [SerializeField] private GameObject _sessionButton;
    [SerializeField] private GameObject _parent;
    [SerializeField] private GameObject _sessionParent;
    [SerializeField] private float _abscisseSize = 1200.0f;
    [SerializeField] private float _ordonneSize = 800.0f;
    [HideInInspector] public GameObject _scrollbar;
    private List<GameObject> _sessionButtons;
    private Dictionary<StatistiqueType, List<GameObject>> _valueDict = new Dictionary<StatistiqueType, List<GameObject>>();
    [SerializeField] private LineRenderer[] _lineList;
    private Dictionary<StatistiqueType, LineRenderer> _lineDict = new Dictionary<StatistiqueType, LineRenderer>();
    private Vector3 _originPosition;
    private Vector3 _sessionPosition;
    private int _startIndex;
    [HideInInspector] public bool _isInitialize = false;


    private void Start()
    {
        if (!_isInitialize)
            Initialize();
    }

    public void Initialize()
    {
        _originPosition = _parent.transform.localPosition;
        _sessionPosition = _sessionParent.transform.localPosition;
        _sessionButtons = new List<GameObject>();

        _lineDict.Add(StatistiqueType.PITCH_AND_ROLL_ACCURACY, _lineList[0]);
        _lineDict.Add(StatistiqueType.PITCH_AND_ROLL_REACTION_TIME, _lineList[1]);
        _lineDict.Add(StatistiqueType.RUDDER_ACCURACY, _lineList[2]);
        _lineDict.Add(StatistiqueType.RUDDER_REACTION_TIME, _lineList[3]);
        _lineDict.Add(StatistiqueType.GAZ_ACCURACY, _lineList[4]);
        _lineDict.Add(StatistiqueType.NUMBER_MEMORY, _lineList[5]);
        _lineDict.Add(StatistiqueType.LIGHT_MEMORY, _lineList[6]);
        _isInitialize = true;
    }

    public void ClearGraph()
    {
        foreach (var s in _valueDict)
        {
            foreach (var k in s.Value)
                Destroy(k);
            s.Value.Clear();
        }
        _valueDict.Clear();

        foreach (var s in _sessionButtons)
        {
            Destroy(s);
        }
        _sessionButtons.Clear();
    }

    public void LoadFromSave()
    {
        _startIndex = Mathf.Clamp(SaveManager.DataInstance.GetNumberSession() - _nbShownSession, 0, SaveManager.DataInstance.GetNumberSession());

        InitializeLineRenderer(_startIndex);
        CreateGraph(_startIndex);
    }

    private float GetRatioFromStartIndex(int startIndex)
    {
        return _abscisseSize / (_minOffset * SaveManager.DataInstance.GetNumberSession() - startIndex);
    }

    private void InitializeLineRenderer(int startIndex)
    {
        foreach (var line in _lineDict)
        {
            if (startIndex == 0)
            {
                line.Value.positionCount = SaveManager.DataInstance.GetNumberSession() + 1;
            }
            else
            {
                line.Value.positionCount = _nbShownSession + 1;
            }
        }
    }
    
    private float SetupScrollbar(int startIndex)
    {
        if (GetRatioFromStartIndex(startIndex) > 1.0f)
        {
            _scrollbar.SetActive(false);
            return 0.0f;
        }
        else
        {
            _scrollbar.SetActive(true);
            _scrollbar.GetComponent<Scrollbar>().size = GetRatioFromStartIndex(startIndex);
            return 1.0f;
        }
    }

    private void CreateValuePoint(float percentage, float startPosition, StatistiqueType type)
    {
        GameObject stat = (GameObject)Instantiate(_valueLayout, _parent.transform) as GameObject;
        if (!_valueDict.ContainsKey(type))
            _valueDict.Add(type, new List<GameObject>());
        _valueDict[type].Add(stat);
        stat.transform.localPosition = new Vector3(startPosition, _ordonneSize * percentage, 0);
    }

    private void CreateGraph(int startIndex)
    {
        float offset = SetupScrollbar(startIndex);
        float tmpPos = 0.0f;

        for (int i = startIndex; i < SaveManager.DataInstance.GetNumberSession(); ++i)
        {
            tmpPos += _minOffset;
            CreateButton(tmpPos, i);
        }

        foreach (var s in SaveManager.DataInstance.GetDict())
        {
            tmpPos = 0.0f;
            for (int i = startIndex; i < s.Value.Count; ++i)
            {
                tmpPos += _minOffset;
                CreateValuePoint(s.Value[i], tmpPos, s.Key);
            }
        }
        OffsetPosition(offset);
    }

    public void OffsetPosition(float offset)
    {
        float npos = _minOffset * (SaveManager.DataInstance.GetNumberSession() - _startIndex) - _abscisseSize;
        npos *= offset;
        _parent.transform.localPosition = new Vector3(_originPosition.x - npos, _originPosition.y, _originPosition.z);
        _sessionParent.transform.localPosition = new Vector3(_sessionPosition.x - npos, _sessionPosition.y, _sessionPosition.z);
        RedrawLines();
    }

    private void RedrawLines()
    {
        if (SaveManager.DataInstance.GetNumberSession() == 0)
            return;

        int index;

        foreach (var line in _lineDict)
        {
            if (_startIndex == 0)
                line.Value.SetPosition(0, _parent.transform.position);
            else
                line.Value.SetPosition(0, new Vector3(_parent.transform.position.x, 
                _parent.transform.TransformPoint(new Vector3(
                    0, _ordonneSize * SaveManager.DataInstance.GetDict()[line.Key][_startIndex - 1], 0)).y, 0));
        }

        foreach (var line in _lineDict)
        {
            index = 1;
            if (!_valueDict.ContainsKey(line.Key))
            {
                line.Value.SetPosition(1, _parent.transform.position);
                continue;
            }
            foreach (var v in _valueDict[line.Key])
            {
                line.Value.SetPosition(index, v.transform.position);
                index++;
            }
        }
    }

    private void CreateButton(float startPosition, int index)
    {
        GameObject session = (GameObject)Instantiate(_sessionButton, _sessionParent.transform);
        session.transform.localPosition = new Vector3(startPosition, _sessionParent.transform.position.y, 0);
        session.GetComponent<SessionButton>().AddDelegate(index);
        _sessionButtons.Add(session);
    }

    public void EnableType(StatistiqueType type, bool enable)
    {
        if (!_valueDict.ContainsKey(type))
            return;
        foreach (var t in _valueDict[type])
        {
            t.SetActive(enable);
        }

        _lineDict[type].enabled = enable;
    }
}
