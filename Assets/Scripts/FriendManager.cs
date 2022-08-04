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
    GameObject GiftPanel;

    [SerializeField]
    Text giftText;
    [SerializeField]
    Text giftbarText;
    [SerializeField]
    Image GiftImage;

    [SerializeField]
    Toggle[] toggles;

    int nowDiary = 0;
    int GetDream = 0;
    #region �� ����
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
    #region ��
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
        if (GameManager.Instance.user.DiaryGet[a])
        {
            nowDiary = a;
            string diaryEx = diary[a].diaryEx1.Replace("000", GameManager.Instance.user.name);
            DiaryImage.sprite = diary[a].diaryImage;
            DiaryTitle.text = "���� : " + diary[a].diaryTitle;
            DiaryWeather.text = "���� : " + diary[a].weather;
            Diaryfeel.text = "��� : " + diary[a].feel;
            DiaryText.text = diaryEx;
        }
        else
        {
            DiaryImage.sprite = null;
            DiaryTitle.text = "���� : ?";
            DiaryWeather.text = "���� : ?";
            Diaryfeel.text = "��� : ?";
            DiaryText.text = "�������";
        }
        
    }

    public void ButtonSet()
    {
        for(int i = 0; i < Buttons.Length; i++)
        {
            if (GameManager.Instance.user.DiaryGet[i])
            {
                Buttons[i].gameObject.transform.GetChild(0).GetComponent<Text>().text = "�����Ϸ�";
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
            GameManager.Instance.user.DiaryGet[a] = true;
            GameManager.Instance.SaveData();
            GetPanel(a);
            ButtonSet();
            UpdateBar();
        }
    }

    public void GetPanel(int a)
    {
        GiftPanel.SetActive(true);
        giftText.text = $"{diary[a].DreamName} ���� �����߽��ϴ�.\n{diary[a].diaryTitle} �׸��ϱ⸦ �����޾ҽ��ϴ�.";
        
    }

    public void ExitPanel(GameObject go)
    {
        GiftPanel.SetActive(false);
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
    }

    public void GoShop()
    {
        GameManager.Instance.GoShop();
    }

    public void NextText()
    {
        if (GameManager.Instance.user.DiaryGet[nowDiary])
        {
            string diaryEx = diary[nowDiary].diaryEx2.Replace("000", GameManager.Instance.user.name);
            DiaryText.text = diaryEx;
        }

    }

    public void backText()
    {
        if (GameManager.Instance.user.DiaryGet[nowDiary])
        {
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
