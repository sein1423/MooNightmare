using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemypool : MonoBehaviour
{
        public static Enemypool Instance;

        [SerializeField]
        private GameObject[] MonsterPrefab;

        Queue<Monster> poolingObjectQueue = new Queue<Monster>();

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

        private Monster CreateNewObject()
        {
            var newObj = Instantiate(MonsterPrefab[Random.Range(0,4)]).GetComponent<Monster>();
            newObj.gameObject.SetActive(false);
            newObj.transform.SetParent(transform);
            return newObj;
        }

        public static Monster GetObject()
        {
            if (Instance.poolingObjectQueue.Count > 0)
            {
                var obj = Instance.poolingObjectQueue.Dequeue();
                obj.health = 8 + (ItemManager.Instance.wavecount * obj.GetComponent<Monster>().waveHealth);
                obj.transform.SetParent(null);
                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                var newObj = Instance.CreateNewObject();
                newObj.health = 8 + (ItemManager.Instance.wavecount * newObj.GetComponent<Monster>().waveHealth);
                newObj.gameObject.SetActive(true);
                newObj.transform.SetParent(null);
                return newObj;
            }
        }

        public static void ReturnObject(Monster obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(Instance.transform);
            Instance.poolingObjectQueue.Enqueue(obj);
            ItemManager.Instance.Enemy++;
        }

}
