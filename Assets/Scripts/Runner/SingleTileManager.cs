using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SingleTileManager : MonoBehaviour
{
    public float m_TileLength = 10f;

    [Header("Obstacles and Coins")]
    public int m_ObstacleCount = 3;
    public int m_CountCoins = 8;

    public Transform m_ObstaclesRoot;
    public Transform m_CoinsRoot;

    public float[] m_Lanes = new float[] { -4f, -2.5f, -1f };

    private void OnEnable()
    {
        if(m_ObstaclesRoot == null)
        {
            GameObject newRoot = new GameObject("ObstaclesRoot");
            newRoot.transform.parent = transform;
            newRoot.transform.position = Vector3.zero;
            m_ObstaclesRoot = newRoot.transform;
        }
        if(m_CoinsRoot == null)
        {
            GameObject newRoot = new GameObject("CoinsRoot");
            newRoot.transform.parent = transform;
            newRoot.transform.position = Vector3.zero;
            m_CoinsRoot = newRoot.transform;
        }
        if(m_ObstaclesRoot.childCount == 0)
        {
            SpawnObstacles();
        }
        if(m_CoinsRoot.childCount == 0)
        {
            SpawnCoins();
        }
    }

    private void OnDisable()
    {
        if (m_ObstaclesRoot.childCount > 0)
        {
            DeSpawnObstacles();
        }
        if (m_CoinsRoot.childCount > 0)
        {
            DeSpawnCoins();
        }
    }
    private void DeSpawnCoins()
    {
        for (int i = m_CoinsRoot.childCount - 1; i >= 0; i--)
        {
            GameObject child = m_CoinsRoot.GetChild(i).gameObject;
            ObjectsPoolManager.m_Instance.ReturnCoin(child);
        }
    }private void DeSpawnObstacles()
    {
        for (int i = m_ObstaclesRoot.childCount - 1; i >= 0; i--)
        {
            GameObject child = m_ObstaclesRoot.GetChild(i).gameObject;
            ObjectsPoolManager.m_Instance.ReturnObstacle(child);
        }
    }
    private void SpawnCoins()
    {
        float lenghtBetweenCoins = 1f;
        int laneIndex = Random.Range(0, m_Lanes.Length);
        for (int i = 0; i < m_CountCoins; i++)
        {
            float zPos = m_CoinsRoot.position.z + lenghtBetweenCoins * (i + 1);
            float xPos = m_Lanes[laneIndex];
            GameObject newCoin = ObjectsPoolManager.m_Instance.GetCoin();
            newCoin.transform.position = new Vector3(xPos, 0, zPos);
            newCoin.transform.parent = m_CoinsRoot;
        }
    }

    private void SpawnObstacles()
    {
        int obstacleLinesToSpawn = Random.Range(1, m_ObstacleCount + 1);

        float lengthBetweenObstacles = m_TileLength / (obstacleLinesToSpawn + 1);

        for (int i = 0; i < obstacleLinesToSpawn; i++)
        {
            float zPos = m_ObstaclesRoot.position.z + lengthBetweenObstacles * (i + 1);
            int obstaclesPerLine = Random.Range(1, 3);

            List<int> lanesToUse = new List<int>();
            while (lanesToUse.Count < obstaclesPerLine)
            {
                int newLaneIndex = Random.Range(0, m_Lanes.Length);
                if(!lanesToUse.Contains(newLaneIndex))
                {
                    lanesToUse.Add(newLaneIndex);
                }
            }

            for (int j = 0; j < obstaclesPerLine; j++)
            {
                int laneIndex = lanesToUse[j];
                float xPos = m_Lanes[laneIndex];
                GameObject newObstacle = ObjectsPoolManager.m_Instance.GetObstacle();
                newObstacle.transform.position = new Vector3(xPos, 0, zPos);
                newObstacle.transform.parent = m_ObstaclesRoot;
            }            
        }
    }

}
