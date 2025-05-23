using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemController : MonoBehaviour
{
    public ItemBaseScript m_ItemBaseScript;
    public GameObject m_ShopItemUI;
    public TextMeshProUGUI m_ShopItemPriceText;
    public TextMeshProUGUI m_ShopItemDescriptionText;
    public Image m_ShopItemIcon;

    public void UpdateItemUI()
    {
        //m_ShopItemUI.SetActive(true);
        m_ShopItemPriceText.text = m_ItemBaseScript.m_Price.ToString();
        m_ShopItemDescriptionText.text = m_ItemBaseScript.m_Description.ToString();
        m_ShopItemIcon.sprite = m_ItemBaseScript.m_SpriteItem;
    }

    public void RemoveData()
    {
        m_ItemBaseScript = null;
        //m_ShopItemUI.SetActive(false);
    }
}
