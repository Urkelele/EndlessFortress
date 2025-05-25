using UnityEngine;
using static UnityEditor.Progress;

public class InventoryControllerUI : MonoBehaviour
{
    public GameObject m_ItemCellPrefab;
    public InventoryItemCellController[] m_InventorytemsCells;
    public Transform m_ParentInventoryCells;
    public int m_NumberCellsPerFile;

    [Header("Showing item stats")]
    public InventoryShowItemInfo m_InventoryShowItemInfo;
    public GameObject m_SelectedItemImage;
    private Transform m_PreviousItemSelected = null;

    [Header("Active / LightWeapon / HeavyWeapon Cells")]
    public InventoryItemCellController m_ActiveItemCell;
    public InventoryItemCellController m_LightWeaponCell;
    public InventoryItemCellController m_HeavyWeaponCell;

    public virtual void UpdateItemsInInventory()
    {
        m_SelectedItemImage.SetActive(false);
        m_InventoryShowItemInfo.FinishedBackwardsAnimation();
        RemoveItems();

        m_InventorytemsCells = m_ParentInventoryCells.GetComponentsInChildren<InventoryItemCellController>();
        InventoryManager inventory = InventoryManager.instance;

        m_LightWeaponCell.StoreItem(inventory.m_CurrentLightWeapon, this);
        m_HeavyWeaponCell.StoreItem(inventory.m_CurrentHeavyWeapon, this);

        if (inventory.m_CurrentActiveItem != null)
        {
            m_ActiveItemCell.StoreItem(inventory.m_CurrentActiveItem, this);
        }

        int numberFilledCells = 0;
        foreach (ItemBaseScript item in inventory.m_PassiveItemsList)
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
                if (itemCell.StoreItem(item, this))
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

    public void ItemClicked(ItemBaseScript item, Transform cellPosition)
    {
        if (cellPosition.position != m_PreviousItemSelected.position)
        {
            m_SelectedItemImage.SetActive(true);
            m_PreviousItemSelected = cellPosition;
            m_SelectedItemImage.transform.position = cellPosition.position;
            m_InventoryShowItemInfo.ShowItemInfo(item);
        }
        else
        {
            m_SelectedItemImage.SetActive(false);
            m_InventoryShowItemInfo.HideItemInfo();
        }
    }
}
