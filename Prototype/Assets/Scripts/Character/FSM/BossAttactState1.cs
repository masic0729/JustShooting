using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// test AttackFSM
/// </summary>
public class BossAttactState1 : EnemyAttackBaseState
{
    EndBossTest boss;
    public BossAttactState1(Enemy enemy) : base(enemy) {

    }

    public override void Enter()
    {
        base.Enter();
        boss = enemy as EndBossTest;

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
        boss.SpreadAttack(10, 0f);
        yield return new WaitForSeconds(1f);
        boss.SpreadAttack(10, 30f);
        yield return new WaitForSeconds(1f);
        
        enemy.ChangeState(new BossMoveState(enemy));
    }
}
