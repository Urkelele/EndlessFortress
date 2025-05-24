using UnityEngine;

public class AttackerEnemyScript : EnemyBaseScript
{
    [SerializeField] protected float m_AttackDamage = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        base .Update();
    }

    public override void PerformAction()
    {
        base.PerformAction();
        m_PlayerCombatScript.m_PlayerHealthController.ReceiveDamage(m_AttackDamage);
        // Attack Animation
        base.m_Animator.SetTrigger("isAttacking");
    }
}
