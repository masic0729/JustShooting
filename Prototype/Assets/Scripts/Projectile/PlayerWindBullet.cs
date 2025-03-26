using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어의 바람속성 유도탄
public class PlayerWindBullet : Bullet
{
    GameObject target;
    TargetBulletManagement targetBulletManager;
    public float rotateAddValue = 0.01f;

    protected override void Start()
    {
        base.Start();
        Init();
    }

    protected override void Update()
    {
        base.Update();
        if(target != null)
        {
            GameObject instanceThisGameobject = this.gameObject;
            targetBulletManager.TunningObject(ref instanceThisGameobject, ref target, rotateAddValue);
        }
        else
        {
            //보스 탐지
            target = targetBulletManager.TargetSearching("Enemy", true);
        }
        if (target == null)
        {
            //보스가 없으니 일반 몬스터 탐지
            target = targetBulletManager.TargetSearching("Enemy");
        }
    }

    protected override void Init()
    {
        base.Init();
        targetBulletManager = gameObject.AddComponent<TargetBulletManagement>();

    }
}
