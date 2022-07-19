using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }

    [SerializeField] ItemSO itemSO;
    List<Item> itemBuffer;
    private void Awake()
    {
        Instance = this;
    }
    
    void SetupItemBuffer()
    {
        itemBuffer = new List<Item>();

        for(int i = 0; i < itemSO.items.Length; i++)
        {
            Item item = itemSO.items[i];
            for(int j = 0; j < item.percent; j++)
                itemBuffer.Add(item);
        }

        for(int i = 0; i < itemBuffer.Count; i++)
        {
            int rand = Random.Range(i, itemBuffer.Count);
            Item temp = itemBuffer[i];
            itemBuffer[i] = itemBuffer[rand];
            itemBuffer[rand] = temp;
        }
    }

    public Item PopItem()
    {
        if(itemBuffer.Count == 0)
            SetupItemBuffer();

        Item item = itemBuffer[0];
        itemBuffer.RemoveAt(0);
        return item;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
            print(PopItem());
    }
}
