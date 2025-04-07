using System.Collections;
using UnityEngine;

public class Enemy_C : Enemy
{
    float targetPosY;
    float targetMoveSpeed;


    Vector2 currentTargetPos;

    bool isArrivePoint = false;

    //todo �̸� ����(���� ��ġ ��������)
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
        targetMoveSpeed = GetMoveSpeed() / 2f * Time.deltaTime;
        currentTargetPos = new Vector2(1f, this.transform.position.y);
        StartCoroutine(AttackEnemyBullet());
    }

    IEnumerator AttackEnemyBullet()
    {
        const float shootTime = 2f; // 2�� ���� �߻��ϴ� �ǹ��� ��
        const int shootCount = 20;
        float reAttackDelay = shootTime / shootCount; //���� �߻��� �߻� �ֱ�

    Repeat:

        yield return new WaitForSeconds(attackDelay);
        for (int i = 0; i < shootCount; i++)
        {

            for(int j = 0; j < shootTransform.Count; j++)
            {
                GameObject instance = Instantiate(enemyProjectiles);

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
