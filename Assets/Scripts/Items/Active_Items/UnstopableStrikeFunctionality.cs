using System.Runtime.Serialization;
using UnityEngine;

public class UnstopableStrikeFunctionality : BaseActiveScript
{
    public float m_DamageMultiplier = 5;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) { UseActive(); }
    }
    public override bool UseActive()
    {
        // Check if the item is an active item using the base function
        if (!base.UseActive()) return false;
        
        FindAnyObjectByType<PlayerCombatScript>().DealDamageToTargetEnemy(PlayerStats.instance.m_RoomsCleared * m_DamageMultiplier);
        
        return true;
    }
}
