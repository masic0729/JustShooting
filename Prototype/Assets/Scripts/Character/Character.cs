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

    //todo 각 배율에 따른 캐릭터들의 이동 및 발사체 적용 구현할 것(급하지 않음)
    [Header("캐릭터의 전투 시스템 공식 배율")]
    [SerializeField]
    protected float attackMultiplier = 1; // 피해를 줄 수 있는 공격력 및 피해 계수. 값이 오를 수록 피해량이 커진다.
    [SerializeField]
    protected float attackDelayMultify = 1; //발사 주기 계수. 값이 오를 수록 초당 공격 횟수가 빨라진다.
    [SerializeField]
    protected float projectileMoveSpeedMultify = 1; //발사체 속도 계수. 값이 클 수록 발사체의 속도가 빨라진다.
    [SerializeField]
    protected float characterGetDamageMultify = 1; //캐릭터가 피해를 받는 배율. 높을 수록 받는 피해가 상승한다.

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
