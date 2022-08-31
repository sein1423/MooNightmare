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
    int CriticalPercent = 10;
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
            if(gameObject.transform.position == player.transform.position)
            {
                Bulletpool.ReturnObject(this);
            }
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
        LookAngle();
    }

    public void LookAngle()
    {
        Vector2 target = arrow.transform.position - player.transform.position;
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, 1f);
        transform.rotation = rotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(Random.Range(0,100) < (CriticalPercent + ItemManager.Instance.CriticalPercent))
            {
                //Bulletpool.ReturnObject(this);
                int Dam = Damage + (int)ItemManager.Instance.AttackPower;
                int Cridam = (int)(Dam * (1.5f + ItemManager.Instance.CriticalDamage));
                collision.gameObject.GetComponent<Monster>().GetDamage(Cridam,true);
            }
            else
            {
                //Bulletpool.ReturnObject(this);
                collision.gameObject.GetComponent<Monster>().GetDamage(Damage + (int)ItemManager.Instance.AttackPower, false);
            }
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            if (collision.gameObject.GetComponent<Boss>().dead)
            {
                return;
            }

            if (Random.Range(0, 100) < (CriticalPercent + ItemManager.Instance.CriticalPercent))
            {
                //Bulletpool.ReturnObject(this);
                int Dam = Damage + (int)ItemManager.Instance.AttackPower;
                int Cridam = (int)(Dam * (1.5f + ItemManager.Instance.CriticalDamage));
                collision.gameObject.GetComponent<Boss>().GetDamage(Cridam,true);
            }
            else
            {
                //Bulletpool.ReturnObject(this);
                collision.gameObject.GetComponent<Boss>().GetDamage(Damage + (int)ItemManager.Instance.AttackPower,false);
            }
        }

    }
}
