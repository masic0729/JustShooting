using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcedBuffEffect : PlayerEffect
{
    public float shieldValue = 1; // 플레이어에게 부여할 실드 값

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // 플레이어에게 실드 부여
        player.SetShield(shieldValue);
    }

    void Update()
    {
        // 실드가 모두 소진되었으면, 이 버프 오브젝트를 제거
        if (player.GetShield() <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        // 실드가 남아있을 경우, 버프 해제 시 실드도 제거
        if (player.GetShield() > 0)
            player.SetShield(0);
    }
}
