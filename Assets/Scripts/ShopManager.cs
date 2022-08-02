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
                DreamName = "������ ���ݸ� ����";
                break;
            case 1:
                DreamName = "���ø�ŭ�� ���� �����̾�!";
                break;
            case 2:
                DreamName = "�ູ�� �䳢����";
                break;
            case 3:
                DreamName = "���� �ٴ� ���� ģ��";
                break;
            case 4:
                DreamName = "�ϴ��� ���ư�����!";
                break;
            case 5:
                DreamName = "���� ������ ģ��";
                break;
        }

        DreamText.text = $"\"{DreamName}\" ���� ȹ���߽��ϴ�.\n���� �����Ϸ� ���ðڽ��ϱ�?";
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
                GetChild(0).GetComponent<Text>().text = "���ԿϷ�";
            }
            else
            {
                DreamItem[i].transform.GetChild(1).gameObject.transform.
                GetChild(0).GetComponent<Text>().text = "����";
            }
            
        }
        
    }

    public void GoFriend()
    {
        GameManager.Instance.GoMyFriend();
    }
}
