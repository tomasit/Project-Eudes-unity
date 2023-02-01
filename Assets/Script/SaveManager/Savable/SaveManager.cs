using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// using ObjectData = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, object>>;

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
    private BalanceValues _balance;

    // public Dictionary<StatistiqueGraph.StatistiqueType, Vector2> GetDictBalance() { return _balance; }
    // public void SetBalance(Dictionary<StatistiqueGraph.StatistiqueType, Vector2> b) { _balance = b; }

    private void Awake() {
        Debug.Log("Singleton awake");
        if (_dataInstance != null) {
            Debug.Log("Singleton always present on the scene, destroy this one");
            Destroy(this);
        } else {
            DontDestroyOnLoad(this);
        }

        _params = new ParametrableValues();
        _balance = new BalanceValues();
        _statDictionary = new Dictionary<StatistiqueGraph.StatistiqueType, List<float>>();

        InitDictionaries();

        Load();
    }

    public void ResetParameters()
    {
        _params = new ParametrableValues();
    }

    public void ResetBalance()
    {
        _balance = new BalanceValues();
    }

    public void SetParameters(ParametrableValues settings)
    {
        _params = new ParametrableValues(settings);
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
    public ParametrableValues GetParameters() { return _params; }
    public BalanceValues GetBalance() { return _balance; }
    public void SetBalance(BalanceValues balance) { _balance = new BalanceValues(balance); }

    public void Load()
    {
        if (SerializationManager.Exist("GlobalParameters"))
        {
            _params = (ParametrableValues)SerializationManager.Load("GlobalParameters");
        }

        if (SerializationManager.Exist("Balancing"))
        {
            _balance = (BalanceValues)SerializationManager.Load("Balancing");
        }

        if (SerializationManager.Exist("GraphData"))
        {
            _statDictionary = (Dictionary<StatistiqueGraph.StatistiqueType, List<float>>)SerializationManager.Load("GraphData");
        }
    }

    public void SaveGraph()
    {
        SerializationManager.Save("GraphData", _statDictionary);
    }

    public void SaveParameters()
    {
        SerializationManager.Save("GlobalParameters", _params);
    }

    public void SaveBalance()
    {
        if (SerializationManager.Exist("GraphData"))
        {
            SerializationManager.EraseSave("GraphData");
            _statDictionary[StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_ACCURACY].Clear();
            _statDictionary[StatistiqueGraph.StatistiqueType.PITCH_AND_ROLL_REACTION_TIME].Clear();
            _statDictionary[StatistiqueGraph.StatistiqueType.GAZ_ACCURACY].Clear();
            _statDictionary[StatistiqueGraph.StatistiqueType.RUDDER_ACCURACY].Clear();
            _statDictionary[StatistiqueGraph.StatistiqueType.RUDDER_REACTION_TIME].Clear();
            _statDictionary[StatistiqueGraph.StatistiqueType.NUMBER_MEMORY].Clear();
            _statDictionary[StatistiqueGraph.StatistiqueType.LIGHT_MEMORY].Clear();
        }

        SerializationManager.Save("Balancing", _balance);
    }
}