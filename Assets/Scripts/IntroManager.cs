using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroManager : MonoBehaviour
{
    #region º¯¼ö
    public static IntroManager Instance;
    [SerializeField] Text userName;
    [SerializeField] GameObject NamePanel;
    [SerializeField] GameObject text;
    [SerializeField] GameObject SkipButton;
    AudioSource AS;
    [Header("Timess for each character")]
    public float timeForCharacter;

    [Header("Times for each character when speed up")]
    public float timeForCharacter_Fast;

    float characterTime;
    string[] dialogsSave;
    TextMeshProUGUI tmpSave;

    public static bool isDialogEnd;

    bool isTypingEnd = false;
    int dialogNumber = 0;
    bool skip = false;

    float timer;
    int ClickCount = 0;
    #endregion

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        timer = timeForCharacter;
        characterTime = timeForCharacter;
        AS = GetComponent<AudioSource>();
        NamePanel.SetActive(false);
    }
    // Start is called before the first frame update
    public void CompleteName()
    {
        if(userName.text.Trim() == "")
        {
            GameManager.Instance.SetUserData("Åä³¢");
        }
        else
        {
            GameManager.Instance.SetUserData(userName.text.Trim());
        }
        if (skip)
        {
            GameManager.Instance.goMain();
        }
        NamePanel.SetActive(false);
        SkipButton.SetActive(true);
        Typing(dialogsSave, tmpSave);
    }
    public void CancleName()
    {
        userName.text = "";
    }

    public void Typing(string[] dialogs, TextMeshProUGUI textobj)
    {
        if(ClickCount == 6)
        {
            dialogs[5] = dialogs[5].Replace("Username", GameManager.Instance.user.name);
        }
        isDialogEnd = false;
        dialogsSave = dialogs;
        tmpSave = textobj;
        if(dialogNumber < dialogs.Length)
        {
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
        int currentChar = 0;
        int charLength = chars.Length;
        isTypingEnd = false;
        AS.Play();

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
            AS.Stop();
            dialogNumber++;
            yield break;
        }
    }

    public void GetInputDown()
    {
        if(dialogsSave != null)
        {
            if (isTypingEnd)
            {
                tmpSave.text = "";
                ClickCount++;
                if(ClickCount == 5)
                {
                    if(GameManager.Instance.user.name == "")
                    {
                        isTypingEnd = false;
                        GetNickName();
                    }
                    else
                    {
                        isTypingEnd = true;
                        ClickCount++;
                        Typing(dialogsSave, tmpSave);
                    }
                }
                else if(ClickCount == 7)
                {
                    GameManager.Instance.goMain();
                }
                else
                {
                    Typing(dialogsSave, tmpSave);
                }
            }
            else
            {
                characterTime = timeForCharacter_Fast;
            }
        }
    }

    public void GetInputUp()
    {
        if(dialogsSave != null)
        {
            characterTime = timeForCharacter;
        }
    }

    void GetNickName()
    {
        NamePanel.SetActive(true);
        SkipButton.SetActive(false);
        isTypingEnd = true;
        ClickCount++;
    }

    public void Skip()
    {
        if (!(GameManager.Instance.user.name == ""))
        {
            GameManager.Instance.goMain();
        }
            if (ClickCount < 5)
        {
            skip = true;
            ClickCount = 4;
            text.SetActive(false);
            isTypingEnd = true;
            GetInputDown();
        }
        else
        {
            GameManager.Instance.goMain();
        }
    }
}
