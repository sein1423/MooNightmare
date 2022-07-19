using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using UnityEngine.Networking;
using System.Net;

[System.Serializable]
public class userData
{
    public string name;
    public int carrot;
    public string lastGameTime;
    public int StarCount;
}

public class Item
{
    public Sprite Icon;
    public string name;
    public ItemType type;
    public float num;
    public float cost;
    public float percent;
}

public enum ItemType
{
    AttackPower, AttackRange, AttackCount, Health, MoveSpeed, ActiveSkill
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    ItemSO itemSO;
    const string Link = "https://docs.google.com/spreadsheets/d/1rd4km6B1GyLqJWLXRQdl16TKyV95esAXdV5L-w5AaaM/edit?usp=sharing";

    IEnumerator DownloadItemSO()
    {
        UnityWebRequest www = UnityWebRequest.Get(Link);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        SetItemSO(data);
    }

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
        StartCoroutine(DownloadItemSO());
    }

    void SetItemSO(string tsv)
    {
        /*string[] row = tsv.Split('\n');
        int rowSize = row.Length;
        int columnSize = row[0].Split('\t').Length;

        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');
            for (int j = 0; j < columnSize; j++)
            {
                print(column[j]);
            }
        }*/

        WebClient wc = new WebClient();
        wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:22.0) Gecko/20100101 Firefox/22.0");
        wc.Headers.Add("DNT", "1");
        wc.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
        wc.Headers.Add("Accept-Encoding", "deflate");
        wc.Headers.Add("Accept-Language", "en-US,en;q=0.5");

        var data = wc.DownloadString(Link);
        string[] row = data.Split('\n');
        for (int i = 2; i < row.Length; i++)
        {
            string[] column = row[i].Split(',');
            string name = column[0];
        }
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
