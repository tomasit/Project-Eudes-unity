using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSaveBalance : MonoBehaviour
{
    [SerializeField] private GameObject _parent;
    
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(delegate { SaveBalance(); });
    }

    public void SaveBalance()
    {
        SaveManager.DataInstance.SaveBalance();
        DestroyThis();
        ViewManager.Show<ParameterView>();
    }

    public void DontSaveBalance()
    {
        FindObjectOfType<BalanceView>(true).RestoreOldParameters();
        DestroyThis();
        ViewManager.Show<ParameterView>();
    }

    public void DestroyThis()
    {
        FindObjectOfType<AudioSourceMaster>().GetAudioSource(AudioSourceMaster.AudioName.click_on_button).Play();
        FindObjectOfType<BalanceView>(true).InteractInputField(true);
        Destroy(_parent);
    }

}
