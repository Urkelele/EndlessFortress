using System.Collections.Generic;
using UnityEngine;

public class DoorTileScript : MonoBehaviour
{
    public List<GameObject> m_NonBossDoorPrefabs = new List<GameObject>();
    public GameObject m_BossDoorPrefab = null;
    public List<GameObject> m_SpawnedDoors = new List<GameObject>();
    public List<Transform> m_DoorPositions = new List<Transform>();

    private void OnEnable()
    {
        //If the next room is a boss spawn just a boss door
        if(RoomTransitionManager.instance.m_NextRoomIsBoss)
        {
            //Instantiate the bossDoor as a child of the middle door position
            Instantiate(m_BossDoorPrefab, m_DoorPositions[1]);
            m_SpawnedDoors.Add(m_BossDoorPrefab);
        }
        else
        {
            for(int i = 0; i < m_DoorPositions.Count; i++)
            {
                
            }
        }
    }

    public void PopRandomDoorFromList()
    {

    }

    //When the tile is disabled, destroy every spawned door
    private void OnDisable()
    {
        foreach(GameObject go in m_SpawnedDoors)
        {
            Destroy(go);
        }
    }

}
