using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 해당 스크립트를 기준으로 캐릭터 객체들은 능력치를 받는다
/// p_damageFromCard, p_skillDamageMultify, p_attackSpeedMultify, p_projectileSizeMultify, p_moveSpeedTransValue
/// randomAddDamage,
/// </summary>
public class StatManager : MonoBehaviour
{
    public static StatManager instance;

    
    [Header("Player")]
    public Player playerData;

    [Header("PlayerStatData")]
    public float p_damageFromCard;                         
    public float p_skillDamageMultify;

    [Tooltip("공격속도 배율")]
    public float p_attackSpeedMultify;

    [Tooltip("치명타 확률 및 데미지 배율")]
    public float criticalPercent;
    public float criticalMultify;
    public bool isCritical = false;

    [Tooltip("발사체 배율")]
    public float p_projectileSizeMultify;

    [Tooltip("이동속도 배율")]
    public float p_moveSpeedTransValue;

    [Tooltip("스킬 랜덤 유무")]
    public bool isRandomSkill = false;

    [Header("CardOptions\nRandomCard")]

    [Tooltip("추가 공격력 수치. 합연산")]
    public float randomAddDamage;

    [Tooltip("추가 이동속도 수치. 곱연산")]
    public float randomAddMoveSpeed;

    [Tooltip("추가 크기 감소 수치. 곱연산")]
    public float randomSizeMultifyValue;

    [Header("CardA")]
    [Tooltip("잃은 목숨비례 공격력 상승값")]
    public float p_bulletDamageUpByLossHp;

    [Header("CardB")]
    [Tooltip("카드의 보호막 피격 시 무적 시간")]
    public float cardInvincibilityTime;

    [Header("EnemyStatData")]

    [Tooltip("데미지")]
    public float e_damage;

    [Tooltip("발사체의 속도 배율")]
    public float e_projectileSpeedMultify;

    [Tooltip("이동속도 배율")]
    public float e_moveSpeedMultify;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        playerData = GameObject.Find("Player").GetComponent<Player>();
    }

    
}
