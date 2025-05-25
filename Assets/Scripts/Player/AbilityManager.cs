using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance;

    public enum Ability 
    { 
        None = -1, 
        IronWill = 0 
    };
    
    public float m_AbilityTotalCooldown = 10f;
    [SerializeField] Ability m_ChosenAbility = 0;
    [SerializeField] HealthController m_PlayerHealthController = null;
    [SerializeField] Outline m_PlayerOutline = null;
    public Sprite m_CurrentAbilitySprite;

    [Header("Shield Params")]
    [SerializeField] private float m_IronWillDamageReduction = 0.25f;
    [SerializeField] private float m_IronWillDurationSeconds = 5f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        m_PlayerHealthController = FindAnyObjectByType<PlayerHealthController>();
        m_PlayerOutline = GameObject.FindGameObjectWithTag("Player").GetComponent<Outline>();
    }
    public void UseAbility()
    {
        switch (m_ChosenAbility)
        {
            case Ability.IronWill:
                Debug.LogWarning("ironwill activated");
                m_PlayerHealthController.m_IncomingDamageMultiplier *= m_IronWillDamageReduction;
                m_PlayerOutline.enabled = true; //Enable the outline to give visual feedback
                Invoke("IronWillCancelation", m_IronWillDurationSeconds);
                break;

            default:
                Debug.LogWarning("Ability not chosen");
                break;
        }
    }

    private void IronWillCancelation()
    {
        m_PlayerHealthController.m_IncomingDamageMultiplier /= m_IronWillDamageReduction;
        m_PlayerOutline.enabled = false;
    }

}
