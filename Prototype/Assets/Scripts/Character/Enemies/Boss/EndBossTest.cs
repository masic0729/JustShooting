using System.Collections;
using UnityEngine;

public class EndBossTest : EndBoss
{
    // 공격 횟수 카운트 변수
    private int attackCount;
    // 공격 시 탄환 회전값 변수
    float attackRotateValue;

    // Start 함수: 초기화 및 부모 클래스 Start 호출
    protected override void Start()
    {
        base.Start();
        Init(); // 추가 초기화
    }

    // Update 함수: 매 프레임 업데이트 호출
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

    // Attack 함수: 공격 로직 처리
    /*public override void Attack()
    {
        AudioManager.Instance.PlaySFX("Attack"); // 공격 사운드 재생
        if (attackCount < 2)
        {
            SpreadAttack(10, 0); // 10발 공격, 회전값 0도
            attackCount++;       // 공격 카운트 증가
            attackRotateValue = 30f; // 회전값 설정
        }
        else
        {
            SpreadAttack(18, 0);  // 18발 공격, 회전값 0도
            attackRotateValue = 0f; // 회전값 초기화
            attackCount = 0;      // 카운트 초기화
            anim.SetBool("Attack", false); // 공격 애니메이션 종료
            base.Attack();   // 부모 클래스 공격 처리 호출
        }
    }*/

    public override void Attack()
    {
        base.Attack();
    }

    public void StartPatten(int pattenIndex)
    {
        if (pattenIndex == 0)
            StartCoroutine(Patten1());
        if (pattenIndex == 1)
            StartCoroutine(Patten1());
    }

    public IEnumerator Patten0()
    {
        // 발사 횟수 지정
        int shootRandom = 10;
        // 탄환 회전각 랜덤 지정 (40도 ~ 60도)
        float shootRandomRotate = Random.Range(40f, 60f);
        float rotateAddValue = 0;
        for (int i = 0; i < shootRandom; i++)
        {
            BounceAttack(-shootRandomRotate + rotateAddValue); // 왼쪽 방향 공격
            BounceAttack(shootRandomRotate - rotateAddValue);  // 오른쪽 방향 공격
            
            rotateAddValue -= 2f;
            yield return new WaitForSeconds(0.2f); // 0.3초 대기
        }
        yield return new WaitForSeconds(attackEndStopTime); // 공격 종료 대기
        ChangeState(new BossMoveState(this));

    }

    public IEnumerator Patten1()
    {
        // 발사 횟수 지정
        int shootRandom = 5;
        // 탄환 회전각 랜덤 지정 (40도~60도)
        float shootRandomRotate = Random.Range(40f, 60f);
        int rotateAddValue = 10;

        int startRot = 0, endRot = 360;
        for (int i = 0; i < shootRandom; i++)
        {
            
            for(int j = startRot; j < endRot; j += 10)
            {
                Vector2 pos;
                pos.x = Mathf.Sin(j * Mathf.Deg2Rad) * 5f;
                pos.y = Mathf.Cos(j * Mathf.Deg2Rad) * 5f;
                TargetArriveAttack(pos);

            }
            yield return new WaitForSeconds(0.3f);
            startRot += rotateAddValue;
            endRot += rotateAddValue;
        }
        yield return new WaitForSeconds(attackEndStopTime); // 공격 종료 대기
        ChangeState(new BossMoveState(this));
    }


    // BounceAttack 함수: 회전값에 따라 탄환을 발사
    void BounceAttack(float rotateValue)
    {
        // 탄환 생성
        GameObject instance = Instantiate(enemyProjectile["EnemyBounceBullet"]);
        // 탄환 데이터 설정 (애니메이션, 속도, 데미지, 생명주기, 태그)
        projectileManage.SetProjectileData(ref instance, attackData.animCtrl, attackData.moveSpeed, 1f, 15f, "Enemy");
        // 지정된 회전값과 손 위치 회전값 합산하여 탄환 회전 후 발사
        attackManage.ShootBulletRotate(ref instance, this.transform, rotateValue + 90);
    }

    /// <summary>
    /// 목표 지점으로 이동 후 자동 삭제되는 총알 발사.
    /// 화면의 중앙(0,0)을 방향으로 총알이 발사된다.
    /// </summary>
    void TargetArriveAttack(Vector2 spawnPos)
    {
        //이곳에 원형으로 소환 후, 소환하자마자 목표 타겟(0,0)으로 바라보며 이동한다
        GameObject instance = Instantiate(enemyProjectile["EnemyTargetDestroyBullet"]);
        instance.transform.position = spawnPos;
        projectileManage.SetProjectileData(ref instance, attackData.animCtrl, attackData.moveSpeed, 1f, 15f, "Enemy");
        attackManage.ShootBulletLookAt(ref instance, instance.GetComponent<EnemyTargetBullet>().GetTargetVector2());
    }
}
