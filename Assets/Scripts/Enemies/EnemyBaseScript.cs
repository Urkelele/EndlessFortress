using UnityEngine;

public class EnemyBaseScript : MonoBehaviour
{
    [Header("BASE ENEMY PARAMS")]
    public HealthController m_HealthController = null;
    public PlayerCombatScript m_PlayerCombatScript = null;
    public Collider m_Collider = null;
    public Animator m_Animator = null;
    public float m_TotalActionCooldown = 0.0f;
    public float m_CurrentActionCooldown = 0.0f;
    public bool m_IsTargeted = false;

    private void Start()
    {
        m_HealthController = GetComponent<HealthController>();
        m_Collider = GetComponent<Collider>();
        m_Animator = GetComponent<Animator>();

        m_CurrentActionCooldown = m_TotalActionCooldown;
    }

    private void Update()
    {
        m_CurrentActionCooldown -= Time.deltaTime;

        if (m_CurrentActionCooldown < 0.0f)
        {
            //Reset timer
            m_CurrentActionCooldown = m_TotalActionCooldown;
            PerformAction();
        }

        CheckIfClicked();
    }

    public virtual void PerformAction()
    {
       
    }

    private void CheckIfClicked()
    {

    }


}
