using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 벽에 반사되는 총알 기능을 가진 클래스 (Bullet 상속)
public class BounceBullet : Bullet
{
    // 각 좌표의 회전 여부. 추후 삭제될 수 있음
    /*bool isCanBounceX;
    bool isCanBounceY;*/

    float turnCool = 0;

    // 오브젝트 생성 시 초기화
    protected override void Start()
    {
        base.Start();
        Init();
    }

    // 매 프레임마다 실행됨
    protected override void Update()
    {
        base.Update();
        turnCool -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        CheckBounce(); // 위치를 확인하여 반사 조건 검사

    }

    // 초기 설정
    protected override void Init()
    {
        base.Init();
        maxMoveX = 10.5f; // 무시해도 됨 (X축 벗어나면 삭제 조건용)
    }

    // 화면 경계를 기준으로 반사 조건을 체크함
    void CheckBounce()
    {
        if (turnCool > 0)
            return;

        Vector3 pos = transform.position;

        // Y축 상단 벽 반사
        if (pos.y > maxMoveY - 0.3f)
        {
            ApplyBounceRotation(Vector3.down); // 아래 방향으로 반사
            turnCool = 1f;
        }
        // Y축 하단 벽 반사
        if (pos.y < -maxMoveY + 0.3f)
        {
            ApplyBounceRotation(Vector3.up); // 위 방향으로 반사
            turnCool = 1f;
        }

        // X축 초과 시 삭제 (튕김 없음)
        if (Mathf.Abs(pos.x) > maxMoveX)
        {
            Destroy(this.gameObject); // 화면 벗어나면 오브젝트 파괴
        }
    }

    // 반사 로직 처리
    void ApplyBounceRotation(Vector3 normal)
    {
        // 현재 Sprite의 방향 (총알이 바라보는 방향 기준)
        Vector3 currentDir = transform.up;

        // 반사 방향 계산 (입사각 = 반사각)
        Vector3 reflected = Vector3.Reflect(currentDir, normal);

        // 회전 각도 계산 (Z축 기준으로 얼마나 회전해야 하는지)
        float angle = Vector3.SignedAngle(currentDir, reflected, Vector3.forward);

        // Sprite 회전만 적용 (총알의 이동 벡터는 그대로 유지)
        transform.Rotate(0f, 0f, angle);
    }

    // 충돌 처리 (기본 Bullet 동작 유지)
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
