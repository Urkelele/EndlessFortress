using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public PlayerHealthController m_PlayerHealthController = null;
    
    public float m_TotalExtraHealth = 0;
    public float m_TotalAttackSpeedMultiplier = 1;
    public float m_TotalDamageDivider = 1;
    public float m_TotalAbilityCooldownReduction = 1;
    public float m_TotalAttackDamageMultiplier = 1;
    public float m_TotalGoldRewardMultipler = 1;
    public float m_TotalLifeSteal = 0;

    [SerializeField] private TriggerType m_ActiveTrigger = TriggerType.NONE;

    public int m_Gold;

    public BaseActiveScript m_CurrentActiveItem;
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

        m_PlayerHealthController = FindAnyObjectByType<PlayerHealthController>();
    }

    private void Update()
    {
        CheckTriggers();
    }

    /// <summary>
    /// Makes all items of the same trigger type activate
    /// </summary>
    /// <param name="triggerType"></param>
    public void EnableItemTrigger(TriggerType triggerType)
    {
        m_ActiveTrigger = triggerType;
    }

    private void CheckTriggers()
    {
        //Only check for triggers when they happen
        if (m_ActiveTrigger == TriggerType.NONE) return;

        Debug.LogWarning("TRIGGER:" +  m_ActiveTrigger);

        //Search through all items of the player and 
        foreach (ItemBaseScript item in m_PassiveItemsList)
        {
            if (item is TriggeredItemsScript triggeredItem && triggeredItem.TriggerType == m_ActiveTrigger)
            {
                Debug.Log("activated:" +  triggeredItem.name);
                triggeredItem.OnTriggerActivated();
            }
        }

        m_ActiveTrigger = TriggerType.NONE;
    }

    /// <summary>
    /// Go through all passive items and addup their stats. Items buffs of Health gets added and multiplier multiplied 
    /// </summary>
    public void UpdateItems()
    {
        //In order to correctly update health up item, which give maxHp but also give that amount of currentHp to the player when picked
        //check the player Hp before updating the items and after, and add the difference

        float maxHpBeforeUpdate = m_PlayerHealthController.m_MaxHealthPoints;

        ResetExtraAttributes();
        float[] currentUpdates;
        for (int i = 0; i < m_PassiveItemsList.Count; i++)
        {
            currentUpdates = m_PassiveItemsList[i].GetExtraAttributes();
            m_TotalExtraHealth += currentUpdates[0];
            m_TotalAttackSpeedMultiplier *= currentUpdates[1];
            m_TotalDamageDivider *= currentUpdates[2];
            m_TotalAbilityCooldownReduction *= currentUpdates[3];
            m_TotalAttackDamageMultiplier *= currentUpdates[4];
            m_TotalGoldRewardMultipler *= currentUpdates[5];
            m_TotalLifeSteal += currentUpdates[6];
        }

        //Update hp total of player
        m_PlayerHealthController.m_MaxHealthPoints = m_PlayerHealthController.m_BaseHp + m_TotalExtraHealth;

        //Compare maxHp before and after update and heal if necessary
        float maxHpAfterUpdate = m_PlayerHealthController.m_MaxHealthPoints;
        m_PlayerHealthController.HealDamage(maxHpAfterUpdate - maxHpBeforeUpdate);

        //Give the player its IncomingDamageMultiplier
        m_PlayerHealthController.m_IncomingDamageMultiplier = (1/m_TotalDamageDivider);
    }
    public void ResetExtraAttributes()
    {
        m_TotalExtraHealth = 0;
        m_TotalAttackSpeedMultiplier = 1;
        m_TotalDamageDivider = 1;
        m_TotalAbilityCooldownReduction = 1;
        m_TotalAttackDamageMultiplier = 1;
        m_TotalGoldRewardMultipler = 1;
        m_TotalLifeSteal = 0;
    }

    /// <summary>
    /// Adds new passive item to the list and updates the items
    /// </summary>
    /// <param name="newPassiveItem"></param>
    public void AddNewPassiveItem(ItemBaseScript newPassiveItem)
    {
        m_PassiveItemsList.Insert(0, newPassiveItem);
        UpdateItems();
    }

    /// <summary>
    /// Checks the list for items with the same name and removes the first one found and returns true, if no items where found returns false
    /// </summary>
    /// <param name="itemName"></param>
    /// <returns></returns>
    public bool RemovePassiveItem(string itemName)
    {
        foreach(ItemBaseScript item in m_PassiveItemsList)
        {
            if (item.m_ItemName == itemName)
            {
                m_PassiveItemsList.Remove(item);
                return true;
            }
        }

        return false;
    }

    public void AddNewLightWeapon(ItemBaseScript newLightWeapon)
    {
        m_CurrentLightWeapon = newLightWeapon;
    }

    public void AddNewHeavySword(ItemBaseScript newHeavyWeapon)
    {
        m_CurrentHeavyWeapon = newHeavyWeapon;
    }

    public void AddNewActive(BaseActiveScript newActiveItem)
    {
        m_CurrentActiveItem = newActiveItem;
    }

    public void AddGold (int goldAmount)
    {
        m_Gold +=(int)(goldAmount * m_TotalGoldRewardMultipler);
    }
}
