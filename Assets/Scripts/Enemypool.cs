using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemypool : MonoBehaviour
{
        public static Enemypool Instance;

        [SerializeField]
        private GameObject[] MonsterPrefab;

        Queue<Monster> HumanQueue = new Queue<Monster>();
        Queue<Monster> goblinQueue = new Queue<Monster>();
        Queue<Monster> snowmanQueue = new Queue<Monster>();

    private void Awake()
        {
            Instance = this;
            Initialize(10);
        }

        private void Initialize(int initCount)
        {
            for (int i = 0; i < initCount; i++)
            {
                HumanQueue.Enqueue(CreateNewObject(0));
                goblinQueue.Enqueue(CreateNewObject(1));
                snowmanQueue.Enqueue(CreateNewObject(2));
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
            if(ItemManager.Instance.Type == MonsterType.Human)
            {
                if (Instance.HumanQueue.Count > 0)
                {
                    var obj = Instance.HumanQueue.Dequeue();
                    obj.health = 8 + (ItemManager.Instance.wavecount * obj.GetComponent<Monster>().waveHealth);
                    obj.transform.SetParent(null);
                    obj.gameObject.SetActive(true);
                    return obj;
                }
                else
                {
                    var newObj = Instance.CreateNewObject(0);
                    newObj.health = 8 + (ItemManager.Instance.wavecount * newObj.GetComponent<Monster>().waveHealth);
                    newObj.gameObject.SetActive(true);
                    newObj.transform.SetParent(null);
                    return newObj;
                }
            }
            else if(ItemManager.Instance.Type == MonsterType.goblin)
        {
            if (Instance.goblinQueue.Count > 0)
            {
                var obj = Instance.goblinQueue.Dequeue();
                obj.health = 8 + (ItemManager.Instance.wavecount * obj.GetComponent<Monster>().waveHealth);
                obj.transform.SetParent(null);
                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                var newObj = Instance.CreateNewObject(1);
                newObj.health = 8 + (ItemManager.Instance.wavecount * newObj.GetComponent<Monster>().waveHealth);
                newObj.gameObject.SetActive(true);
                newObj.transform.SetParent(null);
                return newObj;
            }

        }
        else
        {
            if (Instance.snowmanQueue.Count > 0)
            {
                var obj = Instance.snowmanQueue.Dequeue();
                obj.health = 8 + (ItemManager.Instance.wavecount * obj.GetComponent<Monster>().waveHealth);
                obj.transform.SetParent(null);
                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                var newObj = Instance.CreateNewObject(2);
                newObj.health = 8 + (ItemManager.Instance.wavecount * newObj.GetComponent<Monster>().waveHealth);
                newObj.gameObject.SetActive(true);
                newObj.transform.SetParent(null);
                return newObj;
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
