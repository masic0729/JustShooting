using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBullet : Bullet
{
    //각 좌표의 회전 여부. 추후 삭제될 수 있음
    /*bool isCanBounceX;
    bool isCanBounceY;*/
    protected override void Start()
    {
        base.Start();
        Init();
    }
    protected override void Update()
    {
        base.Update();
        CheckBounce();
    }

    protected override void Init()
    {
        base.Init();
        maxMoveX = 10.5f; // 무시해도 됨
    }

    void CheckBounce()
    {
        Vector3 pos = transform.position;

        // Y축 상단 벽 반사
        if (pos.y > maxMoveY - 0.1f)
        {
            ApplyBounceRotation(Vector3.down);
        }
        // Y축 하단 벽 반사
        else if (pos.y < -maxMoveY + 0.1f)
        {
            ApplyBounceRotation(Vector3.up);
        }

        // X축 초과 시 삭제 (튕김 없음)
        if (Mathf.Abs(pos.x) > maxMoveX)
        {
            Destroy(this.gameObject);
        }
    }

    void ApplyBounceRotation(Vector3 normal)
    {
        // 현재 Sprite의 방향 (총알이 바라보는 방향 기준)
        Vector3 currentDir = transform.up;

        // 반사 방향 계산 (입사각 = 반사각)
        Vector3 reflected = Vector3.Reflect(currentDir, normal);

        // 회전 각도 계산 (Z축 기준)
        float angle = Vector3.SignedAngle(currentDir, reflected, Vector3.forward);

        // Sprite 회전만 적용 (projectileMoveVector는 그대로)
        transform.Rotate(0f, 0f, angle);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
