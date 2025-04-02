using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIcedSkill : PlayerEffect
{
    public GameObject IcedSkillBullet;
    const int shootCount = 15;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player.IcedSkill(IcedSkillBullet);
    }

}
