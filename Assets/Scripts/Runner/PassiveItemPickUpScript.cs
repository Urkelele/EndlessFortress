using UnityEngine;

public class PassiveItemPickUpScript : MonoBehaviour
{
    [SerializeField] ItemBaseScript m_Item = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Invoke("Initialize",0.5f);
    }
    public void Initialize()
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
