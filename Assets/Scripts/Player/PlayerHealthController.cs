using UnityEngine;

public class PlayerHealthController : HealthController
{
    public float m_BaseHp = 100f;
    public Animator m_Animator = null;


    protected override void Awake()
    {
        m_MaxHealthPoints = m_BaseHp;
        m_CurrentHealthPoints = m_MaxHealthPoints;
    }

    protected override void Update()
    {
        base.Update();

        if(m_IsDead)
        {
            // Dead Animation
            m_Animator.SetTrigger("isDead");

            InventoryManager.instance.EnableItemTrigger(TriggerType.PLAYER_DEATH);
        }
    }
}
