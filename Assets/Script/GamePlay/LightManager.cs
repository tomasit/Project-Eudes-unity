using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Security.Cryptography;

public class LightManager : MonoBehaviour
{
    [SerializeField] private AutoLight[] _lights;
    [SerializeField] private Color[] _colors;
    [SerializeField] private bool _startSession = false;
    private Color[] _combinaisonColor;
    private List<float> _frequences;
    private List<bool> _trueLight;
    private int _sessionLightUp;
    private float _timeoutCounter = 0.0f;
    private float _alighCounter = 0.0f;
    private int _currentIndex = 0;
    private bool _timeout = true;

    private void Start()
    {
        _frequences = new List<float>();
        _trueLight = new List<bool>();
        RestartSession();
    }

    public void RestartSession()
    {
        _timeout = true;
        _timeoutCounter = 0.0f;
        _currentIndex = 0;
        _alighCounter = 0.0f;
        _frequences.Clear();
        _trueLight.Clear();

        CalculAll();
        foreach (var light in _lights)
            light.LightOff();
    }

    private void CalculAll()
    {
        var parameters = SaveManager.DataInstance.GetParameters();
        int i = 0;
        for (float t = 0.0f; t < parameters._sessionDuration;)
        {
            float timeout = Random.Range(parameters._lightFrequenceRangeMin, parameters._lightFrequenceRangeMax);
            if (t + timeout + parameters._alightTime < parameters._sessionDuration)
            {
                _frequences.Add(timeout);
                _trueLight.Add(false);
                i += 1;
            }
            t += timeout + parameters._alightTime;
        }
        _sessionLightUp = Random.Range(Mathf.Clamp(parameters._lightUpRangeMin, 0, i), Mathf.Clamp(parameters._lightUpRangeMax, 0, i));
        // Debug.Log(_sessionLightUp);
        
        for (int j = 0, cond = 0; cond != _sessionLightUp; j++)
        {
            if (_trueLight[j % _trueLight.Count])
                continue;
            if (Random.Range(0.0f, 1.0f) > 0.9f)
            {
                _trueLight[j % _trueLight.Count] = true;
                cond++;
            }
        }

        // foreach(var tkt in _trueLight)
        //     Debug.Log(tkt);
    }

    public Color[] GetCombinaison()
    {
        System.Random seed = new System.Random();
        Color[] tmp = _colors;
        tmp = tmp.OrderBy(x => seed.Next()).ToArray();
        return tmp;
    }

    private AutoLight GetRandomLight()
    {
        return _lights[Random.Range(0, _lights.Count() - 1)];
    }

    private void Update()
    {
        if (_currentIndex >= _frequences.Count())
            _startSession = false;
        if (!_startSession)
            return;

        if (!_timeout)
        {
            if (_alighCounter > SaveManager.DataInstance.GetParameters()._alightTime)
            {
                _alighCounter = 0.0f;
                _timeout = true;
            }
        }

        if (_timeout)
        {
            if (_timeoutCounter > _frequences[_currentIndex])
            {
                _timeoutCounter = 0.0f;
                _currentIndex += 1;
                _timeout = false;
            }
        }
        
        if (_timeout)
            _timeoutCounter += Time.deltaTime;
        else
            _alighCounter += Time.deltaTime;

    }
}