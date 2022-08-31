using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Boss : MonoBehaviour
{
    public enum BossType { Boss1, Boss2, Boss3 };
    [SerializeField] BossType bossType;
    [SerializeField] GameObject dusteffect;
    [SerializeField] GameObject balleffect;
    [SerializeField] Vector2[] attacktransform;
    [SerializeField] Vector2[] balltransform;
    [SerializeField, Range(0.005f, 0.5f)] float speed;
    [SerializeField] float health;
    float MaxHealth;
    [SerializeField] GameObject DamagePrefabs;
    Animator ani;
    float attack1time = 0;
    float attack2time = 0;
    bool up = false;
    public bool dead=false;
    GameObject Canvas;

    [SerializeField] AudioSource AS;
    [SerializeField]float attack1cooltime;
    [SerializeField]float attack2cooltime;

    private void Start()
    {
        ani = GetComponent<Animator>();
        Canvas = gameObject.transform.GetChild(0).gameObject;
        MaxHealth = health;
    }
    // Update is called once per frame
    void Update()
    {
        if(ItemManager.Instance.isMenu || ItemManager.Instance.isDead)
        {
            return;
        }

        if (up)
        {
            ani.SetBool("Run",true);
            gameObject.transform.Translate(Vector2.up * speed);
        }
        else
        {
            ani.SetBool("Run", true);
            gameObject.transform.Translate(-Vector2.up * speed);
        }

        if(gameObject.transform.position.y > 3)
        {
            up = false;
        }
        if (gameObject.transform.position.y<-3)
        {
            up = true;
        }


        switch (bossType)
        {
            case BossType.Boss1:
                attack1time += Time.deltaTime;
                break;
            case BossType.Boss2:
                attack2time += Time.deltaTime;
                break;
                default:
                attack1time += Time.deltaTime;
                attack2time += Time.deltaTime;
                break;

        }

        if(attack1time > attack1cooltime)
        {
            var attack = Instantiate(dusteffect);
            attack.transform.position = GameObject.Find("Player").transform.position;
            attack1time = 0f;
        }

        if(attack2time > attack2cooltime)
        {
            var attack = Instantiate(balleffect);
            attack.transform.position = new Vector2(gameObject.transform.position.x,gameObject.transform.position.y - 0.01f)/*balltransform[Random.Range(0, attacktransform.Length)]*/;
            attack2time = 0f;
        }
    }

    public void GetDamage(int Damage , bool cri)
    {
        if (dead)
        {
            return;
        }
        health -= Damage;
        Debug.Log($"{Damage} : {health} : {MaxHealth}");
        var dmg = Instantiate(DamagePrefabs, gameObject.transform);
        dmg.transform.SetParent(Canvas.transform);
        if (cri)
        {
            dmg.GetComponent<TextMeshProUGUI>().color = new Color(255, 0, 0, 255);
        }
        Canvas.transform.GetChild(0).GetComponent<Image>().fillAmount = health / MaxHealth;
        dmg.GetComponent<TextMeshProUGUI>().text = "-" + Damage.ToString();

        if (health < 1)
        {
            dead = true;
            AS.volume = GameManager.Instance.user.effect;
            AS.Play();
            ani.SetTrigger("Death");
            Invoke("next", 5f);
        }
    }

    public void Death()
    {
        ItemManager.Instance.AddCarrot(((ItemManager.Instance.StageCount / 2) + 1) * 50);
        gameObject.SetActive(false);
    }

    public void next()
    {
        ItemManager.Instance.NextStage();
    }
}
