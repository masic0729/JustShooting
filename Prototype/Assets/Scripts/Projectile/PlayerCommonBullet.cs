using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommonBullet : Bullet
{
    //어느 속성인 지 확인하려는 문자값
    public string bulletName;
    float windAttackDelayTransValue = 0.05f;
    Player player;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Init();
        player = GameObject.Find("Player").GetComponent<Player>();
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
        if (bulletName == player.GetPlayerWeaponName() && player.windBulletHitCount < 6 &&
            collision.transform.tag =="Enemy" && bulletName == "Wind")
        {
            player.attackStats.attackDelayMultify -= windAttackDelayTransValue;
            player.windBulletHitCount++;
        }
    }
}
