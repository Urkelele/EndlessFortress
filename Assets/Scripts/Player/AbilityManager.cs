using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public enum Ability { None = -1, IronWill = 0 };
    [SerializeField] float m_AbilityTotalCooldown = 10f;
    private float m_AbilityCurrentCooldown = 0f;
    [SerializeField] Ability m_ChosenAbility = 0;
    [SerializeField] HealthController m_PlayerHealthController = null;

    [Header("Shiel Params")]
    [SerializeField] private float m_IronWillDamageReduction = 0.25f;
    [SerializeField] private float m_IronWillDurationSeconds = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_AbilityCurrentCooldown = m_AbilityTotalCooldown;
    }
    private void Update()
    {
        m_AbilityCurrentCooldown -=Time.deltaTime;
    }
    public void UseAbility()
    {
        if(m_AbilityCurrentCooldown < 0f)
        {
            m_AbilityCurrentCooldown = m_AbilityTotalCooldown;
            switch (m_ChosenAbility)
            {
                case Ability.IronWill:

                    m_PlayerHealthController.m_DamageReduction *= m_IronWillDamageReduction;
                    Invoke("IronWillCancelation", m_IronWillDurationSeconds);
                    break;

                default:
                    break;
            }
        }
    }

    private void IronWillCancelation()
    {
        m_PlayerHealthController.m_DamageReduction = m_PlayerHealthController.m_DamageReduction / m_IronWillDamageReduction;
    }

}
