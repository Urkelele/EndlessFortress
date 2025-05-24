using UnityEngine;

public class RoomTransitionsInput_DEBUG : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RoomTransitionManager.instance.RoomTransition(TransitionType.COMBAT);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            RoomTransitionManager.instance.RoomTransition(TransitionType.SHOP);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            RoomTransitionManager.instance.RoomTransition(TransitionType.HEAL);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RoomTransitionManager.instance.RoomTransition(TransitionType.RUNNER);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            RoomTransitionManager.instance.m_NextRoomIsBoss = true;
            RoomTransitionManager.instance.RoomTransition(TransitionType.BOSS);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            RoomTransitionManager.instance.RoomTransition(TransitionType.CHEST);
        }
    }
}
