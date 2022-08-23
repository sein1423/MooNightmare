using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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


    private void Start()
    {
        //AS = GetComponent<AudioSource>();
        textObj.text = "";
        Typing(dialogStrings, num, textObj);
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
        isDialogEnd = false;
        dialogsSave = dialogs;
        tmpSave = textobj;
        if (dialogNumber < dialogs.Length)
        {
            if(num[dialogNumber] == 1)
            {
                NameBar.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.user.name;
                CharacterImage.sprite = RabbitSprite;
            }
            else
            {
                NameBar.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "æ∆¿Ã";
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
}
