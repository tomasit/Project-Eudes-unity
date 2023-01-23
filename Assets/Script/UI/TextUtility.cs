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
        _tmpro.text = "0";
    }

    public void CheckEmpty()
    {
        if (_tmpro.text == "")
            GetComponent<TMP_InputField>().text = "0";
        if (_tmpro.text == ".")
            GetComponent<TMP_InputField>().text = "0";
        if (_tmpro.text == "-")
            GetComponent<TMP_InputField>().text = "0";
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