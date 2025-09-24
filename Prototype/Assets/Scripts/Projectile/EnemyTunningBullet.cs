using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTunningBullet : Bullet
{
    GameObject target;

    protected override void Start()
    {
        base.Start();
        Init();
        rotateAddValue = 0.1f;
    }

    protected override void Update()
    {
        base.Update();
        if (rotateValue >= 3f)
            rotateValue = 3f;

        if (target == null)
        {

            target = targetBulletManager.TargetSearching(ref thisGameObject, "Player");
            return;
        }

        if (target.GetComponent<Character>().GetCharacterColEnable() == false)
        {
            target = null;
        }
        else
        {
            targetBulletManager.TunningObject(ref thisGameObject, ref target, ref rotateValue, rotateAddValue);
        }
    }

    protected override void Init()
    {
        base.Init();
    }
}
