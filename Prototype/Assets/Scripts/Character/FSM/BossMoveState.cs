using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveState : EnemyState
{
    public BossMoveState(Enemy enemy) : base(enemy) { }

    public override void Enter()
    {
        base.Enter();
        enemy.stateIndex++;
        float randMoveX = Random.Range(1f, 9f);
        float randMoveY;
        if (enemy.GetComponent<EndBossTest>() == true)
        {
            randMoveY = Random.Range(-5f, 1.5f);
        }
        else
        {
            randMoveY = Random.Range(-3.5f, 3.5f);
        }
        stateTime = 1f;
        enemy.arriveVector = new Vector2(randMoveX, randMoveY);
    }

    public override void Update()
    {
        if (Vector2.Distance(enemy.transform.position, enemy.arriveVector) > 0.3f)
        {
            enemy.movement.MoveToPointLerp(ref thisGameObject, enemy.arriveVector, enemy.GetMoveSpeed());
        }
        else
        {
            base.Update();
        }

        if (stateTime <= 0)
        {
            if (enemy.stateIndex >= 3)
            {
                enemy.stateIndex = 0;

                enemy.ChangeState(new BossAttactState(enemy));
            }
            else
            {
                enemy.ChangeState(new BossMoveState(enemy));
            }
        }

    }
}
