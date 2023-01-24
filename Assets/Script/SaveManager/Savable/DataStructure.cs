using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ParametrableValuesEnum
{
    gaz_frequence_x,
    gaz_frequence_y,
    gaz_force_auto_x,
    gaz_force_auto_y,
    gaz_player_speed,
    pedal_speed,
    pitch_and_roll_move_speed,
    pitch_and_roll_rotation_speed,
    light_true_percentage,
    light_frequence_x,
    light_frequence_y,
    alight_time,
    session_duration,
}

[System.Serializable]
public class ParametrableValues
{
    public ParametrableValues() {}

    public ParametrableValues(ParametrableValues other)
    {
        _gazTimeRangeMin = other._gazTimeRangeMin;
        _gazTimeRangeMax = other._gazTimeRangeMax;
        _gazAutoForceMin = other._gazAutoForceMin;
        _gazAutoForceMax = other._gazAutoForceMax;
        _throttleSpeed = other._throttleSpeed;
        _pedalSpeed = other._pedalSpeed;
        _PR_MoveSpeed = other._PR_MoveSpeed;
        _PR_RotationSpeed = other._PR_RotationSpeed;
        _lightTruePercentage = other._lightTruePercentage;
        _lightFrequenceRangeMin = other._lightFrequenceRangeMin;
        _lightFrequenceRangeMax = other._lightFrequenceRangeMax;
        _alightTime = other._alightTime;
        _sessionDuration = other._sessionDuration;
    }

    public float _gazTimeRangeMin = 0.5f;
    public float _gazTimeRangeMax = 1.4f;
    public float _gazAutoForceMin = 1.0f;
    public float _gazAutoForceMax = 1.5f;
    public float _throttleSpeed = 100f;
    public float _pedalSpeed = 20.0f;
    public float _PR_MoveSpeed = 20.0f;
    public float _PR_RotationSpeed = 150.0f;
    public float _lightTruePercentage = 40.0f;
    public float _lightFrequenceRangeMin = 0.5f;
    public float _lightFrequenceRangeMax = 2.0f;
    public float _alightTime = 1.0f;
    public float _sessionDuration = 0.25f;
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

        return (distanceMedium - bounds.x) / (bounds.y - bounds.x);
    }
    
    public static float ComputeBalance(List<int> data, Vector2 bounds)
    {
        float distanceMedium = 0.0f;
        
        foreach (var d in data)
        {
            distanceMedium += (float)d;
        }
        distanceMedium /= data.Count;

        return (distanceMedium - bounds.x) / (bounds.y - bounds.x);
    }

    public static float _pedalsDistance = 1.0f;
    public static float _pedalsReactionTime = 1.0f;
    public static float _pitchAndRollDistance = 1.0f;
    public static float _pitchAndRollReactionTime = 1.0f;
    public static float _gazAccuracy = 1.0f;
}