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
        �׷��ϱ� �� �κ��� �̵� ��ũ��Ʈ�� ������,
        �ش� �Ÿ����� �����ϸ� ���Ľ��� ����Ǵ� ��ũ��Ʈ�� �ۼ��ϸ� �ȴ�.
         */


    }
}
