using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    [Header("PLAYER ATTRIBUTES")]
    public float m_CurrentHealthPoints = 0.0f;
    public float m_ExtraHealthPoints = 0.0f;
    public float m_ExtraAttackSpeed = 0.0f;
    public float m_ExtraDamageReduction = 0.0f;
    public float m_ExtraGold = 0.0f;

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
