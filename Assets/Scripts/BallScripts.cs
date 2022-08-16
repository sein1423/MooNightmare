using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScripts : MonoBehaviour
{
    [SerializeField, Range(0.005f, 5f)] float speed;
    Transform player;
    Vector2 target;
    Vector3 targetRotate;
    Rigidbody2D rb;
    float time = 0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        player = GameObject.Find("Player").transform;
        target = player.position - gameObject.transform.position;
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle - 180f, Vector3.forward);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, 1f);
        transform.rotation = rotation;
        rb.velocity = target.normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.Translate(target.normalized * speed);
        time += Time.deltaTime;

        if(time > 10f)
        {
            Destroy(gameObject);

        }
    }
}
