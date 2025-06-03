using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBossTest3 : EndBoss
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

    /*
    // 주석 처리된 코루틴 공격 예제
    public override IEnumerator EnemyAttack()
    {
        // 플레이어 오브젝트 탐색
        GameObject player = GameObject.Find("Player");
        yield return ShootBullets();        // 플레이어 무시한 총알 발사
        yield return new WaitForSeconds(2.5f); // 대기
        yield return ShootBullets(player);  // 플레이어 타겟팅 총알 발사

        yield return base.EnemyAttack();
    }
    */

    // ShootBullets 함수: 여러 위치에서 탄환 발사 (플레이어 타겟 가능)
    IEnumerator ShootBullets(GameObject player = null)
    {
        const float maxSpawnYRange = 4f;   // Y축 최대 스폰 범위
        const int spawnCount = 6;           // 총알 개수

        // 발사 위치 Y 좌표 배열 생성
        float[] spawnBulletsY = new float[spawnCount];
        for (int i = 0; i < spawnBulletsY.Length; i++)
        {
            spawnBulletsY[i] = maxSpawnYRange / spawnCount * (i * 2) - maxSpawnYRange;
            Debug.Log(spawnBulletsY[i]); // 좌표 디버그 출력
        }

        Vector3 spawnPosition;

        // 각 Y 좌표마다 탄환 생성 및 발사
        for (int i = 0; i < spawnBulletsY.Length; i++)
        {
            spawnPosition = new Vector3(9.9f, spawnBulletsY[i], 0);
            // 탄환 인스턴스 생성
            GameObject instance = Instantiate(enemyProjectile["EnemyBullet"], spawnPosition, shootTransform["HandAttack"].rotation);
            // 탄환 데이터 설정 (애니메이션, 속도, 데미지, 생명주기, 태그)
            projectileManage.SetProjectileData(ref instance, attackData.animCtrl, attackData.moveSpeed, 1f, 5f, "Enemy");

            // 플레이어가 있으면 탄환을 플레이어 방향으로 회전 설정
            if (player != null)
            {
                targetManage.DirectTargetObject(ref instance, ref player);
            }

            // 다음 탄환까지 대기
            yield return new WaitForSeconds(0.4f);
        }
    }

    // 배열 요소를 무작위로 섞는 함수
    void ShuffleArray<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            // 무작위 인덱스 선택
            int randIndex = Random.Range(i, array.Length);
            // 요소 교환
            T temp = array[i];
            array[i] = array[randIndex];
            array[randIndex] = temp;
        }
    }

    // 충돌 처리 함수: 부모 클래스 충돌 처리 호출
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
