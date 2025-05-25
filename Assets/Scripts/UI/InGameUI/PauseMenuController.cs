using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController: MonoBehaviour
{
    public bool m_IsPaused = false;
    public GameObject m_PauseMenuPanel;
    public Scrollbar m_InventoryScrollBar;

    private void OnEnable()
    {
        m_PauseMenuPanel.SetActive(false);
    }

    void Start()
    {
        m_IsPaused = false;
        m_PauseMenuPanel.SetActive(false);
    }

    public void PauseMenu()
    {
        if(!m_IsPaused)
        {
            TimeManager.instance.m_StopTime = true;
            m_IsPaused = true;
            m_PauseMenuPanel.SetActive(true);
            GetComponent<InventoryControllerUI>().UpdateItemsInInventory();
            m_InventoryScrollBar.value = 1;
        }
        else
        {
            TimeManager.instance.m_StopTime = false;
            m_IsPaused = false;
            m_PauseMenuPanel.SetActive(false);

        }
    }
}
