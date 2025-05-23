using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    
    public float m_TotalExtraHealth = 0;
    public float m_TotalAttackSpeedMultiplier = 1;
    public float m_TotalDamageReductionMultiplier = 1;
    public float m_TotalAbilitySpeedMultiplier = 1;
    public float m_TotalAttackDamageMultiplier = 1;
    public float m_TotalGoldRewardMultipler = 1;
    public float m_TotalLifeSteal = 0;

    [SerializeField] private TriggerType m_Trigger = TriggerType.NONE;

    public int m_Gold;

    public ItemBaseScript m_CurrentActiveItem;
    public ItemBaseScript m_CurrentLightWeapon;
    public ItemBaseScript m_CurrentHeavyWeapon;

    public List<ItemBaseScript> m_PassiveItemsList = new List<ItemBaseScript>();

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
    }

    private void Update()
    {
        //DEBUG
        if(Input.GetKeyDown(KeyCode.Space))
        {
            UpdateItems();
        }

        CheckTriggers();
    }

    /// <summary>
    /// Makes all items of the same trigger type activate
    /// </summary>
    /// <param name="triggerType"></param>
    public void EnableItemTrigger(TriggerType triggerType)
    {
        m_Trigger = triggerType;
    }

    private void CheckTriggers()
    {
        //Only check for triggers when they happen
        if (m_Trigger == TriggerType.NONE) return;

        Debug.LogWarning("TRIGGER:" +  m_Trigger);

        //Search through all items of the player and 
        foreach (ItemBaseScript item in m_PassiveItemsList)
        {
            if (item is TriggeredItemsScript triggeredItem && triggeredItem.TriggerType == m_Trigger)
            {
                Debug.Log("activated:" +  triggeredItem.name);
                triggeredItem.OnTriggerActivated();
            }
        }

        m_Trigger = TriggerType.NONE;
    }

    /// <summary>
    /// Go through all passive items and addup their stats. Items buffs of Health gets added and multiplier multiplied 
    /// </summary>
    public void UpdateItems()
    {
        ResetExtraAttributes();
        float[] currentUpdates;
        for (int i = 0; i < m_PassiveItemsList.Count; i++)
        {
            currentUpdates = m_PassiveItemsList[i].GetExtraAttributes();
            m_TotalExtraHealth += currentUpdates[0];
            m_TotalAttackSpeedMultiplier *= currentUpdates[1];
            m_TotalDamageReductionMultiplier *= currentUpdates[2];
            m_TotalAbilitySpeedMultiplier *= currentUpdates[3];
            m_TotalAttackDamageMultiplier *= currentUpdates[4];
            m_TotalGoldRewardMultipler *= currentUpdates[5];
            m_TotalLifeSteal += currentUpdates[6];
        }

        
    }
    public void ResetExtraAttributes()
    {
        m_TotalExtraHealth = 0;
        m_TotalAttackSpeedMultiplier = 1;
        m_TotalDamageReductionMultiplier = 1;
        m_TotalAbilitySpeedMultiplier = 1;
        m_TotalAttackDamageMultiplier = 1;
        m_TotalGoldRewardMultipler = 1;
        m_TotalLifeSteal = 0;
    }
}
