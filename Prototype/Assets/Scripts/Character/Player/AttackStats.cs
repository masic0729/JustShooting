using UnityEngine;
//[CreateAssetMenu(fileName = "AttackStats", menuName = "ScriptableObjects/AttackStats", order = 1)]
public class AttackStats : MonoBehaviour
{
    [Header("공격 객체들의 발사체 데이터")]
    // animCtrl 변수 선언
    public RuntimeAnimatorController animCtrl;             //발사체의 애니메이션
    // sprite 변수 선언
    public Sprite sprite;                           // 발사체의 리소스
    [HideInInspector]
    // damage 변수 선언
    public float damage = 1f;                       // 피해량
    // moveSpeed 변수 선언
    public float moveSpeed = 5f;                    // 발사체 이동 속도
    // attackDelayMultify 변수 선언
    public float attackDelayMultify = 1;            // 공격 주기(공격속도)
    // powerValue 변수 선언
    public float powerValue = 0;                    //각 총알이 정의하는 파워값
    // damageMultiplier 변수 선언
    public float damageMultiplier = 1;              // 피해를 줄 수 있는 공격력 및 피해 계수
    
}