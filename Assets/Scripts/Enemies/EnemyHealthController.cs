using UnityEngine;

public class EnemyHealthController : HealthController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void ReceiveDamage(float damageReceived)
    {
        base.ReceiveDamage(damageReceived);

        //RECEIVE DAMAGE ANIMATION
    }

}
