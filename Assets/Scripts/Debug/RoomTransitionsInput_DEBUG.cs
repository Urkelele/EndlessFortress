using UnityEngine;

public class RoomTransitionsInput_DEBUG : MonoBehaviour
{
#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            RoomTransitionManager.instance.RoomTransition(TransitionType.COMBAT);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            RoomTransitionManager.instance.RoomTransition(TransitionType.SHOP);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            RoomTransitionManager.instance.RoomTransition(TransitionType.HEAL);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            RoomTransitionManager.instance.RoomTransition(TransitionType.RUNNER);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            RoomTransitionManager.instance.m_NextRoomIsBoss = true;
            RoomTransitionManager.instance.RoomTransition(TransitionType.BOSS);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            RoomTransitionManager.instance.RoomTransition(TransitionType.CHEST);
        }
    }
#endif
}
