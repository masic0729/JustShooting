using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public StateMachine enemyState;                                     //FSM 스크립트
    public int stateIndex = 0;
    public Vector2 arriveVector;                                               //객체가 도착하려는 위치값

    //public StateMachine stateMachine;                                   
    protected TargetBulletManagement targetManage;

    public GameObject[] enemyProjectiles;                               //인스펙터에 등록된 발사체 종류
    public Dictionary<string, GameObject> enemyProjectile;           //발사체 종류를 딕셔너리화
    protected Vector2 currentTargetPos;

    [Header("Enemy의 공격 데이터")]
    public EnemyAttackData attackData;

    public ObjectMovement movement;
    
    protected GameObject thisGameObject;
    protected float targetMoveSpeed;
    protected float distanceNeedValue = 1f;

    private float hitSoundCooldown = 0.2f;
    private float lastHitSoundTime = -1f;

    [SerializeField]
    bool isBoss = false; //보스 유무 확인. 기본값은 거짓
    protected bool isSelfPosition = true;

    protected virtual void Awake()
    {
        //stateMachine = new StateMachine();
        //Debug.Log("일단 실행은 되는데, 기능은 안될 수 있다?");
    }

    protected override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        CheckOverGameZone();
    }

    protected override void Init()
    {
        base.Init();
        OnCharacterDeath += DefaultEnemyDestroyEffect;                                      //사망 시 기본폭발 이펙트
        movement = new ObjectMovement();
        thisGameObject = this.gameObject;
        targetManage = new TargetBulletManagement();

        enemyProjectile = new Dictionary<string, GameObject>();
        AddX4();
        attackData.moveSpeed = 10f;

        if (enemyProjectiles != null)
        {
            for (int i = 0; i < enemyProjectiles.Length; i++)
            {
                enemyProjectile[enemyProjectiles[i].name] = enemyProjectiles[i];
            }
        }
        arriveVector = new Vector2(3f, 0);
    }

    void AddX4()
    {
        destroyExplosion = Resources.Load("Prefabs/PlayX4/Explosion", typeof(GameObject)) as GameObject;
        hitExplosion = Resources.Load("Prefabs/PlayX4/EnemyHit", typeof(GameObject)) as GameObject;
    }

    void CheckOverGameZone()
    {
        if(this.transform.position.x < -12f)
        {
            Destroy(this.gameObject);
        }
    }

    void DefaultEnemyDestroyEffect()
    {
        //객체의 중심에 폭발 이펙트 생성
        //ParticleManager.Instance.PlayEffect("EnemyExplosion", this.transform.position); //X4
        GameObject instance = Instantiate(destroyExplosion, transform.position, transform.rotation);
        float randZ = Random.Range(0f, 179f);
        instance.transform.Rotate(0, 0, randZ);

        AudioManager.Instance.PlaySFX("EnemyExplosion");

    }

    public void ChangeState(EnemyState state)
    {
        enemyState.ChangeState(state);
    }

    virtual protected void OnTriggerEnter2D(Collider2D collision)
    {
        const float damageValue = 1f;
        if(collision.transform.name == "Player" && 
            collision.TryGetComponent(out Character character))
        {
            
            if(character != null && characterInteraction != null &&
                character.GetIsInvincibility() == false)
            {
                characterInteraction.SendDamage(ref character, damageValue);
                Debug.Log(character.GetHp());
            }
        }

        if (collision.transform.tag == "Player" && (collision.GetComponent<Projectile>()) )
        {
            float randPos = Random.Range(-0.15f, 0.15f);
            Vector2 spawnHitEffectPosition = new Vector2(collision.transform.position.x + Mathf.Abs(randPos), transform.position.y + randPos);
            //ParticleManager.Instance.PlayEffect("EnemyHit", collision.ClosestPoint(spawnHitEffectPosition)); //X4
            GameObject instance = Instantiate(hitExplosion, transform.position, transform.rotation);

            float randZ = Random.Range(0f, 360f);
            instance.transform.position = collision.ClosestPoint(spawnHitEffectPosition);
            instance.transform.Rotate(0, 0, randZ);
            Destroy(instance, 0.3f);
            DemagedSound();
        }
        /*if (collision.transform.tag == "Player" && collision.GetComponent<PlayerEffect>())
        {
            float randPos = Random.Range(-0.1f, 1f);
            Vector2 spawnHitEffectPosition = new Vector2(collision.transform.position.x, transform.position.y + randPos);
            ParticleManager.Instance.PlayEffect("EnemyHit", collision.transform.position);
            DemagedSound();
        }*/
        
    }

    
    public void DemagedSound()
    {
        if (Time.time - lastHitSoundTime < hitSoundCooldown) return;

        AudioManager.Instance.PlaySFX("EnemyHit");
        lastHitSoundTime = Time.time;
    }
    /// <summary>
    /// getset
    /// </summary>
    /// <param name="pos"></param>
    public void SetTargetPosition(Vector2 pos)
    {
        isSelfPosition = false;
        currentTargetPos = pos;
    }

    public bool GetIsBoss()
    {
        return isBoss;
    }

    public void SetIsBoss(bool state)
    {
        isBoss = state;
    }

    public virtual IEnumerator EnemyAttack()
    {
        Debug.LogWarning(this.name + "의 공격 패턴 미작업");
        yield return null;
    }
}
