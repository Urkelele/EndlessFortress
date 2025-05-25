using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
//using static UnityEditor.Progress;

public class ItemDatabaseManager : MonoBehaviour
{
    public static ItemDatabaseManager Instance;

    public List<ItemBaseScript> m_AllItems = new List<ItemBaseScript>();
    public List<ItemBaseScript> m_UnlockedItems = new List<ItemBaseScript>();
    public List<ItemBaseScript> m_LockedItems = new List<ItemBaseScript>();

    [Header("QUALITY LISTS")]
    public List<ItemBaseScript> m_CommonItems = new List<ItemBaseScript>();
    public List<ItemBaseScript> m_RareItems = new List<ItemBaseScript>();
    public List<ItemBaseScript> m_EpicItems = new List<ItemBaseScript>();
    public List<ItemBaseScript> m_LegendaryItems = new List<ItemBaseScript>();

    [Header("TYPE LISTS")]
    public List<ItemBaseScript> m_ActiveItems = new List<ItemBaseScript>();
    public List<ItemBaseScript> m_PassiveItems = new List<ItemBaseScript>();
    public List<ItemBaseScript> m_AllWeapons = new List<ItemBaseScript>();
    public List<ItemBaseScript> m_LightWeapons = new List<ItemBaseScript>();
    public List<ItemBaseScript> m_HeavyWeapons = new List<ItemBaseScript>();

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
    }

    private void OnEnable()
    {
        CreateAllLists();
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

    public void RefreshUnlockedList()
    {
        m_UnlockedItems.Clear();
        CreateAllLists();
    }

    private void CreateAllLists()
    {
        foreach (ItemBaseScript item in ExternalDataManager.Instance.m_StoredData.unlockedScripts)
        {
            item.m_Unlocked = true;
            m_UnlockedItems.Add(item);
        }

        //Sorts by quality into lists
        foreach (ItemBaseScript item in m_UnlockedItems)
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
                    Debug.LogWarning(item.name + " was not assigned proporly into a quality list");
                    break;
            }

            switch (item.m_TypeItem)
            {
                case ItemBaseScript.ItemType.PASSIVE:
                    m_PassiveItems.Add(item);
                    break;
                case ItemBaseScript.ItemType.ACTIVE:
                    m_ActiveItems.Add(item);
                    break;
                case ItemBaseScript.ItemType.LIGHT_WEAPON:
                    m_AllWeapons.Add(item);
                    m_LightWeapons.Add(item);
                    break;
                case ItemBaseScript.ItemType.HEAVY_WEAPON:
                    m_AllWeapons.Add(item);
                    m_HeavyWeapons.Add(item);   
                    break;
                default:
                    Debug.LogWarning(item.name + " was not assigned proporly into a type list");
                    break;
            }
        }
        m_LockedItems.Clear();
        foreach(ItemBaseScript item in m_AllItems)
        {
            if(!item.m_Unlocked)
            {
                m_LockedItems.Add(item);
            }
        }
    }

    public void UnlockItem(ItemBaseScript item)
    {
        if(item.m_Unlocked == true)
        {
            Debug.Log("Tried to unlock a unlocked item");
        }
        item.m_Unlocked = true;
        ExternalDataManager.Instance.m_StoredData.unlockedScripts.Add(item);
        ExternalDataManager.Instance.SaveToJson();
        RefreshUnlockedList();
    }

    public ItemBaseScript GetRandomItem()
    {
        if (m_AllItems.Count == 0) return null;
        
        int randomItemIndex = Random.Range(0, m_AllItems.Count);
        return m_AllItems[randomItemIndex];
    }
    public ItemBaseScript GetRandomPasiveItem()
    {
        if (m_PassiveItems.Count == 0) return null;

        int randomItemIndex = Random.Range(0, m_PassiveItems.Count);
        return m_PassiveItems[randomItemIndex];
    }

    public ItemBaseScript GetRandomItemOfQuality(ItemBaseScript.ItemQuality quality)
    {
        if (ItemDatabaseManager.Instance.m_UnlockedItems.Count == 0) return null;

        int randomItemIndex = Random.Range(0, ItemDatabaseManager.Instance.m_UnlockedItems.Count);

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