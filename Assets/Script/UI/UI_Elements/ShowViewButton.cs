using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowViewButton : MonoBehaviour
{
    public enum ViewName {
        GRAPHIC,
        SETTINGS,
        MAIN_MENU,
        PAUSE,
    }

    [SerializeField] private ViewName _viewName;

    private void Awake()
    {
        if (_viewName == ViewName.GRAPHIC)
            GetComponent<Button>().onClick.AddListener(delegate { ViewManager.Show<StatistiqueView>(true); });
        else if (_viewName == ViewName.SETTINGS)
            GetComponent<Button>().onClick.AddListener(delegate { ViewManager.Show<ParameterView>(true); });
        else if (_viewName == ViewName.MAIN_MENU)
            GetComponent<Button>().onClick.AddListener(delegate { ViewManager.Show<MainMenuView>(true); });
    }
}
