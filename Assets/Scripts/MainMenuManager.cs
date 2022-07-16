using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public GameObject Option;
    DateTime nowTime;
    [SerializeField]
    TextMeshProUGUI timeText;
    const int MaxStarCount = 4;
    [SerializeField]
    GameObject[] stars = new GameObject[MaxStarCount];
    int ChargeTime = 1;
    // Start is called before the first frame update
    void Start()
    {
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
        if (GameManager.Instance.user.StarCount > 0)
        {
            if (GameManager.Instance.user.StarCount == 4)
            {
                GameManager.Instance.user.lastGameTime = System.DateTime.Now;
            }
            GameManager.Instance.user.StarCount--;
            GameManager.Instance.goToStage();
        }
    }

    public void CountTime()
    {
        TimeSpan timeSpan = (GameManager.Instance.user.lastGameTime + TimeSpan.FromMinutes(ChargeTime)) - nowTime;
        int minute = timeSpan.Minutes;
        int second = timeSpan.Seconds;
        string time = minute + " : " + second;
        Debug.Log((nowTime - GameManager.Instance.user.lastGameTime).Minutes + " : " + (nowTime - GameManager.Instance.user.lastGameTime).Seconds);
        if((nowTime - GameManager.Instance.user.lastGameTime).Minutes >= ChargeTime)
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
                GameManager.Instance.user.lastGameTime = System.DateTime.Now;
            }
            stars[GameManager.Instance.user.StarCount-1].SetActive(false);
            GameManager.Instance.user.StarCount--;
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
            GameManager.Instance.user.lastGameTime = DateTime.Now;
            GameManager.Instance.user.StarCount++;
        }
        else
        {
            Debug.Log("스타의 개수가 최대치입니다");
        }
    }
}
