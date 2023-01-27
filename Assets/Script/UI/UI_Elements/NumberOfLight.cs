using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberOfLight : MonoBehaviour 
{
    [SerializeField] private LightManager _manager;
    private TMP_InputField _tmpro;

    private void Start()
    {
        _tmpro = GetComponent<TMP_InputField>();
        _tmpro.text = "0";
    }

    public void CheckNumber()
    {
        if (_tmpro.text == "-")
            _tmpro.text = "0";
        if (int.Parse(_tmpro.text) < 0)
            _tmpro.text = "0";

        _manager.SetPlayerNumber(int.Parse(_tmpro.text));
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