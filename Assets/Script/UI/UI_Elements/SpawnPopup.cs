using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPopup : MonoBehaviour
{
    [SerializeField] private GameObject _resetSettingsPopup;
    [SerializeField] private GameObject _saveSettingsPopup;
    [SerializeField] private Transform _parent;
    private GameObject _popupRef;

    public void InstantiateResetSettings()
    {
        if (_popupRef != null)
            DeletePopup();
        FindObjectOfType<ParameterView>().InteractInputField(false);

        _popupRef = Instantiate(_resetSettingsPopup, _parent) as GameObject;
    }

    public void InstantiateSaveSettings()
    {
        if (_popupRef != null)
            DeletePopup();

        FindObjectOfType<ParameterView>().InteractInputField(false);

        _popupRef = Instantiate(_saveSettingsPopup, _parent) as GameObject;
    }

    public void DeletePopup()
    {
        if (_popupRef != null)
            Destroy(_popupRef);
        _popupRef = null;
    }
}
