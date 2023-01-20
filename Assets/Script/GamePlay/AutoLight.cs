using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLight : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _lightTR;
    [SerializeField] private SpriteRenderer _lightTL;
    [SerializeField] private SpriteRenderer _lightBR;
    [SerializeField] private SpriteRenderer _lightBL;
    [SerializeField] private Color _defaultColor;

    public void LightOn(Color[] colors)
    {
        _lightTR.color = colors[0];
        _lightTL.color = colors[1];
        _lightBR.color = colors[2];
        _lightBL.color = colors[3];
    }

    public void LightOff()
    {
        _lightTR.color = _defaultColor;
        _lightTL.color = _defaultColor;
        _lightBR.color = _defaultColor;
        _lightBL.color = _defaultColor;
    }
}