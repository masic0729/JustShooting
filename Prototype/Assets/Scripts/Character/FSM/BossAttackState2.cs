using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState2 : EnemyAttackBaseState
{
    protected EndBossTest2 boss;
    public BossAttackState2(Enemy enemy) : base(enemy) { }

    public override void Enter()
    {
        base.Enter();
        boss = enemy as EndBossTest2;
        boss.StartCoroutine(Attack());

    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }

    protected override IEnumerator Attack()
    {
        int shootRandom = Random.Range(1, 5); // 1~4회 발사
        float shootRandomRotate = Random.Range(1, 3); // 1~2값 할당
        float angle = shootRandomRotate == 1 ? 40f : -40f;

        for (int i = 0; i < shootRandom; i++)
        {
            boss.BounceAttack(angle);
            yield return new WaitForSeconds(0.5f);
            boss.BounceAttack(angle);
        }

        yield return new WaitForSeconds(3f);
        enemy.ChangeState(new BossMoveState(enemy));

    }
}
