using UnityEngine;

public class RoomTransitionManager : MonoBehaviour
{
    public static RoomTransitionManager instance;
    
    public bool m_NextRoomIsBoss = false;

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

    public void TransitionToRunner()
    {
        Debug.LogWarning("TRANSITIONING TO RUNNER");
        CheckIfNextRoomsIsBoss();
        //StartRunner();
    }

    public void CheckIfNextRoomsIsBoss()
    {
        if ((PlayerStats.instance.m_RoomsCleared % m_NumOfRoomsBetweenBoss) == 0) m_NextRoomIsBoss = true;
        else m_NextRoomIsBoss = false;
    }
}
