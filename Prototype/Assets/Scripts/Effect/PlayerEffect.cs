using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : IEffect
{
    protected Player player;             // 플레이어 스크립트 참조용 변수
    protected float playerDamage;        // 플레이어의 공격력 등 이펙트에 필요한 수치 저장용

    // Start는 오브젝트 활성화 시 가장 먼저 실행되는 함수
    protected override void Start()
    {
        TargetObject = GameObject.Find("Player");   // 타겟 오브젝트를 Player로 설정
        parentPath = "Skill";                       // 이펙트를 붙일 자식 오브젝트 이름 지정
        base.Start();                               // 부모 클래스(IEffect)의 Start 실행
        player = TargetObject.GetComponent<Player>(); // 플레이어 컴포넌트 캐싱
        playerDamage = StatManager.instance.p_skillDamageMultify;
    }

    /// <summary>
    /// 자식 클래스에서 재정의할 수 있는 적 공격 처리 함수
    /// </summary>
    /// <param cardName="objects">충돌한 적 객체 배열</param>
    protected virtual void EnemyAttack(Collider2D[] objects)
    {
        // 이펙트 발생 시 적에게 데미지를 주는 처리 예정 (구현 생략됨)
        //ParticleManager.Instance.PlayEffect("")
    }
}
