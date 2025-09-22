using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBossTest2 : EndBoss
{
    // Start 함수: 초기화 및 부모 Start 호출
    protected override void Start()
    {
        base.Start();
        Init(); // 추가 초기화
    }

    // Update 함수: 매 프레임 부모 Update 호출
    protected override void Update()
    {
        base.Update();
    }

    // Init 함수: 추가 초기화 (현재 내용 없음)
    protected override void Init()
    {
        base.Init();
    }

    // 충돌 처리 함수: 부모 클래스 충돌 처리 호출
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    //코루틴 기반 공격 예제 (현재 주석 처리됨)
    public IEnumerator SpreadAttack()
    {
        SpreadAttack(10, 0f);
        yield return new WaitForSeconds(0.5f);
        SpreadAttack(10, 30f);
        yield return new WaitForSeconds(1f);
        SpreadAttack(18, 0f);

        yield return new WaitForSeconds(attackEndStopTime);
    }


}
