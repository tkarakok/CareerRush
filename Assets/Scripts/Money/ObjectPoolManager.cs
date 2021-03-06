using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    [System.Serializable]
    public struct MoneyPool
    {
        public GameObject moneyPrefab;
        public int poolSize;
        public Queue<GameObject> moneyPoolQueue;
    };

    public Transform parentInstantiateMoney;

    public MoneyPool moneyPools;

    private void Awake()
    {

        moneyPools.moneyPoolQueue = new Queue<GameObject>();

        for (int i = 0; i < moneyPools.poolSize; i++)
        {
            GameObject money = Instantiate(moneyPools.moneyPrefab);
            money.transform.SetParent(parentInstantiateMoney);
            money.SetActive(false);
            moneyPools.moneyPoolQueue.Enqueue(money);
        }
    }

    /// <summary>
    /// we get a gameobject in queue for collision of gate
    /// </summary>
    /// <returns></returns>
    public GameObject GetMoney()
    {
        GameObject money = moneyPools.moneyPoolQueue.Dequeue();
        money.transform.SetParent(null);
        money.SetActive(true);
        moneyPools.moneyPoolQueue.Enqueue(money);
        return money;
    }
}
