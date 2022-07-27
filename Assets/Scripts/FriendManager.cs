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


    // Start is called before the first frame update
    void Start()
    {
        CarrotText.text = GameManager.Instance.user.carrot.ToString();
        UpdateDiary(0);
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
    }

    public void UpdateDiary(int a)
    {
        string diaryEx = diary[a].diaryEx.Replace("Username", GameManager.Instance.user.name);
        DiaryImage.sprite = diary[a].diaryImage;
        DiaryTitle.text = "제목 : " + diary[a].diaryTitle;
        DiaryWeather.text = "날씨 : " + diary[a].weather;
        Diaryfeel.text = "기분 : " + diary[a].feel;
        DiaryText.text = diaryEx;
    }

    public void ButtonSet()
    {//new Color(114, 114, 114, 255); new Color(243, 155, 155, 255);
        for(int i = 0; i < Buttons.Length; i++)
        {
            if (diary[i].Get)
            {
                Buttons[0].GetComponent<Image>().color = new Color(114, 114, 114, 255);

            }

        }
        Buttons[1].GetComponent<Image>().color = new Color(243, 155, 155, 255);
    }
}
