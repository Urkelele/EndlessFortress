using System.Runtime.Serialization;
using UnityEngine;

[CreateAssetMenu(menuName = "Resources/Items/Active_Items/ExperiencedStrike")]
public class ExperiencedStrikeFunctionality : BaseActiveScript
{
    public float m_DamageMultiplier = 5;

    public override void UseActive()
    {
        base.UseActive();
        //Deal damage to enemy equal to the num of rooms cleared times a mult
        FindAnyObjectByType<PlayerCombatScript>().DealDamageToTargetEnemy(PlayerStats.instance.m_RoomsCleared * m_DamageMultiplier);
    }
}
