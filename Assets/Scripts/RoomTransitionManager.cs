using UnityEngine;

public class RoomTransitionManager : MonoBehaviour
{
    public static RoomTransitionManager instance;
    public EndlessRunnerTileManager m_EndlessRunnerTilesManager;
    public bool m_NextRoomIsBoss = false;
    public Transform m_PlayerRoomPositiom = null;

    [Header("ROOM GAMEOBJECTS")]
    public GameObject m_ShopRoom = null;
    public GameObject m_CombatRoom = null;
    public GameObject m_ChestRoom = null;
    public GameObject m_HealingRoom = null;

    private TransitionType m_CurrentRoom = TransitionType.NONE;

    public int m_NumOfRoomsBetweenBoss = 10;

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

    /// <summary>
    /// Changes the current room to another one, specified by param
    /// </summary>
    /// <param name="nextRoom"></param>
    public void RoomTransition(TransitionType nextRoom)
    {
        m_EndlessRunnerTilesManager.DeactivateDoorsTile();

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
                break;
            default:
                Debug.LogError("TRANSITION TYPE WAS NOT FOUND");
                break;
        }

        m_CurrentRoom = nextRoom;
    }

    public void TransitionToCombat()
    {
        Debug.LogWarning("TRANSITIONING TO COMBAT");
        //CombatManager.instance.StartCombat();
    }
    public void TransitionToShop()
    {
        Debug.LogWarning("TRANSITIONING TO SHOP");
        //StartShop();
    }
    public void TransitionToBoss()
    {
        Debug.LogWarning("TRANSITIONING TO BOSS");
        //CombatManager.instance.StartCombat();
    }
    public void TransitionToHeal()
    {
        Debug.LogWarning("TRANSITIONING TO HEAL");
        //StartHeal();
    }
    public void TransitionToChest()
    {
        Debug.LogWarning("TRANSITIONING TO HEAL");
        //StartHeal();
    }

    public void TransitionToRunner()
    {
        Debug.LogWarning("TRANSITIONING TO RUNNER");
        m_EndlessRunnerTilesManager.RecoverTilesUntilDoors();
        CheckIfNextRoomsIsBoss();
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
