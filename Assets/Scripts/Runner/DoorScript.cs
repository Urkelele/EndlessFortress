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
        BOSS,
        CHEST
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
            // int numOfDoors = System.Enum.GetValues(typeof(DoorType)).Length - 1;
            // DoorType randomDoor = (DoorType)Random.Range(0, numOfDoors);
            // m_DoorType = randomDoor;
            
            // Excluir NONE (-1) y RANDOM (2) del sorteo
            DoorType[] validDoors = new DoorType[]
            {
                DoorType.COMBAT,
                DoorType.SHOP,
                DoorType.HEAL,
                DoorType.BOSS,
                DoorType.CHEST
            };
            m_DoorType = validDoors[Random.Range(0, validDoors.Length)];
        }

        switch (m_DoorType)
        {
            case DoorType.NONE:
                Debug.LogError("DOOR WAS TYPE NONE");
                break;
            case DoorType.COMBAT:
                RoomTransitionManager.instance.RoomTransition(TransitionType.COMBAT);
                break;
            case DoorType.SHOP:
                RoomTransitionManager.instance.RoomTransition(TransitionType.SHOP);
                break;
            case DoorType.HEAL:
                RoomTransitionManager.instance.RoomTransition(TransitionType.HEAL);
                break;
            case DoorType.BOSS:
                RoomTransitionManager.instance.RoomTransition(TransitionType.BOSS);
                break;
            case DoorType.CHEST:
                RoomTransitionManager.instance.RoomTransition(TransitionType.CHEST);
                break;
            default:
                break;
        }

    }
}
