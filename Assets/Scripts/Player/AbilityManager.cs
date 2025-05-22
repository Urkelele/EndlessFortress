using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public enum Ability { None = -1, Shield = 0 };
    [SerializeField] float m_AbilityCooldown = 10f;
    [SerializeField] Ability m_ChosenAbility = 0;
    [SerializeField] HealthController m_PlayerHealthController = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    public void UseAbility()
    {
        switch (m_ChosenAbility)
        {
            case Ability.Shield:

                break;

            default:
                break;
        }
    }
}
