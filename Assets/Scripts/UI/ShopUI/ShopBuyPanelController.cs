using TMPro;
using UnityEngine;

public class ShopBuyPanelController : MonoBehaviour
{
    public GameObject m_BuyPanel;
    public TextMeshProUGUI m_PriceText;
    public int m_Price = 0;

    public bool m_IsClicked;

    [SerializeField] private ShopItemController m_ShopItemController;

    public void SpawnBuyPanel(ShopItemController shopItemController)
    {
        m_ShopItemController = shopItemController;
        m_Price = shopItemController.m_Price;
        m_PriceText.text = m_Price.ToString();
        m_IsClicked = true;
        m_BuyPanel.SetActive(true);
    }

    public void CloseBuyPanel()
    {
        m_IsClicked = false;
        m_BuyPanel.SetActive(false);
    }

    public void ItemBuy()
    {
        m_IsClicked = false;
        m_ShopItemController.ItemBuy();
        m_BuyPanel.SetActive(false);
    }
}
