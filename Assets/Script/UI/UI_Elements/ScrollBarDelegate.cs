using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBarDelegate : MonoBehaviour
{
    [SerializeField] private StatistiqueGraph _graph;

    private void Start() {
        GetComponent<Scrollbar>().onValueChanged.AddListener(delegate { OnScrollbarChange(); });
    }

    private void OnScrollbarChange()
    {
        _graph.OffsetPosition(GetComponent<Scrollbar>().value);
    }
}
