using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI.Extensions;

public class ValueFromSlider : MonoBehaviour
{
    private TextMeshProUGUI _text;
    [SerializeField] private Slider _slider;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _text.text = _slider.value.ToString("0.0");
    }
}
