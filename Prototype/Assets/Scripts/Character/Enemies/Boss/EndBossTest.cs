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

     //코루틴 기반 공격 예제 (현재 주석 처리됨)
    public override IEnumerator Attack()
    {
        SpreadAttack(10, 0f);
        yield return new WaitForSeconds(0.5f);
        SpreadAttack(10, 30f);
        yield return new WaitForSeconds(1f);
        SpreadAttack(18, 0f);

        yield return new WaitForSeconds(attackEndStopTime);
    }

    // SpreadAttack 함수: 원형 탄환 발사
    public void SpreadAttack(int shootCount, float rootRotateValue)
    {
        for (int i = 1; i <= shootCount; i++)
        {
            // 탄환 생성
            GameObject instance = Instantiate(enemyProjectile["EnemyBullet"]);
            // 탄환 데이터 세팅 (애니메이션, 속도, 데미지, 생명주기, 태그)
            projectileManage.SetProjectileData(ref instance, attackData.animCtrl, attackData.moveSpeed, 1f, 5f, "Enemy");
            // 회전값에 따른 방향으로 탄환 발사
            attackManage.ShootBulletRotate(ref instance, this.gameObject.transform, 360 / shootCount * i + rootRotateValue);
        }
    }
}
