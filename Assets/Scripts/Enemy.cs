using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField,Range(0.001f, 1f)] float speed;
    public Transform player;
    public int health;
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
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed); 
        
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
            var carrot = Carrotpool.GetObject();
            carrot.gameObject.transform.position = gameObject.transform.position;
        }
    }


    //당근을 드랍하는 함수
    
}
