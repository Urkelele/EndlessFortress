using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public enum Abilitie { None = -1, Shield = 0 };
    float m_AbilityCooldown = 10f;
    Abilitie m_ChosenAbility = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    public void UseAbility()
    {
        switch (m_ChosenAbility)
        {
            case Abilitie.Shield:
                break;

            default:
                break;
        }
    }
}
