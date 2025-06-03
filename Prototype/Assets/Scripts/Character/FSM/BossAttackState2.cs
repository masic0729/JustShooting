using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState2 : EnemyAttackBaseState
{
    // EndBossTest2 타입 보스 참조 변수
    protected EndBossTest2 boss;

    // 생성자: Enemy 객체를 받아 부모 생성자 호출
    public BossAttackState2(Enemy enemy) : base(enemy) { }

    /// <summary>
    /// 상태 진입 시 호출되는 함수
    /// 보스 캐스팅 후 공격 코루틴 시작
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        boss = enemy as EndBossTest2;
        boss.StartCoroutine(Attack());
    }

    /// <summary>
    /// 매 프레임 상태 갱신 시 호출되는 함수
    /// 기본 업데이트 실행
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
    /// 공격 코루틴
    /// 랜덤 횟수와 방향으로 보스의 BounceAttack을 반복 실행
    /// 이후 일정 시간 대기 후 이동 상태로 전환
    /// </summary>
    protected override IEnumerator Attack()
    {
        // 랜덤으로 1~4회 공격 횟수 결정
        int shootRandom = Random.Range(1, 5);

        // 랜덤으로 1 또는 2 값을 할당하여 각도 결정
        float shootRandomRotate = Random.Range(1, 3);
        float angle = shootRandomRotate == 1 ? 40f : -40f;

        for (int i = 0; i < shootRandom; i++)
        {
            boss.BounceAttack(angle);
            yield return new WaitForSeconds(0.5f);
            boss.BounceAttack(angle);
        }

        // 공격 후 3초 대기
        yield return new WaitForSeconds(3f);

        // 공격 후 보스 이동 상태로 전환
        enemy.ChangeState(new BossMoveState(enemy));
    }
}
