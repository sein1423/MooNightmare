using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float h = 0.0f;
    float v = 0.0f;
    Rigidbody2D rb;

    [SerializeField]float speed = 10.0f;
    [SerializeField] float DamageCoolTime = 1f;
    [SerializeField] public GameObject arrow;
    [SerializeField, Range(1f, 10f)] float arrowSensitive;
    float attackCoolTime = 0.5f;
    float blinktime = 0f;
    public bool isDamage = false;
    float attackTime = 0f;
    float DamageTime = 0f;
    [SerializeField]bool canAttack = true;
    public Vector2 vec;
    void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDamage)
        {
            DamageTime += Time.deltaTime;
            blink();
            if (DamageTime > DamageCoolTime)
            {
                isDamage = false;

                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }

        if (!canAttack)
        {
            attackTime += Time.deltaTime;
            if (attackTime > (attackCoolTime - (attackCoolTime * (ItemManager.Instance.AttackCoolTime * 0.1f))))
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

        if(ItemManager.Instance.playerhealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Move(Vector2 inputVector)
    {
        if (ItemManager.Instance.isMenu)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if(inputVector == Vector2.zero)
        {
            GetComponent<Animator>().SetBool("Move", false);
            return;
        }
        else
        {
            GetComponent<Animator>().SetBool("Move", true);
        }


        h = inputVector.x;
        v = inputVector.y;

        Vector2 moveDirection = (Vector2.up * v) + (Vector2.right * h);
        rb.velocity = moveDirection * (speed + (speed * ItemManager.Instance.MoveSpeed));
        if(h > 0f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if(h < 0f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void Attack(Vector2 inputVector)
    {
        if (ItemManager.Instance.isMenu)
        {
            return;
        }
        if (inputVector == Vector2.zero)
        {
            return;
        }


        inputVector = inputVector.normalized;

        vec= (Vector2)gameObject.transform.position + (inputVector * arrowSensitive);
        arrow.transform.position = vec;

        if (canAttack)
        {
            Bullet[] bullet = new Bullet[ItemManager.Instance.AttackCount];

            Vector3 pos = new Vector3(-inputVector.y, inputVector.x, 0f); // 90 degree rotation
            float delta = -(float)(ItemManager.Instance.AttackCount - 1) / 2;
            for (int i = 0; i < ItemManager.Instance.AttackCount; i++, delta++)
            {
                bullet[i] = Bulletpool.GetObject();
                bullet[i].transform.position = gameObject.transform.position + pos * delta;
                bullet[i].GetComponent<Bullet>().SetDir(inputVector);
            }

            canAttack = false;
        }
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDamage)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("BossAttack"))
        {
            GetDamage();
        }

    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (isDamage)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetDamage();
        }

    }

    void GetDamage()
    {
        isDamage = true;
        DamageTime = 0f; 
        Handheld.Vibrate();
        ItemManager.Instance.playerhealth--;
        ItemManager.Instance.SetHeart();

        if (ItemManager.Instance.playerhealth <= 0)
        {
            ItemManager.Instance.isDead = true;
        }
    }

    void blink()
    {
        if(blinktime < 0.2f)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - blinktime);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, blinktime);
            if(blinktime > 0.4f)
            {
                blinktime = 0;
            }
        }

        blinktime += Time.deltaTime;
    }
}
