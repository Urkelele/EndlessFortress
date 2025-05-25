using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [Header("[PassiveItems,ActiveItems,Weapons]")]
    public ShopItemController[] m_ShopItemController;
    public ShopBuyPanelController m_ShopBuyPanelController;
    public int m_NumberPassiveItems = 2;
    public int m_NumberActiveItems = 1;
    public int m_NumberWeapons = 1;

    public GameObject m_ShopObject;
    public ShopRerollController m_ShopRerollController;
    public GameObject m_MainMenu;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            ClearShop();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SpawnShop();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RerollItemsInShop();
        }
    }

    public void SpawnShop()
    {
        m_ShopObject.SetActive(true);
        m_ShopBuyPanelController.CloseBuyPanel();
        m_ShopRerollController.SpawnReroll(this, false);
        ClearShop();
        SpawnItemsInShop();
    }
    public void RerollItemsInShop()
    {
        m_ShopObject.SetActive(true);
        m_ShopBuyPanelController.CloseBuyPanel();
        ClearShop();
        SpawnItemsInShop();
    }
    public void SpawnItemsInShop()
    {
        ItemBaseScript auxItem;
        ItemDatabaseManager m_ItemDatabaseManager = ItemDatabaseManager.Instance;
        for (int i = 0;i < m_NumberPassiveItems; i++)
        {
            auxItem = m_ItemDatabaseManager.GetRandomItem();
            if (auxItem.m_TypeItem == ItemBaseScript.ItemType.PASSIVE)
            {
                foreach(ShopItemController shopItem in m_ShopItemController)
                {
                    if (shopItem.m_ItemBaseScript == null)
                    {
                        shopItem.m_ItemBaseScript = auxItem;
                        shopItem.UpdateItemUI();
                        break;
                    }
                }
            }else { i--; }
        }
        for (int i = 0; i < m_NumberActiveItems; i++)
        {
            auxItem = m_ItemDatabaseManager.GetRandomItem();
            if (auxItem.m_TypeItem == ItemBaseScript.ItemType.ACTIVE)
            {
                foreach (ShopItemController shopItem in m_ShopItemController)
                {
                    if (shopItem.m_ItemBaseScript == null)
                    {
                        shopItem.m_ItemBaseScript = auxItem;
                        shopItem.UpdateItemUI();
                        break;
                    }
                }
            }
            else { i--; }
        }
        for (int i = 0; i < m_NumberWeapons; i++)
        {
            auxItem = m_ItemDatabaseManager.GetRandomItem();
            if (auxItem.m_TypeItem == ItemBaseScript.ItemType.LIGHT_WEAPON || auxItem.m_TypeItem == ItemBaseScript.ItemType.HEAVY_WEAPON)
            {
                foreach (ShopItemController shopItem in m_ShopItemController)
                {
                    if (shopItem.m_ItemBaseScript == null)
                    {
                        shopItem.m_ItemBaseScript = auxItem;
                        shopItem.UpdateItemUI();
                        break;
                    }
                }
            }
            else { i--; }
        }

    }

    public void ClearShop()
    {
        foreach(ShopItemController shopItem in m_ShopItemController)
        {
            shopItem.RemoveData();
        }
    }

}
