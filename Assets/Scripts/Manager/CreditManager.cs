using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditManager : MonoBehaviour
{
    public GameObject[] Diarys;
    byte alpha = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndingCredit());
    }

    public IEnumerator EndingCredit()
    {
        for (int i = 0; i < Diarys.Length; i++)
        {

            Debug.Log(i);
            Diarys[i].gameObject.SetActive(true);
            for (alpha = 0; alpha < 255; alpha += 5)
            {
                Diarys[i].GetComponent<Image>().color = new Color32(255, 255, 255, alpha);
                yield return new WaitForSeconds(0.03f);
            }
            Debug.Log("³¡");
        }
        yield return new WaitForSeconds(5f);
        GameManager.Instance.goToMain();
        yield break;
    }
}
