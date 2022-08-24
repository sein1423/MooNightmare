using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnim : MonoBehaviour
{
    Vector3 dir;
    float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        resetAnim();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * Time.deltaTime);
        if(time > 1.0f)
        {
            Destroy(gameObject);
        }
        time += Time.deltaTime;
    }

    public void resetAnim()
    {
        transform.localScale = Vector3.one;
        transform.position = gameObject.transform.parent.gameObject.transform.parent.position;
        dir = new Vector3(Random.Range(-1f, 1f), Random.Range(0.5f, 1f)).normalized;
    }
}
