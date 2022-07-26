using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroManager : MonoBehaviour
{
    #region ����
    public static IntroManager Instance;
    [SerializeField] Text userName;
    [SerializeField] GameObject NamePanel;

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

        NamePanel.SetActive(false);
    }
    // Start is called before the first frame update
    public void CompleteName()
    {
        GameManager.Instance.SetUserData(userName.text);
        NamePanel.SetActive(false);
        Typing(dialogsSave, tmpSave);
    }
    public void CancleName()
    {
        userName.text = "";
    }

    public void Typing(string[] dialogs, TextMeshProUGUI textobj)
    {
        if(ClickCount == 7)
        {
            dialogs[6] = dialogs[6].Replace("Username", GameManager.Instance.user.name);
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
                if(ClickCount == 6)
                {
                    isTypingEnd = false;
                    GetNickName();
                }
                else if(ClickCount == 8)
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
        isTypingEnd = true;
        ClickCount++;
    }
}