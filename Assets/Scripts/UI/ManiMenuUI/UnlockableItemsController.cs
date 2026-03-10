using TMPro;
using UnityEngine;

public class UnlockableItemsController : MonoBehaviour
{
    [SerializeField] private UnlockableItemCellController[] m_LockedItems;
    public Transform m_ParentInventoryCells;

    [Header("Showing item stats")]
    public InventoryShowItemInfo m_InventoryShowItemInfo;
    public GameObject m_SelectedItemImage;
    private Transform m_PreviousItemSelected = null;

    public GameObject m_ConfirmationButton;
    [SerializeField] private ItemBaseScript m_CurrentSelectedItem;

    public void UpdateLockedItems()
    {
        m_SelectedItemImage.SetActive(false);
        m_PreviousItemSelected = null;
        m_InventoryShowItemInfo.FinishedBackwardsAnimation();
        RemoveItems();
        m_LockedItems = m_ParentInventoryCells.GetComponentsInChildren<UnlockableItemCellController>();
        ItemDatabaseManager itemDataBase = ItemDatabaseManager.Instance;

        foreach (ItemBaseScript item in itemDataBase.m_LockedItems)
        {
            foreach (UnlockableItemCellController itemCell in m_LockedItems)
            {
                if (itemCell.StoreItem(item, this))
                {
                    break;
                }
            }
        }
    }

    public void RemoveItems()
    {
        foreach (UnlockableItemCellController itemCell in m_LockedItems)
        {
            itemCell.RemoveItem();
        }
    }

    public void ItemClicked(ItemBaseScript item, Transform cellPosition)
    {
        if ( m_PreviousItemSelected == null || cellPosition.position != m_PreviousItemSelected.position)
        {
            m_InventoryShowItemInfo.gameObject.SetActive(true);
            m_SelectedItemImage.SetActive(true);
            m_ConfirmationButton.SetActive(true);
            m_ConfirmationButton.GetComponentInChildren<TextMeshProUGUI>().text = "Cost: " + item.m_UnlockPrice + "<sprite index=0>!";
            m_PreviousItemSelected = cellPosition;
            m_SelectedItemImage.transform.position = cellPosition.position;
            m_InventoryShowItemInfo.ShowItemInfo(item);
            m_CurrentSelectedItem = item;
        }
        else
        {
            m_ConfirmationButton.SetActive(false);
            m_SelectedItemImage.SetActive(false);
            m_InventoryShowItemInfo.HideItemInfo();
            m_InventoryShowItemInfo.gameObject.SetActive(false);
            m_CurrentSelectedItem = null;
        }
    }
    public void UnlockItem()
    {
        if(ExternalDataManager.Instance.m_StoredData.m_AmountTomes >= m_CurrentSelectedItem.m_UnlockPrice)
        {
            ExternalDataManager.Instance.AddTomes(-m_CurrentSelectedItem.m_UnlockPrice);
            foreach (UnlockableItemCellController cell in m_LockedItems)
            {
                if (cell.m_ItemBaseScript == m_CurrentSelectedItem)
                {
                    cell.RemoveItem();
                }
            }
            ItemDatabaseManager.Instance.UnlockItem(m_CurrentSelectedItem);
            m_InventoryShowItemInfo.gameObject.SetActive(false);
            m_ConfirmationButton.SetActive(false);
        }
    }
}
