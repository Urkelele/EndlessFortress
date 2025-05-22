using UnityEngine;

public class EnemyBaseScript : MonoBehaviour
{
    [Header("BASE ENEMY PARAMS")]
    public HealthController m_HealthController = null;
    public float m_ActionCooldown = 0.0f;
    public float m_TimeUntilNextAction = 0.0f;
    public bool m_IsMarked = false;

    private void Start()
    {
        m_HealthController = GetComponent<HealthController>();
        m_TimeUntilNextAction = m_ActionCooldown;
    }

    private void Update()
    {
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
