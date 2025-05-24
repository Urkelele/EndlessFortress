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
        CombatManager.instance.StartCombat();
    }
    public void TransitionToShop()
    {
        //StartShop();
    }
    public void TransitionToBoss()
    {
        CombatManager.instance.StartCombat();
    }
    public void TransitionToHeal()
    {
        //StartHeal();
    }

    public void TransitionToRunner()
    {
        CheckIfNextRoomsIsBoss();
        //StartRunner();
    }

    public void CheckIfNextRoomsIsBoss()
    {
        if ((PlayerStats.instance.m_RoomsCleared % m_NumOfRoomsBetweenBoss) == 0) m_NextRoomIsBoss = true;
        else m_NextRoomIsBoss = false;
    }
}
