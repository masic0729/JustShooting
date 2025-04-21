using System.Collections;
using UnityEngine;

public class Enemy_D : Enemy
{
    GameObject targetPlayer;
    TargetBulletManagement targetManage;

    float targetPosY;

    bool isArrivePoint = false;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Init();
        StartCoroutine(AttackEnemyBullet());
    }

    protected override void Update()
    {
        base.Update();

        if (Vector2.Distance(thisGameObject.transform.position, currentTargetPos) > distanceNeedValue && isArrivePoint == false)
        {
            movement.MoveToPointLerp(ref thisGameObject, currentTargetPos, targetMoveSpeed);
        }
        else if (isArrivePoint == false)
        {
            isArrivePoint = true;
            SetTransTargetTransform();
        }
    }


    protected override void Init()
    {
        base.Init();
        targetManage = new TargetBulletManagement();
        targetPlayer = GameObject.Find("Player");
        if(isSelfPosition == true)
        {
            currentTargetPos = new Vector2(1f, this.transform.position.y);
        }
        targetMoveSpeed = GetMoveSpeed() / 2f;
        targetPosY = transform.position.y;
    }

    IEnumerator AttackEnemyBullet()
    {
        const float shootTime = 3f; // 3초 동안 발사하는 의미의 값
        const int shootCount = 10;
        float reAttackDelay = shootTime / shootCount; //연사 발사의 발사 주기

        Repeat:
        yield return new WaitForSeconds(attackDelay);
        for(int i = 0; i < shootCount; i++)
        {
            GameObject instance = Instantiate(enemyProjectile["EnemyBullet"],transform.position,transform.rotation);
            projectileManage.SetProjectileData(ref instance, attackData.animCtrl, 10f, 1f, 5f, "Enemy");
            targetManage.DirectTargetObject(ref instance, ref targetPlayer);
            yield return new WaitForSeconds(reAttackDelay);

        }
        goto Repeat;
    }

    void SetTransTargetTransform()
    {
        targetPosY = targetPosY * -1f;
        currentTargetPos = new Vector2(transform.position.x, targetPosY);
        isArrivePoint = false;
        distanceNeedValue = 1f;
        Debug.Log(distanceNeedValue);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
