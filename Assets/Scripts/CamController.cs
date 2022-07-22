using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] Transform player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!ItemManager.Instance.isDead)
        {
            gameObject.transform.position = new Vector3(player.position.x, player.position.y, -10f);

        }
    }
}
