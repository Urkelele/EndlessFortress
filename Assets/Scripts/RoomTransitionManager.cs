using System.Xml.Serialization;
using UnityEngine;

public class RoomTransitionManager : MonoBehaviour
{
    public static RoomTransitionManager instance;

    [Header("REFERENCES")]
    public Transform m_PlayerTransform = null;
    public EndlessRunnerTileManager m_EndlessRunnerTilesManager;
    public bool m_NextRoomIsBoss = false;
    public Transform m_PlayerRoomPos = null;
    public Transform m_PlayerRunnerPos = null; 

    [Header("ROOM GAMEOBJECTS")]
    public GameObject m_ShopRoom = null;
    public GameObject m_CombatRoom = null;
    public GameObject m_ChestRoom = null;
    public GameObject m_HealingRoom = null;

    [Header("CAMERAS")]
    public Camera m_CurrentActiveCamera = null;
    public Camera m_RunnerCamera = null;
    public Camera m_RoomCamera = null;

    [Header("CONTROL PARAMS")]
    public int m_NumOfRoomsBetweenBoss = 10;

    private GameObject m_CurrentRoom = null;


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

        m_EndlessRunnerTilesManager = FindAnyObjectByType<EndlessRunnerTileManager>();
        m_PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RoomTransition(TransitionType.COMBAT);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            RoomTransition(TransitionType.SHOP);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            RoomTransition(TransitionType.HEAL);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RoomTransition(TransitionType.RUNNER);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            RoomTransition(TransitionType.BOSS);
        }


    }

    /// <summary>
    /// Changes the current room to another one specified by param. Deactivates the current room gameObject and activates the desired one.
    /// </summary>
    /// <param name="nextRoom"></param>
    public void RoomTransition(TransitionType nextRoom)
    {
        m_EndlessRunnerTilesManager.DeactivateDoorsTile();

        
        //Default to room position and camera and change it later if they player does go to runner
        m_PlayerTransform.position = m_PlayerRoomPos.position;
        m_RunnerCamera.gameObject.SetActive(false);
        m_RoomCamera.gameObject.SetActive(true);
        m_CurrentActiveCamera = m_RoomCamera;

        switch (nextRoom)
        {
            case TransitionType.NONE:
                Debug.LogError("TRANSITION TYPE WAS NONE");
                return;
            case TransitionType.COMBAT:
                TransitionToCombat();
                break;
            case TransitionType.SHOP:
                TransitionToShop();
                break;
            case TransitionType.BOSS:
                TransitionToBoss();
                break;
            case TransitionType.HEAL:
                TransitionToHeal();
                break;
            case TransitionType.RUNNER:
                TransitionToRunner();
                //Tp the player to the runner pos
                m_PlayerTransform.position = m_PlayerRunnerPos.position;
                break;
            default:
                Debug.LogError("TRANSITION TYPE WAS NOT FOUND");
                break;
        }

    }

    public void TransitionToCombat()
    {
        m_CurrentRoom.SetActive(false);
        m_CurrentRoom = m_CombatRoom;
        m_CurrentRoom.SetActive(true);
    }
    public void TransitionToShop()
    {
        m_CurrentRoom.SetActive(false);
        m_CurrentRoom = m_ShopRoom;
        m_CurrentRoom.SetActive(true);
        Debug.LogWarning("TRANSITIONING TO SHOP");
        //StartShop();
    }
    public void TransitionToBoss()
    {
        m_CurrentRoom.SetActive(false);
        m_CurrentRoom = m_CombatRoom;
        m_CurrentRoom.SetActive(true);
        Debug.LogWarning("TRANSITIONING TO BOSS");
        //CombatManager.instance.StartCombat();
    }
    public void TransitionToHeal()
    {
        m_CurrentRoom.SetActive(false);
        m_CurrentRoom = m_HealingRoom;
        m_CurrentRoom.SetActive(true);
        Debug.LogWarning("TRANSITIONING TO HEAL");
        //StartHeal();
    }
    public void TransitionToChest()
    {
        m_CurrentRoom.SetActive(false);
        m_CurrentRoom = m_ChestRoom;
        m_CurrentRoom.SetActive(true);
        Debug.LogWarning("TRANSITIONING TO HEAL");
        //StartHeal();
    }

    public void TransitionToRunner()
    {
        Debug.LogWarning("TRANSITIONING TO RUNNER");
        
        m_EndlessRunnerTilesManager.CalculateTilesUntilDoors();
        CheckIfNextRoomsIsBoss();

        //Set cameras
        m_RoomCamera.gameObject.SetActive(false);
        m_RunnerCamera.gameObject.SetActive(true);
        m_CurrentActiveCamera = m_RunnerCamera;

        //StartRunner();
    }

    public void CheckIfNextRoomsIsBoss()
    {
        if ((PlayerStats.instance.m_RoomsCleared % m_NumOfRoomsBetweenBoss) == 0) m_NextRoomIsBoss = true;
        else m_NextRoomIsBoss = false;
    }

}

public enum TransitionType
{
    NONE = -1,
    COMBAT,
    SHOP,
    BOSS,
    HEAL,
    RUNNER
}
