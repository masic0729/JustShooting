using UnityEngine;


public class Projectile : IObject
{
    protected GameObject thisGameObject;

    protected Vector3 projectileMoveVector;

    //GameObject ProjectileEffect; //정확히 어떤 것들이 있는 지 파악이 안되므로 선언만 하였음
    ObjectInteration projectileInteraction;
    [SerializeField]
    protected float damage; //발사체는 데미지가 있다
//    [SerializeField]
    protected float lifeTimer = 0, lifeTime = 5; // 발사체의 지속 시간을 정의함
    [SerializeField]
    private bool isPoolObject; //발사체의 출처가 풀링된 객채를 확인함
    protected bool isCanMove = true;

    virtual protected void OnEnable()
    {
        //해당 오브젝트가 등장할 때마다 풀링 스크립트 존재 시 사실임을 확인
        if (this.gameObject.GetComponent<PoolProjectile>())
            isPoolObject = true;
        lifeTimer = 0;
    }
    

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        ProjectileMovement();
        AutoDestroy();
    }

    protected override void Init()
    {
        projectileInteraction = new ObjectInteration();

        maxMoveX = 10f;
        maxMoveY = 5.5f;
        thisGameObject = this.gameObject;
        projectileMoveVector = Vector3.up;
    }

    void ProjectileMovement()
    {
        
        if (isCanMove == true)
        {
            ObjectMove(projectileMoveVector);
        }
    }

    void AutoDestroy()
    {
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifeTime || Mathf.Abs(transform.position.x) > maxMoveX || Mathf.Abs(transform.position.y) > maxMoveY)
        {
            CheckPoolObject();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //현재는 이런 식으로 묶었지만, 예외적인 처리가 필요한 경우 조건 처리를 분리해야 할 수 있음
        if(this.transform.tag == "Player" && collision.transform.tag == "Enemy" ||
            this.transform.tag == "Enemy" && collision.transform.tag == "Player")
        {
            Character instanceHitCharacter = collision.GetComponent<Character>();
            if(instanceHitCharacter != null && instanceHitCharacter.GetIsInvincibility() == false)
            {
                if(this.gameObject.GetComponent<PlayerIcedBullet>() == true &&
                    collision.GetComponent<Enemy>().GetIsBoss() == false)
                {
                    SetDamage(999f);
                    projectileInteraction.SendDamage(ref instanceHitCharacter, this.GetDamage());
                }
                else
                {
                    projectileInteraction.SendDamage(ref instanceHitCharacter, this.GetDamage());
                }
                if (collision.transform.tag == "Player" && collision.transform.name == "Player")
                {
                    Debug.Log(collision.GetComponent<Player>().GetHp());
                }
            }
            if (collision.GetComponent<Character>() == true)
            {
                CheckPoolObject(); //발사체인 본인이 소멸하는 것
            }


        }
    }
    

    void CheckPoolObject()
    {
        if(isPoolObject)
        {
            ClearObjectResources();                                       //기본 리소스 모두 삭제
            GetComponent<PoolProjectile>().pool.Release(this.gameObject); //이 객체가 풀링된 객체라면 릴리즈
        }
        else
        {
            Destroy(this.gameObject);                                     //아니라면 일반적인 오브젝트 삭제
        }
    }

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