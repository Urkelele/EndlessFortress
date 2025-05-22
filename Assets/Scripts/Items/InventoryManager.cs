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

    public GameObject m_PassiveItemsFolder;

    public ItemFunctionality m_CurrentAcitveItem;
    public ItemFunctionality m_CurrentLightWeapon;
    public ItemFunctionality m_CurrentHeavyWeapon;


    public List<ItemFunctionality> m_PassiveItemsList = new List<ItemFunctionality>();

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
        m_PassiveItemsList = m_PassiveItemsFolder.GetComponentsInChildren<ItemFunctionality>().ToList();
        float[] currentUpdates;
        for (int i = 0; i < m_PassiveItemsList.Count; i++)
        {
            currentUpdates = m_PassiveItemsList[i].GetExtraAttributes();
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

    public bool InsertItemInParent(ItemBaseScript.ItemType itemType, Transform itemObject)
    {
        switch (itemType)
        {
            case ItemBaseScript.ItemType.NONE:
                return false;

            case ItemBaseScript.ItemType.PASSIVE:
                itemObject.parent = m_PassiveItemsFolder.transform;
                break;

            case ItemBaseScript.ItemType.ACTIVE:
                m_CurrentAcitveItem = itemObject.GetComponent<ItemFunctionality>();
                itemObject.parent = transform;
                break;
            case ItemBaseScript.ItemType.LIGHT_WEAPON:
                m_CurrentLightWeapon = itemObject.GetComponent<ItemFunctionality>();
                itemObject.parent = transform;
                break;
            case ItemBaseScript.ItemType.HEAVY_WEAPON:
                m_CurrentHeavyWeapon = itemObject.GetComponent<ItemFunctionality>();
                itemObject.parent = transform;
                break;
        }
        return true;
    }
}
