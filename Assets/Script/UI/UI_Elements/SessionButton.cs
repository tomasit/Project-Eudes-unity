using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SessionButton : MonoBehaviour
{
    private int _index;
    TMPStatDescription _description;

    private void Awake()
    {
        _description = FindObjectOfType<TMPStatDescription>();
    }

    public void AddDelegate(int index)
    {
        _index = index;
        GetComponent<Button>().onClick.AddListener(delegate { _description.SetDescription(_index); });
    }
}
