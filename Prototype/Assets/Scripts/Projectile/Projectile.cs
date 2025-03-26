using UnityEngine;


public class Projectile : IObject
{
    //GameObject ProjectileEffect; //��Ȯ�� � �͵��� �ִ� �� �ľ��� �ȵǹǷ� ���� �Ͽ���
    ObjectInteration projectileInteraction;
    [SerializeField]
    protected float damage; //�߻�ü�� �������� �ִ�
    [SerializeField]
    protected float lifeTimer = 0, lifeTime; // �߻�ü�� ���� �ð��� ������
    [SerializeField]
    private bool isPoolObject; //�߻�ü�� ��ó�� Ǯ���� ��ä�� Ȯ����
    protected bool isCanMove = true;

    protected void OnEnable()
    {
        //�ش� ������Ʈ�� ������ ������ Ǯ�� ��ũ��Ʈ ���� �� ������� Ȯ��
        if (this.gameObject.GetComponent<PoolProjectile>())
            isPoolObject = true;
        lifeTimer = 0;

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
        lifeTimer += Time.deltaTime;
        if(lifeTimer >= lifeTime)
        {
            CheckPoolObject();
        }
        if(isCanMove == true)
        {
            ObjectMove(Vector3.up);
        }
    }

    protected override void Init()
    {
        maxMoveX = 10.5f;
        maxMoveY = 5.5f;
        projectileInteraction = new ObjectInteration();
        lifeTime = 5;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //����� �̷� ������ ��������, �������� ó���� �ʿ��� ��� ���� ó���� �и��ؾ� �� �� ����
        if(this.transform.tag == "Player" && collision.transform.tag == "Enemy" ||
            this.transform.tag == "Enemy" && collision.transform.tag == "Player")
        {
            Character instanceHitCharacter = collision.GetComponent<Character>();
            if(instanceHitCharacter != null)
            {
                projectileInteraction.SendDamage(ref instanceHitCharacter, this.GetDamage());
                CheckPoolObject(); //�߻�ü�� ������ �Ҹ��ϴ� ��
            }
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