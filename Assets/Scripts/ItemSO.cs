using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="MyScriptable")]
public class ItemSO : ScriptableObject
{
    [SerializeField]
    private string itemName;
    public string ItemName { get { return ItemName; } }

    [SerializeField]
    private ItemType itemType;
    public ItemType ItemType { get { return itemType; } }

    [SerializeField]
    private int itemNum;
    public int ItemNum { get { return itemNum; } }
}
