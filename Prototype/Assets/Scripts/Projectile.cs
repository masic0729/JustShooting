using UnityEngine;
using UnityEditor.Animations;
using UnityEngine.TextCore.Text;

public class Projectile : IObject
{
    //GameObject ProjectileEffect; //정확히 어떤 것들이 있는 지 파악이 안되므로 선언만 하였음

    [SerializeField]
    protected float damage; //발사체는 데미지가 있다
    [SerializeField]
    protected float lifeTime = 10f; // 발사체의 지속 시간을 정의함
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
    protected override void Update()
    {
        base.Update();
    }

    protected override void Init()
    {
        maxMoveX = 10.5f;
        maxMoveY = 5.5f;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //현재는 이런 식으로 묶었지만, 예외적인 처리가 필요한 경우 조건 처리를 분리해야 할 수 있음
        if(this.transform.tag == "Player" && collision.transform.tag == "Enemy" ||
            this.transform.tag == "Enemy" && collision.transform.tag == "Player")
        {
            Character instanceHitCharacter = collision.GetComponent<Character>();
            SendDamage(ref instanceHitCharacter);
            ClearObjectResources();
            CheckPoolObject(); //발사체인 본인이 소멸하는 것
        }
    }
    /// <summary>
    /// 적중한 상대 편의 데이터를 불러와 체력을 감소하는 행위. 만약 대상의 체력이 0이하가 되면 죽음 액션이 실행된다
    /// </summary>
    /// <param name="character"> 발사체 기준 적으로 간주되는 객체</param>
    void SendDamage(ref Character character)
    {
        if (character.GetHp() - damage <= 0)
        {
            //체력이 0이므로 죽음처리
            character.SetHp(0);
            character.OnCharacterDeath?.Invoke();
        }
        else
        {
            //죽지 않으므로 체력감소 처리
            character.SetHp(character.GetHp() - damage);
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