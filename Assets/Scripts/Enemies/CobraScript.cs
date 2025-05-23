using UnityEngine;

public class CobraScript : AttackerEnemyScript
{

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
        if(m_PlayerCombatScript.m_TargetEnemy != this)
        {
            base.PerformAction();
        }
    }
}
