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
        CountTime();
        SetStar();
        UserText.text = $"Welcome {GameManager.Instance.user.name}";
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
        TimeSpan timeSpan = nowTime - lastGameTime;
        int minute = Math.Abs(timeSpan.Minutes);
        int second = Math.Abs(timeSpan.Seconds);
        string time = minute + " : " + (60 - second);
        //Debug.Log(time);
        if(minute >= ChargeTime)
        {
            ChargeStar(minute);
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
            stars[GameManager.Instance.user.StarCount - 1].SetActive(false);
            GameManager.Instance.user.StarCount--;
            GameManager.Instance.SaveData();
        }
        else 
        {
            Debug.Log("��Ÿ�� ������ �����մϴ�");
        }
    }

    public void ChargeStar(int timeDistance)
    {
        if (timeDistance > 4 - GameManager.Instance.user.StarCount) timeDistance = 4 - GameManager.Instance.user.StarCount;
        Debug.Log($"{timeDistance}�� �ð�����");
        
        if (GameManager.Instance.user.StarCount < 4)
        {
            lastGameTime = lastGameTime + TimeSpan.FromMinutes(timeDistance);
            GameManager.Instance.user.lastGameTime = lastGameTime.ToString("yyyy/MM/dd HH:mm:ss");
            GameManager.Instance.user.StarCount+= timeDistance;
            GameManager.Instance.SaveData();
            SetStar();
        }
    }

    public void SetStar()
    {
        for (int n = 0; n < GameManager.Instance.user.StarCount; n++)
        {
            if (!stars[n].activeSelf)
            {
                stars[n].SetActive(true);
                Debug.Log(n + "��° �� ����");
            }
        }
    }

}
