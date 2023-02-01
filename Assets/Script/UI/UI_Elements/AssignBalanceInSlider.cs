using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class AssignBalanceInSlider : MonoBehaviour
{
    [SerializeField] private StatistiqueGraph.StatistiqueType _type;
    [SerializeField] private RangeSlider _slider;
    [SerializeField] private InputValueFromSlider _lowInput;
    [SerializeField] private InputValueFromSlider _highInput;

    public void Start()
    {
        _slider = GetComponent<RangeSlider>();
        WriteDefault();
    }

    public void WriteDefault()
    {
        if (_type == StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_ACCURACY)
        {
            _slider.SetValueWithoutNotify(SaveManager.DataInstance.GetBalance(StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_ACCURACY).x, SaveManager.DataInstance.GetBalance(StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_ACCURACY).y);
        }
        else if (_type == StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_REACTION_TIME)
        {
            _slider.SetValueWithoutNotify(SaveManager.DataInstance.GetBalance(StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_REACTION_TIME).x, SaveManager.DataInstance.GetBalance(StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_REACTION_TIME).y);
        }
        else if (_type == StatistiqueGraph.StatistiqueType.GAZ_ACCURACY)
        {
            _slider.SetValueWithoutNotify(SaveManager.DataInstance.GetBalance(StatistiqueGraph.StatistiqueType.GAZ_ACCURACY).x, SaveManager.DataInstance.GetBalance(StatistiqueGraph.StatistiqueType.GAZ_ACCURACY).y);
        }
        else if (_type == StatistiqueGraph.StatistiqueType.RUDDER_ACCURACY)
        {
            _slider.SetValueWithoutNotify(SaveManager.DataInstance.GetBalance(StatistiqueGraph.StatistiqueType.RUDDER_ACCURACY).x, SaveManager.DataInstance.GetBalance(StatistiqueGraph.StatistiqueType.RUDDER_ACCURACY).y);
        }
        else if (_type == StatistiqueGraph.StatistiqueType.RUDDER_REACTION_TIME)
        {
            _slider.SetValueWithoutNotify(SaveManager.DataInstance.GetBalance(StatistiqueGraph.StatistiqueType.RUDDER_REACTION_TIME).x, SaveManager.DataInstance.GetBalance(StatistiqueGraph.StatistiqueType.RUDDER_REACTION_TIME).y);
        }
        else if (_type == StatistiqueGraph.StatistiqueType.NUMBER_MEMORY)
        {
            _slider.SetValueWithoutNotify(SaveManager.DataInstance.GetBalance(StatistiqueGraph.StatistiqueType.NUMBER_MEMORY).x, SaveManager.DataInstance.GetBalance(StatistiqueGraph.StatistiqueType.NUMBER_MEMORY).y);
        }
        else if (_type == StatistiqueGraph.StatistiqueType.LIGHT_MEMORY)
        {
            _slider.SetValueWithoutNotify(SaveManager.DataInstance.GetBalance(StatistiqueGraph.StatistiqueType.LIGHT_MEMORY).x, SaveManager.DataInstance.GetBalance(StatistiqueGraph.StatistiqueType.LIGHT_MEMORY).y);
        }
        _lowInput.SetText();
        _highInput.SetText();
    }

    public void SetValue(float value, bool isLow)
    {
        if (isLow)
            _slider.LowValue = value;
        else
            _slider.HighValue = value;
    
        _lowInput.SetText();
        _highInput.SetText();
    }

    public void AssignValue(float low, float high)
    {
        if (_type == StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_ACCURACY)
        {
            SaveManager.DataInstance.SetBalance(StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_ACCURACY, low, high);
        }
        else if (_type == StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_REACTION_TIME)
        {
            SaveManager.DataInstance.SetBalance(StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_REACTION_TIME, low, high);
        }
        else if (_type == StatistiqueGraph.StatistiqueType.GAZ_ACCURACY)
        {
            SaveManager.DataInstance.SetBalance(StatistiqueGraph.StatistiqueType.GAZ_ACCURACY, low, high);
        }
        else if (_type == StatistiqueGraph.StatistiqueType.RUDDER_ACCURACY)
        {
            SaveManager.DataInstance.SetBalance(StatistiqueGraph.StatistiqueType.RUDDER_ACCURACY, low, high);
        }
        else if (_type == StatistiqueGraph.StatistiqueType.RUDDER_REACTION_TIME)
        {
            SaveManager.DataInstance.SetBalance(StatistiqueGraph.StatistiqueType.RUDDER_REACTION_TIME, low, high);
        }
        else if (_type == StatistiqueGraph.StatistiqueType.NUMBER_MEMORY)
        {
            SaveManager.DataInstance.SetBalance(StatistiqueGraph.StatistiqueType.NUMBER_MEMORY, low, high);
        }
        else if (_type == StatistiqueGraph.StatistiqueType.LIGHT_MEMORY)
        {
            SaveManager.DataInstance.SetBalance(StatistiqueGraph.StatistiqueType.LIGHT_MEMORY, low, high);
        }
    }
}
