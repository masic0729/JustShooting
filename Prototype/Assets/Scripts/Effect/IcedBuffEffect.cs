using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcedBuffEffect : PlayerEffect
{
    public float shieldValue = 1;

    protected override void Start()
    {
        base.Start();
        player.SetShield(shieldValue);
    }

    void Update()
    {
        if(player.GetShield() <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (player.GetShield() > 0)
            player.SetShield(0);
    }
}
