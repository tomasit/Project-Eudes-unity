using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI.Extensions;

public class ValueFromRangeSlider : MonoBehaviour
{
    private TextMeshProUGUI _text;
    [SerializeField] private RangeSlider _rangeSlider;
    [SerializeField] private bool _isLow = false;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _text.text = _isLow ? _rangeSlider.LowValue.ToString("0.0") : _rangeSlider.HighValue.ToString("0.0");
    }
}
