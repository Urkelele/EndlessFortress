using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;
    
    [Header("CONTROL VARS")]
    public int m_NumOfRoomsBetweenBoss = 10;

    [Header("CLASSES")]
    private PlayerCombatScript m_PlayerCombatScript;
    public List<EnemyBaseScript> m_CombatEnemies;
    public EnemyBaseScript m_CurrentEnemyTarget;
    
    [Header("POSITIONS")]
    public Transform m_PlayerPosition;
    [SerializeField] private Transform[] m_EnemyPositions = new Transform[3];

    [Header("REWARDS")]
    public int m_GoldBattleReward = 0;
    public ItemBaseScript m_ItemReward = null;

    [Header("DROP CHANCES")]
    public float m_ItemDropChance = 0.25f;
    public float m_CommonDropChance = 0.5f;
    public float m_RareDropChance = 0.3f;
    public float m_EpicDropChance = 0.15f;
    public float m_LegendaryDropChance = 0.05f;

    [Header("ENEMY ROOM PREFABS")]
    [SerializeField] private List<GameObject> m_EnemyCompsList = new List<GameObject>();
    [SerializeField] private string m_EnemyCompsFolderPath = "Prefabs/EnemyComps_Prefabs";
    [SerializeField] private List<GameObject> m_BossCompsList = new List<GameObject>();
    [SerializeField] private string m_BossCompsFolderPath = "Prefabs/BossComps_Prefabs";
    private GameObject m_CurrentComp = null;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        m_PlayerCombatScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatScript>();
        LoadCompsPrefabs(m_EnemyCompsList, m_EnemyCompsFolderPath);
        LoadCompsPrefabs(m_BossCompsList, m_BossCompsFolderPath);
    }
    private void Start()
    {
        StartCombat();
    }

    /// <summary>
    /// Loads all gameobjects from a certain Resource/ path, deactivates them and puts them on a list
    /// </summary>
    /// <param name="list"></param>
    /// <param name="prefabPath"></param>
    private void LoadCompsPrefabs(List<GameObject> list, string prefabPath)
    {
        GameObject[] prefabList = Resources.LoadAll<GameObject>(prefabPath);

        foreach (var prefab in prefabList)
        {
            list.Add(prefab);
            prefab.SetActive(false);
        }

        Debug.Log($"Loaded {list.Count} prefabs into the queue.");
    }

    private void StartCombat()
    {
        InventoryManager.instance.EnableItemTrigger(TriggerType.COMBAT_START);
        MovePlayer();
        SpawnEnemies();
        GetDefaultTargetEnemy();
    }

    private void MovePlayer()
    {
        m_PlayerCombatScript.transform.position = m_PlayerPosition.position;
    }

    /// <summary>
    /// Instantiate a random comp. Spawn the enemies in the position and rotation of the spawn transforms
    /// </summary>
    private void SpawnEnemies()
    {
        m_CurrentComp = Instantiate(GetRandomComp());
        m_CurrentComp.SetActive(true);

        for (int i = 0; i < m_CurrentComp.transform.childCount; i++)
        {
            m_CurrentComp.transform.GetChild(i).transform.position = m_EnemyPositions[i].position;
            m_CurrentComp.transform.GetChild(i).transform.rotation = m_EnemyPositions[i].rotation;
            m_CombatEnemies.Add(m_CurrentComp.transform.GetChild(i).GetComponent<EnemyBaseScript>());
        }
    }

    /// <summary>
    /// Returns random comp prefab, depending on room count the comp will be a boss
    /// </summary>
    /// <returns></returns>
    private GameObject GetRandomComp()
    {
        if((PlayerStats.instance.m_RoomsCleared % m_NumOfRoomsBetweenBoss) == 0 && PlayerStats.instance.m_RoomsCleared != 0)
        {
            int randIndex = Random.Range(0, m_BossCompsList.Count);
            return m_BossCompsList[randIndex];
        }
        else
        {
            int randIndex = Random.Range(0, m_EnemyCompsList.Count);
            return m_EnemyCompsList[randIndex];
        }
    }

    private void GetDefaultTargetEnemy()
    {
        //Get random enemy, make it the target and make it so it "was clicked last" to mantain the outline around it
        //TODO: might give problems since the actual last object that was clicked does not turn off
        int randEnemyPos = Random.Range(0, m_CombatEnemies.Count);
        m_PlayerCombatScript.m_TargetEnemy = m_CombatEnemies[randEnemyPos];
        m_CombatEnemies[randEnemyPos].m_ClickDetection.m_IsLastObjectClicked = true;
    }

    private void GiveRewards()
    {
        //Take into account gold reward multiplication, gold number gets rounded
        InventoryManager.instance.m_Gold += (int)(m_GoldBattleReward * InventoryManager.instance.m_TotalGoldRewardMultipler);

        float itemSpawnroll = Random.value;

        if(itemSpawnroll < m_ItemDropChance)
        {
            float qualityRoll = Random.value;

            if (qualityRoll < m_CommonDropChance)
            {
                m_ItemReward = ItemDatabaseManager.Instance.GetRandomItemOfQuality(ItemBaseScript.ItemQuality.COMMON);
            }
            else if (qualityRoll < m_RareDropChance)
            {
                m_ItemReward = ItemDatabaseManager.Instance.GetRandomItemOfQuality(ItemBaseScript.ItemQuality.RARE);
            }
            else if (qualityRoll < m_EpicDropChance)
            {
                m_ItemReward = ItemDatabaseManager.Instance.GetRandomItemOfQuality(ItemBaseScript.ItemQuality.EPIC);
            }
            else if (qualityRoll < m_LegendaryDropChance)
            {
                m_ItemReward = ItemDatabaseManager.Instance.GetRandomItemOfQuality(ItemBaseScript.ItemQuality.LEGENDARY);
            }
        }
    }

    private void EndBattle()
    {
        GiveRewards();

        //Change score stats
        PlayerStats.instance.m_GoldTotal += m_GoldBattleReward;
    }

}
