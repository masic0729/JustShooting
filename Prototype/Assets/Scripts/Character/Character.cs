using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : IObject
{
    // 애니메이터 컴포넌트
    protected Animator anim;
    // 피격 시 이펙트 오브젝트
    public GameObject hitExplosion;
    // 캐릭터 사망 시 이펙트 오브젝트
    public GameObject destroyExplosion;

    // 공격 관련 스탯 모음 객체
    public AttackStats attackStats;
    // 발사체 데이터 관리 객체
    public ProjectileManagement projectileManage;
    // 발사 위치 트랜스폼 배열 (자식 오브젝트)
    private Transform[] shootTransforms;
    // 공격 실행 및 관리 객체
    public AttackManagement attackManage;

    // 캐릭터 사망 시 발생하는 이벤트
    public Action OnCharacterDeath;
    // 캐릭터가 피해를 입었을 때 발생하는 이벤트
    public Action<float> OnCharacterDamaged;
    public Action OnDamage;
    // 발사 위치를 이름 기준으로 저장하는 딕셔너리
    public Dictionary<string, Transform> shootTransform;

    // 현재 체력 및 최대 체력
    [SerializeField]
    protected float hp, maxHp;
    // 보호막 수치 (체력 대신 감소)
    protected float shield;

    // 데미지 전달 및 처리용 객체
    protected ObjectInteraction characterInteraction;

    [Header("캐릭터 전투 시스템 배율")]
    [SerializeField]
    // 공격력 배율 (값이 클수록 피해량 증가)
    protected float attackMultiplier = 1;
    [SerializeField]
    // 공격 간 딜레이 (값이 클수록 공격 속도 느림)
    protected float attackDelay;
    [SerializeField]
    // 발사체 속도 배율
    protected float projectileMoveSpeedMultify = 1;
    [SerializeField]
    // 캐릭터가 받는 피해 배율 (값이 클수록 피해량 증가)
    protected float characterGetDamageMultify = 1;
    // 무적 지속 시간
    protected float commonInvincibilityTime = 0f;               //일반피해에 의한 무적시간
    protected float shieldInvincibilityTime = 0f;               //보호막피해에 의한 무적시간
    // 무적 상태 여부
    protected bool isInvincibility;

    /// <summary>
    /// 초기화 및 컴포넌트 설정, 이벤트 연결
    /// </summary>
    protected override void Init()
    {
        anim = GetComponent<Animator>();
        attackStats = gameObject.AddComponent<AttackStats>();
        attackManage = new AttackManagement();
        projectileManage = new ProjectileManagement();
        characterInteraction = new ObjectInteraction();

        OnCharacterDamaged += TakeDamage;                           //캐릭터는 기본적으로 공격받으면 체력이나 보호막이 감소한다
        //OnDamage += OnInvincibility;

        // 캐릭터 사망 시 게임 오브젝트 삭제
        OnCharacterDeath += DestroyCharacter;
        shootTransform = new Dictionary<string, Transform>();

        // 자식 트랜스폼 배열로 받아옴 (부모 제외)
        shootTransforms = GetComponentsInChildren<Transform>();

        foreach (Transform t in shootTransforms)
        {
            if (t != this.transform)
            {
                shootTransform[t.gameObject.name] = t;
            }
        }
    }

    /// <summary>
    /// 피해를 입으면 무적 상태 시작 및 피해 이벤트 발생
    /// </summary>
    public void OnInvincibility(float InvincibilityTime)
    {
        SetIsInvincibility(true);
        Invoke("TransIsInvincibilityFalse", InvincibilityTime);
        //OnCharacterDamaged?.Invoke();
        
    }

    public void TakeDamage(float damage)
    {
        // 쉴드가 있을 경우 쉴드를 먼저 감소시킴
        if (shield > 0)
        {
            if (shield - damage < 0)
            {
                //SetShield(0);  
                // 쉴드가 0 아래로 내려가면 0으로 설정
                shield = 0;
            }
            else
            {
                //SetShield(GetShield() - damage);  
                // 쉴드 감소
                shield -= damage;
            }
            OnInvincibility(shieldInvincibilityTime);
        }
        else
        {
            // 쉴드가 없으면 체력에서 직접 데미지 감소 처리
            float caluHp = hp - damage;

            if (caluHp <= 0)
            {
                //SetHp(0);  
                // 체력이 0 이하가 되면 0으로 설정하고 죽음 처리 실행
                hp = 0;
                OnCharacterDeath?.Invoke();
            }
            else
            {
                hp -= damage;  // 체력 감소
            }
            OnInvincibility(commonInvincibilityTime);

        }
    }

    /// <summary>
    /// 캐릭터 사망 시 게임 오브젝트 삭제 처리
    /// </summary>
    void DestroyCharacter()
    {
        Destroy(this.gameObject);
    }

    // 체력 관련 get/set
    public void SetHp(float value) => hp = value;
    public float GetHp() => hp;
    public void SetMaxHp(float value) => maxHp = value;
    public float GetMaxHp() => maxHp;

    // 보호막 관련 get/set
    public void SetShield(float value) => shield = value;
    public float GetShield() => shield;

    // 공격력 배율 관련 get/set
    public void SetAttackMultiplier(float value) => attackMultiplier = value;
    public float GetAttackMultiplier() => attackMultiplier;

    // 무적 상태 관련 get/set
    public void SetIsInvincibility(bool state) => isInvincibility = state;
    public bool GetIsInvincibility() => isInvincibility;

    // 무적 지속 시간 반환
    public float GetInvincibilityTime() => commonInvincibilityTime;

    public void SetCommonInvincibilityTime(float time) => commonInvincibilityTime = time;
    public void SetShieldInvincibilityTime(float time) => shieldInvincibilityTime = time;
    public float GetCommonInvincibilityTime() => commonInvincibilityTime;

    /// <summary>
    /// 무적 상태를 종료하는 함수
    /// </summary>
    public void TransIsInvincibilityFalse()
    {
        isInvincibility = false;
    }

    // Start & Update 등은 부모 클래스 호출만 하므로 생략 가능
}
