using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾��� �ٶ��Ӽ� ����ź
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
            //���� Ž��
            target = targetBulletManager.TargetSearching("Enemy", true);
        }
        if (target == null)
        {
            //������ ������ �Ϲ� ���� Ž��
            target = targetBulletManager.TargetSearching("Enemy");
        }
    }

    protected override void Init()
    {
        base.Init();
        targetBulletManager = gameObject.AddComponent<TargetBulletManagement>();

    }
}
