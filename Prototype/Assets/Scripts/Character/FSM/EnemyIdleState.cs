public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(Enemy enemy) : base(enemy) { }

    /// <summary>
    /// 상태 진입 시 호출
    /// 현재는 기본 Enter 기능만 수행
    /// </summary>
    public override void Enter()
    {
        base.Enter();
    }

    /// <summary>
    /// 상태 매 프레임 갱신 시 호출
    /// idle 시간 카운트 및 근접 시 추적 상태로 변경 (주석 처리됨)
    /// </summary>
    public override void Update()
    {
        base.Update();

        /* 
         * idleTime -= Time.deltaTime;
         * if (idleTime <= 0f)
         * {
         *     enemy.stateMachine.ChangeState(new EnemyChaseState(enemy));
         * }
         * 해당 부분 대신 이동 스크립트에서 거리 계산 후
         * 근접 시 상태 변경 구현 필요
         */
    }
}
