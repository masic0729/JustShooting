using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveState : EnemyState
{
    // 생성자: Enemy 객체를 받아 기본 EnemyState 초기화
    public BossMoveState(Enemy enemy) : base(enemy) { }

    /// <summary>
    /// 상태 진입 시 호출되는 함수
    /// 이동 목표 위치를 무작위로 설정하고 상태 시간을 초기화
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        enemy.stateIndex++; // 상태 이동 횟수 증가
        enemy.lastPosition = enemy.transform.position;

        enemy.moveTimer = 0;
        // x 좌표 랜덤 범위 설정
        float randMoveX = Random.Range(3f, 7f);
        // y 좌표 랜덤 범위 설정
        float randMoveY = Random.Range(-3.5f, 3.5f);
        RandStayTime();
        stateTime = 1f; // 상태 지속 시간 설정
        enemy.arrivePosition = new Vector2(randMoveX, randMoveY); // 이동 목표 위치 지정
    }

    void RandStayTime()
    {
        int rand = Random.Range(0, 50);
        stateTime = rand > 25 ? 2 : 1;
    }

    /// <summary>
    /// 매 프레임 상태 갱신 시 호출되는 함수
    /// 목표 위치로 부드럽게 이동하고, 시간이 만료되면 상태 전환
    /// </summary>
    public override void Update()
    {
        // 현재 위치와 목표 위치 간 거리가 0.3 이상이면 이동
        if (Vector2.Distance(enemy.transform.position, enemy.arrivePosition) > 0.3f)
        {
            enemy.movement.MoveToPointLerp(ref thisGameObject, enemy.arrivePosition, ref enemy.moveTimer, 1.2f);
        }
        else
        {
            base.Update(); // 상태 시간 감소
        }

        // 상태 시간이 만료되면 다음 상태 결정
        if (stateTime <= 0)
        {
            if (enemy.stateIndex >= 3)
            {
                enemy.stateIndex = 0; // 상태 횟수 초기화
                enemy.ChangeState(new BossAttactState(enemy)); // 공격 상태로 전환
            }
            else
            {
                enemy.ChangeState(new BossMoveState(enemy)); // 이동 상태를 반복
            }
        }
    }
}
