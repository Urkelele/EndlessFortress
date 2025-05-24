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
    [SerializeField] private PlayerCombatScript m_PlayerCombatScript = null;
    [SerializeField] private PlayerMovement m_PlayerMovement = null;

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

    [SerializeField] private GameObject m_CurrentRoom = null;


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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        m_PlayerTransform = player.transform;
        m_PlayerCombatScript = player.GetComponent<PlayerCombatScript>();
        m_PlayerMovement = player.GetComponent<PlayerMovement>();
    }


    /// <summary>
    /// Changes the current room to another one specified by param. Deactivates the current room gameObject and activates the desired one.
    /// </summary>
    /// <param name="nextRoom"></param>
    public void RoomTransition(TransitionType nextRoom)
    {
        m_EndlessRunnerTilesManager.DeactivateDoorsTile();

        //If the player is in the runner (null) they will go a room and viceversa
        if(m_CurrentRoom == null)
        {
            //Deactivate the runner manager
            m_EndlessRunnerTilesManager.enabled = false;

            //Tp to room
            m_PlayerTransform.position = m_PlayerRoomPos.position;
            m_PlayerTransform.rotation = m_PlayerRoomPos.rotation;
            
            //Change camera
            m_RunnerCamera.gameObject.SetActive(false);
            m_RoomCamera.gameObject.SetActive(true);
            m_CurrentActiveCamera = m_RoomCamera;
        }
        else
        {
            //Activate the runner manager
            m_EndlessRunnerTilesManager.enabled = true;

            //Deactivate the room the player was in
            m_CurrentRoom.SetActive(false);
            m_CurrentRoom = null;
            
            //Tp the player runner
            m_PlayerTransform.position = m_PlayerRunnerPos.position;
            m_PlayerTransform.rotation = m_PlayerRunnerPos.rotation;
            
            //Change camera
            m_RoomCamera.gameObject.SetActive(false);
            m_RunnerCamera.gameObject.SetActive(true);
            m_CurrentActiveCamera = m_RunnerCamera;
        }



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
                TransitionToCombat();
                break;
            case TransitionType.HEAL:
                TransitionToHeal();
                break;
            case TransitionType.RUNNER:
                TransitionToRunner();
                break;
            case TransitionType.CHEST:
                TransitionToChest();
                break;
            default:
                Debug.LogError("TRANSITION TYPE WAS NOT FOUND");
                break;
        }

    }

    private void TransitionToCombat()
    {
        Debug.LogWarning("TRANSITIONING TO COMBAT");
        m_CurrentRoom = m_CombatRoom;
        m_CurrentRoom.SetActive(true);

        //enable combat and disable movement
        m_PlayerCombatScript.enabled = true;
        m_PlayerMovement.enabled = false;

        CombatManager.instance.StartCombat();
    }
    private void TransitionToShop()
    {
        Debug.LogWarning("TRANSITIONING TO SHOP");
        m_CurrentRoom = m_ShopRoom;
        m_CurrentRoom.SetActive(true);
    }

    private void TransitionToHeal()
    {
        Debug.LogWarning("TRANSITIONING TO HEAL");
        m_CurrentRoom = m_HealingRoom;
        m_CurrentRoom.SetActive(true);
    }

    void TransitionToChest()
    {
        Debug.LogWarning("TRANSITIONING TO CHEST");
        m_CurrentRoom = m_ChestRoom;
        m_CurrentRoom.SetActive(true);
    }

    private void TransitionToRunner()
    {
        Debug.LogWarning("TRANSITIONING TO RUNNER");

        m_EndlessRunnerTilesManager.CalculateTilesUntilDoors();
        CheckIfNextRoomsIsBoss();

        //enable movement and disable combat
        m_PlayerCombatScript.enabled = false;
        m_PlayerMovement.enabled = true;
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
    RUNNER,
    CHEST
}
