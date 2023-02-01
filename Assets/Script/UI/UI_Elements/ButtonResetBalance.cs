using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ButtonResetBalance : MonoBehaviour
{
    [SerializeField] private GameObject _parent;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(delegate { ResetBalance(); });
    }

    public void ResetBalance()
    {
        SaveManager.DataInstance.ResetBalance();
        FindObjectOfType<BalanceView>().WriteDefault();
        DestroyThis();
    }

    public void DestroyThis()
    {
        FindObjectOfType<AudioSourceMaster>().GetAudioSource(AudioSourceMaster.AudioName.click_on_button).Play();
        FindObjectOfType<BalanceView>().InteractInputField(true);
        Destroy(_parent);
    }
}
