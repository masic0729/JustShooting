using UnityEngine;
using UnityEditor.Animations;

public class Projectile : IObject
{
    //GameObject ProjectileEffect; //��Ȯ�� � �͵��� �ִ� �� �ľ��� �ȵǹǷ� ���� �Ͽ���
    [SerializeField]
    protected float damage; //�߻�ü�� �������� �ִ�
    [SerializeField]
    protected float lifeTime; // �߻�ü�� ���� �ð��� ������
    [SerializeField]
    private bool isPoolObject; //�߻�ü�� ��ó�� Ǯ���� ��ä�� Ȯ����

    protected void OnEnable()
    {
        //�ش� ������Ʈ�� ������ ������ Ǯ�� ��ũ��Ʈ ���� �� ������� Ȯ��
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
            CheckPoolObject(); //�ش� ��ü�� �Ҹ��ϴ� ��
        }
    }

    void CheckPoolObject()
    {
        if(isPoolObject)
        {
            GetComponent<PoolProjectile>().pool.Release(this.gameObject); //�� ��ü�� Ǯ���� ��ü��� ������
        }
        else
        {
            Destroy(this.gameObject); //�ƴ϶�� �Ϲ����� ������Ʈ ����
        }
    }

    void ClearObjectResources()
    {
        //�⺻ ���ҽ� ��� ����
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