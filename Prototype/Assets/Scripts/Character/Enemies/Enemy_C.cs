using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_C : Enemy
{
    float targetPosY;


    //new
    GameObject[] shootGameObject;
    Vector2[] currentShootPos;

    bool isArrivePoint = false;




    protected override void Start()
    {
        base.Start();
        Init();
    }

    protected override void Update()
    {
        base.Update();

        //move
        if (Vector2.Distance(thisGameObject.transform.position, currentTargetPos) > distanceNeedValue && isArrivePoint == false)
        {
            //ObjectMove(Vector2.left);
            movement.MoveToPointLerp(ref thisGameObject, currentTargetPos, targetMoveSpeed);

        }
        else if (isArrivePoint == false)
        {
            isArrivePoint = true;

            currentShootPos[0] = shootTransform["CommonBullet0"].transform.position;
            currentShootPos[1] = shootTransform["CommonBullet1"].transform.position;
            StartCoroutine(AttackEnemyBullet());
        }


        if (isArrivePoint == true)
        {
            //attackVecTransfer
            if (Vector2.Distance(shootTransform["CommonBullet0"].transform.position, currentShootPos[0]) > 0.05f)
            {
                movement.MoveToPointLerp(ref shootGameObject[0], currentShootPos[0], targetMoveSpeed);
                movement.MoveToPointLerp(ref shootGameObject[1], currentShootPos[1], targetMoveSpeed);
            }
            else
            {
                TransShootPosition();
            }
        }
    }



    protected override void Init()
    {
        base.Init();
        if (isSelfPosition == true)
        {
            //currentTargetPos = new Vector2(1f, this.transform.position.y);
            currentTargetPos = new Vector2(transform.position.x - 22f, targetPosY);
        }
        currentShootPos = new Vector2[2];
        shootGameObject = new GameObject[2];
        
        
        for(int i = 0; i < currentShootPos.Length; i++)
        {
            shootGameObject[i] = shootTransform["CommonBullet" + i.ToString()].gameObject;
        }
        targetMoveSpeed = GetMoveSpeed() / 2f;

    }

    void TransShootPosition()
    {

        /*for (int i = 0; i < 2; i++)
        {
            currentShootPos[i] = shootGameObject[i].transform.position;
            currentShootPos[i] = new Vector2(currentShootPos[i].x, currentShootPos[i].y * -1);
        }
        */
        (currentShootPos[0], currentShootPos[1]) = (currentShootPos[1], currentShootPos[0]);
    }

    IEnumerator AttackEnemyBullet()
    {
        const float shootTime = 2f; // 2초 동안 발사하는 의미의 값
        const int shootCount = 20;
        float reAttackDelay = shootTime / shootCount; //연사 발사의 발사 주기

    Repeat:

        yield return new WaitForSeconds(attackDelay);
        for (int i = 0; i < shootCount; i++)
        {

            for(int j = 0; j < shootTransform.Count; j++)
            {
                GameObject instance = Instantiate(enemyProjectile["EnemyBullet"]);

                projectileManage.SetProjectileData(ref instance, attackData.animCtrl, 10f, 1f, 5f, "Enemy");

                attackManage.ShootBulletRotate(ref instance, shootTransform["CommonBullet" + j.ToString()], 90);

            }
            yield return new WaitForSeconds(reAttackDelay);
        }
        

        goto Repeat;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
