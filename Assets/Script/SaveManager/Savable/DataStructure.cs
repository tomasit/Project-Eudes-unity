using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// J = joystick value (blue things)
// P = Pedals value (red things)
// T = Throttle value (green things)
// N = number value
// L = light value
[System.Serializable]
public class ParametrableValues
{
    public float _gazTimeRangeMin = 0.2f;
    public float _gazTimeRangeMax = 0.7f;
    public float _gazAutoForceMin = 2.0f;
    public float _gazAutoForceMax = 3.0f;
    public float _pedalSpeed = 20.0f;
    public float _pitchAndRollRotationMin = -50.0f;
    public float _pitchAndRollRotationMax = 50.0f;
    public float _pitchAndRollSpeed = 20.0f;
    public int _lightUpRangeMin = 0;
    public int _lightUpRangeMax = 12;
    public float _lightFrequenceRangeMin = 0.0f;
    public float _lightFrequenceRangeMax = 10.0f;
    public float _alightTime = 2.0f;
    public float _sessionDuration = 240.0f;
}