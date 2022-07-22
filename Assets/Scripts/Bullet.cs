using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField, Range(0.1f,5f)]float Maxtime;
    [SerializeField, Range(0.1f, 100f)] float speed;
    float nowtime = 0;

    GameObject player;
    Transform arrow;

    Vector2 dir;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        arrow = player.GetComponent<PlayerController>().arrow.transform;
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(dir);
        GetSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        nowtime += Time.deltaTime;

        if (nowtime > Maxtime)
        {
            Destroy(gameObject);
        }
    }

    public void GetSpeed()
    {
        rb.velocity = dir * speed;
    }

    public void SetDir(Vector2 dir)
    {
        this.dir = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Bulletpool.ReturnObject(this);
            Enemypool.ReturnObject(collision.gameObject.GetComponent<Enemy>());
        }

    }
}
