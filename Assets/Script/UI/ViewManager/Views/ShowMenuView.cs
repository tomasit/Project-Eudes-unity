using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowMenuView : MonoBehaviour 
{
    public void ShowLast()
    {
        ViewManager.ShowLast();
    }

    public void ShowParameters()
    {
        ViewManager.Show<ParameterView>();
    }

    public void ShowBalance()
    {
        ViewManager.Show<BalanceView>();        
    }
}