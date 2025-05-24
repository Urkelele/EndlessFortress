using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemCellController : MonoBehaviour
{
    public ItemBaseScript m_ItemBaseScript;
    public InventoryControllerUI m_InventoryControllerUI;
    public Sprite[] m_ItemBoxSprites;
    public Image m_ItemIconBox;
    public Image m_ItemIcon;


    public bool StoreItem(ItemBaseScript item, InventoryControllerUI inventoryControllerUI)
    {
        if (m_ItemBaseScript != null)
        {
            return false;
        }
        m_InventoryControllerUI = inventoryControllerUI;
        m_ItemBaseScript = item;
        m_ItemIconBox.sprite = m_ItemBoxSprites[(int)item.m_QualityItem];
        m_ItemIcon.enabled = true;
        m_ItemIcon.sprite = item.m_SpriteItem;
        return true;
    }

    public void RemoveItem()
    {
        m_ItemBaseScript = null;
        m_ItemIconBox.sprite = m_ItemBoxSprites[m_ItemBoxSprites.Length - 1];
        m_ItemIcon.enabled = false;
    }

    public void ItemSelected()
    {
        if(m_ItemBaseScript != null)
        {
            m_InventoryControllerUI.ItemClicked(m_ItemBaseScript, transform);
        }
    }

}
