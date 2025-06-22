using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public static StatManager instance;

    [Header("PlayerStatData")]
    public float p_damage;
    public float p_damageMultify;

    [Tooltip("���ݼӵ� ����")]
    public float p_attackSpeedMultify;

    [Tooltip("ġ��Ÿ Ȯ�� �� ������ ����")]
    public float criticalPercent;
    public float criticalMultify;

    [Tooltip("�߻�ü ����")]
    public float p_projectileSpeedMultyfy;

    [Tooltip("�̵��ӵ� ����")]
    public float p_moveSpeed;
    public float p_moveSpeedMultyfy;

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

    //public void 


}
