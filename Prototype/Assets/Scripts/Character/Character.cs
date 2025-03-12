using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class Character : IObject
{
    public AttackStats attackStats; // 공격하는 객체가 소지하는 데이터 모음
    [SerializeField]
    private Transform[] shootTransforms;//캐릭터의 발사위치

    public Action OnCharacterDeath; //캐릭터가 죽을 때 발생하는 이벤트. 필요에 따라서 상위 클래스에 정의할 수 있음
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

    /// <summary>
    /// 해당 클래스부터 모든 하위 클래스들이 개개인이 선언하는 이벤트들을 해당 함수로 액션 행위들을 편집함
    /// </summary>
    protected override void Init()
    {
        //void
        attackStats = gameObject.AddComponent<AttackStats>();
        OnCharacterDeath += DestroyCharacter;
        shootPositions = new Dictionary<string, Transform>();
        
        for(int i = 0; i < shootTransforms.Length; i++)
        {
            shootPositions[shootTransforms[i].name] = shootTransforms[i]; //딕셔너리 명은 캐릭터의 발사위치를 담당하는 오브젝트이름을 사용한다.
            Debug.Log(shootPositions[shootTransforms[i].name]);
        }

    }


    



    private void OnTriggerEnter2D(Collider2D collision)
    {
        //보류
    }

    /// <summary>
    /// 현재는 캐릭터 객체들은 삭제처리하지만, 상황에 따라 일부 캐릭터는 풀링과 비슷한 효과로 처리할 수 있음
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
