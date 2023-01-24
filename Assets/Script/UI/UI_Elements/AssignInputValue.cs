using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class AssignInputValue : MonoBehaviour
{
    public enum RangeParamsValues {
        gaz_auto_force,
        gaz_auto_frequence,
        light_true_percentage,
        light_frequence
    }

    [SerializeField] private RangeParamsValues _type;
    [SerializeField] private RangeSlider _rangeSlider;

    public void Start()
    {
        _rangeSlider = GetComponent<RangeSlider>();
        WriteDefault();
    }

    public void WriteDefault()
    {
        Debug.Log("Write default : x -> " + SaveManager.DataInstance.GetParameters()._gazTimeRangeMin + " y -> " + SaveManager.DataInstance.GetParameters()._gazTimeRangeMax);
        if (_type == RangeParamsValues.gaz_auto_force)
        {
            _rangeSlider.SetValueWithoutNotify(SaveManager.DataInstance.GetParameters()._gazAutoForceMin, SaveManager.DataInstance.GetParameters()._gazAutoForceMax);
        }
        else if (_type == RangeParamsValues.gaz_auto_frequence)
        {
            _rangeSlider.SetValueWithoutNotify(SaveManager.DataInstance.GetParameters()._gazTimeRangeMin, SaveManager.DataInstance.GetParameters()._gazTimeRangeMax);
        }
        else if (_type == RangeParamsValues.light_frequence)
        {
            _rangeSlider.SetValueWithoutNotify(SaveManager.DataInstance.GetParameters()._lightFrequenceRangeMin, SaveManager.DataInstance.GetParameters()._lightFrequenceRangeMax);
        }
        // else if (_valueName == ParametrableValuesEnum.gaz_player_speed)
        //     GetComponent<TMP_InputField>().text = SaveManager.DataInstance.GetParameters()._throttleSpeed;
        // else if (_valueName == ParametrableValuesEnum.pedal_speed)
        //     GetComponent<TMP_InputField>().text = SaveManager.DataInstance.GetParameters()._pedalSpeed;
        // else if (_valueName == ParametrableValuesEnum.pitch_and_roll_move_speed)
        //     GetComponent<TMP_InputField>().text = SaveManager.DataInstance.GetParameters()._PR_MoveSpeed;
        // else if (_valueName == ParametrableValuesEnum.pitch_and_roll_rotation_speed)
        //     GetComponent<TMP_InputField>().text = SaveManager.DataInstance.GetParameters()._PR_RotationSpeed;

        // todo : do it 
        // else if (_valueName == ParametrableValuesEnum.light_true_percentage)
        //     GetComponent<TMP_InputField>().text = SaveManager.DataInstance.GetParameters()._lightTruePercentage;
        // else if (_valueName == ParametrableValuesEnum.alight_time)
        //     GetComponent<TMP_InputField>().text = SaveManager.DataInstance.GetParameters()._alightTime;
        // else if (_valueName == ParametrableValuesEnum.session_duration)
        //     GetComponent<TMP_InputField>().text = SaveManager.DataInstance.GetParameters()._sessionDuration.ToString();
    }

    public void AssignValue(float low, float high)
    {
        // Debug.Log("Low : " + low + " High : " + high);

        if (_type == RangeParamsValues.gaz_auto_force)
        {
            SaveManager.DataInstance.GetParameters()._gazAutoForceMin = low;
            SaveManager.DataInstance.GetParameters()._gazAutoForceMax = high;
        }
        else if (_type == RangeParamsValues.gaz_auto_frequence)
        {
            SaveManager.DataInstance.GetParameters()._gazTimeRangeMin = low;
            SaveManager.DataInstance.GetParameters()._gazTimeRangeMax = high;
        }
        else if (_type == RangeParamsValues.light_frequence)
        {
            SaveManager.DataInstance.GetParameters()._lightFrequenceRangeMin = low;
            SaveManager.DataInstance.GetParameters()._lightFrequenceRangeMax = high;
        }
    }
}
