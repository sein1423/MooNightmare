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
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool userdataget = false;
    private const string jsonFilePath = "/UserData.json";
    
    

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
        LoadUserData();
        Debug.Log(Application.persistentDataPath + jsonFilePath);
        //user.carrot += 10000;
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
        for(int i = 0; i < user.DreamGet.Length; i++)
        {
            user.DreamGet[i] = false;
            user.DiaryGet[i] = false;
        }

        userState = JsonUtility.ToJson(user);


        /*string path = Path.Combine(Application.persistentDataPath, jsonFilePath);
        File.WriteAllText(path, userState);*/

        File.WriteAllText(Application.persistentDataPath + jsonFilePath, JsonUtility.ToJson(user));
    }

    public void LoadUserData()
    {
        /*string path = Path.Combine(Application.persistentDataPath, jsonFilePath);
        string jsonData = File.ReadAllText(path);

        try
        {
            user = JsonUtility.FromJson<userData>(jsonData);
            
        }
        catch (IOException)
        {
            return;
        }*/

        if(File.Exists(Application.persistentDataPath + jsonFilePath))
        {
            string json = File.ReadAllText(Application.persistentDataPath + jsonFilePath);
            user = JsonUtility.FromJson<userData>(json);
            userdataget = true;
        }
        
        
    }

    public void SaveData()
    {
        userState = JsonUtility.ToJson(user);
        /*string path = Path.Combine(Application.persistentDataPath, "Path/userData.Json");
        File.WriteAllText(path, userState);*/
        File.WriteAllText(Application.persistentDataPath + jsonFilePath, JsonUtility.ToJson(user));
    }

    public void GoShop()
    {
        SceneManager.LoadScene("Shop");
    }
}
