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
    }

    protected override void Init()
    {
        base.Init();
        
    }
    

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    public override IEnumerator EnemyAttack()
    {
        SpreadAttack(10, 0f);
        yield return new WaitForSeconds(0.5f);
        SpreadAttack(10, 30f);
        yield return new WaitForSeconds(1f);
        SpreadAttack(18, 0f);

        yield return new WaitForSeconds(attackEndStopTime);


    }

    public void SpreadAttack(int shootCount, float rootRotateValue)
    {
        for (int i = 1; i <= shootCount; i++)
        {
            GameObject instance = Instantiate(enemyProjectile["EnemyBullet"]);
            projectileManage.SetProjectileData(ref instance, attackData.animCtrl, attackData.moveSpeed, 1f, 5f, "Enemy");
            attackManage.ShootBulletRotate(ref instance, this.gameObject.transform, 360 / shootCount * i + rootRotateValue);
        }
    }
}
