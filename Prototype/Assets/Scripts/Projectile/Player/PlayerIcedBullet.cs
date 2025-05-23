using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIcedBullet : Bullet
{
    GameObject target;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Init();
        target = targetBulletManager.TargetSearching(ref thisGameObject, "Enemy", false);

        targetBulletManager.DirectTargetObject(ref thisGameObject, ref target);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void Init()
    {
        base.Init();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        //일반 몬스터일 경우 데미지 증폭(즉사급 데미지)
        /*if(collision.transform.tag == "Enemy" && collision.GetComponent<Enemy>().GetIsBoss() == false)
        {
            SetDamage(999f);
        }*/
        base.OnTriggerEnter2D(collision);
    }
}
