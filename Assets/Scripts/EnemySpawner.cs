using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;


    Transform spawnPoint1;
    Transform spawnPoint2;

    [SerializeField] Transform[] Hole;
    [SerializeField,Range(0.5f,10.5f)] float spawnTime;
    [SerializeField]
    float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ItemManager.Instance.isMenu || ItemManager.Instance.isDead)
        {
            return;
        }
        time += Time.deltaTime;
        if(time > spawnTime)
        {
            MakeEnemy();
            time = 0.0f;
        }
    }

    void MakeEnemy()
    {
        var ene1 = Enemypool.GetObject();
        ene1.transform.position = Hole[Random.Range(0, 8)].transform.position;
        var ene2 = Enemypool.GetObject(); //Instantiate(enemy);
        ene2.transform.position = Hole[Random.Range(0, 8)].transform.position;
    }
}
