using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcedBuffEffect : PlayerEffect
{
    //public float shieldValue = 1; // 플레이어에게 부여할 실드 값

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // 플레이어의 피격 기능 해제
        player.SetIsBuffInvicibility(true);
        //player.SetShield(shieldValue);

        Invoke("DestroyBuff", 10f);                              //10초 뒤 자동 삭제
    }


    private void Update()
    {
        if(player.GetIsBuffInvicibility() == false)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 또한 얼음 보호막 상태에서 피격 시 본래 피격 판정은 그대로 두며,
    /// 얼음 방어막의 본래 기능을 해지한다
    /// </summary>
    /// <param cardName="damage">데미지는 비어있다.</param>
    void DestroyBuff(float damage)
    {
        player.SetIsBuffInvicibility(false);

    }
}
