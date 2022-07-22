using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public enum ItemType
{
    AttackPower,
    AttackRange,
    AttackCoolTime,
    Health,
    MoveSpeed,
    ActiveSkill,
    CriticalDamage,
    CriticalPercent,
    AttackCount,
    Attackdirection
}
public class Item
{
    public Sprite Icon;
    public string name;
    public ItemType type;
    public float num;
    public int unit; //1 : 고정, 2 : 퍼센트
    public float cost;
    public float percent;

    public void SetIndex(string str, int x)
    {
        switch (x)
        {
            case 0:
                //this.Icon = str;
                Icon = null;
                break;
            case 1:
                this.name = str;
                break;
            case 2:
                SetItemType(str);
                break;
            case 3:
                this.num = float.Parse(str);
                break;
            case 4:
                this.unit = int.Parse(str);
                break;
            case 5:
                this.cost = float.Parse(str);
                break;
            case 6:
                this.percent = float.Parse(str);
                break;
        }
    }

    public void SetItemType(string str)
    {
        ItemType t = (ItemType)Enum.Parse(typeof(ItemType), str);
        //Debug.Log(t.ToString());
        this.type = t;
    }
}


public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    const string Link = "https://docs.google.com/spreadsheets/d/1rd4km6B1GyLqJWLXRQdl16TKyV95esAXdV5L-w5AaaM/export?format=tsv&range=A2:G";
    public List<Item> itemBuffer = new List<Item>();
    [SerializeField] ItemPanel itemp1;
    [SerializeField] ItemPanel itemp2;
    [SerializeField] ItemPanel itemp3;
    [SerializeField] GameObject waveshop;
    [SerializeField] Text timeText;
    //public int itemcount;

    [SerializeField] GameObject[] Heart;
    public int playerhealth = 5;

    [SerializeField] float waveTime = 30f;
    float time = 0f;

    private void Awake()
    {
        Instance = this;
        SetHeart();
    }


    IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get(Link);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        SetItemSO(data);
        //print(data);
    }


    void SetItemSO(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;
        //itemcount = rowSize;
        int columnSize = row[0].Split('\t').Length;
        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');
            Item item = new Item();

            for (int j = 0; j < columnSize; j++)
            {
                item.SetIndex(column[j], j);
            }

            for (int num = 0; num < item.percent; num++)
            {
                itemBuffer.Add(item);
            }
        }

        suffleBuffer();
    }

    void suffleBuffer()
    {

        for (int i = 0; i < itemBuffer.Count; i++)
        {
            int rand = UnityEngine.Random.Range(i, itemBuffer.Count);
            Item temp = itemBuffer[i];
            itemBuffer[i] = itemBuffer[rand];
            itemBuffer[rand] = temp;
        }

    }
    public Item PopItem()
    {
        if (itemBuffer.Count < 10)
        {
            StartCoroutine(Start());
        }

        Item item = itemBuffer[0];
        itemBuffer.RemoveAt(0);
        return item;
    }

    private void Update()
    {
        if (!waveshop.activeSelf)
        {
            time += Time.deltaTime;
            timeText.text = (waveTime - time).ToString();
        }

        if(waveTime < time && !waveshop.activeSelf)
        {
            GetItemShop();
        }
    }

    public void GetItemShop()
    {
        Reroll();
        waveshop.SetActive(true);
    }

    public void ExitShop()
    {
        waveshop.SetActive(false);
        time = 0f;
    }

    public void Reroll()
    {
        Item item1 = PopItem();
        itemp1.Setup(item1);
        //print(item1.Icon + ", " + item1.name + ", " + item1.type + ", " + item1.num + ", " + item1.unit + ", " + item1.cost + ", " + item1.percent);
        Item item2 = PopItem();
        itemp2.Setup(item2);
        //print(item2.Icon + ", " + item2.name + ", " + item2.type + ", " + item2.num + ", " + item2.unit + ", " + item2.cost + ", " + item2.percent);
        Item item3 = PopItem();
        itemp3.Setup(item3);
        //print(item3.Icon + ", " + item3.name + ", " + item3.type + ", " + item3.num + ", " + item3.unit + ", " + item3.cost + ", " + item3.percent);
    }

    public void SetHeart()
    {
        for(int i = 0; i < playerhealth; i++)
        {
            Heart[i].SetActive(true);
        }

        for(int i = playerhealth; i < Heart.Length; i++)
        {
            Heart[i].SetActive(false);
        }
    }
}
