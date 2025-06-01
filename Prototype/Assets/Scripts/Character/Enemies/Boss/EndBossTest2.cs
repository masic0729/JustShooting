using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBossTest2 : EndBoss
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

    /*public override IEnumerator EnemyAttack()
    {
        int shootRandom = Random.Range(2, 5); // 2~4회 발사
        float shootRandomRotate = Random.Range(40f, 60f); // 회전각 랜덤

        for (int i = 0; i < shootRandom; i++)
        {
            BounceAttack(-shootRandomRotate);
            BounceAttack(shootRandomRotate);
            yield return new WaitForSeconds(0.5f);

        }
        yield return new WaitForSeconds(attackEndStopTime);


    }*/

    public void BounceAttack(float rotateValue)
    {

        GameObject instance = Instantiate(enemyProjectile["EnemyBounceBullet"]);
        projectileManage.SetProjectileData(ref instance, attackData.animCtrl, attackData.moveSpeed, 1f, 10f, "Enemy");
        attackManage.ShootBulletRotate(ref instance, shootTransform["HandAttack"], rotateValue + shootTransform["HandAttack"].transform.rotation.z);
    
    }
}
