using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommonBullet : Bullet
{
    //어느 속성인 지 확인하려는 문자값
    public string bulletName;
    float windAttackDelayTransValue = 0.05f;
    Player player;

    protected override void OnEnable()
    {
        base.OnEnable();
        Init();
        
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    private void LateUpdate()
    {
        if (bulletName == "Fire")
        {
            //SetLifeTime(0.25f);
            Debug.Log("난 불속성이라 짧아");
        }
    }

    protected override void Init()
    {
        base.Init();
        if(player == null)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }

    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (bulletName == player.GetPlayerWeaponName() && player.windBulletHitCount < 6 &&
            collision.transform.tag =="Enemy" && bulletName == "Wind" && player != null)
        {
            player.attackStats.attackDelayMultify -= windAttackDelayTransValue;
            player.windBulletHitCount++;
        }
        
    }
}
