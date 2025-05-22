using UnityEngine;
using System.Collections.Generic;

public class ItemDatabaseManager : MonoBehaviour
{
    public static ItemDatabaseManager Instance;

    public List<ItemBaseScript> m_AllItems = new List<ItemBaseScript>();

    private void Awake()
    {
        // Singleton simple
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadAllItems();
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

    public ItemBaseScript GetRandomItem()
    {
        if (m_AllItems.Count == 0) return null;
        return m_AllItems[Random.Range(0, m_AllItems.Count)];
    }
}