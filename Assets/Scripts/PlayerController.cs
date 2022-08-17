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
    bool isDamage = false;
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
                gameObject.GetComponent<BoxCollider2D>().enabled = true;

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
        vec= (Vector2)gameObject.transform.position + (inputVector.normalized * arrowSensitive);
        arrow.transform.position = vec;
        Bullet[] bullet = new Bullet[ItemManager.Instance.AttackCount];
        if (canAttack)
        {
            bullet[0] = Bulletpool.GetObject();
            bullet[0].transform.position = gameObject.transform.position;
            bullet[0].GetComponent<Bullet>().SetDir(inputVector.normalized);

            for(int i = 1; i < ItemManager.Instance.AttackCount; i++)
            {
                if (i % 2 == 1)
                {
                    bullet[i] = Bulletpool.GetObject();
                    if(Mathf.Abs(inputVector.normalized.y) >= 0.43 && Mathf.Abs(inputVector.normalized.y) <= 0.48)
                    {
                        bullet[i].transform.position = gameObject.transform.position + (Vector3.Cross(inputVector.normalized, new Vector3(inputVector.normalized.y,inputVector.normalized.x,0)) * (0.5f * (i / 2)));
                    }
                    else
                    {
                        bullet[i].transform.position = gameObject.transform.position + (new Vector3(inputVector.normalized.y, inputVector.normalized.x) * (0.5f * (i / 2)));
                    }
                    bullet[i].GetComponent<Bullet>().SetDir(inputVector.normalized);
                }
                else
                {
                    bullet[i] = Bulletpool.GetObject();

                    if (Mathf.Abs(inputVector.normalized.y) >= 0.43 && Mathf.Abs(inputVector.normalized.y) <= 0.48)
                    {
                        bullet[i].transform.position = gameObject.transform.position + (Vector3.Cross(inputVector.normalized, new Vector3(inputVector.normalized.y, inputVector.normalized.x, 0)) * (0.5f * (i / 2)));
                    }
                    else
                    {
                        bullet[i].transform.position = gameObject.transform.position + (new Vector3(-inputVector.normalized.y, -inputVector.normalized.x) * (0.5f * (i / 2)));

                    }
                    bullet[i].GetComponent<Bullet>().SetDir(inputVector.normalized);
                }
            }


            canAttack = false;
        }
    }

    public void Skill1()
    {
        Debug.Log("��ų1�� ���");
    }
    
    public void Skill2()
    {
        Debug.Log("��ų2�� ���");
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
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

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
