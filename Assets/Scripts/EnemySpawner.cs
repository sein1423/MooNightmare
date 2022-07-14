using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;

    float Maxright;
    float Maxtop;
    float num;
    Vector2 enemytrans;

    [SerializeField]
    float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > 1.0f)
        {
            MakeEnemy();
            time = 0.0f;
        }
    }

    void MakeEnemy()
    {

        Maxright = Random.Range(10, 20);
        Maxtop = Random.Range(10, 20);

        num = Random.Range(0, 4);

        switch (num)
        {
            case 0:
                enemytrans = new Vector2((float)player.position.x + Maxright, (float)player.position.y + Maxtop);
                break;
            case 1:
                enemytrans = new Vector2((float)player.position.x + Maxright, (float)player.position.y - Maxtop);
                break;
            case 2:
                enemytrans = new Vector2((float)player.position.x - Maxright, (float)player.position.y + Maxtop);
                break;
            default:
                enemytrans = new Vector2((float)player.position.x - Maxright, (float)player.position.y - Maxtop);
                break;

        }


        var ene = Enemypool.GetObject(); ; //Instantiate(enemy);
        ene.transform.position = enemytrans;
    }
}
