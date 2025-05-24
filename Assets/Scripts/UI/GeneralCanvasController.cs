using UnityEngine;

public class GeneralCanvasController : MonoBehaviour
{
    [Header("Menus")]
    public GameObject m_MainMenu;
    public GameObject m_MainGameUI;
    public GameObject m_DeadMenu;

    [Header("Resources")]
    public GameObject m_TomesResource;
    public GameObject m_GoldResoruce;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            RunFinished();
        }
    }

    private void Start()
    {
        m_MainMenu.SetActive(true);
        m_MainGameUI.SetActive(false);
        m_DeadMenu.SetActive(false);
        m_TomesResource.SetActive(true);
        m_GoldResoruce.SetActive(false);
    }

    public void StartGame()
    {
        m_MainMenu.SetActive(false);
        m_MainGameUI.SetActive(true);
        m_DeadMenu.SetActive(false);
        m_TomesResource.SetActive(false);
        m_GoldResoruce.SetActive(true);
    }
    public void GoToMainMenu()
    {
        m_MainMenu.SetActive(true);
        m_MainGameUI.SetActive(false);
        m_DeadMenu.SetActive(false);
        m_TomesResource.SetActive(true);
        m_GoldResoruce.SetActive(false);
    }

    public void RunFinished()
    {
        m_MainMenu.SetActive(false);
        m_MainGameUI.SetActive(false);
        m_DeadMenu.SetActive(true);
        m_TomesResource.SetActive(true);
        m_GoldResoruce.SetActive(false);
    }
}
