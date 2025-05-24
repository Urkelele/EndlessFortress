using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemBoxController : MonoBehaviour
{
    public ItemBaseScript m_ItemBaseScript;
    public Sprite[] m_ItemBoxSprites;
    public Image m_ItemIconBox;
    public Image m_ItemIcon;
    //public TextMeshProUGUI m_ItemNameText;
    //public TextMeshProUGUI m_ItemDescriptionText;
    //public TextMeshProUGUI m_ItemTypeText;

    public bool StoreItem(ItemBaseScript item)
    {
        if (m_ItemBaseScript != null)
        {
            return false;
        }
        m_ItemBaseScript = item;
        m_ItemIconBox.sprite = m_ItemBoxSprites[(int)item.m_QualityItem];
        m_ItemIcon.sprite = item.m_SpriteItem;
        //m_ItemNameText.text = item.m_ItemName;
        //m_ItemDescriptionText.text = item.m_Description;
        //switch (item.m_TypeItem)
        //{
        //    case ItemBaseScript.ItemType.PASSIVE:
        //        m_ItemTypeText.text = "Passive";
        //        break;
        //    case ItemBaseScript.ItemType.ACTIVE:
        //        m_ItemTypeText.text = "Active";
        //        break;
        //    case ItemBaseScript.ItemType.LIGHT_WEAPON:
        //        m_ItemTypeText.text = "Light weapon";
        //        break;
        //    case ItemBaseScript.ItemType.HEAVY_WEAPON:
        //        m_ItemTypeText.text = "Heavy weapon";
        //        break;
        //}
        return true;
    }

    public bool RemoveItem(ItemBaseScript item)
    {
        if(m_ItemBaseScript == item)
        {
            m_ItemBaseScript = null;
            m_ItemIconBox.sprite = m_ItemBoxSprites[m_ItemBoxSprites.Length-1];
            m_ItemIcon.gameObject.SetActive(false);
            return true;
        }
        return false;
    }



}
