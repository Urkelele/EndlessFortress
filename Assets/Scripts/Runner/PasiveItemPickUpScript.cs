using UnityEngine;

public class PasiveItemPickUpScript : MonoBehaviour
{
    private Sprite m_ItemSprite = null;
    private SpriteRenderer m_SpriteRenderer = null;
    private ItemBaseScript m_ItemPrefab = null;
    private Collider m_Collider = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int index = Random.Range(0, ItemDatabaseManager.Instance.m_UnlockedItems.Count-1);
        m_ItemPrefab = ItemDatabaseManager.Instance.m_UnlockedItems[index];
        m_ItemSprite = m_ItemPrefab.m_SpriteItem;
        m_Collider = GetComponent<Collider>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.sprite = m_ItemSprite;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.instance.AddPassiveItem(m_ItemPrefab);
        }
    }
}
