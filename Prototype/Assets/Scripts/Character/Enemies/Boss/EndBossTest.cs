using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBossTest : EndBoss
{
    
    protected override void Start()
    {
        base.Start();
        Init();
    }

    protected override void Update()
    {
        base.Update();
        enemyState.Update();
    }

    protected override void Init()
    {
        base.Init();
        enemyState = new StateMachine();
        //Debug.Log(shootTransform.Count);
        ChangeState(new BossSpawnState(this));
    }

    

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    public void SpreadAttack(int shootCount, float rootRotateValue)
    {
        for (int i = 1; i <= shootCount; i++)
        {
            GameObject instance = Instantiate(enemyProjectile["EnemyBullet"]);
            projectileManage.SetProjectileData(ref instance, attackData.animCtrl, 10f, 1f, 5f, "Enemy");
            attackManage.ShootBulletRotate(ref instance, shootTransform["CommonBullet"], 360 / shootCount * i + rootRotateValue);
        }
    }

    
}
