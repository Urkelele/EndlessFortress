using UnityEngine;

public class InventoryControllerUI : MonoBehaviour
{   
    public GameObject m_SlotsPrefab;
    [SerializeField] private InventoryCellController[] m_InventoryCells;
    public Transform m_ParentInventoryCells;

    public void UpdateItemsInInventory()
    {
        m_InventoryCells = m_ParentInventoryCells.GetComponentsInChildren<InventoryCellController>();
        InventoryManager inventory = InventoryManager.instance;
        

    }
}
