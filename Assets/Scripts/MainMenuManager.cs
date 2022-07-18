using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    
    [SerializeField]
    GameObject PopupPanel;
    [SerializeField]
    GameObject BigPanel;
    [SerializeField]
    GameObject SmallPanel;
    [SerializeField]
    GameObject OptionPanel;
    [SerializeField]
    GameObject ShopPanel;
    [SerializeField]
    GameObject ETCPanel;
    [SerializeField]
    GameObject PlayerState;
    [SerializeField]
    GameObject GameScore;

    static Stack<GameObject> PopupStack = new Stack<GameObject>();
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
    public void goFreind()
    {
        GameManager.Instance.GoMyFriend();
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
            Debug.Log("스타의 개수가 부족합니다");
        }
    }

    public void ChargeStar(int timeDistance)
    {
        if (timeDistance > 4 - GameManager.Instance.user.StarCount) timeDistance = 4 - GameManager.Instance.user.StarCount;
        Debug.Log($"{timeDistance}의 시간차이");
        
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
                Debug.Log(n + "번째 별 생성");
            }
        }
    }

    public void GetOption()
    {
        PopupPanel.SetActive(true);
        PopupStack.Push(PopupPanel);
        SmallPanel.SetActive(true);
        PopupStack.Push(SmallPanel);
        OptionPanel.SetActive(true);
        PopupStack.Push(OptionPanel);
    }

    public void GetShop()
    {
        PopupPanel.SetActive(true);
        PopupStack.Push(PopupPanel);
        SmallPanel.SetActive(true);
        PopupStack.Push(SmallPanel);
        ShopPanel.SetActive(true);
        PopupStack.Push(ShopPanel);
    }

    public void GetMyCharactor()
    {
        PopupPanel.SetActive(true);
        PopupStack.Push(PopupPanel);
        BigPanel.SetActive(true);
        PopupStack.Push(BigPanel);
        PlayerState.SetActive(true);
        PopupStack.Push(PlayerState);
    }

    public void BreakPopUp()
    {
        while(PopupStack.Count > 0)
        {
            PopupStack.Pop().SetActive(false);
        }
    }

    public void LookGameScore()
    {
        if (PlayerState.activeSelf)
        {
            PopupStack.Pop().SetActive(false);
            GameScore.SetActive(true);
            PopupStack.Push(GameScore);
        }
    }

    public void LookPlayerState()
    {
        if (GameScore.activeSelf)
        {
            PopupStack.Pop().SetActive(false);
            PlayerState.SetActive(true);
            PopupStack.Push(PlayerState);
        }
    }

}
