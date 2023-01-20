using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ButtonResetSettings : MonoBehaviour
{
    [SerializeField] private GameObject _parent;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(delegate { ResetSettings(); });
    }

    public void ResetSettings()
    {
        SaveManager.DataInstance.ResetParameters();
        FindObjectOfType<ParameterView>().WriteDefault();
        DestroyThis();
    }

    public void DestroyThis()
    {
        FindObjectOfType<AudioSourceMaster>().GetAudioSource(AudioSourceMaster.AudioName.click_on_button).Play();
        FindObjectOfType<ParameterView>().InteractInputField(true);
        Destroy(_parent);
    }
}
