using UnityEngine;

public class PlayerCombatScript : MonoBehaviour
{
    public EnemyBaseScript m_TargetEnemy = null;

    [SerializeField] private float m_LightAttackDamage = 0;
    [SerializeField] private float m_LightAttackTotalCooldown = 0;
    [SerializeField] private float m_LightAttackCurrentCooldown = 0;

    [SerializeField] private float m_HeavyAttackDamage = 0;
    [SerializeField] private float m_HeavyAttackTotalCooldown = 0;
    [SerializeField] private float m_HeavyAttackCurrentCooldown = 0;
    private void OnEnable()
    {
        //Call ItemManagers Get LightAttack
        //Call ItemManagers Get HeavyAttack
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetHit(float dmg)
    {
        //HealthManagers function for receiving damage
        //Receive Damage Animation
    }

    public void UseAbility()
    {
        //AbiltyManager function for using the ability
        //Abilitys visula effect
    }

    public void LightAttack()
    {
        //Attack Animation
    }

    public void HeavyAttack()
    {
        //Attack Animation
    }

    public void UseActiveItem()
    {
        //Call ItemManagers.ActiveItem.Action()
        //Attack Animation

    }
}
