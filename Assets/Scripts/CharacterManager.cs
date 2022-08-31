using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class CharacterManager : MonoBehaviour
{
    #region 별 변수
    DateTime nowTime;
    int ChargeTime = 30;
    DateTime lastGameTime;
    const int MaxStarCount = 4;

    [SerializeField]
    GameObject[] stars = new GameObject[MaxStarCount];
    [SerializeField]
    TextMeshProUGUI timeText;
    [SerializeField]
    GameObject StarPanel;
    [SerializeField]
    TextMeshProUGUI CarrotText;
    [SerializeField]
    TextMeshProUGUI UsernameText;
    [SerializeField]
    Text WaveText;
    [SerializeField]
    Text MonsterText;
    [SerializeField]
    Text TimeText;
    #endregion
    void Start()
    {
        CountTime();
        SetStar();
        lastGameTime = DateTime.Parse(GameManager.Instance.user.lastGameTime);
        CarrotText.text = GameManager.Instance.user.carrot.ToString();
        UsernameText.text = GameManager.Instance.user.name;
        string wave = SetStage();
        WaveText.text = wave;
        MonsterText.text = GameManager.Instance.user.monster.ToString() + " 마리";
        TimeText.text = GameManager.Instance.user.MaxTime.ToString() + " 초";
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
    #region 별
    public void CountTime()
    {
        TimeSpan timeSpan = nowTime - lastGameTime;

        int minute = Math.Abs(timeSpan.Hours * 30) + Math.Abs(timeSpan.Minutes);
        int second = Math.Abs(timeSpan.Seconds);
        string time = ((ChargeTime - 1) - minute) + " : " + (60 - second);
        //Debug.Log(time);
        if (minute >= ChargeTime)
        {
            ChargeStar(minute);
        }

        timeText.text = time;
    }

    public bool UseStar()
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
            return true;
        }
        else
        {
            GetStarPanel();
            return false;
        }
    }

    public void ChargeStar(int timeDistance)
    {
        int ChargeStarCount = timeDistance / ChargeTime;
        if (ChargeStarCount > 4 - GameManager.Instance.user.StarCount) ChargeStarCount = 4 - GameManager.Instance.user.StarCount;

        if (GameManager.Instance.user.StarCount < 4)
        {
            lastGameTime = lastGameTime + TimeSpan.FromMinutes(ChargeStarCount * ChargeTime);
            GameManager.Instance.user.lastGameTime = lastGameTime.ToString("yyyy/MM/dd HH:mm:ss");
            GameManager.Instance.user.StarCount += ChargeStarCount;
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
            }
        }
    }
    public void GetStarPanel()
    {
        StarPanel.SetActive(true);
        StarPanel.GetComponent<AudioSource>().volume = GameManager.Instance.user.effect;
        StarPanel.GetComponent<AudioSource>().Play();
    }

    public void BreakStarPopup()
    {
        StarPanel.SetActive(false);
    }
    #endregion

    public string SetStage()
    {
        switch (GameManager.Instance.user.MaxWave)
        {
            case 0:
                return "0-0";
            case 1:
                return "1-1";
            case 2:
                return "1-2";
            case 3:
                return "1-3";
            case 4:
                return "1-4";
            case 5:
                return "1-5";
            case 6:
                return "1-Boss";
            case 7:
                return "2-1";
            case 8:
                return "2-2";
            case 9:
                return "2-3";
            case 10:
                return "2-4";
            case 11:
                return "2-5";
            case 12:
                return "2-Boss";
            case 13:
                return "3-1";
            case 14:
                return "3-2";
            case 15:
                return "3-3";
            case 16:
                return "3-4";
            case 17:
                return "3-5";
            default:
                return "3-Boss";
        }

        string wavetext = (GameManager.Instance.user.MaxWave % 6) == 0 ? "Boss" : (GameManager.Instance.user.MaxWave % 6).ToString();
        string wave = GameManager.Instance.user.MaxTime == 0 ? "Wave 0" : $"{((GameManager.Instance.user.MaxWave - 1) / 6) + 1}- {wavetext}";
    }

    public void GoMain()
    {
        GameManager.Instance.goMain();
    }

    public void Goshop()
    {
        GameManager.Instance.GoShop();
    }

    public void GoFriend()
    {
        GameManager.Instance.GoMyFriend();
    }

    public void goGame()
    {
        if (UseStar())
        {
            GameManager.Instance.goToStage();
        }
    }

    public void LookOpening()
    {
        GameManager.Instance.GoStory();

    }

    public void LookEnding()
    {
        for(int i = 0; i < GameManager.Instance.user.DiaryGet.Length; i++)
        {
            if (!GameManager.Instance.user.DiaryGet[i])
            {
                StarPanel.SetActive(true);
                return;
            }
        }
        GameManager.Instance.GoEnding();
    }

    public void breakThisPanel(GameObject go)
    {

        go.SetActive(false);
    }
}
