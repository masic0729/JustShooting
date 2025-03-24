using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcedBuffEffect : PlayerEffect
{
    public float shieldValue = 1;

    protected override void Start()
    {
        base.Start();
        TargetObject.GetComponent<Player>().SetShield(shieldValue);
    }

    void Update()
    {
        if(TargetObject.GetComponent<Player>().GetShield() <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (TargetObject.GetComponent<Player>().GetShield() > 0)
            TargetObject.GetComponent<Player>().SetShield(0);
    }
}
