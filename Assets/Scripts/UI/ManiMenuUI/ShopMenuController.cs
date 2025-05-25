using TMPro;
using UnityEngine;

public class ShopMenuController : MonoBehaviour
{
    public bool m_IsInShopMenu;
    public GameObject m_MainMenu;
    public GameObject m_ShopMenu;

    [SerializeField] private ExternalDataManager m_ExternalDataManager;
    private void Start()
    {
        m_ShopMenu.SetActive(false);
        m_IsInShopMenu = false;
        m_ExternalDataManager = FindAnyObjectByType<ExternalDataManager>();

        
    }
    public void ShopMenu()
    {
        if (!m_IsInShopMenu)
        {
            m_IsInShopMenu = true;
            m_ShopMenu.SetActive(true);
            m_MainMenu.SetActive(false);
        }
        else
        {
            m_IsInShopMenu = false;
            m_ShopMenu.SetActive(false);
            m_MainMenu.SetActive(true);
        }
    }

    public void buyTome(TextMeshProUGUI textMesh)
    {
        // Take the text and take only the numbers without the x
        string aux = textMesh.text.Substring(1);
        m_ExternalDataManager.AddTomes(int.Parse(aux));
    }

    
}
