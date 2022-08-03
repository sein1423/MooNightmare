using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static ShopManager Instance;
    [SerializeField] public Text CarrotText;
    [SerializeField] public GameObject Panel;
    [SerializeField] public Text DreamText;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        CarrotText.text = GameManager.Instance.user.carrot.ToString();
        
    }

    // Update is called once per frame
    

    public void BreakPanel()
    {
        Panel.SetActive(false);
    }

    public void GoMain()
    {
        GameManager.Instance.goMain();
    }

    

    public void GoFriend()
    {
        GameManager.Instance.GoMyFriend();
    }
    
}
