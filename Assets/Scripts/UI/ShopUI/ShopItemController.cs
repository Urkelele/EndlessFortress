using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemController : MonoBehaviour
{
    public ItemBaseScript m_ItemBaseScript;
    public TextMeshProUGUI m_ShopItemPriceText;
    public TextMeshProUGUI m_ShopItemDescriptionText;
    public TextMeshProUGUI m_ItemTypeBookMark;
    public Image m_ShopItemIcon;
    public int m_Price;

    public Image m_ItemBox;
    public Sprite[] m_ItemBoxSprites;

    public ShopBuyPanelController m_BuyPanel;
    public Transform m_BuyPanelSpawnPosition;
    public Button m_ItemButton;

    public GameObject m_Rerollpanel;

    public void UpdateItemUI()
    {
        // Reactivate all objects
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        m_ItemTypeBookMark.gameObject.SetActive(true);
        m_ItemBox.transform.GetChild(0).gameObject.SetActive(true);
        m_ItemButton.interactable = true;

        m_Price = m_ItemBaseScript.m_Price;
        m_ShopItemPriceText.text = m_Price.ToString();
        m_ShopItemDescriptionText.text = m_ItemBaseScript.m_Description.ToString();
        m_ShopItemIcon.sprite = m_ItemBaseScript.m_SpriteItem;
        m_ItemBox.sprite = m_ItemBoxSprites[(int)m_ItemBaseScript.m_QualityItem];

        switch(m_ItemBaseScript.m_TypeItem)
        {
            case ItemBaseScript.ItemType.PASSIVE:
                m_ItemTypeBookMark.text = "PASSIVE";
                break;
            case ItemBaseScript.ItemType.ACTIVE:
                m_ItemTypeBookMark.text = "ACTIVE";
                break;
            case ItemBaseScript.ItemType.LIGHT_WEAPON:
                m_ItemTypeBookMark.text = "LIGHT WEAPON";
                break;
            case ItemBaseScript.ItemType.HEAVY_WEAPON:
                m_ItemTypeBookMark.text = "HEAVY WEAPON";
                break;
        }
    }

    public void RemoveData()
    {
        m_ItemBaseScript = null;
        m_ItemTypeBookMark.GetComponentInParent<Animator>().SetTrigger("Play");
    }

    public void IsClicked()
    {
        if (!m_BuyPanel.m_IsClicked)
        {
            m_BuyPanel.transform.position = m_BuyPanelSpawnPosition.position;
            m_BuyPanel.GetComponent<ShopBuyPanelController>().SpawnBuyPanel(this);
        }
    }

    public void ItemBuy()
    {
        InventoryManager m_InventoryManager = InventoryManager.instance;
        m_InventoryManager.m_Gold -= m_Price;
        switch(m_ItemBaseScript.m_TypeItem)
        {
            case ItemBaseScript.ItemType.PASSIVE:
                m_InventoryManager.AddPassiveItem(m_ItemBaseScript);
                break;
            case ItemBaseScript.ItemType.ACTIVE:
                m_InventoryManager.AddNewActive(m_ItemBaseScript);
                break;
            case ItemBaseScript.ItemType.LIGHT_WEAPON:
                m_InventoryManager.AddNewLightWeapon(m_ItemBaseScript);
                break;
            case ItemBaseScript.ItemType.HEAVY_WEAPON:
                m_InventoryManager.AddNewHeavySword(m_ItemBaseScript);
                break;
        }
        m_ItemButton.interactable = false;
        m_ShopItemPriceText.gameObject.SetActive(false);
        m_ShopItemDescriptionText.gameObject.SetActive(false);
        m_ShopItemIcon.gameObject.SetActive(false);
        m_ItemTypeBookMark.gameObject.SetActive(false);
    }
}
