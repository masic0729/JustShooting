using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ش� ��ũ��Ʈ�� �������� ĳ���� ��ü���� �ɷ�ġ�� �޴´�
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

    [Tooltip("���ݼӵ� ����")]
    public float p_attackSpeedMultify;

    [Tooltip("ġ��Ÿ Ȯ�� �� ������ ����")]
    public float criticalPercent;
    public float criticalMultify;
    public bool isCritical = false;

    [Tooltip("�߻�ü ����")]
    public float p_projectileSizeMultify;

    [Tooltip("�̵��ӵ� ����")]
    public float p_moveSpeedTransValue;

    [Tooltip("��ų ���� ����")]
    public bool isRandomSkill = false;

    [Header("CardOptions\nRandomCard")]

    [Tooltip("�߰� ���ݷ� ��ġ. �տ���")]
    public float randomAddDamage;

    [Tooltip("�߰� �̵��ӵ� ��ġ. ������")]
    public float randomAddMoveSpeed;

    [Tooltip("�߰� ũ�� ���� ��ġ. ������")]
    public float randomSizeMultifyValue;

    [Header("CardA")]
    [Tooltip("���� ������ ���ݷ� ��°�")]
    public float p_bulletDamageUpByLossHp;

    [Header("CardB")]
    [Tooltip("ī���� ��ȣ�� �ǰ� �� ���� �ð�")]
    public float cardInvincibilityTime;

    [Header("EnemyStatData")]

    [Tooltip("������")]
    public float e_damage;

    [Tooltip("�߻�ü�� �ӵ� ����")]
    public float e_projectileSpeedMultify;

    [Tooltip("�̵��ӵ� ����")]
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
