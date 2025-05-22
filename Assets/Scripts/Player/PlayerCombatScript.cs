using UnityEngine;

public class PlayerCombatScript : MonoBehaviour
{
    [SerializeField] EnemyBaseScript TargetEnemy = null;

    [SerializeField] float LightAttackDamage = 0;
    [SerializeField] float LightAttackCooldown = 0;
    [SerializeField] float LightAttackCurrentCooldown = 0;

    [SerializeField] float HeavyAttackDamage = 0;
    [SerializeField] float HeavyAttackCooldown = 0;
    [SerializeField] float HeavyAttackCurrentCooldown = 0;
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
