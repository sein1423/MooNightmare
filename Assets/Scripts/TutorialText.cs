using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Text;
    [SerializeField, TextArea] string[] TextArea;
    int count;

    private void OnEnable()
    {
        count = 0;
        TextArea[count] = TextArea[count].Replace("¤·¤·", GameManager.Instance.user.name);
        Text.text = TextArea[count];
    }

    public void touch()
    {
        count++;
        
        if (SceneManager.GetActiveScene().name == "My Friend")
        {
            if(count > 3)
            {
                FriendManager.Instance.TopButton.transform.GetChild(0).GetComponent<Toggle>().isOn = false;
                FriendManager.Instance.TopButton.transform.GetChild(1).GetComponent<Toggle>().isOn = true;
                FriendManager.Instance.LookDiary();
            }
        }

        if(count >= TextArea.Length)
        {
            if (SceneManager.GetActiveScene().name == "My Friend")
            {
                FriendManager.Instance.TopButton.transform.GetChild(0).GetComponent<Toggle>().isOn = true;
                FriendManager.Instance.TopButton.transform.GetChild(1).GetComponent<Toggle>().isOn = false;
                FriendManager.Instance.LookDream();
                GameManager.Instance.user.FriendTutorial = true;
                GameManager.Instance.SaveData();
                gameObject.SetActive(false);
                return;
            }

            if(SceneManager.GetActiveScene().name == "Shop")
            {
                GameManager.Instance.user.ShopTutorial = true;
                GameManager.Instance.SaveData();
                gameObject.SetActive(false);
                return;
            }
        }
        Text.text = TextArea[count];
    }
}
