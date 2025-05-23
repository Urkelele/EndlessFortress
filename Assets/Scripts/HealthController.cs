using UnityEngine;

public class HealthController : MonoBehaviour
{
    [Header("LIFE PARAMS")]
    public float m_MaxHealthPoints = 0.0f;
    public float m_HealthPoints = 0.0f;

    [Header("DAMAGE PARAMS")]
    //Ranges from [0,1], is multiplied to incoming in order to mitigate part of it
    public float m_DamageReduction = 1.0f;

    [Header("CONTROL")]
    public bool m_IsDead;

    [Header("Audio")]
    public AudioClip m_GettingDamagedSound;

    private void Start()
    {
        m_HealthPoints = m_MaxHealthPoints;
    }

    private void Update()
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
            m_HealthPoints -= damageReceived * m_DamageReduction;
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
