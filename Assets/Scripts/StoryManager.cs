using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{

    [Header("Dialogs")]

    [TextArea]
    public string[] dialogStrings;
    AudioSource AS;
    public int[] num;
    /*public GameObject textbox1;
    public GameObject textbox2;*/
    public GameObject[] textbox;
    public TextMeshProUGUI textObj;

    public static bool isDialogEnd;
    string[] dialogsSave;
    TextMeshProUGUI tmpSave;
    RectTransform textboxtransform;
    int dialogNumber = 0;
    bool isTypingEnd = false;
    float timer; 
    float characterTime;
    public GameObject Canvas;
    [SerializeField] Scrollbar scroll;

    [Header("Timess for each character")]
    public float timeForCharacter;

    [Header("Times for each character when speed up")]
    public float timeForCharacter_Fast;


    private void Start()
    {
        AS = GetComponent<AudioSource>();
        textObj.text = "";
        Typing(dialogStrings, num, textObj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Typing(string[] dialogs,int[] num ,TextMeshProUGUI textobj)
    {
        isDialogEnd = false;
        dialogsSave = dialogs;
        tmpSave = textobj;
        if (dialogNumber < dialogs.Length)
        {
            /*if(num[dialogNumber] == 1)
            {
                textobj = textbox1.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            }
            else if (num[dialogNumber] == 2)
            {
                textobj = textbox2.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            }*/
            if(dialogNumber > 0)
            {
                textbox[dialogNumber-1].SetActive(true); 
                textobj = textbox[dialogNumber-1].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                Canvas.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100 + (300 * dialogNumber - 1));
                
            }
            char[] chars = dialogs[dialogNumber].ToCharArray();
            StartCoroutine(Typer(chars, textobj));
        }
        else
        {
            tmpSave.text = "";
            isDialogEnd = true;
            dialogsSave = null;
            tmpSave = null;
            dialogNumber = 0;
        }
    }

    IEnumerator Typer(char[] chars, TextMeshProUGUI textObj)
    {
        AS.Play();
        int currentChar = 0;
        int charLength = chars.Length;
        textObj.text = "";
        isTypingEnd = false;

        while (currentChar < charLength)
        {
            if (timer >= 0)
            {
                yield return null;
                timer -= Time.deltaTime;
            }
            else
            {
                scroll.value = 0f;
                textObj.text += chars[currentChar].ToString();
                currentChar++;
                timer = characterTime;
            }
        }
        if (currentChar >= charLength)
        {
            isTypingEnd = true;
            AS.Stop();
            dialogNumber++;
            yield break;
        }
    }

    public void GetInputDown()
    {
        if (dialogsSave != null)
        {
            if (isTypingEnd)
            {
                if(dialogNumber > 8)
                {
                    GameManager.Instance.GoIntro();
                }
                Typing(dialogsSave,num , tmpSave);
            }
            else
            {
                characterTime = timeForCharacter_Fast;
            }
        }
    }

    public void GetInputUp()
    {
        if (dialogsSave != null)
        {
            characterTime = timeForCharacter;
        }
    }
}
