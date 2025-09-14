using System.Collections;
using UnityEngine;

public class EndBossSpawnReAction : EnemyState
{

    public EndBossSpawnReAction(Enemy enemy) : base(enemy) { }

    public override void Enter()
    {
        base.Enter();
        enemy.anim.SetTrigger("ReAction");
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }

    
}
