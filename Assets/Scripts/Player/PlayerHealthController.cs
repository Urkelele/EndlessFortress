using UnityEngine;

public class PlayerHealthController : HealthController
{
    public float m_BaseHp = 100f;

    protected override void Awake()
    {
        m_MaxHealthPoints = m_BaseHp;
        m_CurrentHealthPoints = m_MaxHealthPoints;
    }
}
