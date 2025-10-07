using System.Collections;
using UnityEngine;

// Enemy_B는 일정 거리 이동 후 멈춰서 총알을 회전 발사하는 적 캐릭터
public class Enemy_B : Enemy
{
    [Header("몬스터의 y값 변경 범위값 설정")]
    [SerializeField]
    // maxY_Range 변수 선언: y축 이동 가능한 최대 범위
    float maxY_Range;
    [SerializeField]
    // targetPosY 변수 선언: 현재 목표 y 위치
    float targetPosY;
    // shootCount 변수 선언: 한 번에 발사할 총알 수
    int shootCount = 8;
    // isArrivePoint 변수 선언: 목표 지점 도달 여부 판단용
    bool isArrivePoint = false;

    // 초기화 및 기본 세팅 함수
    protected override void Start()
    {
        base.Start();
        Init();
    }

    // 매 프레임마다 호출, 이동 및 도착 상태 처리
    protected override void Update()
    {
        base.Update();

        // 목표 위치까지 도달하지 않았다면 부드럽게 이동
        if (Vector2.Distance(thisGameObject.transform.position, currentTargetPos) > distanceNeedValue && isArrivePoint == false)
        {
            movement.MoveToPointLerp(ref thisGameObject, currentTargetPos, ref moveTimer, 3f);
        }
        // 도착했으면 일정 시간 후 새로운 목표로 이동 준비
        else if (isArrivePoint == false)
        {
            isArrivePoint = true;
            Invoke("SetTransTargetTransform", 1f);
        }
    }

    // 변수 초기화 및 공격 루틴 시작
    protected override void Init()
    {
        base.Init();
        attackDelay = 3f; // 공격 대기 시간 설정
        targetMoveSpeed = GetMoveSpeed() / 2f; // 이동 속도는 절반으로 설정
        targetPosY = this.transform.position.y;

        // 고정 위치 사용 시 기본 목표 위치 설정
        if (isSelfPosition == true)
        {
            currentTargetPos = new Vector2(transform.position.x - 22f, targetPosY);
        }

        // 공격 코루틴 시작
        StartCoroutine(AttackEnemyBullet());
    }

    // 새로운 목표 y 위치 랜덤 설정 및 이동 상태 리셋
    void SetTransTargetTransform()
    {
        targetPosY = Random.Range(-maxY_Range, maxY_Range);
        currentTargetPos = new Vector2(transform.position.x, targetPosY);
        isArrivePoint = false;
        moveTimer = 0;
        lastPosition = this.transform.position;
    }

    // 일정 시간 간격으로 원형 탄막 공격 수행 코루틴
    IEnumerator AttackEnemyBullet()
    {
    Repeat:

        yield return new WaitForSeconds(attackDelay);

        for (int i = 1; i <= shootCount; i++)
        {
            GameObject instance = Instantiate(enemyProjectile["EnemyBullet"]);

            projectileManage.SetProjectileData(ref instance, attackData.animCtrl, attackData.moveSpeed, 1f, 10f, "Enemy");

            attackManage.ShootBulletRotate(ref instance, shootTransform["CommonBullet"], 360 / shootCount * i);
        }

        goto Repeat;
    }

    // 충돌 처리 (부모 클래스 호출 유지)
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
