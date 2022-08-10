using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Monster : MonoBehaviour
{
    public enum MonsterType {A,B,C,D}
    [SerializeField] public int waveHealth; 
    [SerializeField, Range(0.001f, 1f)] protected float speed;
    public Transform player;
    public int health;
    public int carrot;
    bool touch = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    

    // Update is called once per frame
    void Update()
    {
        if (ItemManager.Instance.isMenu)
        {
            return;
        }
        if (!touch)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed);
        }

        float h = (player.transform.position.x - gameObject.transform.position.x);
        if (h > 0f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if(h < 0f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
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
            Enemypool.ReturnObject(this);
            ItemManager.Instance.AddCarrot(carrot);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touch=true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        touch=false;
    }
}
