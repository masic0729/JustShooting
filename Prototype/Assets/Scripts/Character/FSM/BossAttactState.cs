using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// test AttackFSM
/// </summary>
public class BossAttactState : EnemyAttackBaseState
{

    public BossAttactState(Enemy enemy) : base(enemy) {

    }

    public override void Enter()
    {
        base.Enter();
        

        enemy.StartCoroutine(Attack());

    }

    public  override void Update()
    {
        base.Update();

    }

    public override void Exit()
    {
        base.Exit();
    }

    protected override IEnumerator Attack()
    {
        yield return enemy.EnemyAttack();
        enemy.ChangeState(new BossMoveState(enemy));

    }
}
