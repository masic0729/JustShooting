using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어의 바람속성 유도탄
public class PlayerWindBullet : Bullet
{
    GameObject target;
    GameObject FakeObject;


    protected override void Start()
    {
        base.Start();
        Init();
    }

    protected override void Update()
    {
        base.Update();

        /*if (target != null)
        {
            targetBulletManager.TunningObject(ref thisGameObject, ref target, ref rotateValue, rotateAddValue);
            rotateValue += 0.4f;
        }
        else
        {
            rotateValue = 0.3f;
            //보스 탐지
            //target = targetBulletManager.TargetSearching("Enemy", true);
            target = targetBulletManager.TargetSearching(ref thisGameObject, "Enemy", true);

        }

        if (target == null)
        {
            //보스가 없으니 일반 몬스터 탐지
            //target = targetBulletManager.TargetSearching("Enemy");

        }*/
        if (target == null)
        {
            target = targetBulletManager.TargetSearching(ref thisGameObject, "Enemy", true);
            targetBulletManager.TunningObject(ref thisGameObject, ref FakeObject, ref rotateValue, rotateAddValue);
        }
        targetBulletManager.TunningObject(ref thisGameObject, ref target, ref rotateValue, rotateAddValue);


    }

    protected override void Init()
    {
        base.Init();

        float rotateRandom = Random.Range(50f, 60f);
        int randValue = Random.Range(0, 2);
        rotateRandom = randValue == 1 ? rotateRandom *= -1 : rotateRandom;
        this.gameObject.transform.Rotate(0, 0, rotateRandom);
        FakeObject = GameObject.Find("FakeObject");
    }
}
