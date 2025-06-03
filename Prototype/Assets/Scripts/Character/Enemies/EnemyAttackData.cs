using UnityEngine;

/// <summary>
/// 적의 공격 관련 데이터를 저장하는 직렬화 가능한 클래스.
/// 주로 발사체에 사용된다.
/// </summary>
[System.Serializable]
public class EnemyAttackData
{
    // 발사체 또는 공격 이펙트에 사용할 스프라이트
    public Sprite sprite;

    // 발사체의 애니메이션 컨트롤러
    public RuntimeAnimatorController animCtrl;

    // 고정된 데미지 값 (불변 상수)
    public const float damage = 1f;

    // 발사체의 이동 속도
    public float moveSpeed;
}
