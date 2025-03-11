using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class Character : IObject
{
    public AttackStats attackStats;
    [SerializeField]
    private Transform[] shootTransforms;//ĳ������ �߻���ġ

    public Dictionary<string, Transform> shootPositions; //�߻���ġ�� ���� ������ ��ųʸ�
    [SerializeField]
    protected float hp, maxHp; //���� ü�� �� �ִ� ü��.
    [SerializeField]
    protected float attackMultiplier = 1; // ���ظ� �� �� �ִ� ���ݷ� �� ���� ���

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

    protected override void Init()
    {
        //void
        shootPositions = new Dictionary<string, Transform>();
        
        for(int i = 0; i < shootTransforms.Length; i++)
        {
            shootPositions[shootTransforms[i].name] = shootTransforms[i]; //��ųʸ� ���� ĳ������ �߻���ġ�� ����ϴ� ������Ʈ�̸��� ����Ѵ�.
            Debug.Log(shootPositions[shootTransforms[i].name]);
        }

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
