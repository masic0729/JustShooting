using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBuffEffect : PlayerEffect
{
    [SerializeField]
    float moveMultiplierValue = 0.2f; // 이동 속도 증가 계수

    // Start는 오브젝트가 활성화될 때 호출되며, 버프 적용 로직을 수행
    protected override void Start()
    {
        base.Start();
        // 플레이어의 이동 계수를 증가시켜 이동 속도 상승 효과를 부여
        player.SetObjectMoveSpeedMultify(player.GetObjectMoveSpeedMultify() + moveMultiplierValue);
    }

    // 버프 오브젝트가 파괴될 때, 효과를 원상 복귀시킴
    private void OnDestroy()
    {
        if (player != null)
        {
            // 이동 속도 증가 계수를 다시 원래대로 되돌림
            player.SetObjectMoveSpeedMultify(player.GetObjectMoveSpeedMultify() - moveMultiplierValue);
        }
        else
        {
            Debug.Log("나는 개똥벌레 플레이어가 없어서 효력 없음"); // 디버그용 메시지
        }
    }
}
