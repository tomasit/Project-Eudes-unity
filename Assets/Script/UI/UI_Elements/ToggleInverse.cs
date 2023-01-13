using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleInverse : MonoBehaviour
{
    [SerializeField] private Toggle[] _toggles;

    private void Start() {
        GetComponent<Toggle>().onValueChanged.AddListener(delegate { onToggleExec(); });
    }

    private void onToggleExec() {
        bool isOn = GetComponent<Toggle>().isOn;
        foreach (var tog in _toggles)
        {
            tog.isOn = !isOn;
            tog.enabled = !isOn;
        }
    }

    public void Inverse(bool inverse)
    {
        GetComponent<Toggle>().isOn = inverse;
    }
}
