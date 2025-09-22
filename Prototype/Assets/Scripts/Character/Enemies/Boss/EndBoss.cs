using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 엔드 보스 클래스.
/// Boss 클래스를 상속하며, 최종 보스 여부에 따른 게임 종료 이벤트 처리 포함.
/// </summary>
public class EndBoss : Boss
{
    [SerializeField]
    // 최종 보스 여부 플래그
    bool isFinalBoss;
    public int maxPatten;

    /// <summary>
    /// 초기화 및 부모 Start 호출
    /// </summary>
    protected override void Start()
    {
        base.Start();
        Init();
    }

    /// <summary>
    /// 매 프레임 업데이트 호출
    /// </summary>
    protected override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// 오브젝트 파괴 시 동작 (현재 내용 없음)
    /// </summary>
    private void OnDestroy()
    {
    }

    /// <summary>
    /// 최종 보스 사망 시 게임 승리 처리
    /// </summary>
    protected void FinalEndBossDeath()
    {
        GameManager.instance.GameEnd(UI_Manager.ScreenInfo.Win);
    }

    /// <summary>
    /// 초기 설정 및 이벤트 구독
    /// </summary>
    protected override void Init()
    {
        base.Init();
        TransBossCollider();

        if (isFinalBoss == true)
        {
            OnCharacterDeath += FinalEndBossDeath; // 최종 보스일 경우 승리 이벤트 연결

        }
        else
        {
            OnCharacterDeath += StageClearAction; // 일반 보스 클리어 이벤트
            OnCharacterDeath += RestartWave;      // 웨이브 재시작 (추후 제거 예정)
        }
    }

    /// <summary>
    /// 스테이지 클리어 시 추가 동작 (맵 변경, 포탈 생성 등)
    /// </summary>
    void StageClearAction()
    {
        Debug.Log("스테이지 클리어. 클리어 이후 맵 변경, 몬스터 데이터, 포탈 생성 등 다양한 기능 추가 요구");
    }

    public void TransBossCollider()
    {
        Debug.Log("보스 콜라이더 변환");
        if (enemyCol.enabled == false)
        {
            enemyCol.enabled = true;

        }
        else if (enemyCol.enabled == true)
        {
            enemyCol.enabled = false;
        }
    }

    /// <summary>
    /// 충돌 처리, 부모 클래스 호출
    /// </summary>
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
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

    public virtual void Attack()
    {
        int result = Random.Range(0, maxPatten);
        anim.SetTrigger("Attack");
        anim.SetInteger("PattenIndex", result);
    }
}
