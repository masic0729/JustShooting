using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : IEffect
{
    protected Player player;
    protected float playerDamage;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        TargetObject = GameObject.Find("Player");
        parentPath = "Skill";
        base.Start();
        player = TargetObject.GetComponent<Player>();
    }

    protected virtual void EnemyAttack(Collider2D[] objects)
    {
        //ParticleManager.Instance.PlayEffect("")
    }
}
