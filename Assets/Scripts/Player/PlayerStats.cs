using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    [Header("SCORE PARAMS")]
    public int m_EnemiesSlain = 0;
    public int m_RoomsCleared = 0;
    public int m_GoldTotal = 0;

    [Header("POINTS FOR EACH SCORE TYPE")]
    public int m_EachEnemySlain = 5;
    public int m_EachRoomCleared = 5;
    public int m_EachGoldGathered = 1;


    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }

    public int CalculateScore()
    {
        return (m_EnemiesSlain * m_EachEnemySlain) + (m_RoomsCleared * m_EachRoomCleared) + (m_GoldTotal * m_EachGoldGathered);
    }
}
