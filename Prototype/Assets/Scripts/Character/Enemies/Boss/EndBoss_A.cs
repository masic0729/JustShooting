using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EndBoss_A 클래스.
/// EndBoss 클래스를 상속하며, 추가 기능 없이 기본 동작만 수행.
/// </summary>
public class EndBoss_A : EndBoss
{
    /// <summary>
    /// 초기화 및 부모 Start 호출
    /// </summary>
    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// 매 프레임 업데이트 호출
    /// </summary>
    protected override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// 초기 설정 및 부모 Init 호출
    /// </summary>
    protected override void Init()
    {
        base.Init();
    }

    /// <summary>
    /// 충돌 처리, 부모 클래스 호출
    /// </summary>
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
