using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIO : MonoBehaviour
{
    float time = 0;
    bool isFadeOut = false;
    bool isStop = false;
    float _fadeTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        resetAnim();
    }

    // Update is called once per frame
    void Update()
    {

        if (isStop)
        {
            if(time > 2f)
            {
                isFadeOut = true;
                isStop = false;
                time = 0;
            }
            time += Time.deltaTime;
            return;
        }

        if (isFadeOut)
        {
            if(time < _fadeTime)
            {
                GetComponent<Image>().color = new Color(1, 1, 1, 1 - time / _fadeTime);
            }
            else
            {
                time = 0;
                FriendManager.Instance.GetEndPanel();
                Destroy(gameObject);
            }
            time += Time.deltaTime;
        }
        else
        {
            
                if (time < 1f)
                {
                    GetComponent<Image>().color = new Color(1, 1, 1, time / 1);
                }
                else
                {
                    time = 0;
                    isStop = true;
                }
                time += Time.deltaTime;
        }
    }

    public void resetAnim()
    {
        GetComponent<Image>().color = new Color(1, 1, 1, 0);
        gameObject.SetActive(true);
        time = 0;
    }
}
