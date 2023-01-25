using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class AssignSliderValue : MonoBehaviour
{
    [SerializeField] private ParametrableValuesEnum _type;
    [SerializeField] private Slider _slider;

    public void Start()
    {
        _slider = GetComponent<Slider>();
        WriteDefault();
    }

    public void WriteDefault()
    {
        if (_type == ParametrableValuesEnum.gaz_player_speed)
        {
            _slider.value = SaveManager.DataInstance.GetParameters()._throttleSpeed;
        }
        else if (_type == ParametrableValuesEnum.pitch_and_roll_move_speed)
        {
            _slider.value = SaveManager.DataInstance.GetParameters()._PR_MoveSpeed;
        }
        else if (_type == ParametrableValuesEnum.pitch_and_roll_rotation_speed)
        {
            _slider.value = SaveManager.DataInstance.GetParameters()._PR_RotationSpeed;
        }
        else if (_type == ParametrableValuesEnum.pedal_speed)
        {
            _slider.value = SaveManager.DataInstance.GetParameters()._pedalSpeed;
        }
        else if (_type == ParametrableValuesEnum.light_true_percentage)
        {
            _slider.value = SaveManager.DataInstance.GetParameters()._lightTruePercentage;
        }
        else if (_type == ParametrableValuesEnum.alight_time)
        {
            _slider.value = SaveManager.DataInstance.GetParameters()._alightTime;
        }
        else if (_type == ParametrableValuesEnum.session_duration)
        {
            _slider.value = SaveManager.DataInstance.GetParameters()._sessionDuration;
        }
    }

    public void AssignValue(float value)
    {
        if (_type == ParametrableValuesEnum.gaz_player_speed)
        {
            SaveManager.DataInstance.GetParameters()._throttleSpeed = value;
        }
        else if (_type == ParametrableValuesEnum.pitch_and_roll_move_speed)
        {
            SaveManager.DataInstance.GetParameters()._PR_MoveSpeed = value;
        }
        else if (_type == ParametrableValuesEnum.pitch_and_roll_rotation_speed)
        {
            SaveManager.DataInstance.GetParameters()._PR_RotationSpeed = value;
        }
        else if (_type == ParametrableValuesEnum.pedal_speed)
        {
            SaveManager.DataInstance.GetParameters()._pedalSpeed = value;
        }
        else if (_type == ParametrableValuesEnum.light_true_percentage)
        {
            SaveManager.DataInstance.GetParameters()._lightTruePercentage = value;
        }
        else if (_type == ParametrableValuesEnum.alight_time)
        {
            SaveManager.DataInstance.GetParameters()._alightTime = value;
        }
        else if (_type == ParametrableValuesEnum.session_duration)
        {
            SaveManager.DataInstance.GetParameters()._sessionDuration = value;
        }
    }
}
