using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FriendManager : MonoBehaviour
{
    [SerializeField]
    GameObject DreamPanel;
    [SerializeField]
    GameObject DiaryPanel;
    [SerializeField]
    Text CarrotText;

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

    // Start is called before the first frame update
    void Start()
    {
        CarrotText.text = GameManager.Instance.user.carrot.ToString();
        UpdateDiary(0);
        ButtonSet();
        UpdateBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        giftText.text = $"{diary[a].DreamName} 꿈을 선물했습니다.\n{diary[a].diaryTitle} 그림일기를 선물받았습니다.";
        
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
}
