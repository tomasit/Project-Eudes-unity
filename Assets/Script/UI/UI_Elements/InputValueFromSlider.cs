using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI.Extensions;

public class InputValueFromSlider : MonoBehaviour
{
    private TMP_InputField _inputField;
    [SerializeField] private RangeSlider _rangeSlider;
    [SerializeField] private bool _isLow = false;

    private void Awake()
    {
        _inputField = GetComponent<TMP_InputField>();
        _inputField.onEndEdit.AddListener(delegate {SetSliderValue();});
    }
    
    private void SetSliderValue()
    {
        if (_inputField.text == "" || _inputField.text == "." || _inputField.text == "-")
            _inputField.text = _isLow ? _rangeSlider.LowValue.ToString("0.0") : _rangeSlider.HighValue.ToString("0.0");
        else
            _rangeSlider.gameObject.GetComponent<AssignBalanceInSlider>().SetValue(float.Parse(_inputField.text.ToString()), _isLow);
    }

    public void SetText()
    {
        _inputField.SetTextWithoutNotify(_isLow ? _rangeSlider.LowValue.ToString("0.0") : _rangeSlider.HighValue.ToString("0.0"));
    }
}
