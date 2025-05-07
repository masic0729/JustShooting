using UnityEngine;

public class BossSpawnState : EnemyState
{
    public BossSpawnState(Enemy enemy) : base(enemy) { }
    public override void Enter()
    {
        base.Enter();
        stateTime = 2f;
        enemy.arriveVector = new Vector2(3f, 0f);
    }

    public override void Update()
    {
        if (Vector2.Distance(enemy.transform.position, enemy.arriveVector) > 0.5f)
        {
            enemy.movement.MoveToPointLerp(ref thisGameObject, enemy.arriveVector, enemy.GetMoveSpeed());
        }
        else
        {
            base.Update();
        }
        if(stateTime <= 0)
        {
            enemy.ChangeState(new BossMoveState(enemy));
        }
    }
}
