using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttackBaseState : EnemyState
{
    public EnemyAttackBaseState(Enemy enemy) : base(enemy) { }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Update()
    {
        base.Update();
        
    }

    public override void Exit()
    {
        base.Exit();

    }

    protected abstract IEnumerator Attack();
}
