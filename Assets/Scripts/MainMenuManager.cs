using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public GameObject Option;
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

    [SerializeField]
    TextMeshProUGUI UserText;
    
    [SerializeField]
    GameObject PopupPanel;
    [SerializeField]
    GameObject SmallPanel;
    [SerializeField]
    GameObject OptionPanel;
    [SerializeField]
    GameObject ShopPanel;
    [SerializeField]
    GameObject ExitPanel;
    [SerializeField]
    TextMeshProUGUI CarrotText;
    [SerializeField]
    GameObject CheatPanel;
    [SerializeField]
    Scrollbar BGM;
    [SerializeField]
    Scrollbar effect;
    [SerializeField, TextArea]
    string[] line;
    [SerializeField] TextMeshProUGUI textbox;
    [SerializeField] GameObject CouponPanel;
    [SerializeField] Text CouponText;
    [SerializeField] GameObject AlertPanel;
    static Stack<GameObject> PopupStack = new Stack<GameObject>();
    // Start is called before the first frame update

    void Start()
    {
        CountTime();
        SetStar();
        UserText.text = $"{GameManager.Instance.user.name}요원 접속완료";
        lastGameTime = DateTime.Parse(GameManager.Instance.user.lastGameTime);
        CarrotText.text = GameManager.Instance.user.carrot.ToString();
        BGM.value = GameManager.Instance.user.BGM;
        effect.value = GameManager.Instance.user.effect;
        textbox.text = line[UnityEngine.Random.Range(0, line.Length)];
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
        if (UseStar())
        {
            GameManager.Instance.goToStage();
        }
    }
    public void goFreind()
    {
        GameManager.Instance.GoMyFriend();
    }

    #region 별
    public void CountTime()
    {
        TimeSpan timeSpan = nowTime - lastGameTime;
        
        int minute = Math.Abs(timeSpan.Hours * 30) + Math.Abs(timeSpan.Minutes);
        int second = Math.Abs(timeSpan.Seconds);
        string time = ((ChargeTime - 1)-minute) + " : " + (60 - second);
        //Debug.Log(time);
        if(minute >= ChargeTime)
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
            GameManager.Instance.user.StarCount+= ChargeStarCount;
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
        PopupStack.Push(StarPanel);
        StarPanel.GetComponent<AudioSource>().volume = GameManager.Instance.user.effect;
        StarPanel.GetComponent<AudioSource>().Play();
    }
    #endregion
    public void GetOption()
    {
        PopupPanel.SetActive(true);
        PopupStack.Push(PopupPanel);
        SmallPanel.SetActive(true);
        PopupStack.Push(SmallPanel);
        OptionPanel.SetActive(true);
        PopupStack.Push(OptionPanel);
    }

    

    public void GetExit()
    {
        PopupPanel.SetActive(true);
        PopupStack.Push(PopupPanel);
        SmallPanel.SetActive(true);
        PopupStack.Push(SmallPanel);
        ExitPanel.SetActive(true);
        PopupStack.Push(ExitPanel);
    }

    public void GetShop()
    {
        GameManager.Instance.GoShop();
    }

    public void GetMyCharactor()
    {
        GameManager.Instance.GoCharacter();
    }

    public void BreakPopUp()
    {
        while(PopupStack.Count > 0)
        {
            PopupStack.Pop().SetActive(false);
        }
    }


    public void ExitGame()
    {
        GameManager.Instance.GameQuit();
    }

    public void CheatStar()
    {
        GameManager.Instance.user.StarCount = 4;
        GameManager.Instance.SaveData();
        SetStar();
    }

    public void CheatCarrot()
    {
        GameManager.Instance.user.carrot += 100;
        GameManager.Instance.SaveData();
        CarrotText.text = GameManager.Instance.user.carrot.ToString();
    }

    public void OpenCheatMenu()
    {
        PopupPanel.SetActive(true);
        PopupStack.Push(PopupPanel);
        SmallPanel.SetActive(true);
        PopupStack.Push(SmallPanel);
        CheatPanel.SetActive(true);
        PopupStack.Push(CheatPanel);
    }

    public void SetBGM(Scrollbar sb)
    {
        GameManager.Instance.user.BGM = sb.value;
        GameManager.Instance.SaveVolumeButton(sb.value);
    }

    public void Seteffect(Scrollbar sb)
    {
        GameManager.Instance.user.effect = sb.value;
    }

    public void OpenCoupon()
    {
        PopupPanel.SetActive(true);
        PopupStack.Push(PopupPanel);
        SmallPanel.SetActive(true);
        PopupStack.Push(SmallPanel);
        CouponPanel.SetActive(true);
        PopupStack.Push(CouponPanel);
    }

    public void userCoupon()
    {
        if (CouponText.text == "GoodDream")
        {
            if (!GameManager.Instance.user.UsedCoupon)
            {
                GameManager.Instance.user.UsedCoupon = true;
                GameManager.Instance.user.carrot += 50;
                CarrotText.text = GameManager.Instance.user.carrot.ToString();
                AlertPanel.SetActive(true);
                AlertPanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = "쿠폰이 정상적으로\n입력되었습니다.";
                //GameManager.Instance.SaveData();
            }
            else
            {
                AlertPanel.SetActive(true);
                AlertPanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = "쿠폰을 이미 사용한 계정입니다.";
            }
        }
        else
        {
            AlertPanel.SetActive(true);
            AlertPanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = "잘못된 번호입니다";
        }
    }

    public void ExitAlert()
    {
        AlertPanel.SetActive(false);
    }
}
