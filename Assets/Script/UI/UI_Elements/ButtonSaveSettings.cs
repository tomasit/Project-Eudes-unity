using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSaveSettings : MonoBehaviour
{
    [SerializeField] private GameObject _parent;
    
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(delegate { SaveSettings(); });
    }

    public void SaveSettings()
    {
        SaveManager.DataInstance.SaveParameters();
        DestroyThis();
        ViewManager.Show<MainMenuView>();
    }

    public void DontSaveSettings()
    {
        FindObjectOfType<ParameterView>(true).RestoreOldParameters();
        DestroyThis();
        ViewManager.Show<MainMenuView>();
    }

    public void DestroyThis()
    {
        FindObjectOfType<AudioSourceMaster>().GetAudioSource(AudioSourceMaster.AudioName.click_on_button).Play();
        FindObjectOfType<ParameterView>(true).InteractInputField(true);
        Destroy(_parent);
    }

}
