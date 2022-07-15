using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float h = 0.0f;
    float v = 0.0f;
    Rigidbody2D rb;

    [SerializeField]float speed = 10.0f;
    [SerializeField] public GameObject arrow;
    [SerializeField, Range(1f, 10f)] float arrowSensitive;
    [SerializeField, Range(0.1f, 5f)] float attackCoolTime;
    [SerializeField] GameObject bulletPrefab;

    float attackTime = 0f;
    bool canAttack = true;
    public Vector2 vec;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canAttack)
        {
            attackTime += Time.deltaTime;
            if (attackTime > attackCoolTime)
            {
                canAttack = true;
                attackTime = 0f;
            }
        }
        

        if (arrow.transform.position == gameObject.transform.position)
        {
            
            arrow.SetActive(false);
        }
        else
        {
            arrow.SetActive(true);
        }
    }

    public void Move(Vector2 inputVector)
    {
        h = inputVector.x;
        v = inputVector.y;

        Vector2 moveDirection = (Vector2.up * v) + (Vector2.right * h);

        rb.velocity = moveDirection * speed;
    }

    public void Attack(Vector2 inputVector)
    {
        vec= (Vector2)gameObject.transform.position + (inputVector.normalized * arrowSensitive);
        arrow.transform.position = vec;

        if (canAttack)
        {
            GameObject bullet = Instantiate(bulletPrefab, gameObject.transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().SetDir(inputVector);
            canAttack = false;
        }
    }

    public void Skill1()
    {
        Debug.Log("스킬1번 사용");
    }
    
    public void Skill2()
    {
        Debug.Log("스킬2번 사용");
    }
}
