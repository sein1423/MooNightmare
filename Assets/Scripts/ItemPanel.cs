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
        this.item = item;
        getItem = false;
        print(item.Icon + ", " + item.name + ", " + item.type + ", " + item.num + ", " + item.unit + ", " + item.cost + ", " + item.percent);

        //Icon = item.Icon;
        ItemName.text = item.name;
        if(item.unit == 1)
        {
            effect.text = item.type.ToString() + " +" + item.num.ToString();
        }
        else
        {
            effect.text = item.type.ToString() + " +" + item.num.ToString()+"%";
        }
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
        }
        else
        {
            Debug.Log("당근이 부족합니다");
        }
    }
}
