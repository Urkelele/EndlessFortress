using UnityEngine;
using UnityEngine.Rendering;

public class GeneralCanvasManager : MonoBehaviour
{
    public static GeneralCanvasManager instance;

    [Header("Menus")]
    public GameObject m_MainMenu;
    public GameObject m_MainGameUI;
    public GameObject m_DeadMenu;
    public GameObject m_CombatUI;
    public GameObject m_ShopMenu;
    public GameObject m_EndCombatMenu;

    [Header("Resources")]
    public GameObject m_TomesResource;
    public GameObject m_GoldResoruce;

    [Header("AUDIO")]
    [SerializeField] AudioSource m_AudioSource = null;
    [SerializeField] AudioClip m_ClickSound = null;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
        m_MainMenu.SetActive(true);
        m_MainGameUI.SetActive(false);
        m_DeadMenu.SetActive(false);
        m_CombatUI.SetActive(false);
        m_ShopMenu.SetActive(false);
        m_EndCombatMenu.SetActive(false);
        m_TomesResource.SetActive(true);
        m_GoldResoruce.SetActive(false);
    }

    public void StartRun()
    {
        m_MainMenu.SetActive(false);
        m_MainGameUI.SetActive(true);
        m_DeadMenu.SetActive(false);
        m_CombatUI.SetActive(false);
        m_ShopMenu.SetActive(false);
        m_EndCombatMenu.SetActive(false);
        m_TomesResource.SetActive(false);
        m_GoldResoruce.SetActive(true);
    }
    public void GoToMainMenu()
    {
        m_MainMenu.SetActive(true);
        m_MainGameUI.SetActive(false);
        m_DeadMenu.SetActive(false);
        m_CombatUI.SetActive(false);
        m_ShopMenu.SetActive(false);
        m_EndCombatMenu.SetActive(false);
        m_TomesResource.SetActive(true);
        m_GoldResoruce.SetActive(false);
    }

    public void RunFinished()
    {
        m_MainMenu.SetActive(false);
        m_MainGameUI.SetActive(false);
        m_DeadMenu.SetActive(true);
        m_CombatUI.SetActive(false);
        m_ShopMenu.SetActive(false);
        m_EndCombatMenu.SetActive(false);
        m_TomesResource.SetActive(true);
        m_GoldResoruce.SetActive(false);
    }

    public void StartCombat()
    {
        m_MainMenu.SetActive(false);
        m_MainGameUI.SetActive(true);
        m_DeadMenu.SetActive(false);
        m_CombatUI.SetActive(true);
        m_ShopMenu.SetActive(false);
        m_EndCombatMenu.SetActive(false);
        m_TomesResource.SetActive(false);
        m_GoldResoruce.SetActive(true);
    }
    public void Endcombat()
    {
        m_MainMenu.SetActive(false);
        m_MainGameUI.SetActive(true);
        m_DeadMenu.SetActive(false);
        m_CombatUI.SetActive(false);
        m_ShopMenu.SetActive(false);
        m_EndCombatMenu.SetActive(true);
        m_TomesResource.SetActive(false);
        m_GoldResoruce.SetActive(true);
    }

    public void OpenShop()
    {
        m_MainMenu.SetActive(false);
        m_MainGameUI.SetActive(true);
        m_DeadMenu.SetActive(false);
        m_CombatUI.SetActive(false);
        m_ShopMenu.SetActive(true);
        m_EndCombatMenu.SetActive(false);
        m_TomesResource.SetActive(false);
        m_GoldResoruce.SetActive(true);
    }

    
    public void OpenChest()
    {
        m_MainMenu.SetActive(false);
        m_MainGameUI.SetActive(true);
        m_DeadMenu.SetActive(false);
        m_CombatUI.SetActive(false);
        m_ShopMenu.SetActive(false);
        m_EndCombatMenu.SetActive(false);
        m_TomesResource.SetActive(false);
        m_GoldResoruce.SetActive(true);
    }
    public void PlaySound()
    {
        m_AudioSource.Play();
    }

}
