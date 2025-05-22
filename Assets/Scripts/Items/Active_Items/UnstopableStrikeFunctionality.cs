using System.Runtime.Serialization;
using UnityEngine;

public class UnstopableStrikeFunctionality : BaseActiveScript
{
    CombatManager m_CombatManager;

    public void Start()
    {
        m_CombatManager = FindAnyObjectByType<CombatManager>();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) { UseActive(); }
    }
    public override bool UseActive()
    {
        // Check if the item is an active item using the base function
        if (!base.UseActive()) return false;
        m_CombatManager.m_CurrentEnemyTarget.GetComponent<HealthController>().ReceiveDamage(10);
        return true;
    }
}
