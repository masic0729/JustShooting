using UnityEngine;

public class Projectile : IObject
{
    protected GameObject thisGameObject; // 현재 발사체 오브젝트 참조

    protected Vector3 projectileMoveVector; // 발사 방향 벡터

    [SerializeField] protected GameObject ProjectileHitEffect; //정확히 어떤 것들이 있는 지 파악이 안되므로 선언만 하였음
    ObjectInteraction projectileInteraction; // 피해 전달 클래스

    [SerializeField]
    protected float damage; //발사체는 데미지가 있다

    //    [SerializeField]
    protected float lifeTimer = 0, lifeTime = 5; // 발사체의 지속 시간을 정의함

    [SerializeField]
    private bool isPoolObject; //발사체의 출처가 풀링된 객채를 확인함
    protected string hitSoundName = null;

    protected bool isCanMove = true; // 이동 가능 여부

    // 오브젝트가 활성화될 때 실행됨 (풀링 포함)
    virtual protected void OnEnable()
    {
        //해당 오브젝트가 등장할 때마다 풀링 스크립트 존재 시 사실임을 확인
        if (this.gameObject.GetComponent<PoolProjectile>())
            isPoolObject = true;
        lifeTimer = 0; // 생존 시간 초기화
    }

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        ProjectileMovement(); // 이동 처리
        AutoDestroy(); // 자동 삭제 처리
    }

    protected override void Init()
    {
        projectileInteraction = new ObjectInteraction(); // 피해 처리 클래스 인스턴스 생성

        maxMoveX = 10f; // 이동 제한 범위
        maxMoveY = 5.5f;
        thisGameObject = this.gameObject; // 자기 자신 참조
        projectileMoveVector = Vector3.up; // 기본 이동 방향 위쪽
    }

    // 이동 벡터에 따라 오브젝트 이동
    void ProjectileMovement()
    {
        if (isCanMove == true)
        {
            ObjectMove(projectileMoveVector);
        }
    }

    // 생존 시간 또는 화면 벗어남 조건으로 삭제 처리
    void AutoDestroy()
    {
        lifeTimer += Time.deltaTime;

        if (lifeTimer >= lifeTime || Mathf.Abs(transform.position.x) > maxMoveX || Mathf.Abs(transform.position.y) > maxMoveY)
        {
            CheckPoolObject(); // 삭제 or 풀에 반환
        }
    }

    // 충돌 처리
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //현재는 이런 식으로 묶었지만, 예외적인 처리가 필요한 경우 조건 처리를 분리해야 할 수 있음
        if (this.transform.tag == "Player" && collision.transform.tag == "Enemy" ||
            this.transform.tag == "Enemy" && collision.transform.tag == "Player")
        {
            Character instanceHitCharacter = collision.GetComponent<Character>();
            instanceHitCharacter.SetHitSoundName(hitSoundName);
            // 무적이 아닌 대상일 경우 피해 적용
            if (instanceHitCharacter != null && instanceHitCharacter.GetIsInvincibility() == false)
            {
                // 특정 조건(플레이어의 얼음 탄환 + 일반 적)일 경우 특수 데미지 적용
                if (this.gameObject.GetComponent<PlayerIcedBullet>() == true &&
                    collision.GetComponent<Enemy>().GetIsBoss() == false)
                {
                    SetDamage(999f);
                    //projectileInteraction.SendDamage(ref instanceHitCharacter, this.GetDamage());
                    instanceHitCharacter.OnCharacterDamaged(GetDamage());
                    instanceHitCharacter.OnDamage?.Invoke();
                }
                else
                {
                    //projectileInteraction.SendDamage(ref instanceHitCharacter, this.GetDamage());
                    instanceHitCharacter.OnCharacterDamaged(GetDamage());
                    instanceHitCharacter.OnDamage?.Invoke();
                }


                if (ProjectileHitEffect != null)
                {
                    //해당 부분에 이펙트 발생
                    float randPos = Random.Range(-0.15f, 0.15f);
                    Vector2 spawnHitEffectPosition = new Vector2(collision.transform.position.x + Mathf.Abs(randPos), transform.position.y + randPos);
                    ParticleManager.Instance.PlayEffect(ProjectileHitEffect.name, collision.ClosestPoint(spawnHitEffectPosition));
                }
            }

            // 충돌 대상이 캐릭터일 경우 발사체 삭제
            if (collision.GetComponent<Character>() == true)
            {
                CheckPoolObject(); //발사체인 본인이 소멸하는 것
            }
        }
    }

    // 오브젝트가 풀링된 객체인지 확인하고 삭제 또는 반환 처리
    void CheckPoolObject()
    {
        if (isPoolObject)
        {
            ClearObjectResources();                                       //기본 리소스 모두 삭제
            GetComponent<PoolProjectile>().pool.Release(this.gameObject); //이 객체가 풀링된 객체라면 릴리즈
        }
        else
        {
            Destroy(this.gameObject);                                     //아니라면 일반적인 오브젝트 삭제
        }
    }

    // 오브젝트가 소멸될 때 리소스를 초기화함
    void ClearObjectResources()
    {
        GetComponent<SpriteRenderer>().sprite = null;
        GetComponent<Animator>().runtimeAnimatorController = null;
    }

    //getset

    public void SetDamage(float damageValue)
    {
        damage = damageValue;
    }

    public float GetDamage()
    {
        return damage;
    }

    public void SetLifeTime(float timeSeconds)
    {
        lifeTime = timeSeconds;
    }

    public float GetLifeTime()
    {
        return lifeTime;
    }

    public bool GetIsPoolObject()
    {
        return isPoolObject;
    }
}
