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
}
