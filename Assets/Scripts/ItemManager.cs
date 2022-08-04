using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;


public enum ItemType
{
    AttackPower,
    AttackRange,
    AttackCoolTime,
    Health,
    MoveSpeed,
    ActiveSkill,
    CriticalDamage,
    CriticalPercent,
    AttackCount,
    Attackdirection
}
public class Item
{
    public Sprite Icon;
    public string name;
    public ItemType type;
    public float num;
    public int unit;
    public string Text;
    public int cost;
    public float percent;

    public void SetIndex(string str, int x)
    {
        switch (x)
        {
            case 0:
                //this.Icon = str;
                Icon = null;
                break;
            case 1:
                this.name = str;
                break;
            case 2:
                SetItemType(str);
                break;
            case 3:
                this.num = float.Parse(str);
                break;
            case 4:
                this.unit = int.Parse(str);
                break;
            case 5:
                this.Text = str;
                break;
            case 6:
                this.cost = int.Parse(str);
                break;
            case 7:
                this.percent = float.Parse(str);
                Debug.Log(this.percent);
                break;
        }
    }

    public void SetItemType(string str)
    {
        ItemType t = (ItemType)Enum.Parse(typeof(ItemType), str);
        this.type = t;
    }
}


public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    const string Link = "https://docs.google.com/spreadsheets/d/1rd4km6B1GyLqJWLXRQdl16TKyV95esAXdV5L-w5AaaM/export?format=tsv&range=B2:I";

    public List<Item> itemBuffer = new List<Item>();
    [SerializeField] ItemPanel itemp1;
    [SerializeField] ItemPanel itemp2;
    [SerializeField] ItemPanel itemp3;
    [SerializeField] GameObject waveshop;
    [SerializeField] Text timeText;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject TextObj;
    [SerializeField] Text gameCarrotText;
    [SerializeField] Text showWaveText;
    [SerializeField] Text showCarrotText;
    [SerializeField] GameObject Carrot;
    [SerializeField] GameObject HeartPanel1;
    [SerializeField] GameObject HeartPanel2;
    [SerializeField] TextMeshProUGUI gameoverText;
    [SerializeField] TextMeshProUGUI[] StatesText;
    [SerializeField] GameObject MoveLever;
    [SerializeField] GameObject AttackLever;
    [SerializeField] GameObject PauseBg;
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject OptionPanel;
    [SerializeField] GameObject[] Heart;
    [SerializeField] GameObject LockButton;
    [SerializeField] GameObject ExitPanel;
    public int playerhealth = 5;
    public bool isDead = false;
    public bool isMenu = false;
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] float waveTime = 30f;
    public int wavecount = 1;
    float time = 0f;
    public int GetCarrot = 0;
    public int Enemy = 0;
    //int ActiveSkill,
    public float AttackPower = 0;
    public float AttackRange = 0;
    public float AttackCoolTime = 0;
    public float Health = 0;
    public float MoveSpeed = 0;
    public float CriticalDamage = 0;
    public float CriticalPercent= 0;
    public int AttackCount = 0;
    public bool Lock = false;

    static Stack<GameObject> PopupStack = new Stack<GameObject>();

    private void Awake()
    {
        Instance = this;
        SetHeart();
        waveText.text = "Wave " + wavecount.ToString();
        gameCarrotText.text = GetCarrot.ToString();
    }


    IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get(Link);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        SetItemSO(data);
        //print(data);
    }

    void SetItemSO(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;
        //itemcount = rowSize;
        int columnSize = row[0].Split('\t').Length;
        print(rowSize);
        print(columnSize);
        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');
            Item item = new Item();

            for (int j = 0; j < columnSize; j++)
            {
                item.SetIndex(column[j], j);
            }

            for (int num = 0; num < item.percent; num++)
            {
                itemBuffer.Add(item);
            }
        }

        suffleBuffer();
    }

    void suffleBuffer()
    {

        for (int i = 0; i < itemBuffer.Count; i++)
        {
            int rand = UnityEngine.Random.Range(i, itemBuffer.Count);
            Item temp = itemBuffer[i];
            itemBuffer[i] = itemBuffer[rand];
            itemBuffer[rand] = temp;
        }

    }
    public Item PopItem()
    {
        if (itemBuffer.Count < 10)
        {
            StartCoroutine(Start());
        }

        Item item = itemBuffer[0];
        itemBuffer.RemoveAt(0);
        return item;
    }

    private void Update()
    {
        if (ItemManager.Instance.isDead)
        {
            GameOver();
            return;
        }

        

        if (!isMenu)
        {
            time += Time.deltaTime;
            timeText.text = (waveTime - time).ToString("F1");

        }

        if(waveTime < time && !waveshop.activeSelf)
        {
            GetItemShop();
        }
    }

    public void GetItemShop()
    {
        isMenu = true;
        if (Lock)
        {
            ShopLock();
        }
        else
        {
            Reroll();
        }
        
        TextObj.SetActive(false);
        Carrot.SetActive(false);
        HeartPanel1.SetActive(false);
        HeartPanel2.SetActive(false);
        MoveLever.GetComponent<JoyStick>().DragEnd(); 
        AttackLever.GetComponent<JoyStick>().DragEnd();
        MoveLever.SetActive(false);
        AttackLever.SetActive(false);
        UpdateStates();
        waveshop.SetActive(true);
        showWaveText.text = $"Shop(Wave{wavecount})";
        showCarrotText.text = GetCarrot.ToString();
    }

    public void ExitShop()
    {
        waveshop.SetActive(false);
        TextObj.SetActive(true);
        Carrot.SetActive(true);
        HeartPanel1.SetActive(true);
        HeartPanel2.SetActive(true);
        MoveLever.SetActive(true);
        AttackLever.SetActive(true);
        wavecount++;
        waveText.text = "Wave " + wavecount.ToString();
        gameCarrotText.text = GetCarrot.ToString(); 
        SetHeart();
        time = 0f;
        isMenu = false;
    }

    public void Reroll()
    {
        
        Item item1 = PopItem();
        while((CriticalPercent >= 90) && (item1.type == ItemType.CriticalPercent))
        {
            item1 = PopItem();
        }
        itemp1.Setup(item1);
        //print(item1.Icon + ", " + item1.name + ", " + item1.type + ", " + item1.num + ", " + item1.unit + ", " + item1.cost + ", " + item1.percent);
        Item item2 = PopItem(); 
        while (CriticalPercent >= 90 && item2.type == ItemType.CriticalPercent)
        {
            item2 = PopItem();
        }
        itemp2.Setup(item2);
        //print(item2.Icon + ", " + item2.name + ", " + item2.type + ", " + item2.num + ", " + item2.unit + ", " + item2.cost + ", " + item2.percent);
        Item item3 = PopItem(); 
        while (CriticalPercent >= 90 && item3.type == ItemType.CriticalPercent)
        {
            item3 = PopItem();
        }
        itemp3.Setup(item3);
        //print(item3.Icon + ", " + item3.name + ", " + item3.type + ", " + item3.num + ", " + item3.unit + ", " + item3.cost + ", " + item3.percent);
    }

    public void RerollButton()
    {
        if(GetCarrot < 10)
        {
            return;
        }
        GetCarrot -= 10;
        showCarrotText.text = GetCarrot.ToString();
        Reroll();
    }

    public void SetHeart()
    {
        for(int i = 0; i < playerhealth; i++)
        {
            Heart[i].SetActive(true);
        }

        for(int i = playerhealth; i < Heart.Length; i++)
        {
            Heart[i].SetActive(false);
        }

    }

    public void AddCarrot()
    {
        GetCarrot += wavecount;
        gameCarrotText.text = GetCarrot.ToString();
    }

    public void GameOver()
    {
        waveshop.SetActive(false);
        TextObj.SetActive(false);
        Carrot.SetActive(false);
        HeartPanel1.SetActive(false);
        HeartPanel2.SetActive(false);
        MoveLever.SetActive(false);
        AttackLever.SetActive(false);
        


        if (!GameOverPanel.activeSelf)
        {
            GameOverPanel.SetActive(true);
            gameoverText.text = $"최고 기록 : {GameManager.Instance.user.MaxWave.ToString()}\n" +
                $"현재 기록 : {wavecount}\n" +
                $"획득한 빛나는 당근 : {((wavecount - 1) * 3).ToString()}\n" +
                $"보유한 빛나는 당근 {GameManager.Instance.user.carrot.ToString()}\n " +
                $"처치한 적의 수 : {Enemy.ToString()}\n " +
                $"생존한 시간 : {(((wavecount - 1) * waveTime) + time).ToString("F2")}";
        }
    }

    public void GetItem(Item item)
    {
        if(item.type == ItemType.Health && playerhealth == 10)
        {
            return;
        }

        GetCarrot -= item.cost;
        showCarrotText.text = GetCarrot.ToString();
        switch (item.type)
        {
            case ItemType.AttackPower:
                AttackPower += item.num;
                break;
            case ItemType.AttackRange:
                AttackRange += item.num;
                break;
            case ItemType.AttackCoolTime:
                AttackCoolTime += item.num;
                break;
            case ItemType.Health:
                playerhealth += (int)item.num;
                break;
            case ItemType.MoveSpeed:
                MoveSpeed += item.num;
                break;
            case ItemType.CriticalDamage:
                CriticalDamage += item.num;
                break;
            case ItemType.CriticalPercent:
                CriticalPercent += (item.num * 100f);
                break;
            case ItemType.AttackCount:
                AttackCount += (int)item.num;
                break;

        }
        UpdateStates();
    }

    public void UpdateStates()
    {
        StatesText[0].text = (10+AttackPower).ToString("F0");
        StatesText[1].text = "+" + (AttackRange*5f).ToString("F1") + "m";
        StatesText[2].text = "+" + (AttackCoolTime*100f).ToString("F0") + "%";
        StatesText[3].text = "+" + playerhealth.ToString("F0");
        StatesText[4].text = "+" + (MoveSpeed * 100f).ToString("F0") + "%";
        StatesText[5].text = "+" + (CriticalDamage * 100f).ToString("F0") + "%";
        StatesText[6].text = "+" +  (10+CriticalPercent).ToString("F0") + "%";
        StatesText[7].text = "+" + AttackCount.ToString("F0");
    }

    public void Pause()
    {
        PauseBg.SetActive(true);
        PopupStack.Push(PauseBg);
        isMenu = true;
        PausePanel.SetActive(true);
        PopupStack.Push(PausePanel);
    }

    public void Option()
    {
        (PopupStack.Pop()).SetActive(false);
        OptionPanel.SetActive(true);
        PopupStack.Push(OptionPanel);
    }

    public void BreakPanel()
    {
        while (PopupStack.Count > 0)
        {
            PopupStack.Pop().SetActive(false);
        }

        if (!waveshop.activeSelf)
        {
            isMenu = false;
        }
    }

    public void BreakOption()
    {
        (PopupStack.Pop()).SetActive(false);
        PausePanel.SetActive(true);
        PopupStack.Push(PausePanel);
    }

    public void BreakExit()
    {
        (PopupStack.Pop()).SetActive(false);
        PausePanel.SetActive(true);
        PopupStack.Push(PausePanel);
    }

    public void GetExit()
    {
        (PopupStack.Pop()).SetActive(false);
        ExitPanel.SetActive(true);
        PopupStack.Push(ExitPanel);
    }

    public void GameQuit()
    {
        BreakPanel();
        isDead = true;
        GameOver();
    }

    public void GameOverSave()
    {
        if (GameManager.Instance.user.MaxWave < wavecount)
        {
            GameManager.Instance.user.MaxWave = wavecount;
        }
        GameManager.Instance.user.carrot += (wavecount - 1) * 3;
        GameManager.Instance.user.MaxTime = (int)(((wavecount-1) * waveTime) + time);
        GameManager.Instance.user.monster = Enemy;
        GameManager.Instance.user.LastGameDay = DateTime.Now.ToString("yyyy/MM/dd");
        GameManager.Instance.SaveData();
    }

    public void EndGame()
    {
        GameOverSave();
        GameManager.Instance.goMain();
    }
    public void ShopLock()
    {
        if (Lock)
        {
            LockButton.GetComponent<Image>().color = new Color32(255,255,255, 255);
            Lock = false;
            LockButton.transform.GetChild(0).GetComponent<Text>().text = "잠금";
        }
        else
        {
            LockButton.GetComponent<Image>().color = new Color32(114, 114, 114, 255);
            Lock = true;
            LockButton.transform.GetChild(0).GetComponent<Text>().text = "잠금해제";
        }
    }
}
