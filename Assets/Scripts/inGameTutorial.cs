using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inGameTutorial : MonoBehaviour
{
    [SerializeField] GameObject[] tutorialPannel;
    int count = 0;

    public void NextTutorial()
    {
        if(count >= 3)
        {
            return;
        }
        tutorialPannel[count++].SetActive(false);
        tutorialPannel[count].SetActive(true);
    }

    public void BackTutorial()
    {
        if (count <= 0)
        {
            return;
        }
        tutorialPannel[count--].SetActive(false);
        tutorialPannel[count].SetActive(true);
    }

    public void ExitThisTutorial()
    {
        ItemManager.Instance.inGametutorial.SetActive(true);
        gameObject.SetActive(false);
    }
}
