using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackStats", menuName = "ScriptableObjects/AttackStats", order = 1)]
public class AttackStats : ScriptableObject
{
    [Header("공격 객체들의 발사체 데이터")]
    public AnimatorController animCtrl;         // 발사체의 애니메이션
    public Sprite sprite;                       // 발사체의 리소스
    public float damage;                        // 피해량
    public float attackSpeed;                   // 공격 주기 (초당 공격 횟수)
    //protected float attackMultiplier = 1;     // 피해를 줄 수 있는 공격력 및 피해 계수 //보류


}