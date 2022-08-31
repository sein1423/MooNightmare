using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class FriendManager : MonoBehaviour
{
    public static FriendManager Instance;

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
    Sprite[] baby;
    [SerializeField]
    Image FriendImage;
    [SerializeField]
    GameObject DiaryNullImage;

    int nowDiary = 0;
    int GetDream = 0;
    int num = 0;
    float time = 0;
    bool fadein = false;
    float blinktime = 0f;
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

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

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

        if (GetDream == 6)
        {
            blink();
        }
    }

    void blink()
    {
        if (blinktime < 0.5f)
        {
            GiftImage.gameObject.transform.parent.GetComponent<Image>().color = new Color(0.92f, 0.90f, 0.51f, 1 - blinktime);
            GiftImage.color = new Color(0.92f, 0.90f, 0.51f, 1 - blinktime);
        }
        else
        {
            GiftImage.gameObject.transform.parent.GetComponent<Image>().color = new Color(0.92f, 0.90f, 0.51f, blinktime);
            GiftImage.color = new Color(0.92f, 0.90f, 0.51f, blinktime);
            if (blinktime > 1.0f)
            {
                blinktime = 0;
            }
        }

        blinktime += Time.deltaTime;
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
            if(!stars[n].activeSelf)
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
        DiaryNullImage.SetActive(false);
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
            DiaryNullImage.SetActive(false);
            string diaryEx = diary[a].diaryEx1.Replace("000", GameManager.Instance.user.name);
            DiaryImage.sprite = diary[a].diaryImage;
            DiaryTitle.text = "제목 : " + diary[a].diaryTitle;
            DiaryWeather.text = "날씨 : " + diary[a].weather;
            Diaryfeel.text = "기분 : " + diary[a].feel;
            DiaryText.text = diaryEx;
        }
        else
        {
            DiaryNullImage.SetActive(true);
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
                Buttons[i].GetComponent<Image>().color = new Color32(255, 230, 153, 255);
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
            GetPanel(a);
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
        Gift3Panel.SetActive(true);
        DreamText.text = $"{diary[num].diaryTitle} 그림일기를\n 선물받았습니다..";
        GameManager.Instance.user.DiaryGet[num] = true;
        ButtonSet();
        UpdateBar();
        GameManager.Instance.SaveData();
    }

    public void MakePanel(GameObject go)
    {
        Gift2Panel.SetActive(false);
        var GM = Instantiate(go);
        GM.transform.SetParent(GameObject.Find("Canvas").transform);
        GM.GetComponent<RectTransform>().position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
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

        switch (GetDream)
        {
            case 0:
                FriendImage.sprite = baby[GetDream];
                break;
            case 1:
                FriendImage.sprite = baby[GetDream];
                break;
            case 2:
                FriendImage.sprite = baby[GetDream];
                break;
            case 3:
                FriendImage.sprite = baby[GetDream];
                break;
            case 4:
                FriendImage.sprite = baby[GetDream];
                break;
            case 5:
                FriendImage.sprite = baby[GetDream];
                break;
            case 6:
                FriendImage.sprite = baby[GetDream];
                GiftImage.color = new Color(255, 255, 0, 255);
                GiftImage.gameObject.AddComponent<Button>().onClick.AddListener(delegate { LookEnding(); });
                giftbarText.text = "엔딩 보기";
                giftbarText.gameObject.AddComponent<Button>().onClick.AddListener(delegate { LookEnding(); });
                break;
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

    public void LookEnding()
    {
        GameManager.Instance.GoEnding();
    }

    public void BreakThisPanel(GameObject go)
    {
        go.SetActive(false);
    }
}
