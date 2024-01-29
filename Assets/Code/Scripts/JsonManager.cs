using System;
using System.IO;
using UnityEngine;

public class JsonManager : MonoBehaviour
{
    public static JsonManager Instance;
    [HideInInspector] public UserData userData;
    private string jsonpath;

    // Start is called before the first frame update
    void Start()
    {
        // Only one JsonManager on scene.
        if (!Instance) Instance = this;
        else { Destroy(gameObject); return; }
        DontDestroyOnLoad(gameObject);

        // Default status
        jsonpath = $"{Application.persistentDataPath}/userdata.json";
        UserData defaultData = JsonUtility.FromJson<UserData>(Resources.Load<TextAsset>("MainData").text);

        // Create user json if the file doesnt exist
        if (!File.Exists(jsonpath)) SaveDataJSON(defaultData);

        // Load the user's json file
        LoadDataJSON();
    }

    // Saves the user data with new values
    public void SaveDataJSON(UserData save) { File.WriteAllText(jsonpath, JsonUtility.ToJson(save)); }
    public void LoadDataJSON() { userData = JsonUtility.FromJson<UserData>(File.ReadAllText(jsonpath)); }
}


// User data json serializable class
[Serializable]
public class UserData
{
    public int souls;
    [SerializeField] public Highscore highscore;
    [SerializeField] public Upgrades upgrades;
    [SerializeField] public Achievements achievements;

    [Serializable]
    public class Achievements
    {
        public bool die;
        public bool die100;
    }

    [Serializable]
    public class Forcefield
    {
        public int currentLevel;
        public L1 L1;
        public L2 L2;
        public L3 L3;
        public L4 L4;
        public L5 L5;
    }

    [Serializable]
    public class Highscore
    {
        public int total;
    }

    [Serializable]
    public class L1
    {
        public int nextUpgradeCost;
        public int duration;
        public int hits;
    }

    [Serializable]
    public class L2
    {
        public int nextUpgradeCost;
        public double duration;
        public int hits;
    }

    [Serializable]
    public class L3
    {
        public int nextUpgradeCost;
        public int duration;
        public int hits;
    }

    [Serializable]
    public class L4
    {
        public int nextUpgradeCost;
        public double duration;
        public int hits;
    }

    [Serializable]
    public class L5
    {
        public int nextUpgradeCost;
        public int duration;
        public int hits;
    }

    [Serializable]
    public class Magnet
    {
        public int currentLevel;
        public L1 L1;
        public L2 L2;
        public L3 L3;
        public L4 L4;
        public L5 L5;
    }

    [Serializable]
    public class Root
    {
        public int souls;
        public Highscore highscore;
        public Upgrades upgrades;
        public Achievements achievements;
    }

    [Serializable]
    public class Upgrades
    {
        public Magnet magnet;
        public Forcefield forcefield;
    }
}