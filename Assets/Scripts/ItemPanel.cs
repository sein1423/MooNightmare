using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    Sprite Icon;
    Text ItemName;
    Text effect;

    public Item item;
    // Start is called before the first frame update
    void Start()
    {
        Icon = gameObject.transform.GetChild(0).GetComponent<Sprite>();
        ItemName = gameObject.transform.GetChild(1).GetComponent<Text>();
        effect = gameObject.transform.GetChild(2).GetComponent<Text>();
    }

    // Update is called once per frame
    public void Setup(Item item)
    {
        this.item = item;

        Icon = item.Icon;
        ItemName.text = item.name;
        switch (item.type)
        {
            /*case ItemType.AttackPower:
                effect.text = "공격력 증가 +" + (item.num);*/
        }
    }
}
