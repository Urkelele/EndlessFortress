using System.Security.Cryptography;
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
    public GameObject m_ChestRewardMenu;
    public GameObject m_DailyRewardMenu;

    [Header("Resources")]
    public GameObject m_TomesResource;
    public GameObject m_GoldResoruce;

    [Header("AUDIO")]
    [SerializeField] AudioSource m_AudioSource = null;
    [SerializeField] AudioClip m_ClickSound = null;

    [Header("Components")]
    public GameObject m_Player;

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
        GoToMainMenu();
    }

    public void RestartRun()
    {
        TimeManager.instance.m_StopTime = false;
        m_Player.SetActive(true);
        m_MainMenu.SetActive(false);
        m_MainGameUI.SetActive(true);
        m_DeadMenu.SetActive(false);
        m_CombatUI.SetActive(false);
        m_ShopMenu.SetActive(false);
        m_EndCombatMenu.SetActive(false);
        m_TomesResource.SetActive(false);
        m_GoldResoruce.SetActive(true);
        m_ChestRewardMenu.SetActive(false);
        m_DailyRewardMenu.SetActive(false);
        RunnerManager.instance.RestartRun();
    }

    public void ReturnToRunWithDelaySeconds(int delaySeconds)
    {
        Invoke("ReturnToRun",delaySeconds);
    }

    public void Revive()
    {
        if (m_Player.GetComponent<PlayerCombatScript>().isActiveAndEnabled && PlayerStats.instance.m_GoldTotal !=0)
        {
            StartCombat();
        }
        else
        {
            EndlessRunnerTileManager.Instance.m_IsInRunner = true;
            ReturnToRun();
        }
    }

    public void ReturnToRun()
    {
        TimeManager.instance.m_StopTime = false;
        m_Player.SetActive(true);
        m_MainMenu.SetActive(false);
        m_MainGameUI.SetActive(true);
        m_DeadMenu.SetActive(false);
        m_CombatUI.SetActive(false);
        m_ShopMenu.SetActive(false);
        m_EndCombatMenu.SetActive(false);
        m_TomesResource.SetActive(false);
        m_GoldResoruce.SetActive(true);
        m_ChestRewardMenu.SetActive(false);
        m_DailyRewardMenu.SetActive(false);
    }


    public void GoToMainMenu()
    {
        TimeManager.instance.m_StopTime = true;
        EndlessRunnerTileManager.Instance.ControlRunner(false);
        m_Player.SetActive(false);
        m_MainMenu.SetActive(true);
        m_MainGameUI.SetActive(false);
        m_DeadMenu.SetActive(false);
        m_CombatUI.SetActive(false);
        m_ShopMenu.SetActive(false);
        m_EndCombatMenu.SetActive(false);
        m_TomesResource.SetActive(true);
        m_GoldResoruce.SetActive(false);
        m_ChestRewardMenu.SetActive(false);
        m_DailyRewardMenu.SetActive(true);
    }

    public void RunFinished()
    {
        TimeManager.instance.m_StopTime = true;
        EndlessRunnerTileManager.Instance.ControlRunner(false);
        m_MainMenu.SetActive(false);
        m_MainGameUI.SetActive(false);
        m_DeadMenu.SetActive(true);
        
        m_CombatUI.SetActive(false);
        m_ShopMenu.SetActive(false);
        m_EndCombatMenu.SetActive(false);
        m_TomesResource.SetActive(true);
        m_GoldResoruce.SetActive(false);
        m_ChestRewardMenu.SetActive(false);
        m_DailyRewardMenu.SetActive(false);
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
        m_ChestRewardMenu.SetActive(false);
    }
    public void Endcombat()
    {
        TimeManager.instance.m_StopTime = true;
        m_MainMenu.SetActive(false);
        m_MainGameUI.SetActive(true);
        m_DeadMenu.SetActive(false);
        m_CombatUI.SetActive(false);
        m_ShopMenu.SetActive(false);
        m_EndCombatMenu.SetActive(true);
        m_TomesResource.SetActive(false);
        m_GoldResoruce.SetActive(true);
        m_ChestRewardMenu.SetActive(false);
        m_DailyRewardMenu.SetActive(false);
    }

    public void OpenShop()
    {
        TimeManager.instance.m_StopTime = true;
        FindAnyObjectByType<ShopController>().SpawnShop();
        m_MainMenu.SetActive(false);
        m_MainGameUI.SetActive(true);
        m_DeadMenu.SetActive(false);
        m_CombatUI.SetActive(false);
        m_ShopMenu.SetActive(true);
        m_EndCombatMenu.SetActive(false);
        m_TomesResource.SetActive(false);
        m_GoldResoruce.SetActive(true);
        m_ChestRewardMenu.SetActive(false);
        m_DailyRewardMenu.SetActive(false);
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
        FindAnyObjectByType<ChestRewardScript>().SpawnChestReward();
        m_ChestRewardMenu.SetActive(true);
        m_DailyRewardMenu.SetActive(false);
    }
    public void PlaySound()
    {
        m_AudioSource.Play();
    }

}
