using System;
using UnityEngine;

public class TreantScript : EnemyBaseScript
{
    [SerializeField] private float m_HealAmount = 10.0f;
    private EnemyBaseScript m_LowestHealthEnemy = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();   
    }

    public override void PerformAction()
    {
        GetLowestHealthEnemy();
        if(m_LowestHealthEnemy != null)
        {
            m_LowestHealthEnemy.m_HealthController.HealDamage(m_HealAmount);

            // Heal Animation
            base.m_Animator.SetTrigger("isHealing");
        }
    }

    private void GetLowestHealthEnemy()
    {
        foreach (EnemyBaseScript enemy in CombatManager.instance.m_CombatEnemies)
        {
            if (m_LowestHealthEnemy == null && !enemy.GetComponent<TreantScript>())
            {
                m_LowestHealthEnemy = enemy;
            }
            else
            {
                if (m_LowestHealthEnemy.m_HealthController.m_CurrentHealthPoints > enemy.m_HealthController.m_CurrentHealthPoints)
                {
                    m_LowestHealthEnemy = enemy;
                }
            }

        }
    }
}
