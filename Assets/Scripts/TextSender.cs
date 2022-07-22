using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TextSender : MonoBehaviour
{
    [Header("Dialogs")]

    [TextArea]
    public string[] dialogStrings;
    public TextMeshProUGUI textObj;


    

    private void Start()
    {
        IntroManager.Instance.Typing(dialogStrings, textObj);
    }
}
