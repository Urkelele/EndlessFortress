using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public enum DoorType
    {
        NONE = -1,
        COMBAT,
        SHOP,
        RANDOM,
        HEAL,
        BOSS
    }
    private Collider m_DoorCollider = null;
    public DoorType m_DoorType = DoorType.NONE;

    private void Start()
    {
        m_DoorCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //If not collided with player, ignore
        if (!other.CompareTag("Player")) return;

        //If the door is random we pick a random door, we subtract one from the length since NONE doesnt count
        if(m_DoorType == DoorType.RANDOM)
        {
            int numOfDoors = System.Enum.GetValues(typeof(DoorType)).Length - 1;
            DoorType randomDoor = (DoorType)Random.Range(0, numOfDoors);
            m_DoorType = randomDoor;
        }

        switch (m_DoorType)
        {
            case DoorType.NONE:
                Debug.LogError("DOOR WAS TYPE NONE");
                break;
            case DoorType.COMBAT:
                RoomTransitionManager.instance.TransitionToCombat();
                break;
            case DoorType.SHOP:
                RoomTransitionManager.instance.TransitionToShop();
                break;
            case DoorType.HEAL:
                RoomTransitionManager.instance.TransitionToHeal();
                break;
            case DoorType.BOSS:
                RoomTransitionManager.instance.TransitionToCombat();
                break;
            default:
                break;
        }
    }
}
