using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class EndingManager : MonoBehaviour
{

    [Header("Dialogs")]

    [TextArea]
    public string[] dialogStrings;
    //AudioSource AS;
    public int[] num;
    public TextMeshProUGUI textObj;

    public static bool isDialogEnd;
    string[] dialogsSave;
    TextMeshProUGUI tmpSave;
    int dialogNumber = 0;
    bool isTypingEnd = false;
    float timer;
    float characterTime;

    [SerializeField] Image CharacterImage;
    [SerializeField] Sprite RabbitSprite;
    [SerializeField] Sprite HumanSprite;

    [SerializeField] GameObject NameBar;

    [Header("Timess for each character")]
    public float timeForCharacter;

    [Header("Times for each character when speed up")]
    public float timeForCharacter_Fast;
    [SerializeField] Transform Arrow;
    float time = 0f;
    [SerializeField]
    Sprite[] EndingImage;
    [SerializeField]
    Image EndingBackGround;


    private void Start()
    {
        //AS = GetComponent<AudioSource>();
        textObj.text = "";
        Typing(dialogStrings, num, textObj);
        NameBar.SetActive(false);
        CharacterImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if((int)time % 2 == 1)
        {
            Arrow.transform.Translate(0, -0.05f, 0);
        }
        else
        {
            Arrow.transform.Translate(0, 0.05f, 0);
        }
        time += Time.deltaTime;
    }

    public void Typing(string[] dialogs, int[] num, TextMeshProUGUI textobj)
    {
        dialogs[dialogNumber] = dialogs[dialogNumber].Replace("00", "�䲤��");
        isDialogEnd = false;
        dialogsSave = dialogs;
        tmpSave = textobj;
        Debug.Log(dialogNumber);
        ChangeBackGround(dialogNumber);
        if (dialogNumber < dialogs.Length)
        {
            if (num[dialogNumber] == 2)
            {
                NameBar.SetActive(false);
                CharacterImage.gameObject.SetActive(false);
            }
            if(num[dialogNumber] == 1)
            {
                NameBar.SetActive(true);
                CharacterImage.gameObject.SetActive(true);
                NameBar.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "�䳢";
                CharacterImage.sprite = RabbitSprite;
            }
            else
            {
                NameBar.SetActive(true);
                CharacterImage.gameObject.SetActive(true);
                NameBar.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "����";
                CharacterImage.sprite = HumanSprite;
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

    private void ChangeBackGround(int dialogNumber)
    {
        switch (dialogNumber)
        {
            case 2:
                EndingBackGround.sprite = EndingImage[5];
                break;
            case 3:
                EndingBackGround.sprite = EndingImage[0];
                break;
            case 4:
            case 16:
                EndingBackGround.sprite = EndingImage[1];
                break;
            case 15:
            case 17:
                EndingBackGround.sprite = EndingImage[3];
                break;
            case 20:
                EndingBackGround.sprite = EndingImage[6];
                break;
            case 21:
                EndingBackGround.sprite = EndingImage[7];
                break;
            case 22:
                EndingBackGround.sprite = EndingImage[4];
                break;
            case 23:
                EndingBackGround.sprite = EndingImage[8];
                break;
        }
    }

    IEnumerator Typer(char[] chars, TextMeshProUGUI textObj)
    {
        //AS.Play();
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
                textObj.text += chars[currentChar].ToString();
                currentChar++;
                timer = characterTime;
            }
        }
        if (currentChar >= charLength)
        {
            isTypingEnd = true;
            //AS.Stop();
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
                if(dialogNumber >= dialogStrings.Length)
                {
                    GameManager.Instance.goMain();
                }
                Typing(dialogsSave, num, tmpSave);
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

    public void Skip()
    {
        GameManager.Instance.goMain();
    }
}
