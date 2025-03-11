using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class Character : IObject
{
    public AttackStats attackStats;
    [SerializeField]
    private Transform[] shootTransforms;//캐릭터의 발사위치

    public Dictionary<string, Transform> shootPositions; //발사위치를 최종 저장할 딕셔너리
    [SerializeField]
    protected float hp, maxHp; //현재 체력 및 최대 체력.
    [SerializeField]
    protected float attackMultiplier = 1; // 피해를 줄 수 있는 공격력 및 피해 계수

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
            shootPositions[shootTransforms[i].name] = shootTransforms[i]; //딕셔너리 명은 캐릭터의 발사위치를 담당하는 오브젝트이름을 사용한다.
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
