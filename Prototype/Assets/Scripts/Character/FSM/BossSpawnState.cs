using UnityEngine;

public class BossSpawnState : EnemyState
{
    // 생성자: Enemy 객체를 받아 기본 EnemyState 초기화
    public BossSpawnState(Enemy enemy) : base(enemy) { }

    /// <summary>
    /// 상태 진입 시 호출되는 함수
    /// 이동 목표 위치와 상태 지속 시간을 설정
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        stateTime = 2f;                             // 상태 지속 시간 2초 설정
        enemy.arriveVector = new Vector2(3f, 0f);  // 도착할 위치 설정
    }

    /// <summary>
    /// 매 프레임 상태 갱신 시 호출되는 함수
    /// 목표 위치로 이동하다가, 시간이 끝나면 다음 상태로 전환
    /// </summary>
    public override void Update()
    {
        // 목표 위치와 현재 위치 간 거리 계산 후 이동
        if (Vector2.Distance(enemy.transform.position, enemy.arriveVector) > 0.5f)
        {
            enemy.movement.MoveToPointLerp(ref thisGameObject, enemy.arriveVector, enemy.GetMoveSpeed());
        }
        else
        {
            base.Update(); // 상태 시간 감소
        }

        // 상태 시간이 0 이하가 되면 다음 상태로 변경
        if (stateTime <= 0)
        {
            enemy.ChangeState(new BossMoveState(enemy));
        }
    }
}
