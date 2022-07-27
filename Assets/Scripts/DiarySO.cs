using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/DiarySO")]
public class DiarySO : ScriptableObject
{
    public Sprite diaryImage;
    public string diaryTitle;
    public string weather;
    public string feel;
    [TextArea]
    public string diaryEx;
    public bool Get;
}