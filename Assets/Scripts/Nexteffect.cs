using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexteffect : MonoBehaviour
{
    [SerializeField] GameObject NexteffectPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateNextEffect()
    {
        var attack = Instantiate(NexteffectPrefabs);
        attack.transform.position = gameObject.transform.position;
        Destroy(gameObject);
    }
}
