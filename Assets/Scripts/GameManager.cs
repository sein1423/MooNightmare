using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;

[System.Serializable]
public class userData
{
    public string name;
    public int carrot;
    public string lastGameTime;
    public int StarCount;
    public int MaxWave;
    public int MaxTime;
    public int monster;
    public string LastGameDay;
    public bool[] DreamGet = new bool[6];
    public bool[] DiaryGet = new bool[6];
    public float BGM;
    public float effect;
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    AudioSource AS;
    public bool userdataget = false;
    private const string jsonFilePath = "/UserData.json";
    [SerializeField] AudioClip Main;
    [SerializeField] AudioClip Game;

    

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if(Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public userData user;
    string userState;

    void Start()
    {
        AS = GetComponent<AudioSource>();
        LoadUserData();
        Debug.Log(Application.persistentDataPath + jsonFilePath);
    }

    

    // Update is called once per frame
    void Update()
    {
    }

    public void goToMain()
    {
        if (userdataget)
        {
            SceneManager.LoadScene("Main");
        }
        else
        {
            SceneManager.LoadScene("Intro");
        }
    }

    public void goToStage()
    {
        SceneManager.LoadScene("Game");
    }

    public void goMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void GameQuit()
    {
        Debug.Log("게임종료");
        Application.Quit();
    }

    public void GoMyFriend()
    {
        SceneManager.LoadScene("My Friend");
    }

    public void SetUserData(string inputname)
    {
        user = new userData();
        user.name = inputname;
        user.carrot = 10000;
        user.lastGameTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        user.StarCount = 4;
        user.MaxTime = 0;
        user.MaxWave = 0;
        user.monster = 0;
        user.LastGameDay = DateTime.Now.ToString("yyyy/MM/dd");
        user.BGM = 1;
        user.effect = 1;
        for(int i = 0; i < user.DreamGet.Length; i++)
        {
            user.DreamGet[i] = false;
            user.DiaryGet[i] = false;
        }

        userState = JsonUtility.ToJson(user);

        File.WriteAllText(Application.persistentDataPath + jsonFilePath, JsonUtility.ToJson(user));
    }

    public void LoadUserData()
    {
        if(File.Exists(Application.persistentDataPath + jsonFilePath))
        {
            string json = File.ReadAllText(Application.persistentDataPath + jsonFilePath);
            user = JsonUtility.FromJson<userData>(json);
            userdataget = true;
            LoadValues();
        }
    }

    public void SaveData()
    {
        userState = JsonUtility.ToJson(user);
        File.WriteAllText(Application.persistentDataPath + jsonFilePath, JsonUtility.ToJson(user));
    }

    public void GoShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void GoCharacter()
    {
        SceneManager.LoadScene("My Charactor");
    }
    void LoadValues()
    {
        AS.volume = user.BGM;
    }

    public void SaveVolumeButton(float value)
    {
        user.BGM = value;
        LoadValues();
        SaveData();
    }
    public void SaveListenerButton(float value)
    {
        user.effect = value;
        LoadValues();
        SaveData();
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            if (!(AS.clip == Game))
            {
                AS.clip = Game;
                AS.Play();
            }
        }
        else if(scene.name == "Main")
        {
            if(!(AS.clip == Main))
            {
                AS.clip = Main;
                AS.Play();
            }
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
