using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyBoss : EndBoss
{
    // Start 함수: 부모 Start 함수 호출
    protected override void Start()
    {
        base.Start();
    }

    // Update 함수: 부모 Update 함수 호출
    protected override void Update()
    {
        base.Update();
    }

    // Init 함수: 부모 Init 함수 호출
    protected override void Init()
    {
        base.Init();
    }

    // 충돌 처리 함수: 부모 OnTriggerEnter2D 함수 호출
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
