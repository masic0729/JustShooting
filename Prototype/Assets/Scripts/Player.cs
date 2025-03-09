using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
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
    //float 
    
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
        currentBullet = CurrentPlayerBullet.Wind;
        commonBullets = new Dictionary<string, PlayerBulletData>();
        for(int i = 0; i < commonBulletDatas.Length; i++)
        {
            //��ųʸ�ȭ
            commonBullets[commonBulletDatas[i].weaponName] = commonBulletDatas[i];
        }
    }

    void Attack() //�����ϴ� ��ɵ��� ����
    {
        //�Ϲ� �Ѿ� ����
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            
            //PoolManager.Instance.Pools["TestSkillBullet"].Get();
            ShootCommmonBullet(currentBullet);
        }

        //��ų Ű
    }

    void TestFunctions()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {

            //PoolManager.Instance.Pools["TestSkillBullet"].Get();
            currentBullet = CurrentPlayerBullet.Wind;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {

            //PoolManager.Instance.Pools["TestSkillBullet"].Get();
            currentBullet = CurrentPlayerBullet.Iced;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {

            //PoolManager.Instance.Pools["TestSkillBullet"].Get();
            currentBullet = CurrentPlayerBullet.Fire;
        }
    }

    void ShootCommmonBullet(CurrentPlayerBullet currentState) //�Ϲ� �Ѿ˰���
    {
        GameObject instanceCommonBullet = PoolManager.Instance.Pools["PlayerCommonBullet"].Get(); //�ν��Ͻ�ȭ
        SetCommonBulletData(ref instanceCommonBullet); //�߻�ü ���ҽ� ������ �ε�
        instanceCommonBullet.transform.position = shootPositions["CommonBullet"].position; //�߻�ü ��ġ ����

    }

    void SetCommonBulletData(ref GameObject commonBullet) //�Ѿ��� ���ҽ��� ���� �����͸� �ҷ��� �߻��Ϸ��� �Ѿ˿� ����
    {
        commonBullet.GetComponent<SpriteRenderer>().sprite = commonBullets[currentBullet.ToString()].sprite;
        commonBullet.GetComponent<Animator>().runtimeAnimatorController = commonBullets[currentBullet.ToString()].animCtrl;
        commonBullet.tag = "PlayerAttack";
    }
}
