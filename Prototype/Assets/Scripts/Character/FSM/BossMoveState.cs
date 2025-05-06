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
        float randMoveX = Random.Range(2f,10f);
        float randMoveY = Random.Range(-4f, 4f);
        stateTime = 1f;
        enemy.arriveVector = new Vector2(randMoveX, randMoveY);
    }

    public override void Update()
    {
        if (Vector2.Distance(enemy.transform.position, enemy.arriveVector) > 0.5f)
        {
            enemy.movement.MoveToPointLerp(ref thisGameObject, enemy.arriveVector, enemy.GetMoveSpeed());
            Debug.Log("나 움직이는 중");
        }
        else
        {
            base.Update();
        }

        if (stateTime <= 0)
        {
            enemy.ChangeState(new BossMoveState(enemy));
            Debug.Log("변경");
        }
    }
}
