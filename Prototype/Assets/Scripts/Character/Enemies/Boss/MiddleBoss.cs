using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleBoss : Boss
{
    // Start 함수: 부모 Start 호출
    protected override void Start()
    {
        base.Start();
    }

    // Update 함수: 부모 Update 호출
    protected override void Update()
    {
        base.Update();
    }

    // OnDestroy 함수: 객체가 파괴될 때 호출 (현재 내용 없음)
    private void OnDestroy()
    {

    }

    // Init 함수: 초기화 및 이벤트 구독
    protected override void Init()
    {
        base.Init();
        OnCharacterDeath += RestartWave; // 보스 사망 시 웨이브 재개 함수 연결
    }

    // 충돌 처리: 부모 충돌 처리 호출
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
