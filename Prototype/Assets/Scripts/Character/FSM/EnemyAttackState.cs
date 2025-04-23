using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    Player target;
    public EnemyAttackState(Enemy enemy) : base(enemy){ }

    public override void Enter()
    {
        base.Enter();
        target = GameObject.Find("Player").GetComponent<Player>();
    }
    public override void Update()
    {
        base.Update();

    }
}