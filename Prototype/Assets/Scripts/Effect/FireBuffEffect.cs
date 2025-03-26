using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBuffEffect : PlayerEffect
{
    [SerializeField]
    float attackUpMultity = 0.5f;
    float attackUpValue;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        attackUpValue = player.GetAttackMultiplier() + attackUpMultity;
        player.SetAttackMultiplier(attackUpValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        attackUpValue = player.GetAttackMultiplier() - attackUpMultity;
        player.SetAttackMultiplier(attackUpValue);
    }
}
