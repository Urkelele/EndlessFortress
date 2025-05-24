using UnityEngine;

public class HealthController : MonoBehaviour
{
    [Header("LIFE PARAMS")]
    public float m_MaxHealthPoints = 0.0f;
    public float m_CurrentHealthPoints = 0.0f;

    [Header("DAMAGE PARAMS")]
    public float m_IncomingDamageMultiplier = 1.0f; //This value is multiplied to incoming damage

    [Header("CONTROL")]
    public bool m_IsDead = false;

    [Header("Audio")]
    public AudioClip m_GettingDamagedSound;

    protected virtual void Awake()
    {
        m_CurrentHealthPoints = m_MaxHealthPoints;   
    }

    protected virtual void Update()
    {
        if (m_CurrentHealthPoints <= 0)
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
            m_CurrentHealthPoints -= damageReceived * m_IncomingDamageMultiplier;
            if (m_GettingDamagedSound != null)
            {
                // AÑADIR SOUND EFFECT MANAGER
                //SoundEffectsManager.instance.PlaySoundFXClip(m_DamageSound, transform, 0.8f);
            }
        }
    }

    /// <summary>
    /// Heals certain ammount, caps the total to maxhealthpoints
    /// </summary>
    /// <param name="healing"></param>
    public virtual void HealDamage(float healing)
    {
        if (m_CurrentHealthPoints < m_MaxHealthPoints)
        {
            m_CurrentHealthPoints += healing;

            if(m_CurrentHealthPoints > m_MaxHealthPoints)
            {
                m_CurrentHealthPoints = m_MaxHealthPoints;
            }
        }
    }
}
