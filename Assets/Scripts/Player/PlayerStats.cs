using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    [Header("SCORE PARAMS")]
    public int m_EnemiesSlain = 0;
    public int m_RoomsCleared = 0;
    public int m_GoldTotal = 0;

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
}
