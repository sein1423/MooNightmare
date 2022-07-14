using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if(Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            GameQuit();
        }
    }

    public void goToMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void goToStage()
    {
        SceneManager.LoadScene("Game");
    }

    public void GameQuit()
    {
        Debug.Log("게임종료");
        Application.Quit();
    }
}
