using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 테스트용 적 캐릭터 클래스.
/// Enemy 클래스를 상속하며, 기본 동작만 수행.
/// </summary>
public class TestEnemy : Enemy
{
    /// <summary>
    /// 초기화 및 부모 Start 호출
    /// </summary>
    protected override void Start()
    {
        base.Start();
        Init();
    }

    /// <summary>
    /// 매 프레임 호출, 부모 Update 호출
    /// </summary>
    protected override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// 초기 설정, 부모 Init 호출
    /// </summary>
    protected override void Init()
    {
        base.Init();
    }

    /// <summary>
    /// 충돌 처리, 부모 OnTriggerEnter2D 호출
    /// </summary>
    /// <param cardName="collision">충돌한 콜라이더</param>
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
