using System.Collections.Generic;
using UnityEngine;

public class EndlessRunnerTileManager : MonoBehaviour
{
    public PlayerHealthController m_PlayerHealthController = null;

    public static EndlessRunnerTileManager Instance;

    [Header("Tile Options")]
    public GameObject[] m_TilePrefabs;
    public float m_TileLength = 10f;
    public int m_IndexToSpawn = 5;
    public int m_MaxInstancesPerTilePrefab = 3;
    public int m_CurrentTilesUntilDoors = 15;


    [Header("Movement Options")]
    public float m_MaxSpeed = 5f;
    public float m_SlowSpeed = 2f;
    private float m_CurrentSpeed;
    public float m_TimeToRecover = 5f;

    public List<GameObject> m_ActiveTiles = new List<GameObject> ();
    public List<GameObject> m_TilePool = new List<GameObject> ();
    public GameObject m_DoorsTilePrefab = null;
    private GameObject m_DoorsTile = null;

    public bool m_IsInRunner = false;

    private void Start()
    {
        InitializePool();
        m_CurrentSpeed = m_MaxSpeed;
        CalculateTilesUntilDoors();
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    public void ControlRunner(bool start)
    {
        m_IsInRunner = start;
        if (start)
        {
            for (int i = 0; i < m_ActiveTiles.Count; i++)
            {
                Transform thisTile = m_ActiveTiles[i].transform;
                thisTile.gameObject.SetActive(false);
                m_ActiveTiles.RemoveAt(i);
                i--;

                Debug.LogWarning("REMOVED TILE");

                if (thisTile != m_DoorsTile)
                {
                    m_TilePool.Add(thisTile.gameObject);
                }
                
            }

            PlaceInitialTiles();
        }
    }

    //Disable all active tiles
    private void OnDisable()
    {
        foreach(GameObject activeTile in m_ActiveTiles)
        {
            activeTile.SetActive(false);
        }
    }

    private void OnEnable()
    {
        ControlRunner(true);
    }

    private void InitializePool()
    {
        foreach (var tile in m_TilePrefabs)
        {
            for (int i = 0; i < m_MaxInstancesPerTilePrefab; i++)
            {
                GameObject newTile = GameObject.Instantiate(tile);
                m_TilePool.Add(newTile);
                newTile.SetActive(false);
            }
        }
        m_DoorsTile = GameObject.Instantiate(m_DoorsTilePrefab);
        m_DoorsTile.SetActive(false );
    }

    private void PlaceInitialTiles()
    {
        CalculateTilesUntilDoors();
        for (int i = 0; i < m_IndexToSpawn; i++)
        {
            PlaceRandomTile(i);
        }
    }

    private void PlaceRandomTile(int positionIndex = 0)
    {
        if(m_CurrentTilesUntilDoors > 0)
        {
            if (m_TilePool.Count == 0) return;
            int index = Random.Range(0, m_TilePool.Count);
            GameObject tile = m_TilePool[index];
            m_TilePool.RemoveAt(index);

            float zpos = positionIndex * m_TileLength;
            tile.transform.position = new Vector3(2.5f, 0, zpos);

            m_ActiveTiles.Add(tile);
            // TODO: if positionIndex == m_IndexToSpawn, spawn items
            tile.SetActive(true);
            m_CurrentTilesUntilDoors--;
        }
        else
        {
            float zpos = positionIndex * m_TileLength;
            m_DoorsTile.transform.position = new Vector3(2.5f, 0, zpos);
            m_ActiveTiles.Add(m_DoorsTile);
            m_DoorsTile.SetActive(true);
            CalculateTilesUntilDoors();
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        // Stop the time if the TimeManager says so
        if (TimeManager.instance.m_StopTime) { return; }

        if (m_IsInRunner == true)
        {
            MoveActiveTiles();
        }
    }

    void MoveActiveTiles()
    {
        for (int i = 0; i < m_ActiveTiles.Count; i++)
        {
            Transform thisTile = m_ActiveTiles[i].transform;
            thisTile.position += Vector3.back * m_CurrentSpeed * Time.deltaTime;
            if(thisTile.position.z <= -m_TileLength*2)
            {
                thisTile.gameObject.SetActive(false);
                m_ActiveTiles.RemoveAt(i);
                i--;
                if(thisTile != m_DoorsTile)
                {
                    m_TilePool.Add(thisTile.gameObject);
                }
                PlaceRandomTile(m_IndexToSpawn-2); // Spawn a new tile
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
    }

    public void CalculateTilesUntilDoors()
    {

        m_CurrentTilesUntilDoors = (m_IndexToSpawn) + PlayerStats.instance.m_RoomsCleared;
    }


    //public void DeactivateDoorsTile()
    //{
    //    m_DoorsTile.SetActive(false);
    //}
}
