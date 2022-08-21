using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField] float attacktime;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(ItemManager.Instance.isMenu || ItemManager.Instance.isDead)
        {
            return;
        }

        if(time > attacktime)
        {
            time = 0f;
            var ball = MonsterBulletpool.GetObject();
            ball.GetComponent<MonsterBullet>().nowtime = 0f;
            ball.GetComponent<MonsterBullet>().GetSpeed(gameObject.transform.position);
        }
        time += Time.deltaTime;
    }

}
