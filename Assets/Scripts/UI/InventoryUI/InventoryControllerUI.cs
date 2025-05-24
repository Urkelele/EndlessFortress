using UnityEngine;
using static UnityEditor.Progress;

public class InventoryControllerUI : MonoBehaviour
{   
    public GameObject m_ItemCellPrefab;
    [SerializeField] private InventoryItemCellController[] m_InventorytemsCells;
    public Transform m_ParentInventoryCells;
    public int m_NumberCellsPerFile;

    [Header("Active / LightWeapon / HeavyWeapon Cells")]
    public InventoryItemCellController m_ActiveItemCell;
    public InventoryItemCellController m_LightWeaponCell;
    public InventoryItemCellController m_HeavyWeaponCell;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            ItemBaseScript item = ItemDatabaseManager.Instance.GetRandomItem();
                if(item.m_TypeItem == ItemBaseScript.ItemType.PASSIVE )
            {
                InventoryManager.instance.AddNewPassiveItem(item);
            }
            Debug.Log("RemoveEndInput");
        }
    }

    public void UpdateItemsInInventory()
    {
        RemoveItems();
        
        m_InventorytemsCells = m_ParentInventoryCells.GetComponentsInChildren<InventoryItemCellController>();
        InventoryManager inventory = InventoryManager.instance;

        m_LightWeaponCell.StoreItem(inventory.m_CurrentLightWeapon);
        m_HeavyWeaponCell.StoreItem(inventory.m_CurrentHeavyWeapon);

        if(inventory.m_CurrentActiveItem != null)
        {
            m_ActiveItemCell.StoreItem(inventory.m_CurrentActiveItem);
        }

        int numberFilledCells = 0;
        foreach(ItemBaseScript item in inventory.m_PassiveItemsList)
        {
            if (numberFilledCells >= m_InventorytemsCells.Length)
            {
                for (int i = 0; i < m_NumberCellsPerFile; i++)
                {
                    Instantiate(m_ItemCellPrefab, m_ParentInventoryCells);
                }
                m_InventorytemsCells = m_ParentInventoryCells.GetComponentsInChildren<InventoryItemCellController>();
            }
            foreach (InventoryItemCellController itemCell in m_InventorytemsCells)
            {
                if(itemCell.StoreItem(item))
                {
                    numberFilledCells++;
                    break;
                }
            }            
        }
    }

    public void RemoveItems()
    {
        foreach (InventoryItemCellController itemCell in m_InventorytemsCells)
        {
            itemCell.RemoveItem();
        }
        m_ActiveItemCell.RemoveItem();
        m_LightWeaponCell.RemoveItem();
        m_HeavyWeaponCell.RemoveItem();
    }
}
