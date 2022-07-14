using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    int count = 0;
    float size = 0.0f;
    [SerializeField]float sizeover = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector2(1 + size , 1 + size);
    }
/*
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
        count++;
        size = sizeover * count;
        GameManager.Instance.enemycount++;
    }*/
}
