using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

// Enemy_C는 도착 후 양쪽 총구를 번갈아 이동시키며 연사 공격을 반복하는 적이다
public class Enemy_C : Enemy
{
    // targetPosY 변수 선언 (현재 미사용)
    float targetPosY;

    // 총구 위치 참조용 배열
    GameObject[] shootGameObject;
    Vector2[] currentShootPos;

    // 도착 여부 플래그
    bool isArrivePoint = false;

    // 초기화 시 호출
    protected override void Start()
    {
        base.Start();
        Init();
    }

    // 매 프레임 호출, 이동 및 총구 위치 관리, 공격 제어
    protected override void Update()
    {
        base.Update();

        // 목표 위치에 도달하지 않았으면 부드럽게 이동
        if (Vector2.Distance(thisGameObject.transform.position, currentTargetPos) > distanceNeedValue && isArrivePoint == false)
        {
            movement.MoveToPointLerp(ref thisGameObject, currentTargetPos, targetMoveSpeed);
        }
        // 도달 시: 총구 위치 초기화 후 공격 코루틴 시작
        else if (isArrivePoint == false)
        {
            isArrivePoint = true;

            currentShootPos[0] = shootTransform["CommonBullet0"].transform.position;
            currentShootPos[1] = shootTransform["CommonBullet1"].transform.position;
            StartCoroutine(AttackEnemyBullet());
        }

        // 목표 도달 후 총구 위치를 번갈아 이동
        if (isArrivePoint == true)
        {
            // 현재 위치와 목표 위치 간 차이가 크면 이동 수행
            if (Vector2.Distance(shootTransform["CommonBullet0"].transform.position, currentShootPos[0]) > 0.05f)
            {
                movement.MoveToPointLerp(ref shootGameObject[0], currentShootPos[0], targetMoveSpeed);
                movement.MoveToPointLerp(ref shootGameObject[1], currentShootPos[1], targetMoveSpeed);
            }
            else
            {
                TransShootPosition(); // 두 총구 위치 교환
            }
        }
    }

    // 초기 변수 설정 및 자동 목표 위치 지정
    protected override void Init()
    {
        base.Init();

        if (isSelfPosition == true)
        {
            currentTargetPos = new Vector2(transform.position.x - 22f, transform.position.y);
        }

        currentShootPos = new Vector2[2];
        shootGameObject = new GameObject[2];

        for (int i = 0; i < currentShootPos.Length; i++)
        {
            shootGameObject[i] = shootTransform["CommonBullet" + i.ToString()].gameObject;
        }

        targetMoveSpeed = GetMoveSpeed() / 2f;
    }

    // 총구 위치를 서로 교환하는 함수
    void TransShootPosition()
    {
        (currentShootPos[0], currentShootPos[1]) = (currentShootPos[1], currentShootPos[0]);
    }

    // 두 총구에서 번갈아가며 일정 간격으로 연사 공격하는 코루틴
    IEnumerator AttackEnemyBullet()
    {
        const float shootTime = 2f; // 총 연사 시간
        const int shootCount = 20;  // 총 발사 횟수
        float reAttackDelay = shootTime / shootCount; // 탄 사이 간격

    Repeat:

        yield return new WaitForSeconds(attackDelay);

        for (int i = 0; i < shootCount; i++)
        {
            for (int j = 0; j < shootTransform.Count; j++)
            {
                GameObject instance = Instantiate(enemyProjectile["EnemyBullet"]);

                // 발사체 데이터 설정 (애니메이션, 속도, 데미지, 지속시간, 태그)
                projectileManage.SetProjectileData(ref instance, attackData.animCtrl, attackData.moveSpeed, 1f, 5f, "Enemy");

                // 90도 회전 각도로 탄환 발사
                attackManage.ShootBulletRotate(ref instance, shootTransform["CommonBullet" + j.ToString()], 90);
            }

            yield return new WaitForSeconds(reAttackDelay);
        }

        goto Repeat; // 무한 루프
    }

    // 충돌 처리 (부모 기능 유지)
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
