using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [SerializeField] Text userName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CompleteName()
    {
        GameManager.Instance.SetUserData(userName.text);
    }

    public void CancleName()
    {
        userName.text = "";
    }
}
