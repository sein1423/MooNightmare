using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DreamPanel : MonoBehaviour
{
    [SerializeField] DreamSO dream;
    [SerializeField] GameObject dreamIcon;
    [SerializeField] GameObject dreamText;
    [SerializeField] GameObject dreamCostText;
    [SerializeField] GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        dreamIcon.GetComponent<Image>().sprite = dream.dreamImage;
        dreamText.GetComponent<Text>().text = dream.dreamEx;
        dreamCostText.GetComponent<Text>().text = dream.cost.ToString();
        SetButtonText();
    }

    public void GetDream()
    {
        if (GameManager.Instance.user.DreamGet[dream.dreamnum])
        {
            return;
        }

        if (GameManager.Instance.user.carrot < dream.cost)
        {
            return;
        }

        GameManager.Instance.user.carrot -= dream.cost;
        GameManager.Instance.user.DreamGet[dream.dreamnum] = true;
        SetButtonText();
        ShopManager.Instance.CarrotText.text = GameManager.Instance.user.carrot.ToString();
        GameManager.Instance.SaveData();
        Dreampp();
    }

    public void Dreampp()
    {
        ShopManager.Instance.Panel.SetActive(true);
        string DreamName = dream.dreamTitle;

        ShopManager.Instance.DreamText.text = $"\"{DreamName}\" 꿈을 획득했습니다.\n꿈을 선물하러 가시겠습니까?";
    }

    public void SetButtonText()
    {
        if (GameManager.Instance.user.DreamGet[dream.dreamnum])
        {
            button.transform.GetChild(0).GetComponent<Text>().text = "구입완료";
        }
        else
        {
            button.transform.GetChild(0).GetComponent<Text>().text = "구입";
        }

    }
}
