using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBuffEffect : PlayerEffect
{
    [SerializeField]
    float attackUpMultity = 0.5f; // 공격력 상승 배율

    float attackUpValue; // 적용된 공격력 수치 저장용 변수

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // 현재 공격력 배율에 버프 배율을 더한 값을 저장 및 적용
        attackUpValue = player.GetAttackMultiplier() + attackUpMultity;
        player.SetAttackMultiplier(attackUpValue);
    }

    // Update is called once per frame
    void Update()
    {
        // 별도 갱신 로직 없음 (필요시 확장 가능)
    }

    private void OnDestroy()
    {
        // 오브젝트가 삭제될 때 버프를 해제하여 원래 공격력으로 복귀
        attackUpValue = player.GetAttackMultiplier() - attackUpMultity;
        player.SetAttackMultiplier(attackUpValue);
    }
}
