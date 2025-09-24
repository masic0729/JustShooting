using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBossTest3 : EndBoss
{

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
    public override void Attack()
    {
        base.Attack();
    }

    public void StartPatten(int pattenIndex)
    {
        if (pattenIndex == 0)
            StartCoroutine(Patten0());
        if (pattenIndex == 1)
            StartCoroutine(Patten0());
    }

    public IEnumerator Patten0()
    {
        // 발사 횟수 지정
        const int shootCount = 3;
        const int shootBulletsCount = 3;

        Vector2 spawnPos = transform.position;

        for(int i = 0; i < shootCount; i++)
        {
            Vector2 yTransValue = new Vector2(0, 0.5f);
            float addSpeedValue = 0.1f;
            for (int j = 0; j < shootBulletsCount; j++)
            {
                TunningAttack(spawnPos + yTransValue, addSpeedValue);
                TunningAttack(spawnPos - yTransValue, addSpeedValue);
                yTransValue *= 2;
                addSpeedValue *= 2;
            }
            yield return new WaitForSeconds(0.2f);

        }
        yield return new WaitForSeconds(attackEndStopTime); // 공격 종료 대기
        ChangeState(new BossMoveState(this));

    }

    public IEnumerator Patten1()
    {
        /*// 발사 횟수 지정
        int shootRandom = 5;
        // 탄환 회전각 랜덤 지정 (40도~60도)
        float shootRandomRotate = Random.Range(40f, 60f);
        int rotateAddValue = 10;
        int randStartAddRotate = Random.Range(0, 300);

        int startRot = 0, endRot = 300;
        bool isRotateRight = Random.Range(0, 100) > 50 ? true : false;
        for (int i = 0; i < shootRandom; i++)
        {

            for (int j = startRot + randStartAddRotate; j < endRot + randStartAddRotate; j += 10)
            {
                Vector2 pos;
                pos.x = Mathf.Sin(j * Mathf.Deg2Rad) * 12f;
                pos.y = Mathf.Cos(j * Mathf.Deg2Rad) * 12f;
                TargetArriveAttack(pos);

            }
            yield return new WaitForSeconds(0.3f);
            if (isRotateRight == true)
            {
                startRot += rotateAddValue;
                endRot += rotateAddValue;
            }
            else
            {

                startRot -= rotateAddValue;
                endRot -= rotateAddValue;
            }
        }*/
        yield return new WaitForSeconds(attackEndStopTime); // 공격 종료 대기
        ChangeState(new BossMoveState(this));
    }


    // BounceAttack 함수: 회전값에 따라 탄환을 발사
    void TunningAttack(Vector2 spawnPos, float addSpeedValue)
    {
        // 탄환 생성
        GameObject instance = Instantiate(enemyProjectile["EnemyTunningBullet"], spawnPos, transform.rotation);
        instance.transform.Rotate(0, 0, 90);

        // 탄환 데이터 설정 (애니메이션, 속도, 데미지, 생명주기, 태그)
        projectileManage.SetProjectileData(ref instance, attackData.animCtrl, attackData.moveSpeed * 0.7f + addSpeedValue, 1f, 7f, "Enemy");
    }

    /// <summary>
    /// 목표 지점으로 이동 후 자동 삭제되는 총알 발사.
    /// 화면의 중앙(0,0)을 방향으로 총알이 발사된다.
    /// </summary>
    void TargetArriveAttack(Vector2 spawnPos)
    {
        //이곳에 원형으로 소환 후, 소환하자마자 목표 타겟(0,0)으로 바라보며 이동한다
        GameObject instance = Instantiate(enemyProjectile["EnemyTargetBullet"]);
        instance.transform.position = spawnPos;
        projectileManage.SetProjectileData(ref instance, attackData.animCtrl, attackData.moveSpeed * 0.5f, 1f, 15f, "Enemy");
        attackManage.ShootBulletLookAt(ref instance, instance.GetComponent<EnemyTargetBullet>().GetTargetVector2());
    }
}
