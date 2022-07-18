using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendManager : MonoBehaviour
{
    [SerializeField]
    GameObject Dream;
    [SerializeField]
    GameObject Diary;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goBack()
    {
        GameManager.Instance.goMain();
    }

    public void LookDream()
    {
        if (Dream.activeSelf)
        {
            return;
        }
        Diary.SetActive(false);
        Dream.SetActive(true);
    }

    public void LookDiary()
    {
        if (Diary.activeSelf)
        {
            return;
        }
        Dream.SetActive(false);
        Diary.SetActive(true);
    }
}
