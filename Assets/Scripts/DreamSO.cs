using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/DreamSO")]
public class DreamSO : ScriptableObject
{
    public int dreamnum;
    public Sprite dreamImage;
    public string dreamTitle;
    [TextArea]
    public string dreamEx;
    public int cost;
}