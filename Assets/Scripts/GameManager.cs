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
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Start is called before the first frame update
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            GameQuit();
        }
    }

    public void goToMain()
    {
        if (user != null)
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

    public void GameQuit()
    {
        Debug.Log("게임종료");
        Application.Quit();
    }
    public void SetUserData(string inputname)
    {
        user = new userData();
        user.name = inputname;
        user.carrot = 0;
        user.lastGameTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        user.StarCount = 4;

        userState = JsonUtility.ToJson(user);
        string path = Path.Combine(Application.dataPath, "Path/userData.Json");
        File.WriteAllText(path, userState);
        Debug.Log(path + " : " + userState);

        SceneManager.LoadScene("Main");
    }

    public void LoadUserData()
    {
        string path = Path.Combine(Application.dataPath, "Path/userData.Json");
        string jsonData = File.ReadAllText(path);

        try
        {
            user = JsonUtility.FromJson<userData>(jsonData);
        }
        catch (IOException)
        {
            return;
        }
    }

    public void SaveData()
    {
        userState = JsonUtility.ToJson(user);
        string path = Path.Combine(Application.dataPath, "Path/userData.Json");
        File.WriteAllText(path, userState);
        Debug.Log(path + " : " + userState);
    }
}
