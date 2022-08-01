using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    [SerializeField] Image Icon;
    [SerializeField] Text ItemName;
    [SerializeField] Text effect;
    [SerializeField] Text costText;

    public Item item;
    bool getItem;
    // Start is called before the first frame update

    // Update is called once per frame
    public void Setup(Item item)
    {
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        this.item = item;
        getItem = false;
        print(item.Icon + ", " + item.name + ", " + item.type + ", " + item.num + ", " + item.unit + ", " + item.cost + ", " + item.percent);

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
            costText.text = "구입완료";
        }
        else
        {
            Debug.Log("당근이 부족합니다");
        }
    }
}
