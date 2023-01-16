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
    public float _throttleSpeed = 2.0f;
    public float _pedalSpeed = 20.0f;
    public float _PR_MoveSpeed = 20.0f;
    public float _PR_RotationSpeed = 150.0f;
    public int _lightUpRangeMin = 0;
    public int _lightUpRangeMax = 12;
    public float _lightFrequenceRangeMin = 0.0f;
    public float _lightFrequenceRangeMax = 10.0f;
    public float _alightTime = 2.0f;
    public float _sessionDuration = 240.0f;
}

// if you change this structure, all the balance off the game will change.
// this mean that graphic won't be exact cause he does not care about the balancing
public class GameBalance {
    public static float ComputeBalance(List<float> data, Vector2 bounds)
    {
        float distanceMedium = 0.0f;
        
        foreach (var d in data)
        {
            distanceMedium += d;
        }
        distanceMedium /= data.Count;

        return (distanceMedium - bounds.x) / (bounds.y - bounds.x) * 100.0f;
    }
    
    public static float ComputeBalance(List<int> data, Vector2 bounds)
    {
        float distanceMedium = 0.0f;
        
        foreach (var d in data)
        {
            distanceMedium += (float)d;
        }
        distanceMedium /= data.Count;

        return (distanceMedium - bounds.x) / (bounds.y - bounds.x) * 100.0f;
    }

    public static float _pedalsDistance = 1.0f;
    public static float _pedalsReactionTime = 1.0f;
    public static float _pitchAndRollDistance = 1.0f;
    public static float _pitchAndRollReactionTime = 1.0f;
    public static float _gazAccuracy = 1.0f;
}