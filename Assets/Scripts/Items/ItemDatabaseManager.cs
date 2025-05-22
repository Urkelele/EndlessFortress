using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using static UnityEditor.Progress;

public class ItemDatabaseManager : MonoBehaviour
{
    public static ItemDatabaseManager Instance;

    public List<ItemBaseScript> m_AllItems = new List<ItemBaseScript>();
    public List<ItemBaseScript> m_CommonItems = new List<ItemBaseScript>();
    public List<ItemBaseScript> m_RareItems = new List<ItemBaseScript>();
    public List<ItemBaseScript> m_EpicItems = new List<ItemBaseScript>();
    public List<ItemBaseScript> m_LegendaryItems = new List<ItemBaseScript>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadAllItems();
        CreateTierLists();
    }

    private void LoadAllItems()
    {
        m_AllItems = new List<ItemBaseScript>(Resources.LoadAll<ItemBaseScript>("Items"));

        if (m_AllItems.Count == 0)
        {
            Debug.LogWarning("[ItemDatabaseManager] No items found in Resources/Items/");
        }
        else
        {
            Debug.Log($"[ItemDatabaseManager] Loaded {m_AllItems.Count} items.");
        }
    }


    private void CreateTierLists()
    {
        //Sorts by quality into lists
        foreach (ItemBaseScript item in m_AllItems)
        {
            switch (item.m_QualityItem)
            {
                case ItemBaseScript.ItemQuality.COMMON:
                    m_CommonItems.Add(item);
                    break;
                case ItemBaseScript.ItemQuality.RARE:
                    m_RareItems.Add(item);
                    break;
                case ItemBaseScript.ItemQuality.EPIC:
                    m_EpicItems.Add(item);
                    break;
                case ItemBaseScript.ItemQuality.LEGENDARY:
                    m_LegendaryItems.Add(item);
                    break;
                default:
                    Debug.LogWarning(item.name + " was not assigned proporly into a tier list");
                    break;
            }
        }
    }

    public ItemBaseScript GetRandomItem()
    {
        if (m_AllItems.Count == 0) return null;
        
        int randomItemIndex = Random.Range(0, m_AllItems.Count);
        return m_AllItems[randomItemIndex];
    }

    public ItemBaseScript GetRandomItemOfQuality(ItemBaseScript.ItemQuality quality)
    {
        if (m_AllItems.Count == 0) return null;

        int randomItemIndex = Random.Range(0, m_AllItems.Count);

        switch (quality)
        {
            case ItemBaseScript.ItemQuality.COMMON:
                return m_CommonItems[randomItemIndex];

            case ItemBaseScript.ItemQuality.RARE:
                return m_RareItems[randomItemIndex];

            case ItemBaseScript.ItemQuality.EPIC:
                return m_EpicItems[randomItemIndex];

            case ItemBaseScript.ItemQuality.LEGENDARY:
                return m_LegendaryItems[randomItemIndex];

            default:
                return null;
        }

    }
}