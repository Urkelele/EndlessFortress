using UnityEngine;

public class ShopMenuController : MonoBehaviour
{
    public bool m_IsInShopMenu;
    public GameObject m_MainMenu;
    public GameObject m_ShopMenu;

    private void Start()
    {
        m_ShopMenu.SetActive(false);
        m_IsInShopMenu = false;
    }
    public void ShopMenu()
    {
        if (!m_IsInShopMenu)
        {
            Time.timeScale = 0f;
            m_IsInShopMenu = true;
            m_ShopMenu.SetActive(true);
            m_MainMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            m_IsInShopMenu = false;
            m_ShopMenu.SetActive(false);
            m_MainMenu.SetActive(true);
        }
    }
}
