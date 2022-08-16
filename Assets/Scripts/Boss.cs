using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public enum BossType { Boss1, Boss2, Boss3 };
    [SerializeField] BossType bossType;
    [SerializeField] GameObject dusteffect;
    [SerializeField] GameObject balleffect;
    [SerializeField] Vector2[] attacktransform;
    [SerializeField] Vector2[] balltransform;
    float attack1time = 0;
    float attack2time = 0;


    [SerializeField]float attack1cooltime;
    [SerializeField]float attack2cooltime;


    // Update is called once per frame
    void Update()
    {
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
            attack.transform.position = attacktransform[Random.Range(0, attacktransform.Length)];
            attack1time = 0f;
        }

        if(attack2time > attack2cooltime)
        {
            var attack = Instantiate(balleffect);
            attack.transform.position = balltransform[Random.Range(0, attacktransform.Length)];
            attack2time = 0f;
        }
    }
}
