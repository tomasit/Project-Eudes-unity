using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMPStatDescription : MonoBehaviour
{
    private TextMeshProUGUI _tmpro;

    private void Awake()
    {
        _tmpro = GetComponent<TextMeshProUGUI>();
    }

    public void SetDescription(int index)
    {
        var dict = SaveManager.DataInstance.GetDict();

        _tmpro.text = "SESSION " + (index + 1) + 
        "\n\nReaction time :\n -Pitch&roll : " + string.Format("{0:0.00}", dict[StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_REACTION_TIME][index] * 100) + 
        "%\n -Rudder : " + string.Format("{0:0.00}", dict[StatistiqueGraph.StatistiqueType.RUDDER_REACTION_TIME][index] * 100) + 
        "%\n\nAccuracy :\n -Pitch&roll : " + string.Format("{0:0.00}", dict[StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_ACCURACY][index] * 100) + 
        "%\n -Rudder : " + string.Format("{0:0.00}", dict[StatistiqueGraph.StatistiqueType.RUDDER_ACCURACY][index] * 100) + 
        "%\n -Gaz : " + string.Format("{0:0.00}", dict[StatistiqueGraph.StatistiqueType.GAZ_ACCURACY][index] * 100) + 
        "%\n\nMemory : \n -Number : " + string.Format("{0:0.00}", dict[StatistiqueGraph.StatistiqueType.NUMBER_MEMORY][index] * 100) + 
        "%\n -Light : " + string.Format("{0:0.00}", dict[StatistiqueGraph.StatistiqueType.LIGHT_MEMORY][index] * 100) + "%";
    }
}
