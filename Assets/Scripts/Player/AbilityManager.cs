using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public enum Ability { None = -1, Shield = 0 };
    [SerializeField] float m_AbilityTotalCooldown = 10f;
    private float m_AbilityCurrentCooldown = 0f;
    [SerializeField] Ability m_ChosenAbility = 0;
    [SerializeField] HealthController m_PlayerHealthController = null;

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
                case Ability.Shield:

                    break;

                default:
                    break;
            }
        }
    }
}
