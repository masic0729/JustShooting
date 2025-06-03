using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 임시용 클래스
/// </summary>
public class EnemyBullet : Bullet
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start(); // Bullet의 Start 호출
        Init(); // 초기화
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update(); // Bullet의 Update 호출
    }

    // 적 총알의 초기 설정
    protected override void Init()
    {
        base.Init(); // Bullet의 Init 호출
    }

    // 충돌 처리
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision); // Bullet의 충돌 처리 유지
    }
}
