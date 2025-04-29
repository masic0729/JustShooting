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

        enemy.arriveVector = new Vector2(randMoveX, randMoveY);
    }

    public override void Update()
    {
        base.Update();
        if (Vector2.Distance(enemy.transform.position, enemy.arriveVector) > 0.5f)
        {
            enemy.movement.MoveToPointNormal(ref thisGameObject, enemy.arriveVector, enemy.GetMoveSpeed());
            Debug.Log("나 움직이는 중");
        }
        else
        {
            //보스 패턴의 경우 이동 3번 한 후, 공격하는 방식이기에 이러하다.
            if(enemy.stateIndex >= 3)
            {
                //enemy.enemyState.ChangeState(new EnemyAttackState(enemy));
                Debug.Log("이제 공격 구현 해야됨");
            }
            else
            { 
                enemy.enemyState.ChangeState(new BossMoveState(enemy));
            }
        }
    }
}
