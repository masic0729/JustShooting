using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    // 공격 대상 플레이어 변수
    Player target;

    // 생성자: Enemy를 받아 기본 EnemyState 초기화
    public EnemyAttackState(Enemy enemy) : base(enemy) { }

    /// <summary>
    /// 상태 진입 시 호출
    /// 플레이어 오브젝트를 찾아 target에 할당
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        target = GameObject.Find("Player").GetComponent<Player>();
    }

    /// <summary>
    /// 매 프레임 상태 갱신 시 호출
    /// 현재 로직 없음, 필요 시 공격 동작 추가 가능
    /// </summary>
    public override void Update()
    {
        base.Update();
    }
}
