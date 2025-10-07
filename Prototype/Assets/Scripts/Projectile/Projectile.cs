using UnityEngine;

public class Projectile : IObject
{
    protected GameObject thisGameObject; // ���� �߻�ü ������Ʈ ����

    protected Vector3 projectileMoveVector; // �߻� ���� ����

    [SerializeField] protected GameObject ProjectileHitEffect; //��Ȯ�� � �͵��� �ִ� �� �ľ��� �ȵǹǷ� ���� �Ͽ���
    ObjectInteraction projectileInteraction; // ���� ���� Ŭ����

    [SerializeField]
    protected float damage; //�߻�ü�� �������� �ִ�

    //    [SerializeField]
    protected float lifeTimer = 0, lifeTime = 5; // �߻�ü�� ���� �ð��� ������

    [SerializeField]
    private bool isPoolObject; //�߻�ü�� ��ó�� Ǯ���� ��ä�� Ȯ����
    protected string hitSoundName = null;

    protected bool isCanMove = true; // �̵� ���� ����

    // ������Ʈ�� Ȱ��ȭ�� �� ����� (Ǯ�� ����)
    virtual protected void OnEnable()
    {
        //�ش� ������Ʈ�� ������ ������ Ǯ�� ��ũ��Ʈ ���� �� ������� Ȯ��
        if (this.gameObject.GetComponent<PoolProjectile>())
            isPoolObject = true;
        lifeTimer = 0; // ���� �ð� �ʱ�ȭ
    }

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        ProjectileMovement(); // �̵� ó��
        AutoDestroy(); // �ڵ� ���� ó��
    }

    protected override void Init()
    {
        projectileInteraction = new ObjectInteraction(); // ���� ó�� Ŭ���� �ν��Ͻ� ����

        maxMoveX = 10f; // �̵� ���� ����
        maxMoveY = 5.5f;
        thisGameObject = this.gameObject; // �ڱ� �ڽ� ����
        projectileMoveVector = Vector3.up; // �⺻ �̵� ���� ����
    }

    // �̵� ���Ϳ� ���� ������Ʈ �̵�
    void ProjectileMovement()
    {
        if (isCanMove == true)
        {
            ObjectMove(projectileMoveVector);
        }
    }

    // ���� �ð� �Ǵ� ȭ�� ��� �������� ���� ó��
    void AutoDestroy()
    {
        lifeTimer += Time.deltaTime;

        if (lifeTimer >= lifeTime || Mathf.Abs(transform.position.x) > maxMoveX || Mathf.Abs(transform.position.y) > maxMoveY)
        {
            CheckPoolObject(); // ���� or Ǯ�� ��ȯ
        }
    }

    // �浹 ó��
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //����� �̷� ������ ��������, �������� ó���� �ʿ��� ��� ���� ó���� �и��ؾ� �� �� ����
        if (this.transform.tag == "Player" && collision.transform.tag == "Enemy" ||
            this.transform.tag == "Enemy" && collision.transform.tag == "Player")
        {
            Character instanceHitCharacter = collision.GetComponent<Character>();
            instanceHitCharacter.SetHitSoundName(hitSoundName);
            // ������ �ƴ� ����� ��� ���� ����
            if (instanceHitCharacter != null && instanceHitCharacter.GetIsInvincibility() == false)
            {
                // Ư�� ����(�÷��̾��� ���� źȯ + �Ϲ� ��)�� ��� Ư�� ������ ����
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
                    //�ش� �κп� ����Ʈ �߻�
                    float randPos = Random.Range(-0.15f, 0.15f);
                    Vector2 spawnHitEffectPosition = new Vector2(collision.transform.position.x + Mathf.Abs(randPos), transform.position.y + randPos);
                    ParticleManager.Instance.PlayEffect(ProjectileHitEffect.name, collision.ClosestPoint(spawnHitEffectPosition));
                }
            }

            // �浹 ����� ĳ������ ��� �߻�ü ����
            if (collision.GetComponent<Character>() == true)
            {
                CheckPoolObject(); //�߻�ü�� ������ �Ҹ��ϴ� ��
            }
        }
    }

    // ������Ʈ�� Ǯ���� ��ü���� Ȯ���ϰ� ���� �Ǵ� ��ȯ ó��
    void CheckPoolObject()
    {
        if (isPoolObject)
        {
            ClearObjectResources();                                       //�⺻ ���ҽ� ��� ����
            GetComponent<PoolProjectile>().pool.Release(this.gameObject); //�� ��ü�� Ǯ���� ��ü��� ������
        }
        else
        {
            Destroy(this.gameObject);                                     //�ƴ϶�� �Ϲ����� ������Ʈ ����
        }
    }

    // ������Ʈ�� �Ҹ�� �� ���ҽ��� �ʱ�ȭ��
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
