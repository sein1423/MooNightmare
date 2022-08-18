using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    [SerializeField] Sprite[] Icon;
    [SerializeField] Text ItemName;
    [SerializeField] Text effect;
    [SerializeField] Text costText;
    [SerializeField] GameObject CostText;
    [SerializeField] GameObject comText;

    public Item item;
    bool getItem;
    // Start is called before the first frame update

    // Update is called once per frame
    public void Setup(Item item)
    {
        comText.SetActive(false);
        CostText.SetActive(true);
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Icon[item.Icon];
        gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        this.item = item;
        getItem = false;

        //Icon = item.Icon;
        ItemName.text = item.name;
        
        effect.text = item.Text;
        
        costText.text = item.cost.ToString();
    }

    public void GetItem()
    {
        if (getItem)
        {
            return;
        }

        if (ItemManager.Instance.GetCarrot >= item.cost)
        {
            ItemManager.Instance.GetItem(item);
            getItem = true;
            gameObject.GetComponent<Image>().color = new Color32(144, 144, 144, 255);
            gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(144, 144, 144, 255);
            comText.SetActive(true);
            CostText.SetActive(false);
            GetComponent<AudioSource>().volume = GameManager.Instance.user.effect;
            GetComponent<AudioSource>().Play();
        }
        else
        {
            Debug.Log("당근이 부족합니다");
        }
    }
}
