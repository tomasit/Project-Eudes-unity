using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatistiqueView : AView 
{
    [SerializeField] private StatistiqueGraph _graph;
    [SerializeField] private Toggle[] _tgls;

    public override void Show()
    {
        base.Show();

        if (!_graph._isInitialize)
            _graph.Initialize();

        _graph.ClearGraph();
        _graph.LoadFromSave();

        _graph.EnableType(StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_ACCURACY, false);
        _graph.EnableType(StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_REACTION_TIME, false);
        _graph.EnableType(StatistiqueGraph.StatistiqueType.RUDDER_ACCURACY, false);
        _graph.EnableType(StatistiqueGraph.StatistiqueType.RUDDER_REACTION_TIME, false);
        _graph.EnableType(StatistiqueGraph.StatistiqueType.GAZ_ACCURACY, false);
        _graph.EnableType(StatistiqueGraph.StatistiqueType.NUMBER_MEMORY, false);
        _graph.EnableType(StatistiqueGraph.StatistiqueType.LIGHT_MEMORY, false);


        foreach (var t in _tgls)
        {
            t.isOn = false;
            t.enabled = false;
        }

        FindObjectOfType<ToggleInverse>(true).Inverse(true);
    }

    public override void Hide()
    {
        base.Hide();
        FindObjectOfType<ToggleInverse>(true).Inverse(false);
        _graph._scrollbar.GetComponent<Scrollbar>().value = 1.0f;
    }
}