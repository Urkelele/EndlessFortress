using System.Collections.Generic;
using UnityEngine;

public class DoorTileScript : MonoBehaviour
{
    public List<GameObject> m_NonBossDoorPrefabs = new List<GameObject>();
    private List<GameObject> m_DoorList = new List<GameObject>(); //List that is used to spawn the doors
    public GameObject m_BossDoorPrefab = null;
    private List<GameObject> m_SpawnedDoors = new List<GameObject>();
    public List<Transform> m_DoorPositions = new List<Transform>();

    private void OnEnable()
    {
        //If the next room is a boss spawn just a boss door
        if(RoomTransitionManager.instance.m_NextRoomIsBoss)
        {
            //Instantiate the bossDoor as a child of the middle door position
            GameObject bossDoor = Instantiate(m_BossDoorPrefab, m_DoorPositions[1]);
            m_SpawnedDoors.Add(bossDoor);
        }
        else
        {
            //Get random doors without repetition and instantiate them in the correct positions
            m_DoorList = new List<GameObject>(m_NonBossDoorPrefabs);
            for(int i = 0; i < m_DoorPositions.Count; i++)
            {
                GameObject randDoor = Instantiate(PopRandomDoorFromList(), m_DoorPositions[i]);
                m_SpawnedDoors.Add(randDoor);
            }
        }
    }

    /// <summary>
    /// Returns a random door and remove it from the list
    /// </summary>
    /// <returns></returns>
    public GameObject PopRandomDoorFromList()
    {
        Debug.Log(m_DoorList.Count);
        int randomIndex = Random.Range(0, m_DoorList.Count);
        GameObject randDoor = m_DoorList[randomIndex];
        m_DoorList.Remove(randDoor);
        return randDoor;
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
