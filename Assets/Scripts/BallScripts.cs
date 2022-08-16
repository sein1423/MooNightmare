using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScripts : MonoBehaviour
{
    [SerializeField, Range(0.005f, 5f)] float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector2.left * speed);

        if(gameObject.transform.position.x < -10)
        {
            Destroy(gameObject);

        }
    }
}
