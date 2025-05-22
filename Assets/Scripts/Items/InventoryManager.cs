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



    public List<ItemFunctionality> m_ItemsList = new List<ItemFunctionality>();

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            UpdateItems();
        }
    }

    public void UpdateItems()
    {
        ExtraAttributesTo_0();
        m_ItemsList = GetComponentsInChildren<ItemFunctionality>().ToList();
        float[] currentUpdates;
        for (int i = 0; i < m_ItemsList.Count; i++)
        {
            currentUpdates = m_ItemsList[i].GetExtraAttributes();
            m_TotalExtraHealth += currentUpdates[0];
            m_TotalAttackSpeedMultiplier += currentUpdates[1];
            m_TotalDamageReductionMultiplier += currentUpdates[2];
        }
    }
    public void ExtraAttributesTo_0()
    {
        m_TotalExtraHealth = 0;
        m_TotalAttackSpeedMultiplier = 0;
        m_TotalDamageReductionMultiplier = 0;
    }
}
