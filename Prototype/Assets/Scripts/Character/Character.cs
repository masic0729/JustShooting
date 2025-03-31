using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;


public class Character : IObject
{
    public AttackStats attackStats; // �����ϴ� ��ü�� �����ϴ� ������ ����
    [SerializeField]
    private Transform[] shootTransforms;//ĳ������ �߻���ġ
    protected AttackManagement attackManage;

    public Action OnCharacterDeath; //ĳ���Ͱ� ���� �� �߻��ϴ� �̺�Ʈ. �ʿ信 ���� ���� Ŭ������ ������ �� ����
    public Dictionary<string, Transform> shootTransform; //�߻���ġ�� ���� ������ ��ųʸ�
    [SerializeField]
    protected float hp, maxHp; //���� ü�� �� �ִ� ü��.
    protected float shield; //��ȣ��, ��ȣ�� ���� �� ü�� ��� ����
    
    [Header("ĳ������ ���� �ý��� ���� ����")]
    [SerializeField]
    protected float attackMultiplier = 1; // ���ظ� �� �� �ִ� ���ݷ� �� ���� ���. ���� ���� ���� ���ط��� Ŀ����.
    [SerializeField]
    protected float attackDelay; //�߻� �ֱ�. ���� ���� ���� �ʴ� ���� Ƚ���� ��������.
    [SerializeField]
    protected float projectileMoveSpeedMultify = 1; //�߻�ü �ӵ� ���. ���� Ŭ ���� �߻�ü�� �ӵ��� ��������.
    [SerializeField]
    protected float characterGetDamageMultify = 1; //ĳ���Ͱ� ���ظ� �޴� ����. ���� ���� �޴� ���ذ� ����Ѵ�.

    private bool isInvincibility; //���� ����


    // Start is called before the first frame update
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
        attackStats = gameObject.AddComponent<AttackStats>();
        attackManage = new AttackManagement();
        //������ �ܼ��� ü�� ���Ҹ� ������
        OnCharacterDeath += DestroyCharacter;
        shootTransform = new Dictionary<string, Transform>();
        
        for(int i = 0; i < shootTransforms.Length; i++)
        {
            shootTransform[shootTransforms[i].name] = shootTransforms[i]; //��ųʸ� ���� ĳ������ �߻���ġ�� ����ϴ� ������Ʈ�̸��� ����Ѵ�.
        }

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

    public void TransIsInvincibilityFalse()
    {
        isInvincibility = false;
    }

}
