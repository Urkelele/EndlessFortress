using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public ShopItemController[] m_ShopItemController;
    public int m_NumberPassiveItems = 4;
    public int m_NumberActiveItems = 1;
    public int m_NumberWeapons = 1;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            SpawnItemsInShop();
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            ExitShop();
        }
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
                    }
                }
            }
            else { i--; }
        }

    }

    public void ExitShop()
    {
        foreach(ShopItemController shopItem in m_ShopItemController)
        {
            shopItem.RemoveData();
        }
    }
}
