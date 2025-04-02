using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_C : Enemy
{
    float targetPosY;
    float targetMoveSpeed;

    Vector2 currentTargetPos;

    bool isArrivePoint = false;

    //todo 이름 변경(무기 위치 변경으로)
    bool isArriveTargetPos = false;


    protected override void Start()
    {
        base.Start();
        Init();
    }

    protected override void Update()
    {
        base.Update();

        if (Vector2.Distance(thisGameObject.transform.position, currentTargetPos) > 0.1f && isArrivePoint == false)
        {
            //ObjectMove(Vector2.left);
            movement.MoveToPointNormal(ref thisGameObject, currentTargetPos, targetMoveSpeed);

        }
        else if (isArrivePoint == false)
        {
            isArrivePoint = true;
            SetTransTargetTransform();

        }
    }

    void SetTransTargetTransform()
    {
        
        isArriveTargetPos = false;
    }

    protected override void Init()
    {
        base.Init();
    }

    IEnumerator AttackEnemyBullet()
    {
        const float reAttackDelay = 0.2f; //연사 발사의 발사 주기
        const int shootCount = 10;

    Repeat:

        yield return new WaitForSeconds(attackDelay);

        for (int i = 1; i <= shootCount; i++)
        {
            GameObject instance = Instantiate(enemyProjectiles);

            projectileManage.SetProjectileData(ref instance, attackData.animCtrl, 10f, 1f, 5f, "Enemy");

            attackManage.ShootBulletRotate(ref instance, shootTransform["CommonBullet"], 360 / shootCount * i);
            
            yield return new WaitForSeconds(reAttackDelay);
        }

        goto Repeat;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
