using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{

    [SerializeField, Range(0.005f, 5f)] float speed;
    Transform player;
    Vector2 target;
    public float nowtime = 0f;
    Rigidbody2D rb;
    bool isSetting = false;
    [SerializeField] float time;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSetting)
        {
            return;
        }
        nowtime += Time.deltaTime;

        if (ItemManager.Instance.isMenu || ItemManager.Instance.isDead)
        {
            MonsterBulletpool.ReturnObject(this);
        }
        if (nowtime > time)
        {
            MonsterBulletpool.ReturnObject(this);

        }
    }
    public void GetSpeed(Vector2 position)
    {
        isSetting = true;
        rb = GetComponent<Rigidbody2D>();
        gameObject.transform.position = position;
        player = GameObject.Find("Player").transform;
        target = player.position - gameObject.transform.position;
        rb.velocity = target.normalized * speed;
        isSetting = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MonsterBulletpool.ReturnObject(this);
        }
    }
}
