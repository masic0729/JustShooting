using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//�ӽ÷� ����������
using UnityEngine.UI;

public class Player : Character
{
    public Text text; //�ӽÿ�
    enum CurrentPlayerBullet //�÷��̾��� ���� ����Ÿ�� Ȯ�ο�
    {
        Wind,
        Iced,
        Fire,
        Lightning
    }
    CurrentPlayerBullet currentBullet;

    public PlayerBulletData[] commonBulletDatas; //�Ϲ� �Ѿ� ������
    public Dictionary<string, PlayerBulletData> commonBullets; //��ųʸ��� ������ ��
    float power, maxPower = 100; //��ų�� ����ϱ� ���� ��. �ִ� 100���� ���� �� �ִ�.
    float attackSpeed = 0.5f; //�÷��̾��� ���� �ֱ�.�⺻���� 0.5�̴�.
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Init();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        Attack();
        TestFunctions();
    }

    protected override void Init()
    {
        base.Init();

        //��ųʸ�ȭ�� ���� ���� ������¸� �ʱ�ȭ�Ѵ�
        commonBullets = new Dictionary<string, PlayerBulletData>(); 
        for(int i = 0; i < commonBulletDatas.Length; i++)
        {
            commonBullets[commonBulletDatas[i].weaponName] = commonBulletDatas[i];
        }
        SetCurrentCommonBulletData(CurrentPlayerBullet.Wind); //���� ���� �ʱ�ȭ

    }

    void Attack() //�����ϴ� ��ɵ��� ����
    {
        //�Ϲ� �Ѿ� ����
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            
            //PoolManager.Instance.Pools["TestSkillBullet"].Get();
            ShootCommmonBullet(currentBullet);
        }

        //��ų Ű. ���� �߰� ����
    }


    //����� �׽�Ʈ �ǵ��� Ű�� �Է��Ѵ�.

    void TestFunctions()
    {
        
        if (Input.GetKeyDown(KeyCode.A))
        {

            //PoolManager.Instance.Pools["TestSkillBullet"].Get();
            SetCurrentCommonBulletData(CurrentPlayerBullet.Wind);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {

            //PoolManager.Instance.Pools["TestSkillBullet"].Get();
            SetCurrentCommonBulletData(CurrentPlayerBullet.Iced);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {

            //PoolManager.Instance.Pools["TestSkillBullet"].Get();
            SetCurrentCommonBulletData(CurrentPlayerBullet.Fire);
        }
    }

    /// <summary>
    /// �Ϲ� �Ѿ˰���
    /// </summary>
    /// <param name="currentState">���� �÷��̾ �߻��Ϸ��� �Ϲ� �Ѿ� ����</param>
    void ShootCommmonBullet(CurrentPlayerBullet currentState)
    {
        GameObject instanceCommonBullet = PoolManager.Instance.Pools["PlayerCommonBullet"].Get(); //�ν��Ͻ�ȭ
        SetCommonBulletData(ref instanceCommonBullet); //�߻�ü ���ҽ� ������ �ε�
        instanceCommonBullet.transform.position = shootPositions["CommonBullet"].position; //�߻�ü ��ġ ����

    }


    /// <summary>
    /// �Ѿ��� ���ҽ��� �䱸�ϴ� ��� �����͸� attackStats�� �ҷ��� ����
    /// </summary>
    /// <param name="commonBullet">�Ϲ� �Ѿ� ������Ʈ�� ����Ų��</param>
    void SetCommonBulletData(ref GameObject commonBullet)
    {
        commonBullet.tag = "Player";
        commonBullet.GetComponent<SpriteRenderer>().sprite = attackStats.sprite;
        commonBullet.GetComponent<Animator>().runtimeAnimatorController = attackStats.animCtrl;

        commonBullet.GetComponent<Projectile>().SetDamage(this.attackStats.damage);
        commonBullet.GetComponent<Projectile>().SetMoveSpeed(this.attackStats.moveSpeed);
    }

    void SetCurrentCommonBulletData(CurrentPlayerBullet bulletState)
    {
        currentBullet = bulletState;
        //���� �߻��Ϸ��� �Ѿ� ������ ���� �� �׿� �´� �ɷ�ġ ����
        attackStats.sprite = commonBullets[bulletState.ToString()].sprite;
        attackStats.animCtrl = commonBullets[bulletState.ToString()].animCtrl;
        attackStats.damage = commonBullets[bulletState.ToString()].damage;
        attackStats.moveSpeed = commonBullets[bulletState.ToString()].moveSpeed;

        //���� ���⿡ ���� �ɷ�ġ ���� Ȯ�ο� �ؽ�Ʈ ���
        text.text = "sprite : " + attackStats.sprite + "\nanimCtrl : " + attackStats.animCtrl + "\ndamage : " +
            attackStats.damage + "\nmoveSpeed : " + attackStats.moveSpeed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //���̳� ���� ������ ���� ��쿡 ������ ó��
        if (collision.transform.tag == "Enemy" || collision.transform.tag == "EnemyAttack")
        {
            Character instanceEnemyInfo = collision.GetComponent<Character>();
            
            
        }
    }
}
