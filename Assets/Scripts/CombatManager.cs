using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatManager : MonoBehaviour
{
    [Header("CLASSES")]
    private PlayerCombatScript m_PlayerCombatScript;
    [SerializeField] private List<EnemyBaseScript> m_CombatEnemies;
    private InventoryManager m_InventoryManager;
    
    [Header("POSITIONS")]
    public Transform m_CharacterPosition;
    [SerializeField] private Transform[] m_EnemyPositions = new Transform[3];
    [SerializeField] private List<GameObject> m_EnemyPool;

    [Header("REWARDS")]
    public int m_GoldBattleReward = 0;

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
    }

    private void EndBattle()
    {
        GiveRewards();

        //Change score stats
        PlayerStats.instance.m_GoldTotal += m_GoldBattleReward;
    }

}
