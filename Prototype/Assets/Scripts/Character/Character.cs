using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;


public class Character : IObject
{
    protected Animator anim;
    public GameObject hitExplosion;
    public GameObject destroyExplosion;

    public AttackStats attackStats;                                     //�����ϴ� ��ü�� �����ϴ� ������ ����
    public ProjectileManagement projectileManage;
    private Transform[] shootTransforms;                                //ĳ������ �߻���ġ
    public AttackManagement attackManage;                            //���� ��Ŀ� ���� ����

    public Action OnCharacterDeath;                                     //ĳ���Ͱ� ���� �� �߻��ϴ� �̺�Ʈ. �ʿ信 ���� ���� Ŭ������ ������ �� ����
    public Action OnCharacterDamaged;                                   //ĳ���Ͱ� ���ظ� ���� �� �߻��ϴ� �̺�Ʈ. �������� ������, �Ҹ���, ����Ʈ, ���� ȹ�� ��� �پ��� �׼��� ����
    public Dictionary<string, Transform> shootTransform;                //�߻���ġ�� ���� ������ ��ųʸ�

    [SerializeField]
    protected float hp, maxHp;                                          //���� ü�� �� �ִ� ü��.
    protected float shield;                                             //��ȣ��, ��ȣ�� ���� �� ü�� ��� ����

    
    protected ObjectInteration characterInteraction;                    //���ʹ� �⺻������ �������� 1�����̴�

    [Header("ĳ������ ���� �ý��� ���� ����")]
    [SerializeField]
    protected float attackMultiplier = 1;                               //���ظ� �� �� �ִ� ���ݷ� �� ���� ���. ���� ���� ���� ���ط��� Ŀ����.
    [SerializeField]
    protected float attackDelay;                                        //�߻� �ֱ�. ���� ���� ���� �ʴ� ���� Ƚ���� ��������.
    [SerializeField]
    protected float projectileMoveSpeedMultify = 1;                     //�߻�ü �ӵ� ���. ���� Ŭ ���� �߻�ü�� �ӵ��� ��������.
    [SerializeField]
    protected float characterGetDamageMultify = 1;                      //ĳ���Ͱ� ���ظ� �޴� ����. ���� ���� �޴� ���ذ� ����Ѵ�.
    protected float invincibilityTime = 0f;                             //���� �ð�
    protected bool  isInvincibility;                                    //���� ����

    

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// �ش� Ŭ�������� ��� ���� Ŭ�������� �������� �����ϴ� �̺�Ʈ���� �ش� �Լ��� �׼� �������� ������
    /// </summary>
    protected override void Init()
    {
        anim = GetComponent<Animator>();
        attackStats = gameObject.AddComponent<AttackStats>();
        attackManage = new AttackManagement();
        projectileManage = new ProjectileManagement();
        characterInteraction = new ObjectInteration();

        //������ �ܼ��� ü�� ���Ҹ� ������
        OnCharacterDeath += DestroyCharacter;
        shootTransform = new Dictionary<string, Transform>();

        // shootTransforms �迭�� �ڽ� ������Ʈ�� �ʱ�ȭ
        shootTransforms = GetComponentsInChildren<Transform>(); // �θ� ������Ʈ�� ��� �ڽ� Transform�� ������


        foreach (Transform shootTransformItem in shootTransforms)
        {
            if (shootTransformItem != this.transform) // �θ� ������Ʈ�� ����
            {
                shootTransform[shootTransformItem.gameObject.name] = shootTransformItem;
            }
        }
        /*for (int i = 0; i < shootTransforms.Length; i++)
        {
            shootTransform[shootTransforms[i].gameObject.name] = shootTransforms[i]; //��ųʸ� ���� ĳ������ �߻���ġ�� ����ϴ� ������Ʈ�̸��� ����Ѵ�.
        }*/

    }

    public void OnDamage()
    {
        SetIsInvincibility(true);
        Invoke("TransIsInvincibilityFalse", invincibilityTime);
        OnCharacterDamaged?.Invoke();
    }

    /// <summary>
    /// ����� ĳ���� ��ü���� ����ó��������, ��Ȳ�� ���� �Ϻ� ĳ���ʹ� Ǯ���� ����� ȿ���� ó���� �� ����
    /// </summary>
    void DestroyCharacter()
    {
        Destroy(this.gameObject);
    }

    //getset
    public void SetHp(float value)
    {
        hp = value;
    }

    public float GetHp()
    {
        return hp;
    }

    public void SetMaxHp(float value)
    {
        maxHp = value;
    }

    public float GetMaxHp()
    {
        return maxHp;
    }

    public void SetShield(float value)
    {
        shield = value;
    }

    public float GetShield()
    {
        return shield;
    }


    public void SetAttackMultiplier(float value)
    {
        attackMultiplier = value;
    }

    public float GetAttackMultiplier()
    {
        return attackMultiplier;
    }

    public void SetIsInvincibility(bool state)
    {
        isInvincibility = state;
    }

    public bool GetIsInvincibility()
    {
        return isInvincibility;
    }

    public float GetInvincibilityTime()
    {
        return invincibilityTime;
    }

    public void TransIsInvincibilityFalse()
    {
        isInvincibility = false;
    }

}
