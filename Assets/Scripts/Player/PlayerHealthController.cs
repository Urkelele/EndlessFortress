using UnityEngine;

public class PlayerHealthController : HealthController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ReceiveDamage(float damageReceived)
    {
        base.ReceiveDamage(damageReceived * m_DamageReduction);
        PlayerStats.instance.m_CurrentHealthPoints = m_HealthPoints;
    }
    public override void HealDamage(float healing)
    {
        base.HealDamage(healing);
        PlayerStats.instance.m_CurrentHealthPoints = m_HealthPoints;
    }
}
