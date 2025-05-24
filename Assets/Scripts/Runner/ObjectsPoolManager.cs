using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPoolManager : MonoBehaviour
{
    public static ObjectsPoolManager m_Instance;

    public GameObject[] m_ObstaclePrefabs;
    public GameObject m_CoinPrefab;
    public GameObject m_PremiumCoin;

    public List<GameObject> m_ObstaclePool = new List<GameObject>();
    public Queue<GameObject> m_CoinPool = new Queue<GameObject>();
    public Queue<GameObject> m_PremiumCoinPool = new Queue<GameObject>();

    public int m_ObstaclePoolSize = 10;
    public int m_CoinPoolSize = 30;
    public int m_PremiumPoolSize = 5;

    public void Awake()
    {
        // This is the instance of the class
        m_Instance = this;
        InitializeObstaclePool();
        PopulatePool(m_CoinPrefab, m_CoinPool, m_CoinPoolSize);
        PopulatePool(m_PremiumCoin, m_PremiumCoinPool, m_PremiumPoolSize);
    }
    private void InitializeObstaclePool()
    {
        foreach (var obstacle in m_ObstaclePrefabs)
        {
            for (int i = 0; i < m_ObstaclePoolSize; i++)
            {
                GameObject newObstacle = GameObject.Instantiate(obstacle);
                newObstacle.SetActive(false);
                m_ObstaclePool.Add(newObstacle);
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
        return GetFromPool(m_ObstaclePrefab, m_ObstaclePool);
    }

    public GameObject GetCoin()
    {
        if(Random.Range(0,100) < 1) // if 1% chance to get a premium coin
        {
            return GetFromPool(m_PremiumCoin, m_PremiumCoinPool);
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
        thisCoin.SetActive(false);
        thisCoin.transform.parent = null;
        m_CoinPool.Enqueue(thisCoin);
    }
    public void ReturnObstacle(GameObject thisObstacle)
    {
        thisObstacle.SetActive(false);
        thisObstacle.transform.parent = null;
        m_ObstaclePool.Enqueue(thisObstacle);
    }
    public void ReturnPremiumCoin(GameObject thisCoin)
    {
        thisCoin.SetActive(false);
        thisCoin.transform.parent = null;
        m_PremiumCoinPool.Enqueue(thisCoin);
    }

}
