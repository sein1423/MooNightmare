using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    ParticleSystem ps;
    AudioSource ass;
    // Start is called before the first frame update
    void Start()
    {
        ps = gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
        ass = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ps.Play();
            ass.Play();
        }
    }
}
