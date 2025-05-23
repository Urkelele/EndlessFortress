using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public float m_TotalExtraHealth;
    public float m_TotalAttackSpeedMultiplier;
    public float m_TotalDamageReductionMultiplier;

    public int m_Gold;

    public ItemBaseScript m_CurrentActiveItem;
    public ItemBaseScript m_CurrentLightWeapon;
    public ItemBaseScript m_CurrentHeavyWeapon;


    public List<ItemBaseScript> m_PassiveItemsList = new List<ItemBaseScript>();

    private void Update()
    {
        //DEBUG
        if(Input.GetKeyDown(KeyCode.Space))
        {
            UpdateItems();
        }
    }

    /// <summary>
    /// Go through all passive items and addup their stats. Items buffs of Health gets added and multiplier multiplied 
    /// </summary>
    public void UpdateItems()
    {
        ExtraAttributesTo_0();
        float[] currentUpdates;
        for (int i = 0; i < m_PassiveItemsList.Count; i++)
        {
            currentUpdates = m_PassiveItemsList[i].GetExtraAttributes();
            m_TotalExtraHealth += currentUpdates[0];
            m_TotalAttackSpeedMultiplier *= currentUpdates[1];
            m_TotalDamageReductionMultiplier *= currentUpdates[2];
        }
    }
    public void ExtraAttributesTo_0()
    {
        m_TotalExtraHealth = 0;
        m_TotalAttackSpeedMultiplier = 0;
        m_TotalDamageReductionMultiplier = 0;
    }
}
