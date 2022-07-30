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
    Toggle[] toggles;

    // Start is called before the first frame update
    void Start()
    {
        CarrotText.text = GameManager.Instance.user.carrot.ToString();
        UpdateDiary(0); 
        ButtonSet();
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
            string diaryEx = diary[a].diaryEx.Replace("Username", GameManager.Instance.user.name);
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
                Buttons[i].GetComponent<Image>().color = new Color32(243, 155, 155, 255);
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
}
