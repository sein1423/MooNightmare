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
        switch (ItemManager.Instance.wavecount % 4)
        {
            case 0:
                spawnPoint1 = Hole[3];
                spawnPoint2 = Hole[4];
                break;
            case 1:
                spawnPoint1 = Hole[0];
                spawnPoint2 = Hole[7];
                break;
            case 2:
                spawnPoint1 = Hole[1];
                spawnPoint2 = Hole[6];
                break;
            default:
                spawnPoint1 = Hole[2];
                spawnPoint2 = Hole[5];
                break;

        }

        var ene1 = Enemypool.GetObject();
        ene1.transform.position = Hole[Random.Range(0, 8)].transform.position;
        var ene2 = Enemypool.GetObject(); //Instantiate(enemy);
        ene2.transform.position = Hole[Random.Range(0, 8)].transform.position;
    }
}
