using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI.Extensions;
using UnityEngine.UI;

public class BalanceView : AView 
{
    [SerializeField] private AssignBalanceInSlider[] _inputs;
    [HideInInspector] public BalanceValues _tmpBalance;
    [SerializeField] private GameObject _resetBalancePopup;
    [SerializeField] private GameObject _saveBalancePopup;
    [SerializeField] private Transform _parent;
    private GameObject _popupRef;

    public void InstantiateResetBalance()
    {
        if (_popupRef != null)
            DeletePopup();
        InteractInputField(false);

        _popupRef = Instantiate(_resetBalancePopup, _parent) as GameObject;
    }

    public void InstantiateSaveBalance()
    {
        if (_popupRef != null)
            DeletePopup();

        InteractInputField(false);

        _popupRef = Instantiate(_saveBalancePopup, _parent) as GameObject;
    }

    public void DeletePopup()
    {
        if (_popupRef != null)
            Destroy(_popupRef);
        _popupRef = null;
    }

    public override void Show()
    {
        base.Show();
        _tmpBalance = SaveManager.DataInstance.GetBalance();
        WriteDefault();
    }

    public override void Hide()
    {
        base.Hide();
    }

    public void WriteDefault()
    {
        foreach (var input in _inputs)
        {
            input.WriteDefault();
        }
    }

    public void RestoreOldParameters()
    {
        SaveManager.DataInstance.SetBalance(_tmpBalance);
    }

    public void InteractInputField(bool interact)
    {
        foreach (var input in _inputs)
        {
            input.SetInteraction(interact);
        }
    }
}