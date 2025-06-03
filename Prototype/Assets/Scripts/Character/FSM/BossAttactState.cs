using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 보스 공격 상태를 나타내는 FSM 상태 클래스입니다.
/// </summary>
public class BossAttactState : EnemyAttackBaseState
{
    // 생성자: Enemy 객체를 받아 기본 EnemyAttackBaseState 초기화
    public BossAttactState(Enemy enemy) : base(enemy)
    {
    }

    /// <summary>
    /// 상태 진입 시 호출되는 함수
    /// 애니메이터에서 공격 애니메이션 활성화
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        // 공격 애니메이션 시작
        enemy.GetComponent<Animator>().SetBool("Attack", true);
        // 공격 코루틴 호출 가능 (주석 처리됨)
        // enemy.StartCoroutine(Attack());
    }

    /// <summary>
    /// 매 프레임 상태 갱신 시 호출되는 함수
    /// 기본 업데이트 호출
    /// </summary>
    public override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// 상태 종료 시 호출되는 함수
    /// 기본 종료 동작 실행
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }

    /// <summary>
    /// 공격 동작 코루틴 (구현 필요)
    /// 현재는 대기 후 이동 상태로 전환
    /// </summary>
    protected override IEnumerator Attack()
    {
        // 실제 공격 구현 대기 (주석 처리됨)
        // yield return enemy.EnemyAttack();

        yield return null;

        // 공격 후 이동 상태로 변경
        enemy.ChangeState(new BossMoveState(enemy));
    }
}
