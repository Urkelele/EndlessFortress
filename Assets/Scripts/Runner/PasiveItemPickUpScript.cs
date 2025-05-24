using UnityEngine;

public class PasiveItemPickUpScript : MonoBehaviour
{
    private Sprite m_ItemSprite = null;
    private ItemBaseScript m_ItemPrefab = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int index = Random.Range(0, ItemDatabaseManager.Instance.m_UnlockedItems.Count);
        m_ItemPrefab = ItemDatabaseManager.Instance.m_UnlockedItems[index];
        m_ItemSprite = m_ItemPrefab.m_SpriteItem;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
