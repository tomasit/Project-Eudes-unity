using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ObjectData = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, object>>;

public class SaveManager : MonoBehaviour
{
    private static SaveManager _dataInstance = null;
    public static SaveManager DataInstance {
        get {
            if (_dataInstance == null) {

                _dataInstance = FindObjectOfType<SaveManager>();
                if (_dataInstance == null) {
                    _dataInstance = new GameObject().AddComponent<SaveManager>();
                }
            }
            return _dataInstance;
        }
    }

    private Dictionary<StatistiqueGraph.StatistiqueType, List<float>> _statDictionary;
    private ParametrableValues _params;

    public ParametrableValues GetParameters()
    {
        return _params;
    }

    private void Awake() {
        Debug.Log("Singleton awake");
        if (_dataInstance != null) {
            Debug.Log("Singleton always present on the scene, destroy this one");
            Destroy(this);
        } else {
            DontDestroyOnLoad(this);
        }

        _params = new ParametrableValues();

        _statDictionary = new Dictionary<StatistiqueGraph.StatistiqueType, List<float>>();
        _statDictionary.Add(StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_ACCURACY, new List<float>());
        _statDictionary.Add(StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_REACTION_TIME, new List<float>());
        _statDictionary.Add(StatistiqueGraph.StatistiqueType.GAZ_ACCURACY, new List<float>());
        _statDictionary.Add(StatistiqueGraph.StatistiqueType.RUDDER_ACCURACY, new List<float>());
        _statDictionary.Add(StatistiqueGraph.StatistiqueType.RUDDER_REACTION_TIME, new List<float>());
        _statDictionary.Add(StatistiqueGraph.StatistiqueType.NUMBER_MEMORY, new List<float>());
        _statDictionary.Add(StatistiqueGraph.StatistiqueType.LIGHT_MEMORY, new List<float>());

        Load();
    }

    public int GetNumberSession()
    {
        return _statDictionary[StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_ACCURACY].Count;
    }

    public Dictionary<StatistiqueGraph.StatistiqueType, List<float>> GetDict()
    {
        return _statDictionary;
    }

    private int i = 0;

    public void Test()
    {
        foreach (var s in _statDictionary)
            s.Value.Clear();
        
        int j = 0;
        int r = Random.Range(0, 150);
        foreach (var s in _statDictionary)
        {
            if (i == 0)
            {
                for (int k = 0; k < 9; ++k)
                    s.Value.Add(Random.Range(0.0f, 1.0f));
            }
            else
            {
                for (int k = 0; k < r; ++k)
                    s.Value.Add(Random.Range(0.0f, 1.0f));
            }
            // s.Value.Add(Random.Range(0.0f, 1.0f));
        }
        ++i;
    }

    public void Load()
    {
    }

    public void Save()
    {
    }
}