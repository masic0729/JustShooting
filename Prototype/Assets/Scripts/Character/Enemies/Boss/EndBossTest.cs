using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBossTest : EndBoss
{
    
    protected override void Start()
    {
        base.Start();
        Init();
    }

    protected override void Update()
    {
        base.Update();
        enemyState.Update();
    }

    protected override void Init()
    {
        base.Init();
        enemyState = new StateMachine();

        TestChangeState(new BossSpawnState(this));
    }

    public void TestChangeState(EnemyState state)
    {
        enemyState.ChangeState(state);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
