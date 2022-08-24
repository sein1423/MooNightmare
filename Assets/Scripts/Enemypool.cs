using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemypool : MonoBehaviour
{
        public static Enemypool Instance;

        [SerializeField]
        private GameObject[] MonsterPrefab;

        Queue<Monster> TypeAQueue = new Queue<Monster>();
        Queue<Monster> TypeBQueue = new Queue<Monster>();
        Queue<Monster> TypeCQueue = new Queue<Monster>();
        Queue<Monster> TypeDQueue = new Queue<Monster>();

    private void Awake()
        {
            Instance = this;
            Initialize(10);
        }

        private void Initialize(int initCount)
        {
            for (int i = 0; i < initCount; i++)
            {
                TypeBQueue.Enqueue(CreateNewObject(0));
                TypeCQueue.Enqueue(CreateNewObject(1));
                TypeDQueue.Enqueue(CreateNewObject(2));
                TypeAQueue.Enqueue(CreateNewObject(3));
            }
        }

        private Monster CreateNewObject(int num)
        {
            var newObj = Instantiate(MonsterPrefab[num]).GetComponent<Monster>();
            newObj.gameObject.SetActive(false);
            newObj.transform.SetParent(transform);
            return newObj;
        }

        public static Monster GetObject()
        {
            if(Random.Range(0,10) < 7)
            {
            if (Instance.TypeAQueue.Count > 0)
            {
                var obj = Instance.TypeAQueue.Dequeue();
                obj.SetSlime();
                obj.maxH = obj.health + (ItemManager.Instance.wavecount * obj.waveHealth);
                obj.MH = obj.maxH;
                obj.transform.SetParent(null);
                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                var newObj = Instance.CreateNewObject(3);
                newObj.SetSlime();
                newObj.maxH = newObj.health + (ItemManager.Instance.wavecount * newObj.waveHealth);
                newObj.MH = newObj.maxH;
                newObj.gameObject.SetActive(true);
                newObj.transform.SetParent(null);
                return newObj;
            }
        }
            else
            {

            if (ItemManager.Instance.Type == MonsterType.TypeB)
            {
                if (Instance.TypeBQueue.Count > 0)
                {
                    var obj = Instance.TypeBQueue.Dequeue();
                    obj.maxH = obj.health + (ItemManager.Instance.wavecount * obj.waveHealth);
                    obj.MH = obj.maxH;
                    obj.transform.SetParent(null);
                    obj.gameObject.SetActive(true);
                    return obj;
                }
                else
                {
                    var newObj = Instance.CreateNewObject(0);
                    newObj.maxH = newObj.health + (ItemManager.Instance.wavecount * newObj.waveHealth);
                    newObj.MH = newObj.maxH;
                    newObj.gameObject.SetActive(true);
                    newObj.transform.SetParent(null);
                    return newObj;
                }
            }
            else if (ItemManager.Instance.Type == MonsterType.TypeC)
            {
                if (Instance.TypeCQueue.Count > 0)
                {
                    var obj = Instance.TypeCQueue.Dequeue();
                    obj.maxH = obj.health + (ItemManager.Instance.wavecount * obj.waveHealth);
                    obj.MH = obj.maxH;
                    obj.transform.SetParent(null);
                    obj.gameObject.SetActive(true);
                    return obj;
                }
                else
                {
                    var newObj = Instance.CreateNewObject(1);
                    newObj.maxH = newObj.health + (ItemManager.Instance.wavecount * newObj.waveHealth);
                    newObj.MH = newObj.maxH;
                    newObj.gameObject.SetActive(true);
                    newObj.transform.SetParent(null);
                    return newObj;
                }

            }
            else
            {
                if (Instance.TypeDQueue.Count > 0)
                {
                    var obj = Instance.TypeDQueue.Dequeue();
                    obj.maxH = obj.health + (ItemManager.Instance.wavecount * obj.waveHealth);
                    obj.MH = obj.maxH;
                    obj.transform.SetParent(null);
                    obj.gameObject.SetActive(true);
                    return obj;
                }
                else
                {
                    var newObj = Instance.CreateNewObject(2);
                    newObj.maxH = newObj.health + (ItemManager.Instance.wavecount * newObj.waveHealth);
                    newObj.MH = newObj.maxH;
                    newObj.gameObject.SetActive(true);
                    newObj.transform.SetParent(null);
                    return newObj;
                }
            }
        }
            
        }

        public static void ReturnObject(Monster obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(Instance.transform);
            //Instance.poolingObjectQueue.Enqueue(obj);
            ItemManager.Instance.Enemy++;
        }

}
