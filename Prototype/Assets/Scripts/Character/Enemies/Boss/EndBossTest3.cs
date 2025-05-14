using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBossTest3 : EndBoss
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

    public override IEnumerator EnemyAttack()
    {
        GameObject player = GameObject.Find("Player");
        yield return ShootBullets();
        yield return new WaitForSeconds(2.5f);
        yield return ShootBullets(player);

        yield return base.EnemyAttack();
    }

    IEnumerator ShootBullets(GameObject player = null)
    {
        const float maxSpawnYRange = 4f;
        const int spawnCount = 6;
        float[] spawnBulletsY = new float[spawnCount];
        for (int i = 0; i < spawnBulletsY.Length; i++)
        {
            spawnBulletsY[i] = maxSpawnYRange / spawnCount * (i * 2) - maxSpawnYRange;
            Debug.Log(spawnBulletsY[i]);
        }

        Vector3 spawnPosition;

        for (int i = 0; i < spawnBulletsY.Length; i++)
        {
            spawnPosition = new Vector3(9.9f, spawnBulletsY[i], 0);
            GameObject instance = Instantiate(enemyProjectile["EnemyBullet"], spawnPosition, shootTransform["HandAttack"].rotation);
            projectileManage.SetProjectileData(ref instance, attackData.animCtrl, attackData.moveSpeed, 1f, 5f, "Enemy");
            if(player != null)
            {
                targetManage.DirectTargetObject(ref instance, ref player);
            }
            
            yield return new WaitForSeconds(0.4f);
        }
    }

    void ShuffleArray<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int randIndex = Random.Range(i, array.Length);
            T temp = array[i];
            array[i] = array[randIndex];
            array[randIndex] = temp;
        }
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
