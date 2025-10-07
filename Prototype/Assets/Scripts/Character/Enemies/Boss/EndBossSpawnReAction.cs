using System.Collections;
using UnityEngine;

public class EndBossSpawnReAction : EnemyState
{
    float timer = 0;
    bool reActioned = false;
    public EndBossSpawnReAction(Enemy enemy) : base(enemy) { }

    public override void Enter()
    {
        base.Enter();
        enemy.anim.SetTrigger("ReAction");

    }

    public override void Update()
    {
        base.Update();
        timer += Time.deltaTime;
        if (timer >= 0.55f && reActioned == false)
        {
            enemy.GetComponent<EndBoss>().ShowReAction();
            reActioned = true;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    
}
