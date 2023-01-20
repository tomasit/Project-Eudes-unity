using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WriteTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tmpro;

    private void WriteCount(float count)
    {

    }

    public void WriteSessionDuration()
    {
        var duration = SaveManager.DataInstance.GetParameters()._sessionDuration < 1 ? "Session Duration : " + (SaveManager.DataInstance.GetParameters()._sessionDuration * 60.0f).ToString() + " scd" : "Session Duration : " + SaveManager.DataInstance.GetParameters()._sessionDuration.ToString() + " mn";
        _tmpro.text = duration;
    }
}
