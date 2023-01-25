using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class AssignRangedValue : MonoBehaviour
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
        if (_type == RangeParamsValues.gaz_auto_force)
        {
            _rangeSlider.SetValueWithoutNotify(SaveManager.DataInstance.GetParameters()._gazAutoForceMin, SaveManager.DataInstance.GetParameters()._gazAutoForceMax);
        }
        else if (_type == RangeParamsValues.gaz_auto_frequence)
        {
            _rangeSlider.SetValueWithoutNotify(SaveManager.DataInstance.GetParameters()._gazTimeRangeMin, SaveManager.DataInstance.GetParameters()._gazTimeRangeMax);
        }
        else if (_type == RangeParamsValues.light_true_percentage)
        {
            _rangeSlider.SetValueWithoutNotify(SaveManager.DataInstance.GetParameters()._lightTruePercentageMin, SaveManager.DataInstance.GetParameters()._lightTruePercentageMax);            
        }
        else if (_type == RangeParamsValues.light_frequence)
        {
            _rangeSlider.SetValueWithoutNotify(SaveManager.DataInstance.GetParameters()._lightFrequenceRangeMin, SaveManager.DataInstance.GetParameters()._lightFrequenceRangeMax);
        }
    }

    public void AssignValue(float low, float high)
    {
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
        else if (_type == RangeParamsValues.light_true_percentage)
        {
            SaveManager.DataInstance.GetParameters()._lightTruePercentageMin = low;
            SaveManager.DataInstance.GetParameters()._lightTruePercentageMax = high;
        }
        else if (_type == RangeParamsValues.light_frequence)
        {
            SaveManager.DataInstance.GetParameters()._lightFrequenceRangeMin = low;
            SaveManager.DataInstance.GetParameters()._lightFrequenceRangeMax = high;
        }
    }
}
