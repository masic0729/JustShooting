using UnityEngine;


public class Projectile : IObject
{
    protected GameObject thisGameObject;

    protected Vector3 projectileMoveVector;

    //GameObject ProjectileEffect; //��Ȯ�� � �͵��� �ִ� �� �ľ��� �ȵǹǷ� ���� �Ͽ���
    ObjectInteration projectileInteraction;
    [SerializeField]
    protected float damage; //�߻�ü�� �������� �ִ�
//    [SerializeField]
    protected float lifeTimer = 0, lifeTime = 5; // �߻�ü�� ���� �ð��� ������
    [SerializeField]
    private bool isPoolObject; //�߻�ü�� ��ó�� Ǯ���� ��ä�� Ȯ����
    protected bool isCanMove = true;

    virtual protected void OnEnable()
    {
        //�ش� ������Ʈ�� ������ ������ Ǯ�� ��ũ��Ʈ ���� �� ������� Ȯ��
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
        //����� �̷� ������ ��������, �������� ó���� �ʿ��� ��� ���� ó���� �и��ؾ� �� �� ����
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
            CheckPoolObject(); //�߻�ü�� ������ �Ҹ��ϴ� ��

        }
    }
    

    void CheckPoolObject()
    {
        if(isPoolObject)
        {
            ClearObjectResources();                                       //�⺻ ���ҽ� ��� ����
            GetComponent<PoolProjectile>().pool.Release(this.gameObject); //�� ��ü�� Ǯ���� ��ü��� ������
        }
        else
        {
            Destroy(this.gameObject);                                     //�ƴ϶�� �Ϲ����� ������Ʈ ����
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