using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatManager : MonoBehaviour
{
    [Header("CLASSES")]
    private PlayerCombatScript m_PlayerCombatScript;
    public List<EnemyBaseScript> m_CombatEnemies;
    private InventoryManager m_InventoryManager;
    public EnemyBaseScript m_CurrentEnemyTarget;
    
    [Header("POSITIONS")]
    public Transform m_CharacterPosition;
    [SerializeField] private Transform[] m_EnemyPositions = new Transform[3];
    [SerializeField] private List<GameObject> m_EnemyPool;

    [Header("REWARDS")]
    public int m_GoldBattleReward = 0;
    public float m_ItemDropChange = 0.25f;
    public ItemBaseScript m_ItemReward = null;

    [Header("DROP CHANCES")]
    public float m_CommonDropChance = 0.5f;
    public float m_RareDropChance = 0.3f;
    public float m_EpicDropChance = 0.15f;
    public float m_LegendaryDropChance = 0.05f;

    private void StartBattle()
    {
        GetDefaultTargetEnemy();
    }

    private void GetDefaultTargetEnemy()
    {
        //Get random enemy, make it the target and make it so it "was clicked last" to mantain the outline around it
        //TODO: might give problems since the actual last object that was clicked does not turn off
        int randEnemyPos = Random.Range(0, m_EnemyPositions.Length);
        m_PlayerCombatScript.m_TargetEnemy = m_CombatEnemies[randEnemyPos];
        m_CombatEnemies[randEnemyPos].m_ClickDetection.m_IsLastObjectClicked = true;
    }

    private void SpawnEnemies()
    {

    }

    private void GiveRewards()
    {
        m_InventoryManager.m_Gold += m_GoldBattleReward;

        float itemSpawnroll = Random.value;

        if(itemSpawnroll < m_ItemDropChange)
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
