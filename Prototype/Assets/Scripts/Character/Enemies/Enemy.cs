using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public StateMachine enemyState;                                     //FSM ��ũ��Ʈ
    public int stateIndex = 0;
    public Vector2 arriveVector;                                               //��ü�� �����Ϸ��� ��ġ��

    //public StateMachine stateMachine;                                   

    public GameObject[] enemyProjectiles;                               //�ν����Ϳ� ��ϵ� �߻�ü ����
    public Dictionary<string, GameObject> enemyProjectile;           //�߻�ü ������ ��ųʸ�ȭ
    protected Vector2 currentTargetPos;

    [Header("Enemy�� ���� ������")]
    public EnemyAttackData attackData;

    public ObjectMovement movement;
    
    protected GameObject thisGameObject;
    protected float targetMoveSpeed;
    protected float distanceNeedValue = 1f;

    private float hitSoundCooldown = 0.05f;
    private float lastHitSoundTime = -1f;

    [SerializeField]
    bool isBoss = false; //���� ���� Ȯ��. �⺻���� ����
    protected bool isSelfPosition = true;

    protected virtual void Awake()
    {
        //stateMachine = new StateMachine();
        //Debug.Log("�ϴ� ������ �Ǵµ�, ����� �ȵ� �� �ִ�?");
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
        OnCharacterDeath += DefaultEnemyDestroyEffect;                                      //��� �� �⺻���� ����Ʈ
        movement = new ObjectMovement();
        thisGameObject = this.gameObject;
        enemyProjectile = new Dictionary<string, GameObject>();
        

        if (enemyProjectiles != null)
        {
            for (int i = 0; i < enemyProjectiles.Length; i++)
            {
                enemyProjectile[enemyProjectiles[i].name] = enemyProjectiles[i];
            }
        }
        arriveVector = new Vector2(3f, 0);
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
        //��ü�� �߽ɿ� ���� ����Ʈ ����
        ParticleManager.Instance.PlayEffect("EnemyExplosion", this.transform.position);
        AudioManager.Instance.PlaySFX("EnemyExplosion");

    }

    public void ChangeState(EnemyState state)
    {
        enemyState.ChangeState(state);
    }

    virtual protected void OnTriggerEnter2D(Collider2D collision)
    {
        const float damageValue = 1f;
        if(collision.transform.name == "Player" && collision.TryGetComponent(out Character character))
        {
            
            if(character != null && characterInteraction != null)
            {
                characterInteraction.SendDamage(ref character, damageValue);
                Debug.Log(character.GetHp());
            }
        }

        if (collision.transform.tag == "Player" && collision.GetComponent<Projectile>())
        {
            float randY = Random.Range(-0.1f, 1f);
            Vector2 spawnHitEffectPosition = new Vector2(collision.transform.position.x, transform.position.y + randY);
            ParticleManager.Instance.PlayEffect("EnemyHit", collision.ClosestPoint(spawnHitEffectPosition));
            DemagedSound();
        }
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

    
}
