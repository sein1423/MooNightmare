using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class FriendManager : MonoBehaviour
{
    [SerializeField]
    GameObject DreamPanel;
    [SerializeField]
    GameObject DiaryPanel;
    [SerializeField]
    TextMeshProUGUI CarrotText;

    [SerializeField]
    Image DiaryImage;
    [SerializeField]
    Text DiaryTitle;
    [SerializeField]
    Text DiaryWeather;
    [SerializeField]
    Text Diaryfeel;
    [SerializeField]
    Text DiaryText;

    [SerializeField]
    DiarySO[] diary;

    [SerializeField]
    GameObject[] Buttons;

    [SerializeField]
    GameObject Gift1Panel;
    [SerializeField]
    GameObject Gift2Panel;
    [SerializeField]
    GameObject Gift3Panel;

    [SerializeField]
    Text giftText;
    [SerializeField]
    Text giftbarText;
    [SerializeField]
    Text DreamText;
    [SerializeField]
    Image GiftImage;

    [SerializeField]
    Toggle[] toggles;

    [SerializeField]
    Sprite defaultFriend;
    [SerializeField]
    Sprite NextFriend;
    [SerializeField]
    Image FriendImage;

    int nowDiary = 0;
    int GetDream = 0;
    int num = 0;
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
        UpdateDiary(0);
        ButtonSet();
        UpdateBar();
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

    public void goBack()
    {
        GameManager.Instance.goMain();
    }

    public void LookDream()
    {
        if (DreamPanel.activeSelf)
        {
            return;
        }
        DiaryPanel.SetActive(false);
        DreamPanel.SetActive(true);
    }

    public void LookDiary()
    {
        if (DiaryPanel.activeSelf)
        {
            return;
        }
        DreamPanel.SetActive(false);
        DiaryPanel.SetActive(true);

        for(int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                UpdateDiary(i);
            }
        }
    }

    public void UpdateDiary(int a)
    {
        nowDiary = a;
        if (DiaryPanel.activeSelf)
        {
            getsound();
        }
        if (GameManager.Instance.user.DiaryGet[a])
        {
            string diaryEx = diary[a].diaryEx1.Replace("000", GameManager.Instance.user.name);
            DiaryImage.sprite = diary[a].diaryImage;
            DiaryTitle.text = "제목 : " + diary[a].diaryTitle;
            DiaryWeather.text = "날씨 : " + diary[a].weather;
            Diaryfeel.text = "기분 : " + diary[a].feel;
            DiaryText.text = diaryEx;
        }
        else
        {
            DiaryImage.sprite = null;
            DiaryTitle.text = "제목 : ?";
            DiaryWeather.text = "날씨 : ?";
            Diaryfeel.text = "기분 : ?";
            DiaryText.text = "비어있음";
        }
        
    }

    public void getsound()
    {
        GetComponent<AudioSource>().volume = GameManager.Instance.user.effect;
        GetComponent<AudioSource>().Play();
    }
    public void ButtonSet()
    {
        for(int i = 0; i < Buttons.Length; i++)
        {
            if (GameManager.Instance.user.DiaryGet[i])
            {
                Buttons[i].gameObject.transform.GetChild(0).GetComponent<Text>().text = "선물완료";
                Buttons[i].GetComponent<Image>().color = new Color32(0, 0, 255, 255);
            }
            else if (GameManager.Instance.user.DreamGet[i])
            {
                Buttons[i].GetComponent<Image>().color = new Color32(243, 155, 155, 255);
            }
            else
            {
                Buttons[i].GetComponent<Image>().color = new Color32(114, 114, 114, 255);
            }

        }
        
    }

    public void DreamGift(int a)
    {
        if (GameManager.Instance.user.DreamGet[a] && !GameManager.Instance.user.DiaryGet[a])
        {
            GameManager.Instance.SaveData();
            GetPanel(a);
            ButtonSet();
            UpdateBar();
        }
    }

    public void GetPanel(int a)
    {
        num = a;
        Gift1Panel.SetActive(true);
        giftText.text = $"{diary[num].DreamName} 꿈을 선물하시겠습니까?";
        
    }
    //선물했습니다.\n{diary[a].diaryTitle} 그림일기를 선물받았습니다.

    public void Canclediary()
    {
        Gift1Panel.SetActive(false);
    }
    public void GetNextPanel()
    {
        Gift1Panel.SetActive(false);
        Gift2Panel.SetActive(true);
    }

    public void GetEndPanel()
    {
        Gift2Panel.SetActive(false);
        Gift3Panel.SetActive(true);
        DreamText.text = $"{diary[num].diaryTitle} 그림일기를\n 선물받았습니다..";
        GameManager.Instance.user.DiaryGet[num] = true;
    }

    public void ExitPanel()
    {
        Gift3Panel.SetActive(false);
    }

    public void UpdateBar()
    {
        GetDream = 0;
        for (int i = 0; i < GameManager.Instance.user.DiaryGet.Length; i++)
        {
            if (GameManager.Instance.user.DiaryGet[i])
            {
                GetDream++;
            }
        }

        giftbarText.text = (GetDream / (float)GameManager.Instance.user.DiaryGet.Length * 100f).ToString("F0") + "%";
        GiftImage.fillAmount = GetDream / (float)GameManager.Instance.user.DiaryGet.Length;
        if(GiftImage.fillAmount < 0.1f)
        {
            FriendImage.sprite = defaultFriend;
        }
        else
        {
            FriendImage.sprite = NextFriend;
        }
    }

    public void GoShop()
    {
        GameManager.Instance.GoShop();
    }

    public void NextText()
    {
        if (GameManager.Instance.user.DiaryGet[nowDiary])
        {
            GetComponent<AudioSource>().volume = GameManager.Instance.user.effect;
            GetComponent<AudioSource>().Play();
            string diaryEx = diary[nowDiary].diaryEx2.Replace("000", GameManager.Instance.user.name);
            DiaryText.text = diaryEx;
        }

    }

    public void backText()
    {
        if (GameManager.Instance.user.DiaryGet[nowDiary])
        {
            GetComponent<AudioSource>().volume = GameManager.Instance.user.effect;
            GetComponent<AudioSource>().Play();
            string diaryEx = diary[nowDiary].diaryEx1.Replace("000", GameManager.Instance.user.name);
            DiaryText.text = diaryEx;
        }
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
