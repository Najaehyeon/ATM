using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }

    public UserData userData;
    public UserData UserData
    {
        get { return userData; }
        set { userData = value; }
    }
    private string saveKey = "UserData";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadUserData();
    }

    public void SaveUserData()
    {
        string json = JsonUtility.ToJson(UserData);
        PlayerPrefs.SetString(saveKey, json);
    }

    public void LoadUserData()
    {
        if (PlayerPrefs.HasKey(saveKey))
        {
            string json = PlayerPrefs.GetString(saveKey);
            userData = JsonUtility.FromJson<UserData>(json);
        }
        else
        {
            userData = new UserData("Jay Na", 100000, 50000);
            SaveUserData();
        }
    }
}
