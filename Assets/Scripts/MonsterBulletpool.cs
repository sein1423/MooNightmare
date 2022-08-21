using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBulletpool : MonoBehaviour
{
    public static MonsterBulletpool Instance;

    [SerializeField]
    private GameObject ballObjectPrefab;

    Queue<MonsterBullet> ballObjectQueue = new Queue<MonsterBullet>();

    private void Awake()
    {
        Instance = this;
        Initialize(10);
    }

    private void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            ballObjectQueue.Enqueue(CreateNewObject());
        }
    }

    private MonsterBullet CreateNewObject()
    {
        var newObj = Instantiate(ballObjectPrefab).GetComponent<MonsterBullet>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static MonsterBullet GetObject()
    {
        if (Instance.ballObjectQueue.Count > 0)
        {
            var obj = Instance.ballObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.transform.SetParent(null);
            newObj.gameObject.SetActive(true);
            return newObj;
        }

    }

    public static void ReturnObject(MonsterBullet obj)
    {
        Instance.ballObjectQueue.Enqueue(obj);
        obj.nowtime = 0f;
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
    }
}
