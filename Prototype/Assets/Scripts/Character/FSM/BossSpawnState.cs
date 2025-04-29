using UnityEngine;

public class BossSpawnState : EnemyState
{
    public BossSpawnState(Enemy enemy) : base(enemy) { }
    public override void Enter()
    {
        base.Enter();
        
        enemy.arriveVector = new Vector2(3f, 0f);
    }

    public override void Update()
    {
        base.Update();
        if (Vector2.Distance(enemy.transform.position, enemy.arriveVector) > 0.5f)
        {
            enemy.movement.MoveToPointLerp(ref thisGameObject, enemy.arriveVector, enemy.GetMoveSpeed());
            Debug.Log("나 움직이는 중");
        }
        else
        {
            enemy.enemyState.ChangeState(new BossMoveState(enemy));
        }
    }
}
