using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float m_MaxHealthPoints;
    public float m_HealthPoints;
    public bool m_IsDead;

    [Header("Audio")]
    public AudioClip m_DamageSound;

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


    public void DealDamage(float damageDealt)
    {
        if (!m_IsDead)
        {
            m_HealthPoints -= damageDealt;
            if (m_DamageSound != null)
            {
                // AÑADIR SOUND EFFECT MANAGER
                //SoundEffectsManager.instance.PlaySoundFXClip(m_DamageSound, transform, 0.8f);
            }
        }
    }

    public void HealDamage(float healing)
    {
        if (m_HealthPoints < m_MaxHealthPoints)
        {
            m_HealthPoints += healing;
        }
    }
}
