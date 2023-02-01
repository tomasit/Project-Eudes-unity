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

    public void SetInteraction(bool interact)
    {
        _slider.interactable = interact;
        _lowInput.GetComponent<TMP_InputField>().interactable = interact;
        _highInput.GetComponent<TMP_InputField>().interactable = interact;
    }

    public void WriteDefault()
    {
        if (_type == StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_ACCURACY)
        {
            _slider.SetValueWithoutNotify(SaveManager.DataInstance.GetBalance()._pitchAndRollAccuracyMin, SaveManager.DataInstance.GetBalance()._pitchAndRollAccuracyMax);
        }
        else if (_type == StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_REACTION_TIME)
        {
            _slider.SetValueWithoutNotify(SaveManager.DataInstance.GetBalance()._pitchAndRollReactionTimeMin, SaveManager.DataInstance.GetBalance()._pitchAndRollReactionTimeMax);
        }
        else if (_type == StatistiqueGraph.StatistiqueType.RUDDER_ACCURACY)
        {
            _slider.SetValueWithoutNotify(SaveManager.DataInstance.GetBalance()._rubberAccuracyMin, SaveManager.DataInstance.GetBalance()._rubberAccuracyMax);
        }
        else if (_type == StatistiqueGraph.StatistiqueType.RUDDER_REACTION_TIME)
        {
            _slider.SetValueWithoutNotify(SaveManager.DataInstance.GetBalance()._rubberReactionTimeMin, SaveManager.DataInstance.GetBalance()._rubberReactionTimeMax);
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
            SaveManager.DataInstance.GetBalance()._pitchAndRollAccuracyMin = low;
            SaveManager.DataInstance.GetBalance()._pitchAndRollAccuracyMax = high;
        }
        else if (_type == StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_REACTION_TIME)
        {
            SaveManager.DataInstance.GetBalance()._pitchAndRollReactionTimeMin = low;
            SaveManager.DataInstance.GetBalance()._pitchAndRollReactionTimeMax = high;
        }
        else if (_type == StatistiqueGraph.StatistiqueType.RUDDER_ACCURACY)
        {
            SaveManager.DataInstance.GetBalance()._rubberAccuracyMin = low;
            SaveManager.DataInstance.GetBalance()._rubberAccuracyMax = high;
        }
        else if (_type == StatistiqueGraph.StatistiqueType.RUDDER_REACTION_TIME)
        {
            SaveManager.DataInstance.GetBalance()._rubberReactionTimeMin = low;
            SaveManager.DataInstance.GetBalance()._rubberReactionTimeMax = high;
        }
    }
}
