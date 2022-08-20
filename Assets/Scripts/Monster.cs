using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Monster : MonoBehaviour
{
    [SerializeField] public int waveHealth; 
    [SerializeField, Range(0.001f, 1f)] protected float speed;
    public GameObject player;
    public int health;
    public int carrot;
    bool touch = false;
    public MonsterType type;
    bool dead = false;
    float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player"); 
        if (type == MonsterType.TypeD)
        {
            carrot = (ItemManager.Instance.StageCount / 2) + 1;
            switch ((ItemManager.Instance.StageCount / 2) + 1)
            {
                case 1:
                    //1스테이지 시작체력 설정
                    //health = ;
                    break;
                case 2:
                    //2스테이지 시작체력 설정
                    //health = ;
                    break;
                case 3:
                    //3스테이지 시작체력 설정
                    //health = 86;
                    break;
            }
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

    public void GetDamage(int Damage)
    {
        health -= Damage;

        if (health < 1)
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


    public void Dead()
    {
        Enemypool.ReturnObject(this);
        ItemManager.Instance.AddCarrot(carrot);
    }
}
