using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrotpool : MonoBehaviour
{
    public static Carrotpool Instance;

    [SerializeField]
    private GameObject poolingObjectPrefab;

    Queue<Carrot> poolingObjectQueue = new Queue<Carrot>();

    private void Awake()
    {
        Instance = this;
        Initialize(10);
    }

    private void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    private Carrot CreateNewObject()
    {
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<Carrot>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static Carrot GetObject()
    {
        if (Instance.poolingObjectQueue.Count > 0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    public static void ReturnObject(Carrot obj)
    {
        obj.time = 0f;
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);
    }
}
