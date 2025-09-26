using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Player : Character
{
    // 플레이어 무기 종류 열거형
    enum BulletType { Wind, Iced, Fire, Lightning }
    // 현재 선택된 총알 타입
    BulletType currentBullet;

    [Header("Components & Stats")]
    // 디버그용 텍스트 UI 컴포넌트
    public TextMeshProUGUI debugText;
    public TextMeshProUGUI randomCardText;
    
    // 플레이어 파워 관련 컴포넌트
    public PlayerPower powerStats;
    // 총알 데이터 배열
    public PlayerBulletData[] bulletDataArray;
    // 버프 및 스킬 프리팹 배열
    public GameObject[] buffPrefabs, skillPrefabs;
    CircleCollider2D circleCollider;
    GameObject hitBox;

    // 총알 데이터 딕셔너리 (무기 이름을 키로)
    Dictionary<string, PlayerBulletData> bulletDataDict = new();
    // 버프 딕셔너리 (무기 이름을 키로)
    Dictionary<string, GameObject> buffDict = new();
    // 스킬 딕셔너리 (무기 이름을 키로)
    Dictionary<string, GameObject> skillDict = new();

    // 공격 타이머
    float attackTimer;
    // 파워 리셋 지연 상수
    const float PowerRestartDelay = 5f;
    // 바람 총알 히트 카운트
    public int windBulletHitCount;
    // 스킬 위치 트랜스폼
    public Transform skillTrans;

    // 게임 시작 시 초기화 및 부모 Start 호출
    protected override void Start()
    {
        base.Start();
        Init();
    }

    // 매 프레임 입력 처리 및 디버그 업데이트
    protected override void Update()
    {
        // 게임이 멈춘 경우 처리 중지
        if (Time.timeScale <= 0) return;

        base.Update();
        HandleInput();         // 키 입력 처리 함수 호출
        UpdateDebugText();     // 디버그 텍스트 갱신
    }

    // 초기 설정 및 딕셔너리 초기화
    override protected void Init()
    {
        base.Init();
        InitDic(); // 딕셔너리 초기화

        maxMoveX = 9.5f;                                                                        // 이동 가능 최대 X 좌표
        maxMoveY = 4.5f;                                                                        // 이동 가능 최대 Y 좌표
        attackDelay = 0.1f;                                                                     // 공격 간 딜레이 초기화
        SetMoveSpeed(10f);                                                                      // 이동 속도 설정
        powerStats = GetComponent<PlayerPower>();
        commonInvincibilityTime = 2f;                                                           // 무적 시간 설정
        OnDamage += GetDamageEffect;                                                            // 데미지 입었을 때 효과 재생
        OnDamage += UpdateHpUI;                                                                 // 체력 UI 갱신
        OnCharacterDeath += PlayerDeath;                                                        // 플레이어 사망 처리
        SetShieldInvincibilityTime(5f);                                                         //플레이어는 보호막에 의한 무적 시간은 5초이다
        circleCollider = GetComponent<CircleCollider2D>();

        // 히트 이펙트 오브젝트 인스턴스 생성 및 비활성화
        hitExplosion = Instantiate(hitExplosion, transform.position, transform.rotation);
        hitExplosion.transform.parent = this.transform;
        hitExplosion.SetActive(false);

        SetCurrentBullet(BulletType.Wind);                                                      // 기본 총알 설정
        //StartCoroutine(powerStats.DefaultPowerUp()); // 기본 파워업 코루틴 시작
    }

    // 딕셔너리 초기화 함수
    void InitDic()
    {
        for (int i = 0; i < bulletDataArray.Length; i++)
        {
            var key = bulletDataArray[i].weaponName;                                            // 무기 이름 키
            bulletDataDict[key] = bulletDataArray[i];                                           // 총알 데이터 저장
            buffDict[key] = buffPrefabs[i];                                                     // 버프 프리팹 저장
            skillDict[key] = skillPrefabs[i];                                                   // 스킬 프리팹 저장
        }
    }

    // 입력 처리 함수
    void HandleInput()
    {
        MoveInput();                                                                            // 이동 입력 처리
        HandleAttack();                                                                         // 공격 입력 처리
        HandleWeaponSwitch();                                                                   // 무기 변경 입력 처

        if (Input.GetKeyDown(KeyCode.F1))
        {
            gameObject.SetActive(false);                                                        // F1 눌렀을 때 비활성화
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            // 무적 토글
            if (GetIsInvincibility())
                SetIsInvincibility(false);
            else
                SetIsInvincibility(true);
        }
    }

    // 이동 입력 처리 함수
    void MoveInput()
    {
        Vector3 dir = Vector3.zero; // 이동 방향 초기화

        // 방향키 입력 감지 및 이동 방향 지정
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -maxMoveX) dir += Vector3.left;
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < maxMoveX) dir += Vector3.right;
        if (Input.GetKey(KeyCode.UpArrow) && transform.position.y < maxMoveY) dir += Vector3.up;
        if (Input.GetKey(KeyCode.DownArrow) && transform.position.y > -maxMoveY) dir += Vector3.down;

        if (dir != Vector3.zero)
        {
            anim.SetBool("Move", true);

        }
        else
        {
            anim.SetBool("Move", false);
        }

        ObjectMove(dir); // 실제 이동 처리
    }

    // 공격 처리 함수
    void HandleAttack()
    {
        attackTimer += Time.deltaTime * attackStats.attackDelayMultify; // 시간 누적
        // 공격 딜레이 경과 시 총알 발사
        if (attackTimer >= attackDelay )
        {
            FireBullet(currentBullet);
            attackTimer = 0;
        }
    }

    public void SetTransCollider(float value)
    {
        float radius = circleCollider.radius *= value;
        circleCollider.radius = radius;
        Transform instanceHitBox = transform.Find("HitBox");
        hitBox = instanceHitBox.gameObject;
        if (hitBox != null)
        {
            hitBox.transform.localScale = new Vector3(radius, radius, radius);

        }
        else
        {
            Debug.Log("없음");
        }

    }

    // 무기 변경 처리 함수
    void HandleWeaponSwitch()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return; // 스페이스 키 입력 없으면 종료

        // 파워가 최대일 경우 스킬 발동 및 파워 초기화
        if (powerStats.isPowerMax)
        {
            powerStats.playerPower = 0;
            powerStats.isPowerMax = false;
            powerStats.SetIsActivedSkill(true);
            ActivateSkill();
            Invoke(nameof(ResetPowerRegen), PowerRestartDelay);
        }
        else
        {
            AudioManager.Instance.PlaySFX("WeaponSwitch"); // 스위치 효과음 재생
        }
        powerStats.PowerUpTimerReset();                     //무기 변경할 때마다 파워 타이머는 초기화된다

        // 총알 타입 순차 변경 (0~2 반복)
        currentBullet = (BulletType)(((int)currentBullet + 1) % 3);
        SetCurrentBullet(currentBullet); // 변경된 총알 설정
    }

    // 현재 총알 타입 설정 함수
    void SetCurrentBullet(BulletType type)
    {
        currentBullet = type;
        PlayerBulletData data = bulletDataDict[type.ToString()]; // 총알 데이터 조회

        // 공격 스탯에 총알 데이터 반영
        attackStats.sprite = data.sprite;
        attackStats.animCtrl = data.animCtrl;
        attackStats.moveSpeed = data.moveSpeed * StatManager.instance.p_moveSpeedTransValue;
        attackStats.attackDelayMultify = data.attackDelayMultify * StatManager.instance.p_attackSpeedMultify;
        attackStats.damage = 1 + StatManager.instance.randomAddDamage;

        attackStats.damageMultiplier = data.attackMultify;
        powerStats.powerUpValue = data.powerValue;

        windBulletHitCount = 0; // 바람 총알 히트 카운트 초기화

        UI_Manager.instance.UpdateWeaponUI(currentBullet.ToString()); // UI 업데이트
    }

    // 총알 발사 함수
    void FireBullet(BulletType type)
    {
        int count = (type == BulletType.Fire) ? 5 : 1;                                              // 화염은 5발, 나머지는 1발
        float angle = (type == BulletType.Fire) ? -20f : 0f;                                        // 화염 각도 시작값
        float bulletSize = StatManager.instance.p_projectileSizeMultify;
        for (int i = 0; i < count; i++)
        {
            GameObject bullet = PoolManager.Instance.Pools["PlayerCommonBullet"].Get();             // 풀에서 총알 가져오기
            float rand = Random.Range(0f, 100f);

            PlayerCommonBullet bulletData = bullet.GetComponent<PlayerCommonBullet>();
            if (StatManager.instance.isCritical && rand <= StatManager.instance.criticalPercent)
            {
                
                bulletData.SetCritical();
            }
            ApplyBulletData(ref bullet);                                                            // 총알 데이터 적용
            attackManage.ShootBulletRotate(ref bullet, shootTransform["CommonBullet"], angle);      // 총알 발사 및 회전
            bullet.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
            angle += 10f; // 각도 증가
        }
    }

    // 총알 데이터 적용 함수
    void ApplyBulletData(ref GameObject bullet)
    {
        string key = currentBullet.ToString(); // 현재 총알 키
        bullet.GetComponent<SpriteRenderer>().sprite = attackStats.sprite;
        bullet.GetComponent<PlayerCommonBullet>().bulletName = key;

        var data = bulletDataDict[key];
        // 번개 총알은 스프라이트, 그 외는 애니메이터 컨트롤러 사용
        if (key == "Lightning")
        {
            projectileManage.SetProjectileData(ref bullet, attackStats.sprite,
                attackStats.moveSpeed, attackStats.damage * attackStats.damageMultiplier,
                data.lifeTime, "Player");
        }
        else
        {
            projectileManage.SetProjectileData(ref bullet, attackStats.animCtrl,
                attackStats.moveSpeed, attackStats.damage * attackStats.damageMultiplier,
                data.lifeTime, "Player");
            
        }
        Debug.Log(bullet.GetComponent<PlayerCommonBullet>().GetDamage());
    }

    // 체력 UI 업데이트 함수
    void UpdateHpUI()
    {
        UI_Manager.instance.UpdatePlayerHP(GetHp());
    }

    // 피해 효과 처리 함수
    void GetDamageEffect()
    {
        if(GetHp() != 0)
        {
            StartCoroutine(EffectCycle(hitExplosion)); // 히트 이펙트 재생 (X4)
        }
        AudioManager.Instance.PlaySFX("PlayerHitSample"); // 히트 효과음 재생
    }

    // 플레이어 사망 처리 함수
    void PlayerDeath()
    {
        //StartCoroutine(EffectCycle(destroyExplosion)); // 사망 이펙트 재생 (X4)
        Instantiate(destroyExplosion, skillTrans.position, transform.rotation); // 사망 이펙트 인스턴스화
        AudioManager.Instance.PlaySFX("PlayerHitSample"); // 사망 효과음 재생
        GameManager.instance.GameEnd(UI_Manager.ScreenInfo.Lose); // 게임 종료 처리 (패배)
        gameObject.SetActive(false);                 // 플레이어 비활성화

    }

    // 이펙트 재생 코루틴
    IEnumerator EffectCycle(GameObject effect)
    {
        effect.SetActive(true);
        ParticleSystem ps = effect.GetComponent<ParticleSystem>();
        ps.Play();
        yield return new WaitForSeconds(ps.main.duration); // 이펙트 재생 시간 대기
        effect.SetActive(false);
    }

    // 스킬 활성화 함수
    void ActivateSkill()
    {
        BulletType instanceType;
        if(StatManager.instance.isRandomSkill)
        {
            instanceType = (BulletType)Random.Range((int)0, (int)4);
        }
        else
        {
            instanceType = currentBullet;
        }
        Instantiate(skillDict[instanceType.ToString()]); // 현재 무기 스킬 인스턴스화
    }

    // 파워 재생 초기화 함수
    void ResetPowerRegen() => powerStats.SetIsActivedSkill(false);

    // 파워 온 함수
    public void PowerOn()
    {
        Instantiate(buffDict[currentBullet.ToString()]); // 현재 무기 버프 인스턴스화
        AudioManager.Instance.PlaySFX("PowerOn");       // 파워 온 효과음 재생
    }

    // 바람 스킬 함수
    public void WindSkill(GameObject bullet, int count)
    {
        StartCoroutine(SkillShoot(bullet, count, 0.8f));  // 바람 스킬 총알 연사 시작
        AudioManager.Instance.PlaySFX("WindSkillShoot"); // 바람 스킬 효과음 재생
    }

    // 얼음 스킬 함수
    public void IcedSkill(GameObject bullet)
    {
        StartCoroutine(SkillShoot(bullet, 15, 0.7f));    // 얼음 스킬 총알 연사 시작
        AudioManager.Instance.PlaySFX("IcedSkill");      // 얼음 스킬 효과음 재생
    }

    // 스킬 총알 연사 코루틴
    IEnumerator SkillShoot(GameObject prefab, int count, float damageRate)
    {
        float delay = attackDelay * attackStats.attackDelayMultify; // 총알 발사 간 딜레이 계산

        for (int i = 0; i < count; i++)
        {
            GameObject instance = Instantiate(prefab, shootTransform["Skill"].position, shootTransform["Skill"].rotation); // 스킬 총알 인스턴스화
            if (instance != null)
            {
                instance.tag = "Player"; // 태그 설정
                instance.GetComponent<Projectile>().SetMoveSpeed(attackStats.moveSpeed * 2f); // 속도 설정
                instance.GetComponent<Projectile>().SetDamage(attackStats.damage * StatManager.instance.p_skillDamageMultify * attackStats.damageMultiplier * damageRate); // 데미지 설정
            }
            yield return new WaitForSeconds(delay); // 딜레이 대기
        }
    }

    // 디버그 텍스트 업데이트 함수
    void UpdateDebugText()
    {
        if (debugText == null) return;

        debugText.text = $"hp: {GetHp()}\n" +
                         $"weapon: {currentBullet}\n" +
                         $"damage: {attackStats.damage * attackStats.damageMultiplier}\n" +
                         $"AttackDelay: {attackDelay * attackStats.attackDelayMultify}\n" +
                         $"moveSpeed: {attackStats.moveSpeed}\n" +
                         $"power: {powerStats.powerUpValue}\n" +
                         $"playerMoveSpeed: {moveSpeed * objectMoveSpeedMultify}\n" +
                         $"PlayerPowerValue: {powerStats.playerPower}\n" +
                         $"무적 여부: {GetIsInvincibility().ToString()}";
    }

    public void ShowCardText(string text)
    {
        StartCoroutine(RenderCardText(text));
    }

    public IEnumerator RenderCardText(string text)
    {
        float showTime = 2;
        randomCardText.gameObject.SetActive(true);
        randomCardText.text = text;
        yield return new WaitForSeconds(showTime);
        randomCardText.text = "";
        randomCardText.gameObject.SetActive(false);
    }

    // 현재 플레이어 무기 이름 반환 함수
    public string GetPlayerWeaponName() => currentBullet.ToString();
}
