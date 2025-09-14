using System.Collections;
using UnityEngine;

/// <summary>
/// Enemy_D는 일정 위치에 도달한 후 Y축 상/하를 반복 이동하며,
/// 플레이어를 추적하는 탄환을 연사하는 적이다.
/// </summary>
public class Enemy_D : Enemy
{
    // targetPlayer 변수 선언: 플레이어 오브젝트 참조
    GameObject targetPlayer;
    // targetPosY 변수 선언: 목표 Y 좌표
    float targetPosY;
    // isArrivePoint 변수 선언: 목표 위치 도달 여부 플래그
    bool isArrivePoint = false;

    // 초기화 및 발사 코루틴 시작
    protected override void Start()
    {
        base.Start();
        Init(); // 초기 위치 및 타겟 설정
        StartCoroutine(AttackEnemyBullet()); // 연속 발사 코루틴 실행
    }

    // 매 프레임 이동 처리 및 목표 위치 변경 제어
    protected override void Update()
    {
        base.Update();

        // 현재 위치가 목표 위치와 거리가 허용치 이상이고 아직 도착하지 않았으면 이동
        if (Vector2.Distance(thisGameObject.transform.position, currentTargetPos) > distanceNeedValue && isArrivePoint == false)
        {
            movement.MoveToPointLerp(ref thisGameObject, currentTargetPos, ref moveTimer, 1.2f);
        }
        // 목표에 도착했으면 다음 위치 설정
        else if (isArrivePoint == false)
        {
            isArrivePoint = true;
            SetTransTargetTransform();
        }
    }

    // 초기 값 세팅 및 플레이어 타겟팅 설정
    protected override void Init()
    {
        base.Init();
        targetPlayer = GameObject.Find("Player"); // 플레이어 오브젝트 탐색 및 할당
        targetPosY = transform.position.y;

        // 이동 좌표 자동 설정 (좌측으로 x 22만큼 이동)
        if (isSelfPosition == true)
        {
            currentTargetPos = new Vector2(transform.position.x - 22f, targetPosY);
        }

        targetMoveSpeed = GetMoveSpeed() / 2f; // 이동 속도 조절
    }

    /// <summary>
    /// 일정 간격으로 플레이어를 추적하는 탄환을 10발씩 발사하는 코루틴
    /// </summary>
    IEnumerator AttackEnemyBullet()
    {
        const float shootTime = 3f; // 총 발사 시간
        const int shootCount = 10;  // 발사할 탄환 수
        float reAttackDelay = shootTime / shootCount; // 탄환 발사 간격

    Repeat:
        yield return new WaitForSeconds(attackDelay); // 초기 대기

        // 탄환 연속 발사 루프
        for (int i = 0; i < shootCount; i++)
        {
            // 발사체 인스턴스 생성
            GameObject instance = Instantiate(enemyProjectile["EnemyBullet"], transform.position, transform.rotation);

            // 발사체 데이터 세팅 (애니메이션, 속도, 데미지, 생존 시간, 태그)
            projectileManage.SetProjectileData(ref instance, attackData.animCtrl, attackData.moveSpeed, 1f, 5f, "Enemy");

            // 플레이어 방향으로 탄환 회전 및 조준
            targetManage.DirectTargetObject(ref instance, ref targetPlayer);

            yield return new WaitForSeconds(reAttackDelay); // 다음 발사까지 대기
        }

        goto Repeat; // 무한 반복
    }

    /// <summary>
    /// 도착 후 Y축 위치를 반전시켜 상하로 이동 반복
    /// </summary>
    void SetTransTargetTransform()
    {
        lastPosition = this.transform.position;

        moveTimer = 0;
        if (transform.position.y < 0)
        {
            targetPosY = 4f; // 위쪽으로 이동
        }
        else
        {
            targetPosY = -4f; // 아래쪽으로 이동
        }

        currentTargetPos = new Vector2(transform.position.x, targetPosY);
        isArrivePoint = false;
        distanceNeedValue = 1f; // 목표 도달 허용 거리
    }

    // 충돌 처리 (부모 클래스 호출 유지)
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
