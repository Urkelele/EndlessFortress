using System.Collections.Generic;
using UnityEngine;

public class EndlessRunnerTileManager : MonoBehaviour
{
    public PlayerHealthController m_PlayerHealthController = null;

    [Header("Tile Options")]
    public GameObject[] m_TilePrefabs;
    public float m_TileLength = 10f;
    public int m_IndexToSpawn = 5;
    public int m_MaxInstancesPerTilePrefab = 3;

    [Header("Movement Options")]
    public float m_MaxSpeed = 5f;
    public float m_SlowSpeed = 2f;
    private float m_CurrentSpeed;
    public float m_TimeToRecover = 5f;

    public List<GameObject> m_ActiveTiles = new List<GameObject> ();
    public List<GameObject> m_TilePool = new List<GameObject> ();

    private void Start()
    {
        m_PlayerHealthController = FindAnyObjectByType<PlayerHealthController> ();
    }
    void Awake()
    {
        m_CurrentSpeed = m_MaxSpeed;
        InitializePool();
        PlaceInitialTiles();
    }

    private void InitializePool()
    {
        foreach (var tile in m_TilePrefabs)
        {
            for (int i = 0; i < m_MaxInstancesPerTilePrefab; i++)
            {
                GameObject newTile = GameObject.Instantiate(tile);
                newTile.SetActive(false);
                m_TilePool.Add(newTile);
            }
        }
    }

    private void PlaceInitialTiles()
    {
        for (int i = 0; i < m_IndexToSpawn; i++)
        {
            PlaceRandomTile(i);
        }
    }

    private void PlaceRandomTile(int positionIndex = 0)
    {
        if (m_TilePool.Count == 0) return;
        int index = Random.Range(0, m_TilePool.Count);
        GameObject tile = m_TilePool[index];
        m_TilePool.RemoveAt(index);

        float zpos = positionIndex * m_TileLength;
        tile.transform.position = new Vector3 (0, 0, zpos);

        m_ActiveTiles.Add(tile);
        // TODO: if positionIndex == m_IndexToSpawn, spawn items
        tile.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        MoveActiveTiles();
    }

    void MoveActiveTiles()
    {
        for (int i = 0; i < m_ActiveTiles.Count; i++)
        {
            Transform thisTile = m_ActiveTiles[i].transform;
            thisTile.position += Vector3.back * m_CurrentSpeed * Time.deltaTime;
            if(thisTile.position.z <= -m_TileLength)
            {
                thisTile.gameObject.SetActive(false);
                m_ActiveTiles.RemoveAt(i);
                i--;
                m_TilePool.Add(thisTile.gameObject);
                PlaceRandomTile(m_IndexToSpawn-1); // Spawn a new tile
            }
        }
    }

    public void ObstacleHit()
    {
        m_CurrentSpeed = m_SlowSpeed;
        m_PlayerHealthController.ReceiveDamage(m_PlayerHealthController.m_MaxHealthPoints / 4);
        Invoke("RecoverSpeed", m_TimeToRecover);
    }

    private void RecoverSpeed()
    {
        m_CurrentSpeed = m_MaxSpeed;
        Debug.Log("GOTTA GO FAST");
    }
}
