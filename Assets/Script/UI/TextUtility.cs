using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUtility : MonoBehaviour 
{
    private TMP_InputField _tmpro;

    private void Start()
    {
        _tmpro = GetComponent<TMP_InputField>();
        _tmpro.contentType = TMP_InputField.ContentType.DecimalNumber;
        _tmpro.ForceLabelUpdate();
    }

    // public void CheckText()
    // {
    //     if (_tmpro.text == null)
    //     {
    //         _tmpro.text = "0.0";
    //         return;
    //     }

    //     float value = 0.0f;
    //     if (!float.TryParse(_tmpro.text, out value))
    //         _tmpro.text = "0.0";
    // }
}