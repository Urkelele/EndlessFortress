using UnityEngine;

public class PassiveItemPickUpScript : MonoBehaviour
{
    [SerializeField] ItemBaseScript m_Item = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        //int index = Random.Range(0, ItemDatabaseManager.Instance.m_UnlockedItems.Count - 1);
        //m_Item = ItemDatabaseManager.Instance.m_UnlockedItems[index];
        m_Item = ItemDatabaseManager.Instance.GetRandomPasiveItem();
        GetComponent<SpriteRenderer>().sprite = m_Item.m_SpriteItem;
    }

    void OnEnable()
    {
        m_Item = ItemDatabaseManager.Instance.GetRandomPasiveItem();
        GetComponent<SpriteRenderer>().sprite = m_Item.m_SpriteItem;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.instance.AddNewPassiveItem(m_Item);
        }
    }
}
