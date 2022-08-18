using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public class ShopManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static ShopManager Instance;
    [SerializeField] public TextMeshProUGUI CarrotText;
    [SerializeField] public GameObject Panel;
    [SerializeField] public Text DreamText;

    private void Awake()
    {
        Instance = this;
    }

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
    #endregion
    void Start()
    {
        CountTime();
        SetStar();
        lastGameTime = DateTime.Parse(GameManager.Instance.user.lastGameTime);
        CarrotText.text = GameManager.Instance.user.carrot.ToString();
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

    // Update is called once per frame


    public void BreakPanel()
    {
        Panel.SetActive(false);
    }

    public void GoMain()
    {
        GameManager.Instance.goMain();
    }

    

    public void GoFriend()
    {
        GameManager.Instance.GoMyFriend();
    }

    public void GoCharactor()
    {
        GameManager.Instance.GoCharacter();
    }
    public void goGame()
    {
        if (UseStar())
        {
            GameManager.Instance.goToStage();
        }
    }
}
