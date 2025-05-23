using UnityEngine;

public class HealthController : MonoBehaviour
{
    [Header("LIFE PARAMS")]
    public float m_MaxHealthPoints = 0.0f;
    public float m_HealthPoints = 0.0f;

    [Header("DAMAGE PARAMS")]
    public float m_IncomingDamageMultiplier = 1.0f; //This value is multiplied to incoming damage

    [Header("CONTROL")]
    public bool m_IsDead = false;

    [Header("Audio")]
    public AudioClip m_GettingDamagedSound;

    protected virtual void Awake()
    {
        m_HealthPoints = m_MaxHealthPoints;   
    }

    protected virtual void Update()
    {
        if (m_HealthPoints <= 0)
        {
            m_IsDead = true;
        }
        else
        {
            m_IsDead = false;
        }
    }


    public virtual void ReceiveDamage(float damageReceived)
    {
        if (!m_IsDead)
        {
            // Subtract life taking into account damage reduction
            m_HealthPoints -= damageReceived * m_IncomingDamageMultiplier;
            if (m_GettingDamagedSound != null)
            {
                // AÑADIR SOUND EFFECT MANAGER
                //SoundEffectsManager.instance.PlaySoundFXClip(m_DamageSound, transform, 0.8f);
            }
        }
    }

    public virtual void HealDamage(float healing)
    {
        if (m_HealthPoints < m_MaxHealthPoints)
        {
            m_HealthPoints += healing;
        }
    }
}
