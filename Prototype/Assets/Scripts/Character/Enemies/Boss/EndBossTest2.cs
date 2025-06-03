using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBossTest2 : EndBoss
{
    // Start 함수: 초기화 및 부모 Start 호출
    protected override void Start()
    {
        base.Start();
        Init(); // 추가 초기화
    }

    // Update 함수: 매 프레임 부모 Update 호출
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

    /* 주석 처리된 코루틴 공격 예제
    public override IEnumerator EnemyAttack()
    {
        // 발사 횟수 랜덤 지정 (2~4회)
        int shootRandom = Random.Range(2, 5);
        // 탄환 회전각 랜덤 지정 (40도~60도)
        float shootRandomRotate = Random.Range(40f, 60f);

        for (int i = 0; i < shootRandom; i++)
        {
            BounceAttack(-shootRandomRotate); // 왼쪽 방향 공격
            BounceAttack(shootRandomRotate);  // 오른쪽 방향 공격
            yield return new WaitForSeconds(0.5f); // 0.5초 대기
        }
        yield return new WaitForSeconds(attackEndStopTime); // 공격 종료 대기
    }
    */

    // BounceAttack 함수: 회전값에 따라 탄환을 발사
    public void BounceAttack(float rotateValue)
    {
        // 탄환 생성
        GameObject instance = Instantiate(enemyProjectile["EnemyBounceBullet"]);
        // 탄환 데이터 설정 (애니메이션, 속도, 데미지, 생명주기, 태그)
        projectileManage.SetProjectileData(ref instance, attackData.animCtrl, attackData.moveSpeed, 1f, 10f, "Enemy");
        // 지정된 회전값과 손 위치 회전값 합산하여 탄환 회전 후 발사
        attackManage.ShootBulletRotate(ref instance, shootTransform["HandAttack"], rotateValue + shootTransform["HandAttack"].transform.rotation.z);
    }
}
