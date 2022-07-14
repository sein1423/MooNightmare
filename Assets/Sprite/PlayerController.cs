using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float h = 0.0f;
    float v = 0.0f;
    Rigidbody2D rb;

    [SerializeField]float speed = 10.0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector2 moveDirection = (Vector2.up * v) + (Vector2.right * h);

        rb.velocity = moveDirection*speed;

        //소리지르는거 스페이스바로 하고
        //소리 지르는걸 적이 맞으면 제거
    }
}
