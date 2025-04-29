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
            Debug.Log("�� �����̴� ��");
        }
        else
        {
            //���� ������ ��� �̵� 3�� �� ��, �����ϴ� ����̱⿡ �̷��ϴ�.
            if(enemy.stateIndex >= 3)
            {
                //enemy.enemyState.ChangeState(new EnemyAttackState(enemy));
                Debug.Log("���� ���� ���� �ؾߵ�");
            }
            else
            { 
                enemy.enemyState.ChangeState(new BossMoveState(enemy));
            }
        }
    }
}
