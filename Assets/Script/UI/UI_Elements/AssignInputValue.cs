using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AssignInputValue : MonoBehaviour
{
    [SerializeField] private ParametrableValuesEnum _valueName;
    private ParameterView _manager;
    public void Start()
    {
        _manager = FindObjectOfType<ParameterView>(true);
        var inputField = GetComponent<TMP_InputField>();
        inputField.contentType = TMP_InputField.ContentType.DecimalNumber;
        inputField.ForceLabelUpdate();
        GetComponent<TMP_InputField>().onValueChanged.AddListener(CheckEmpty);
        WriteDefault();
    }

    public void CheckEmpty(string value)
    {
        if (value == "")
            GetComponent<TMP_InputField>().text = "0";
        if (value == ".")
            GetComponent<TMP_InputField>().text = "0";
        if (value == "-")
            GetComponent<TMP_InputField>().text = "0";
        AssignValue();
    }

    public void WriteDefault()
    {
        if (_valueName == ParametrableValuesEnum.gaz_force_auto_x)
            GetComponent<TMP_InputField>().text = SaveManager.DataInstance.GetParameters()._gazAutoForceMin.ToString();
        else if (_valueName == ParametrableValuesEnum.gaz_force_auto_y)
            GetComponent<TMP_InputField>().text = SaveManager.DataInstance.GetParameters()._gazAutoForceMax.ToString();
        else if (_valueName == ParametrableValuesEnum.gaz_frequence_x)
            GetComponent<TMP_InputField>().text = SaveManager.DataInstance.GetParameters()._gazTimeRangeMin.ToString();
        else if (_valueName == ParametrableValuesEnum.gaz_frequence_y)
            GetComponent<TMP_InputField>().text = SaveManager.DataInstance.GetParameters()._gazTimeRangeMax.ToString();
        else if (_valueName == ParametrableValuesEnum.gaz_player_speed)
            GetComponent<TMP_InputField>().text = SaveManager.DataInstance.GetParameters()._throttleSpeed.ToString();
        else if (_valueName == ParametrableValuesEnum.pedal_speed)
            GetComponent<TMP_InputField>().text = SaveManager.DataInstance.GetParameters()._pedalSpeed.ToString();
        else if (_valueName == ParametrableValuesEnum.pitch_and_roll_move_speed)
            GetComponent<TMP_InputField>().text = SaveManager.DataInstance.GetParameters()._PR_MoveSpeed.ToString();
        else if (_valueName == ParametrableValuesEnum.pitch_and_roll_rotation_speed)
            GetComponent<TMP_InputField>().text = SaveManager.DataInstance.GetParameters()._PR_RotationSpeed.ToString();
        else if (_valueName == ParametrableValuesEnum.light_true_percentage)
            GetComponent<TMP_InputField>().text = SaveManager.DataInstance.GetParameters()._lightTruePercentage.ToString();
        else if (_valueName == ParametrableValuesEnum.light_frequence_x)
            GetComponent<TMP_InputField>().text = SaveManager.DataInstance.GetParameters()._lightFrequenceRangeMin.ToString();
        else if (_valueName == ParametrableValuesEnum.light_frequence_y)
            GetComponent<TMP_InputField>().text = SaveManager.DataInstance.GetParameters()._lightFrequenceRangeMax.ToString();
        else if (_valueName == ParametrableValuesEnum.alight_time)
            GetComponent<TMP_InputField>().text = SaveManager.DataInstance.GetParameters()._alightTime.ToString();
        else if (_valueName == ParametrableValuesEnum.session_duration)
            GetComponent<TMP_InputField>().text = SaveManager.DataInstance.GetParameters()._sessionDuration.ToString();
    }

    public void AssignValue()
    {
        if (_valueName == ParametrableValuesEnum.gaz_force_auto_x)
            SaveManager.DataInstance.GetParameters()._gazAutoForceMin = float.Parse(GetComponent<TMP_InputField>().text);
        else if (_valueName == ParametrableValuesEnum.gaz_force_auto_y)
            SaveManager.DataInstance.GetParameters()._gazAutoForceMax = float.Parse(GetComponent<TMP_InputField>().text);
        else if (_valueName == ParametrableValuesEnum.gaz_frequence_x)
            SaveManager.DataInstance.GetParameters()._gazTimeRangeMin = float.Parse(GetComponent<TMP_InputField>().text);
        else if (_valueName == ParametrableValuesEnum.gaz_frequence_y)
            SaveManager.DataInstance.GetParameters()._gazTimeRangeMax = float.Parse(GetComponent<TMP_InputField>().text);
        else if (_valueName == ParametrableValuesEnum.gaz_player_speed)
            SaveManager.DataInstance.GetParameters()._throttleSpeed = float.Parse(GetComponent<TMP_InputField>().text);
        else if (_valueName == ParametrableValuesEnum.pedal_speed)
            SaveManager.DataInstance.GetParameters()._pedalSpeed = float.Parse(GetComponent<TMP_InputField>().text);
        else if (_valueName == ParametrableValuesEnum.pitch_and_roll_move_speed)
            SaveManager.DataInstance.GetParameters()._PR_MoveSpeed = float.Parse(GetComponent<TMP_InputField>().text);
        else if (_valueName == ParametrableValuesEnum.pitch_and_roll_rotation_speed)
            SaveManager.DataInstance.GetParameters()._PR_RotationSpeed = float.Parse(GetComponent<TMP_InputField>().text);
        else if (_valueName == ParametrableValuesEnum.light_true_percentage)
            SaveManager.DataInstance.GetParameters()._lightTruePercentage = float.Parse(GetComponent<TMP_InputField>().text);
        else if (_valueName == ParametrableValuesEnum.light_frequence_x)
            SaveManager.DataInstance.GetParameters()._lightFrequenceRangeMin = float.Parse(GetComponent<TMP_InputField>().text);
        else if (_valueName == ParametrableValuesEnum.light_frequence_y)
            SaveManager.DataInstance.GetParameters()._lightFrequenceRangeMax = float.Parse(GetComponent<TMP_InputField>().text);
        else if (_valueName == ParametrableValuesEnum.alight_time)
            SaveManager.DataInstance.GetParameters()._alightTime = float.Parse(GetComponent<TMP_InputField>().text);
        else if (_valueName == ParametrableValuesEnum.session_duration)
            SaveManager.DataInstance.GetParameters()._sessionDuration = float.Parse(GetComponent<TMP_InputField>().text);
    }
}
