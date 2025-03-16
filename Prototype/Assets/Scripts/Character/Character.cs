using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class Character : IObject
{
    public AttackStats attackStats; // �����ϴ� ��ü�� �����ϴ� ������ ����
    [SerializeField]
    private Transform[] shootTransforms;//ĳ������ �߻���ġ

    public Action OnCharacterDeath; //ĳ���Ͱ� ���� �� �߻��ϴ� �̺�Ʈ. �ʿ信 ���� ���� Ŭ������ ������ �� ����
    public Dictionary<string, Transform> shootPositions; //�߻���ġ�� ���� ������ ��ųʸ�
    [SerializeField]
    protected float hp, maxHp; //���� ü�� �� �ִ� ü��.

    //todo �� ������ ���� ĳ���͵��� �̵� �� �߻�ü ���� ������ ��(������ ����)
    [Header("ĳ������ ���� �ý��� ���� ����")]
    [SerializeField]
    protected float attackMultiplier = 1; // ���ظ� �� �� �ִ� ���ݷ� �� ���� ���. ���� ���� ���� ���ط��� Ŀ����.
    [SerializeField]
    protected float attackDelayMultify = 1; //�߻� �ֱ� ���. ���� ���� ���� �ʴ� ���� Ƚ���� ��������.
    [SerializeField]
    protected float projectileMoveSpeedMultify = 1; //�߻�ü �ӵ� ���. ���� Ŭ ���� �߻�ü�� �ӵ��� ��������.
    [SerializeField]
    protected float characterGetDamageMultify = 1; //ĳ���Ͱ� ���ظ� �޴� ����. ���� ���� �޴� ���ذ� ����Ѵ�.

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
        //void
        attackStats = gameObject.AddComponent<AttackStats>();
        OnCharacterDeath += DestroyCharacter;
        shootPositions = new Dictionary<string, Transform>();
        
        for(int i = 0; i < shootTransforms.Length; i++)
        {
            shootPositions[shootTransforms[i].name] = shootTransforms[i]; //��ųʸ� ���� ĳ������ �߻���ġ�� ����ϴ� ������Ʈ�̸��� ����Ѵ�.
            Debug.Log(shootPositions[shootTransforms[i].name]);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //����
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


    public void SetAttackMultiplier(float value)
    {
        attackMultiplier = value;
    }

    public float GetAttackMultiplier()
    {
        return attackMultiplier;
    }
}
