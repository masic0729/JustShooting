using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleBossTest : MiddleBoss
{
    // Start 함수: 부모 Start 호출 및 초기화 수행
    protected override void Start()
    {
        base.Start();
        Init();
    }

    // Update 함수: 부모 Update 호출
    protected override void Update()
    {
        base.Update();
    }

    // Init 함수: 부모 Init 호출
    protected override void Init()
    {
        base.Init();
    }

    // 충돌 처리: 부모 충돌 처리 호출
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
