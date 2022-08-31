using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public static CamController Instance;

    [SerializeField] public Transform target;
    void Start()
    {
        Instance = this;
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ItemManager.Instance.isDead)
        {
            gameObject.transform.position = new Vector3(target.position.x, target.position.y, -10f);

        }
    }
}
