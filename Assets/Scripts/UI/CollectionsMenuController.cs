using TMPro;
using UnityEngine;

public class CollectionsMenuController : MonoBehaviour
{
    public bool m_IsInCollectionsMenu;
    public GameObject m_MainMenu;
    public GameObject m_CollectionsMenu;


    private void Start()
    {
        m_CollectionsMenu.SetActive(false);
        m_IsInCollectionsMenu = false;
    }
    public void CollectionsMenu()
    {
        if (!m_IsInCollectionsMenu)
        {
            m_IsInCollectionsMenu = true;
            m_CollectionsMenu.SetActive(true);
            m_MainMenu.SetActive(false);
        }
        else
        {
            m_IsInCollectionsMenu = false;
            m_CollectionsMenu.SetActive(false);
            m_MainMenu.SetActive(true);
        }
    }
}
