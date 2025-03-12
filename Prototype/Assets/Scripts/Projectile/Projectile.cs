using UnityEngine;
using UnityEditor.Animations;
using UnityEngine.TextCore.Text;

public class Projectile : IObject
{
    //GameObject ProjectileEffect; //��Ȯ�� � �͵��� �ִ� �� �ľ��� �ȵǹǷ� ���� �Ͽ���

    [SerializeField]
    protected float damage; //�߻�ü�� �������� �ִ�
    [SerializeField]
    protected float lifeTime = 10f; // �߻�ü�� ���� �ð��� ������
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
        //����� �̷� ������ ��������, �������� ó���� �ʿ��� ��� ���� ó���� �и��ؾ� �� �� ����
        if(this.transform.tag == "Player" && collision.transform.tag == "Enemy" ||
            this.transform.tag == "Enemy" && collision.transform.tag == "Player")
        {
            Character instanceHitCharacter = collision.GetComponent<Character>();
            SendDamage(ref instanceHitCharacter);
            ClearObjectResources();
            CheckPoolObject(); //�߻�ü�� ������ �Ҹ��ϴ� ��
        }
    }
    /// <summary>
    /// ������ ��� ���� �����͸� �ҷ��� ü���� �����ϴ� ����. ���� ����� ü���� 0���ϰ� �Ǹ� ���� �׼��� ����ȴ�
    /// </summary>
    /// <param name="character"> �߻�ü ���� ������ ���ֵǴ� ��ü</param>
    void SendDamage(ref Character character)
    {
        if (character.GetHp() - damage <= 0)
        {
            //ü���� 0�̹Ƿ� ����ó��
            character.SetHp(0);
            character.OnCharacterDeath?.Invoke();
        }
        else
        {
            //���� �����Ƿ� ü�°��� ó��
            character.SetHp(character.GetHp() - damage);
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