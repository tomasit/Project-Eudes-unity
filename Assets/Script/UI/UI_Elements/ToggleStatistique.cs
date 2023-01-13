using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleStatistique : MonoBehaviour
{
    [SerializeField] private StatistiqueGraph _graph;
    [SerializeField] private StatistiqueGraph.StatistiqueType _type;

    private void Start() {
        GetComponent<Toggle>().onValueChanged.AddListener(delegate { onToggleExec(); });
    }

    private void onToggleExec()
    {
        _graph.EnableType(_type, GetComponent<Toggle>().isOn);
    }
}
