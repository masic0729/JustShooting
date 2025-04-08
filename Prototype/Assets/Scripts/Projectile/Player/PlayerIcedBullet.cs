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
        base.OnTriggerEnter2D(collision);
        if(collision.transform.tag == "Enemy")
        {
            //������ �ƴϸ� ���, ������ ������ ���� ���� ��ŭ �ο��ϱ⿡ �Ϲ� ���Ϳ� ���� ���� ó��
            if(collision.GetComponent<Enemy>().GetIsBoss() == false)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
