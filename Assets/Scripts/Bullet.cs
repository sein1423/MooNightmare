using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField, Range(0.1f,5f)]float Maxtime;
    [SerializeField, Range(0.1f, 100f)] float speed;
    public float nowtime = 0;
    int Damage = 10;
    int CriticalPercent;
    GameObject player;
    Transform arrow;

    Vector2 dir;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
        arrow = player.GetComponent<PlayerController>().arrow.transform;
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        if (ItemManager.Instance.isMenu)
        {
            return;
        }

        nowtime += Time.deltaTime;

        if (nowtime > (Maxtime + (Maxtime * ItemManager.Instance.AttackRange)) || (ItemManager.Instance.isDead))
        {
            Bulletpool.ReturnObject(this);
        }

    }

    public void GetSpeed()
    {
        rb.velocity = dir * speed;
    }

    public void SetDir(Vector2 dir)
    {
        this.dir = dir;
        GetSpeed();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(Random.Range(0,100) < (CriticalPercent + ItemManager.Instance.CriticalPercent))
            {
                Bulletpool.ReturnObject(this);
                int Dam = Damage + (int)ItemManager.Instance.AttackPower;
                int Cridam = (int)(Dam * (1.5f + ItemManager.Instance.CriticalDamage));
                collision.gameObject.GetComponent<Enemy>().GetDamage(Cridam);
            }
            else
            {
                Bulletpool.ReturnObject(this);
                collision.gameObject.GetComponent<Enemy>().GetDamage(Damage + (int)ItemManager.Instance.AttackPower);
            }

            
        }

    }
}
