using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 적 캐릭터의 기본 행동을 정의하는 클래스
public class Enemy : Character
{
    // 적의 상태 머신 관리용 변수
    public StateMachine enemyState;
    // 상태 인덱스 (보스 등 복잡한 상태 처리 시 사용)
    public int stateIndex = 0;


    // 적이 이동 전 현재 위치를 기억한다
    public Vector2 lastPosition;
    // 적이 도달하려는 목표 위치 벡터
    public Vector2 arrivePosition;

    // 타겟팅 관련 관리 클래스
    protected TargetBulletManagement targetManage;

    // 발사체 프리팹 배열 (인스펙터 등록용)
    public GameObject[] enemyProjectiles;
    // 발사체 이름과 프리팹을 매핑하는 딕셔너리
    public Dictionary<string, GameObject> enemyProjectile;

    // 현재 타겟 위치 저장용 변수
    protected Vector2 currentTargetPos;

    [Header("Enemy의 공격 데이터")]
    // 적 공격 관련 데이터 클래스
    public EnemyAttackData attackData;

    // 이동 제어용 클래스
    public ObjectMovement movement;

    // 현재 자신의 게임 오브젝트 참조 변수
    protected GameObject thisGameObject;
    // 목표 이동 속도
    protected float targetMoveSpeed;
    // 목표 위치에 도달 허용 오차 거리
    protected float distanceNeedValue = 1f;

    // 피격음 재생 쿨타임(초)
    private float hitSoundCooldown = 0.2f;
    // 마지막 피격음 재생 시점 기록
    private float lastHitSoundTime = -1f;
    public float moveTimer = 0f;

    [SerializeField]
    // 보스 여부 플래그
    bool isBoss = false;
    // 고정 위치 사용 여부 플래그
    protected bool isSelfPosition = true;

    // 초기 Awake 호출 (FSM 등 외부에서 설정)
    protected virtual void Awake()
    {
        // FSM은 외부에서 설정하도록 유지
    }

    // 시작 시 부모 Start 호출
    protected override void Start()
    {
        base.Start();
    }

    // 매 프레임 호출, 기본 업데이트 후 화면 영역 벗어남 체크
    protected override void Update()
    {
        base.Update();
        CheckOverGameZone();
    }

    // 초기화 함수, 부모 Init 호출 후 각종 변수 초기화
    protected override void Init()
    {
        base.Init();
        // 사망 시 기본 폭발 이펙트 연결
        OnCharacterDeath += DefaultEnemyDestroyEffect;
        lastPosition = this.transform.position;
        movement = new ObjectMovement();
        thisGameObject = this.gameObject;
        targetManage = new TargetBulletManagement();

        enemyProjectile = new Dictionary<string, GameObject>();

        attackData.moveSpeed = 10f;

        // 발사체 배열을 딕셔너리로 변환하여 관리
        if (enemyProjectiles != null)
        {
            for (int i = 0; i < enemyProjectiles.Length; i++)
            {
                enemyProjectile[enemyProjectiles[i].name] = enemyProjectiles[i];
            }
        }

        // 기본 도착 위치 설정
        arrivePosition = new Vector2(3f, 0);
    }

    // 화면 영역 벗어남 체크 후 오브젝트 파괴
    void CheckOverGameZone()
    {
        if (this.transform.position.x < -12f)
        {
            Destroy(this.gameObject);
        }
    }

    // 사망 시 폭발 이펙트 및 사운드 실행
    void DefaultEnemyDestroyEffect()
    {
        ParticleManager.Instance.PlayEffect("EnemyExplosion", this.transform.position);
        AudioManager.Instance.PlaySFX("EnemyExplosion");
    }

    // 상태 변경 요청 처리, 상태 머신에 위임
    public void ChangeState(EnemyState state)
    {
        enemyState.ChangeState(state);
    }

    // 충돌 감지 및 처리
    virtual protected void OnTriggerEnter2D(Collider2D collision)
    {
        const float damageValue = 1f;

        // 플레이어와 충돌 시 데미지 처리
        if (collision.transform.name == "Player" &&
            collision.TryGetComponent(out Character character))
        {
            if (character != null && characterInteraction != null &&
                character.GetIsInvincibility() == false)
            {
                character.OnCharacterDamaged(damageValue);
                character.OnDamage?.Invoke();
            }
        }

        // 플레이어의 발사체와 충돌 시 히트 이펙트 출력
        if (collision.transform.tag == "Player" && (collision.GetComponent<Projectile>()))
        {
            float randPos = Random.Range(-0.15f, 0.15f);
            Vector2 spawnHitEffectPosition = new Vector2(collision.transform.position.x + Mathf.Abs(randPos), transform.position.y + randPos);
            ParticleManager.Instance.PlayEffect("EnemyHit", collision.ClosestPoint(spawnHitEffectPosition));
            DemagedSound();
        }
    }

    // 피격 시 효과음 재생 (쿨타임 적용)
    public void DemagedSound()
    {
        if (Time.time - lastHitSoundTime < hitSoundCooldown) return;

        AudioManager.Instance.PlaySFX("EnemyHit");
        lastHitSoundTime = Time.time;
    }

    // 적이 목표 위치로 이동하게 설정
    public void SetTargetPosition(Vector2 pos)
    {
        isSelfPosition = false;
        currentTargetPos = pos;
    }

    // 보스 여부 반환
    public bool GetIsBoss()
    {
        return isBoss;
    }

    // 보스 여부 설정
    public void SetIsBoss(bool state)
    {
        isBoss = state;
    }

    // 적의 기본 공격 함수 (상속 시 오버라이드 가능)
    /*virtual public void Attack()
    {
        Debug.Log("공격끝");
        ChangeState(new BossMoveState(this));
    }*/

    virtual public IEnumerator Attack()
    {
        Debug.Log("공격");
        yield return null;
    }
}
