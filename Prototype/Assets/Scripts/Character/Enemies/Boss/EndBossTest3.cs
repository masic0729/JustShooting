using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.Rendering.DebugUI.Table;

public class EndBossTest3 : EndBoss
{
    [SerializeField] Transform patten0Shooter;
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
            StartCoroutine(Patten1());
        if (pattenIndex == 2)
            StartCoroutine(Patten2());
    }

    public override void ShowReAction()
    {
        base.ShowReAction();
        //사운드 구현
    }

    public IEnumerator Patten0()
    {
        AudioManager.Instance.PlaySFX("Boss3Patten1");

        // 발사 횟수 지정
        const int shootCount = 3;
        const int shootBulletsCount = 3;

        Vector2 spawnPos = patten0Shooter.position;

        for (int i = 0; i < shootCount; i++)
        {
            float rotateValue = 30f;
            float addSpeedValue = 0.1f;

            for (int j = 0; j < shootBulletsCount; j++)
            {
                TunningAttack(spawnPos, addSpeedValue, 0);

                TunningAttack(spawnPos, addSpeedValue, rotateValue);
                TunningAttack(spawnPos, addSpeedValue, -rotateValue);
                rotateValue += 30;
                addSpeedValue *= 2;
            }
            yield return new WaitForSeconds(0.2f);

        }
        yield return new WaitForSeconds(attackEndStopTime); // 공격 종료 대기
        ChangeState(new BossMoveState(this));

    }

    public IEnumerator Patten1()
    {
        AudioManager.Instance.PlaySFX("Boss3Patten2");

        float startSpawnY = 5f;
        const float startSpawnX = 10f;
        float addSpeedX = 0f;
        const float shootDelay = 0.2f;

        for (int i = 0; i < 5; i++)
        {
            Vector2 resultSpawnPos = new Vector2(startSpawnX, startSpawnY);
            StartCoroutine(RepeatPatten1(resultSpawnPos, addSpeedX));
            startSpawnY -= 2.0f;
            yield return new WaitForSeconds(shootDelay);
        }
        addSpeedX += 4f;
        startSpawnY += 0.25f;

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 5; i++)
        {
            Vector2 resultSpawnPos = new Vector2(startSpawnX, startSpawnY);
            StartCoroutine(RepeatPatten1(resultSpawnPos, addSpeedX));

            startSpawnY += 2f;
            yield return new WaitForSeconds(shootDelay);
        }

        yield return new WaitForSeconds(attackEndStopTime); // 공격 종료 대기
        ChangeState(new BossMoveState(this));
    }
    
    IEnumerator RepeatPatten1(Vector2 spawnPos, float addSpeedValue)
    {
        for(int i = 0; i < 10; i++)
        {
            CommonAttack(spawnPos, addSpeedValue);
            yield return new WaitForSeconds(0.07f);

        }
    }

    public IEnumerator Patten2()
    {

        Player player = GameObject.Find("Player").GetComponent<Player>();

        // 발사 횟수 지정
        const int shootCount = 2;

        for (int i = 0; i < shootCount; i++)
        {
            if (player == null)
                break;


            Vector2 playerpos = new Vector2(player.transform.position.x, 6.5f);

            StartCoroutine(DownAttack(playerpos));
            yield return new WaitForSeconds(1.5f);

        }
        yield return new WaitForSeconds(attackEndStopTime); // 공격 종료 대기
        ChangeState(new BossMoveState(this));

    }

    // BounceAttack 함수: 회전값에 따라 탄환을 발사
    void TunningAttack(Vector2 spawnPos, float addSpeedValue, float rot)
    {
        // 탄환 생성
        GameObject instance = Instantiate(enemyProjectile["EnemyTunningBullet"], spawnPos, transform.rotation);
        instance.transform.Rotate(0, 0, 90 + rot);

        // 탄환 데이터 설정 (애니메이션, 속도, 데미지, 생명주기, 태그)
        projectileManage.SetProjectileData(ref instance, attackData.animCtrl, attackData.moveSpeed * 1f + addSpeedValue, 1f, 7f, "Enemy");
    }

    void CommonAttack(Vector2 spawnPos, float addSpeedValue)
    {
        GameObject instance = Instantiate(enemyProjectile["EnemyBullet"], spawnPos, transform.rotation);
        instance.transform.Rotate(0, 0, 90);
        projectileManage.SetProjectileData(ref instance, attackData.animCtrl, attackData.moveSpeed * 1f + addSpeedValue, 1f, 7f, "Enemy");
    }

    IEnumerator DownAttack(Vector2 spawnPos)
    {
        AudioManager.Instance.PlaySFX("Boss3Patten3");

        const int shootBulletsCount = 70;
        for(int i = 0; i < shootBulletsCount; i++)
        {
            float randX = Random.Range(-1.0f, 1.0f);
            Vector2 resultSpawnPos = new Vector2(spawnPos.x + randX, 6.5f);

            GameObject instance = Instantiate(enemyProjectile["EnemyBullet"], resultSpawnPos, transform.rotation);
            instance.transform.Rotate(0, 0, 180);
            projectileManage.SetProjectileData(ref instance, attackData.animCtrl, attackData.moveSpeed * 1.0f, 1f, 7f, "Enemy");
            yield return new WaitForSeconds(0.05f);
        }
    }
}
