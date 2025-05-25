using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectsPoolManager : MonoBehaviour
{
    public static ObjectsPoolManager m_Instance;

    public GameObject[] m_ObstaclePrefabs;
    public GameObject m_CoinPrefab;
    public GameObject[] m_PremiumPickUps;

    public List<GameObject> m_ObstaclePool = new List<GameObject>();
    public Queue<GameObject> m_CoinPool = new Queue<GameObject>();
    public List<GameObject> m_PremiumPickUpPool = new List<GameObject>();

    public int m_ObstaclePoolSize = 10;
    public int m_CoinPoolSize = 30;
    public int m_PremiumPickUpPoolSize = 5;

    public void Awake()
    {
        // This is the instance of the class
        m_Instance = this;
        
    }
    private void OnEnable()
    {
        InitializePool(m_ObstaclePrefabs, m_ObstaclePool, m_ObstaclePoolSize);
        PopulatePool(m_CoinPrefab, m_CoinPool, m_CoinPoolSize);
        InitializePool(m_PremiumPickUps, m_PremiumPickUpPool, m_PremiumPickUpPoolSize);
    }
    private void InitializePool(GameObject[] prefabs, List<GameObject> pool,int poolSize)
    {
        foreach (var prefab in prefabs)
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject newInstance = GameObject.Instantiate(prefab);
                newInstance.SetActive(false);
                pool.Add(newInstance);
            }
        }
    }
    public void PopulatePool(GameObject thisPrefab, Queue<GameObject> thisQueue, int thisAmount)
    {
        for (int i = 0; i < thisAmount; i++)
        {
            GameObject newObject = GameObject.Instantiate(thisPrefab);
            newObject.SetActive(false);
            thisQueue.Enqueue(newObject);
        }
    }

    public GameObject GetObstacle()
    {
        int index = Random.Range(0, m_ObstaclePool.Count);
        GameObject obstacle = m_ObstaclePool[index];
        m_ObstaclePool.RemoveAt(index);
        obstacle.SetActive(true);
        return obstacle;
    }
    public GameObject GetPremiumCoin()
    {
        int index = Random.Range(0, m_PremiumPickUpPool.Count);
        GameObject premiumPickUp = m_PremiumPickUpPool[index];
        m_PremiumPickUpPool.RemoveAt(index);
        premiumPickUp.SetActive(true);
        return premiumPickUp;
    }

    public GameObject GetCoin()
    {
        if(Random.Range(0,100) < 1) // if 1% chance to get a premium coin
        {
            return GetPremiumCoin();
        }
        return GetFromPool(m_CoinPrefab, m_CoinPool);
    }

    private GameObject GetFromPool(GameObject thisPrefab, Queue<GameObject> thisQueue)
    {
        if (thisQueue.Count == 0)
        {
            GameObject newObject = GameObject.Instantiate(thisPrefab);
            newObject.SetActive(false);
            thisQueue.Enqueue(newObject);
        }
        GameObject objectToReturn = thisQueue.Dequeue();
        objectToReturn.SetActive(true);
        return objectToReturn;
    }

    public void ReturnCoin(GameObject thisCoin)
    {
        InventoryManager.instance.AddGold(1);
        thisCoin.SetActive(false);
        thisCoin.transform.parent = null;
        m_CoinPool.Enqueue(thisCoin);
    }
    public void ReturnObstacle(GameObject thisObstacle)
    {
        thisObstacle.SetActive(false);
        thisObstacle.transform.parent = null;
        m_ObstaclePool.Add(thisObstacle);
    }
    public void ReturnPremiumCoin(GameObject thisCoin)
    {
        //ADD ONE TOME FUNCTION
        thisCoin.SetActive(false);
        thisCoin.transform.parent = null;
        m_PremiumPickUpPool.Add(thisCoin);
    }

}
