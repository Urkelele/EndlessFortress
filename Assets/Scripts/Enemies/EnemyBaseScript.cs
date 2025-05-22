using UnityEngine;

public class EnemyBaseScript : MonoBehaviour
{
    [Header("BASE ENEMY PARAMS")]
    public HealthController m_HealthController = null;
    public HealthController m_PlayerHealthController = null;
    public Collider m_Collider = null;
    public Animator m_Animator = null;
    public float m_ActionCooldown = 0.0f;
    public float m_TimeUntilNextAction = 0.0f;
    public bool m_IsMarked = false;

    private void Start()
    {
        m_HealthController = GetComponent<HealthController>();
        m_Collider = GetComponent<Collider>();
        m_Animator = GetComponent<Animator>();

        m_TimeUntilNextAction = m_ActionCooldown;
    }

    private void Update()
    {
        m_TimeUntilNextAction -= Time.deltaTime;

        if (m_TimeUntilNextAction < 0.0f)
        {
            //Reset timer
            m_TimeUntilNextAction = m_ActionCooldown;
            PerformAction();
        }
    }

    public virtual void PerformAction()
    {
       
    }


}
