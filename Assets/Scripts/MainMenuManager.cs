using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public GameObject Option;
    public static int starCount = 4;
    DateTime nowTime;
    [SerializeField]
    TextMeshProUGUI timeText;

    [SerializeField]
    GameObject[] stars = new GameObject[starCount];
    int ChargeTime = 1;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (starCount < 4)
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
        if (starCount > 0)
        {
            if (starCount == 4)
            {
                GameManager.Instance.lastGameTime = System.DateTime.Now;
            }
            starCount--;
            GameManager.Instance.goToStage();
        }
    }

    public void CountTime()
    {
        TimeSpan timeSpan = (GameManager.Instance.lastGameTime + TimeSpan.FromMinutes(ChargeTime)) - nowTime;
        int minute = timeSpan.Minutes;
        int second = timeSpan.Seconds;
        string time = minute + " : " + second;
        Debug.Log((nowTime - GameManager.Instance.lastGameTime).Minutes + " : " + (nowTime - GameManager.Instance.lastGameTime).Seconds);
        if((nowTime - GameManager.Instance.lastGameTime).Minutes >= ChargeTime)
        {
            ChargeStar();
        }
        
        timeText.text = time;
    }

    public void UseStar()
    {
        if (starCount > 0)
        {
            if (starCount == 4)
            {
                GameManager.Instance.lastGameTime = System.DateTime.Now;
            }
            stars[starCount-1].SetActive(false);
            starCount--;
        }
        else 
        {
            Debug.Log("스타의 개수가 부족합니다");
        }
    }

    public void ChargeStar()
    {
        if (starCount < 4)
        {
            stars[starCount].SetActive(true);
            GameManager.Instance.lastGameTime = DateTime.Now;
            starCount++;
        }
        else
        {
            Debug.Log("스타의 개수가 최대치입니다");
        }
    }
}
