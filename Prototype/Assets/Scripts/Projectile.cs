using UnityEngine;
using UnityEditor.Animations;

public class Projectile : IObject
{
    //GameObject ProjectileEffect; //정확히 어떤 것들이 있는 지 파악이 안되므로 선언만 하였음
    [SerializeField]
    protected float damage; //발사체는 데미지가 있다
    [SerializeField]
    protected float lifeTime; // 발사체의 지속 시간을 정의함
    [SerializeField]
    private bool isPoolObject; //발사체의 출처가 풀링된 객채를 확인함

    protected void OnEnable()
    {
        //해당 오브젝트가 등장할 때마다 풀링 스크립트 존재 시 사실임을 확인
        if (this.gameObject.GetComponent<PoolProjectile>())
            isPoolObject = true;

    }
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    protected override void Init()
    {
        maxMoveX = 10.5f;
        maxMoveY = 5.5f;
        Debug.Log("Projectile");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.transform.tag == "PlayerAttack" && collision.transform.tag == "Enemy" ||
            this.transform.tag == "EnemyAttack" && collision.transform.tag == "Player")
        {
            ClearObjectResources();
            CheckPoolObject(); //해당 객체가 소멸하는 것
        }
    }

    void CheckPoolObject()
    {
        if(isPoolObject)
        {
            GetComponent<PoolProjectile>().pool.Release(this.gameObject); //이 객체가 풀링된 객체라면 릴리즈
        }
        else
        {
            Destroy(this.gameObject); //아니라면 일반적인 오브젝트 삭제
        }
    }

    void ClearObjectResources()
    {
        //기본 리소스 모두 삭제
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