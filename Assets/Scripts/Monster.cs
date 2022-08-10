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
            ItemManager.Instance.AddCarrot(carrot);
        }
    }
}
