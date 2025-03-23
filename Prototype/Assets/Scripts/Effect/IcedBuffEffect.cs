using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcedBuffEffect : PlayerEffect
{
    public float shieldValue = 1;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        TargetObject.GetComponent<Player>().SetShield(shieldValue);
    }

    private void OnDestroy()
    {
        if (TargetObject.GetComponent<Player>().GetShield() > 0)
            TargetObject.GetComponent<Player>().SetShield(0);
    }
}
