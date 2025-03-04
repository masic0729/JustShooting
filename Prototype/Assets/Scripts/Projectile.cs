using UnityEngine;
using UnityEditor.Animations;

public class Projectile : IObject
{
    //각각 스프라이트와 애니메이션은 둘 중 하나만 있으면 된다.
    public Sprite[] projectileSprites; //각 발사체의 이미지
    public AnimatorController[] projectileAnimations; //각 발사체의 애니메이션

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
        Init();
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