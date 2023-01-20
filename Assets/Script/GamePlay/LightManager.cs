using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Security.Cryptography;

public class LightManager : ASessionObject
{
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _alightColor;
    [SerializeField] private AutoLight[] _lights;
    private List<float> _frequences;
    private List<bool> _trueLight;
    private int _sessionLightUp;
    private float _timeoutCounter = 0.0f;
    private int _currentIndex = 0;
    private bool _timeout = true;
    private AutoLight _currentLight;
    private int _colorIndex;
    private int _trueLightNb = 0;
    private int _playerResponse;
    public AutoLight test;
    

    private void Start()
    {
        _frequences = new List<float>();
        _trueLight = new List<bool>();
        
        foreach (var light in _lights)
            light.LightOff();
    }

    public override void StartSession()
    {
        _started = true;
        _timeout = true;
        _trueLightNb = 0;
        _timeoutCounter = 0.0f;
        _currentIndex = 0;
        _playerResponse = 0;
        _colorIndex = Random.Range(0, 4);
        test.LightOn(GetCombinaison());

        CalculAll();
    }

    public override void StopSession()
    {
        _started = false;
        SaveSessionData();
        _frequences.Clear();
        _trueLight.Clear();

        foreach (var light in _lights)
            light.LightOff();
    }

    public override void SaveSessionData()
    {
        SaveManager.DataInstance.GetDict()[StatistiqueGraph.StatistiqueType.LIGHT_MEMORY].Add(
            _playerResponse / _trueLightNb);
    }

    private void CalculAll()
    {
        var parameters = SaveManager.DataInstance.GetParameters();
        int i = 0;
        for (float t = 0.0f; t < parameters._sessionDuration * 60.0f;)
        {
            float timeout = Random.Range(parameters._lightFrequenceRangeMin, parameters._lightFrequenceRangeMax);
            t += timeout + parameters._alightTime;
            if (t < parameters._sessionDuration * 60.0f)
            {
                _frequences.Add(timeout);
                _trueLight.Add(false);
                i += 1;
            }
        }
        _trueLightNb = (int)Mathf.Round((float)i / 100.0f * parameters._lightTruePercentage);
        
        for (int j = 0, cond = 0; cond != _trueLightNb; j++)
        {
            if (_trueLight[j % _trueLight.Count])
                continue;
            if (Random.Range(0.0f, 1.0f) > 0.9f)
            {
                _trueLight[j % _trueLight.Count] = true;
                cond++;
            }
        }

        Debug.Log("End calcul");

        foreach(var tkt in _trueLight)
            Debug.Log("Light up : " + tkt);
    }

    public Color[] GetCombinaison()
    {
        var tmp =  new Color[4];

        tmp[0] = _colorIndex == 0 ? _alightColor : _defaultColor;
        tmp[1] = _colorIndex == 1 ? _alightColor : _defaultColor;
        tmp[2] = _colorIndex == 2 ? _alightColor : _defaultColor;
        tmp[3] = _colorIndex == 3 ? _alightColor : _defaultColor;

        return tmp;
    }

    public Color[] GetOtherCombinaison()
    {
        var exclude = new HashSet<int>() { _colorIndex };
        var range = Enumerable.Range(0, 4).Where(i => !exclude.Contains(i));

        var rand = new System.Random();
        int index = rand.Next(0, 4 - exclude.Count);
        int nIndex = range.ElementAt(index);

        var tmp =  new Color[4];

        tmp[0] = nIndex == 0 ? _alightColor : _defaultColor;
        tmp[1] = nIndex == 1 ? _alightColor : _defaultColor;
        tmp[2] = nIndex == 2 ? _alightColor : _defaultColor;
        tmp[3] = nIndex == 3 ? _alightColor : _defaultColor;

        return tmp;
    }

    private AutoLight GetRandomLight()
    {
        return _lights[Random.Range(0, _lights.Count())];
    }

    private void Update()
    {
        if (!CanRun())
            return;

        if (_currentIndex >= _frequences.Count())
            return;

        if (!_timeout)
        {
            if (_timeoutCounter > SaveManager.DataInstance.GetParameters()._alightTime)
            {
                _timeoutCounter = 0.0f;
                _timeout = true;
                _currentLight.LightOff();
            }
            else
            {
                _timeoutCounter += Time.deltaTime;
            }
        }
        else
        {
            if (_timeoutCounter > _frequences[_currentIndex])
            {
                _currentLight = GetRandomLight();
                _currentLight.LightOn(_trueLight[_currentIndex] ? GetCombinaison() : GetOtherCombinaison());
                _timeoutCounter = 0.0f;
                _currentIndex += 1;
                _timeout = false;
            }
            else
            {
                _timeoutCounter += Time.deltaTime;
            }
        }
    }
}