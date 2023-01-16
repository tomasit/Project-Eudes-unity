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
    private Dictionary<StatistiqueGraph.StatistiqueType, Vector2> _balance;

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
        _balance = new Dictionary<StatistiqueGraph.StatistiqueType, Vector2>();

        InitDictionaries();

        Load();
    }

    private void InitDictionaries()
    {
        _statDictionary.Add(StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_ACCURACY, new List<float>());
        _statDictionary.Add(StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_REACTION_TIME, new List<float>());
        _statDictionary.Add(StatistiqueGraph.StatistiqueType.GAZ_ACCURACY, new List<float>());
        _statDictionary.Add(StatistiqueGraph.StatistiqueType.RUDDER_ACCURACY, new List<float>());
        _statDictionary.Add(StatistiqueGraph.StatistiqueType.RUDDER_REACTION_TIME, new List<float>());
        _statDictionary.Add(StatistiqueGraph.StatistiqueType.NUMBER_MEMORY, new List<float>());
        _statDictionary.Add(StatistiqueGraph.StatistiqueType.LIGHT_MEMORY, new List<float>());
    }

    public int GetNumberSession() { return _statDictionary[StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_ACCURACY].Count; }
    public Dictionary<StatistiqueGraph.StatistiqueType, List<float>> GetDict() { return _statDictionary; }
    public Vector2 GetBalance(StatistiqueGraph.StatistiqueType balanceType) { return _balance[balanceType]; }
    public ParametrableValues GetParameters() { return _params; }

    public void Load()
    {
        if (SerializationManager.Exist("GlobalParameters"))
        {
            _params = (ParametrableValues)SerializationManager.Load("GlobalParameters");
        }

        if (SerializationManager.Exist("GraphData"))
        {
            _statDictionary = (Dictionary<StatistiqueGraph.StatistiqueType, List<float>>)SerializationManager.Load("GlobalParameters");
        }

        if (SerializationManager.Exist("Balancing"))
        {
            _balance = (Dictionary<StatistiqueGraph.StatistiqueType, Vector2>)SerializationManager.Load("GlobalParameters");
        }
    }

    public void Save()
    {
    }
}