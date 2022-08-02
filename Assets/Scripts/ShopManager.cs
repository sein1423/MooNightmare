using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] DreamItem;
    int DreamCost = 2500;
    [SerializeField] Text CarrotText;
    [SerializeField] GameObject Panel;
    [SerializeField] Text DreamText;
    void Start()
    {
        CarrotText.text = GameManager.Instance.user.carrot.ToString();
        SetButtonText();
    }

    // Update is called once per frame
    public void GetDream(int a)
    {
        if (GameManager.Instance.user.DreamGet[a])
        {
            return;
        }

        if(GameManager.Instance.user.carrot < DreamCost)
        {
            return;
        }

        GameManager.Instance.user.carrot -= DreamCost;
        GameManager.Instance.user.DreamGet[a] = true;
        SetButtonText();
        CarrotText.text = GameManager.Instance.user.carrot.ToString();
        GameManager.Instance.SaveData();
        DreamPanel(a);
    }

    public void DreamPanel(int a)
    {
        Panel.SetActive(true);
        string DreamName = "";

        switch (a) 
        {
            case 0:
                DreamName = "달콤한 초콜릿 동산";
                break;
            case 1:
                DreamName = "오늘만큼은 내가 영웅이야!";
                break;
            case 2:
                DreamName = "행복한 토끼랜드";
                break;
            case 3:
                DreamName = "구름 바다 돌고래 친구";
                break;
            case 4:
                DreamName = "하늘을 날아가보자!";
                break;
            case 5:
                DreamName = "나의 소중한 친구";
                break;
        }

        DreamText.text = $"\"{DreamName}\" 꿈을 획득했습니다.\n꿈을 선물하러 가시겠습니까?";
    }

    public void BreakPanel()
    {
        Panel.SetActive(false);
    }

    public void GoMain()
    {
        GameManager.Instance.goMain();
    }

    public void SetButtonText()
    {
        for(int i = 0; i < DreamItem.Length; i++)
        {
            if (GameManager.Instance.user.DreamGet[i])
            {
                DreamItem[i].transform.GetChild(1).gameObject.transform.
                GetChild(0).GetComponent<Text>().text = "구입완료";
            }
            else
            {
                DreamItem[i].transform.GetChild(1).gameObject.transform.
                GetChild(0).GetComponent<Text>().text = "구입";
            }
            
        }
        
    }

    public void GoFriend()
    {
        GameManager.Instance.GoMyFriend();
    }
}
