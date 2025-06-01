using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// test AttackFSM
/// </summary>
public class BossAttactState : EnemyAttackBaseState
{

    public BossAttactState(Enemy enemy) : base(enemy) 
    {

    }

    public override void Enter()
    {
        base.Enter();
        //enemy.StartCoroutine(Attack());
        enemy.GetComponent<Animator>().SetBool("Attack", true);
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
        //yield return enemy.EnemyAttack();
        yield return null;
        enemy.ChangeState(new BossMoveState(enemy));

    }
}
