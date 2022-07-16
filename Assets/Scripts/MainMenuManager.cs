using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public GameObject Option;
    DateTime nowTime;
    int ChargeTime = 1;
    DateTime lastGameTime;
    const int MaxStarCount = 4;

    [SerializeField]
    GameObject[] stars = new GameObject[MaxStarCount];
    [SerializeField]
    TextMeshProUGUI timeText;
    [SerializeField]
    TextMeshProUGUI UserText;
    // Start is called before the first frame update
    void Start()
    {
        UserText.text = $"환영합니다 {GameManager.Instance.user.name}님";
        for(int n = MaxStarCount - 1; n >= GameManager.Instance.user.StarCount; n--)
        {
            stars[n].SetActive(false);
        }
        lastGameTime = DateTime.Parse(GameManager.Instance.user.lastGameTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.user.StarCount < 4)
        {
            nowTime = DateTime.Now;
            CountTime();
        }
        else
        {
            timeText.text = "";
        }
    }

    public void optionControll()
    {
        if (Option.activeSelf)
        {
            Option.SetActive(false);
        }
        else
        {
            Option.SetActive(true);
        }
    }

    public void goGame()
    {
        UseStar();
        GameManager.Instance.goToStage();
    }

    public void CountTime()
    {
        TimeSpan timeSpan = (lastGameTime + TimeSpan.FromMinutes(ChargeTime)) - nowTime;
        int minute = timeSpan.Minutes;
        int second = timeSpan.Seconds;
        string time = minute + " : " + second;
        if(minute >= ChargeTime)
        {
            ChargeStar();
        }
        
        timeText.text = time;
    }

    public void UseStar()
    {
        if (GameManager.Instance.user.StarCount > 0)
        {
            if (GameManager.Instance.user.StarCount == 4)
            {
                lastGameTime = DateTime.Now;
                GameManager.Instance.user.lastGameTime = lastGameTime.ToString("yyyy/MM/dd HH:mm:ss");
            }
            stars[GameManager.Instance.user.StarCount-1].SetActive(false);
            GameManager.Instance.user.StarCount--;
            GameManager.Instance.SaveData();
        }
        else 
        {
            Debug.Log("스타의 개수가 부족합니다");
        }
    }

    public void ChargeStar()
    {
        if (GameManager.Instance.user.StarCount < 4)
        {
            stars[GameManager.Instance.user.StarCount].SetActive(true);
            lastGameTime = DateTime.Now;
            GameManager.Instance.user.lastGameTime = lastGameTime.ToString("yyyy/MM/dd HH:mm:ss");
            GameManager.Instance.user.StarCount++;
            GameManager.Instance.SaveData();
        }
    }
}
