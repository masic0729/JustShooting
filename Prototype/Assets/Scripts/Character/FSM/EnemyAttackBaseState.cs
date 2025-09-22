using System.Collections;


public abstract class EnemyAttackBaseState : EnemyState
{
    // 생성자: Enemy 객체를 받아 기본 EnemyState 초기화
    public EnemyAttackBaseState(Enemy enemy) : base(enemy) { }

    /// <summary>
    /// 상태 진입 시 호출되는 함수
    /// 상위 클래스 기본 Enter 호출
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        enemy.GetComponent<EndBoss>().Attack();
    }

    /// <summary>
    /// 매 프레임 상태 갱신 시 호출되는 함수
    /// 상위 클래스 기본 Update 호출
    /// </summary>
    public override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// 상태 종료 시 호출되는 함수
    /// 상위 클래스 기본 Exit 호출
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }

    // 추상 메서드: 공격 행동을 코루틴으로 구현하도록 강제
    //protected abstract IEnumerator Attack();
}
