using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public static StatManager instance;

    [Header("PlayerStatData")]
    public float p_damage;
    public float p_damageMultify;

    [Tooltip("공격속도 배율")]
    public float p_attackSpeedMultify;

    [Tooltip("치명타 확률 및 데미지 배율")]
    public float criticalPercent;
    public float criticalMultify;

    [Tooltip("발사체 배율")]
    public float p_projectileSpeedMultyfy;

    [Tooltip("이동속도 배율")]
    public float p_moveSpeed;
    public float p_moveSpeedMultyfy;

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

    //public void 


}
