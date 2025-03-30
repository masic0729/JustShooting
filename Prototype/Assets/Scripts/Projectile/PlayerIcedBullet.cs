using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIcedBullet : Bullet
{
    GameObject target;
    TargetBulletManagement targetBulletManager;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Init();
        targetBulletManager = gameObject.AddComponent<TargetBulletManagement>();
        target = targetBulletManager.TargetSearching("Enemy");
        float angle = Mathf.Atan2(target.transform.position.y - this.transform.position.y, target.transform.position.x - this.transform.position.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
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
            //보스가 아니면 즉사, 보스면 얼음의 정규 피해 만큼 부여하기에 일반 몬스터에 대한 예외 처리
            if(collision.GetComponent<Enemy>().GetIsBoss() == false)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
