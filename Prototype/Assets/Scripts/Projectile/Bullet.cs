using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 기본 투사체 Projectile을 상속받은 일반 총알 클래스
public class Bullet : Projectile
{
    protected TargetBulletManagement targetBulletManager; // 타겟을 관리하는 클래스 (타겟 추적용)

    public float rotateValue = 1.0f; // 현재 회전 값
    protected float rotateDefaultValue; // 초기 회전 값 저장용
    protected float rotateAddValue = 1.5f; // 회전 속도 증가값

    protected override void OnEnable()
    {
        base.OnEnable(); // 오브젝트 풀링 활성화 시 호출
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start(); // Projectile의 초기화 호출
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update(); // Projectile의 업데이트 호출
    }

    // 총알의 초기 설정 처리
    protected override void Init()
    {
        base.Init();
        //Debug.Log("Bullet");
        targetBulletManager = new TargetBulletManagement(); // 타겟 추적 클래스 인스턴스 생성
        rotateDefaultValue = rotateValue; // 초기 회전 값을 저장
    }

    // 충돌 처리
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision); // Projectile의 충돌 처리 유지
    }

    // 회전 값을 초기값으로 되돌리는 함수
    public void InitRotateValue()
    {
        rotateValue = rotateDefaultValue;
    }
}
