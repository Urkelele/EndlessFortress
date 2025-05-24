using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemCellController : MonoBehaviour
{
    public ItemBaseScript m_ItemBaseScript;
    public Sprite[] m_ItemBoxSprites;
    public Image m_ItemIconBox;
    public Image m_ItemIcon;


    public bool StoreItem(ItemBaseScript item)
    {
        if (m_ItemBaseScript != null)
        {
            return false;
        }
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



}
