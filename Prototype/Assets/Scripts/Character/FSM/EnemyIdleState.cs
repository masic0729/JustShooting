public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(Enemy enemy) : base(enemy) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        /*idleTime -= Time.deltaTime;
        if (idleTime <= 0f)
        {
            enemy.stateMachine.ChangeState(new EnemyChaseState(enemy));
        }
        그러니까 이 부분을 이동 스크립트를 돌려서,
        해당 거리값과 근접하면 스탠스가 변경되는 스크립트를 작성하면 된다.
         */


    }
}
