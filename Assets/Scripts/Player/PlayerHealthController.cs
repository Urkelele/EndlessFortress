using UnityEngine;

public class PlayerHealthController : HealthController
{
    public override void ReceiveDamage(float damageReceived)
    {
        base.ReceiveDamage(damageReceived);
        PlayerStats.instance.m_CurrentHealthPoints = m_HealthPoints;
    }
    public override void HealDamage(float healing)
    {
        base.HealDamage(healing);
        PlayerStats.instance.m_CurrentHealthPoints = m_HealthPoints;
    }
}
