using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    [SerializeField] public int waveHealth; 
    [SerializeField, Range(0.001f, 1f)] protected float speed;
    public GameObject player;
    int MaxHealth;
    public int maxH
    {
        get { return MaxHealth; }
        set { MaxHealth = value; }
    }

    public int MH;
    public int health;
    public int carrot;
    bool touch = false;
    public MonsterType type;
    bool dead = false;
    float time = 0f;
    public GameObject Canvas;
    [SerializeField]GameObject DamagePrefabs;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Canvas = gameObject.transform.GetChild(0).gameObject;
        if (type == MonsterType.TypeD)
        {
            SetSlime();
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        if (ItemManager.Instance.isMenu)
        {
            return;
        }

        if (ItemManager.Instance.isBoss)
        {
            Enemypool.ReturnObject(this);
        }


        if (player.GetComponent<PlayerController>().isDamage || dead)
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }

        if (!touch && !dead)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed);
            GetComponent<Animator>().SetBool("Walk", true);
        }

        if (touch)
        {
            time += Time.deltaTime;
            if(time > 3f)
            {
                touch = false;
                time = 0f;
            }
        }

        float h = (player.transform.position.x - gameObject.transform.position.x);
        if (h > 0f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(h < 0f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (ItemManager.Instance.isDead)
        {
            Enemypool.ReturnObject(this);
        }

    }

    public void OnEnable()
    {
        Canvas = gameObject.transform.GetChild(0).gameObject;
        Canvas.transform.GetChild(0).GetComponent<Image>().fillAmount = 1.0f;
    }
    public void GetDamage(int Damage, bool cri)
    {
        Debug.Log($"{Damage} : {health + waveHealth * ItemManager.Instance.wavecount} : {MaxHealth}");
        MaxHealth -= Damage;
        Debug.Log($"{Damage} : {health} : {MaxHealth}");
        gameObject.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
        var dmg = Instantiate(DamagePrefabs,gameObject.transform);
        dmg.transform.SetParent(Canvas.transform);
        if (cri)
        {
            dmg.GetComponent<TextMeshProUGUI>().color = new Color(255, 0, 0, 255);
        }
        Canvas.transform.GetChild(0).GetComponent<Image>().fillAmount = MaxHealth / (float)MH;
        dmg.GetComponent<TextMeshProUGUI>().text = "-" + Damage.ToString();
        if (MaxHealth < 1)
        {
            dead = true;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<AudioSource>().volume = GameManager.Instance.user.effect;
            GetComponent<AudioSource>().Play();
            GetComponent<Animator>().SetTrigger("Attack");
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touch=true;
        }
    }

    public void SetSlime()
    {

        carrot = (ItemManager.Instance.StageCount / 2) + 1;
        switch ((ItemManager.Instance.StageCount / 2) + 1)
        {
            case 1:
                //1스테이지 시작체력 설정
                health = 0;
                break;
            case 2:
                //2스테이지 시작체력 설정
                health = 140;
                break;
            case 3:
                //3스테이지 시작체력 설정
                health = 990;
                break;
        }
    }
    public void Dead()
    {
        Enemypool.ReturnObject(this);
        ItemManager.Instance.AddCarrot((ItemManager.Instance.StageCount / 2) + 1);
    }
}
