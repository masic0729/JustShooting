using System.Collections;
using UnityEngine;

public class Enemy_D : Enemy
{
    GameObject targetPlayer;
    TargetBulletManagement targetManage;

    Vector2 currentTargetPos;

    float targetMoveSpeed;
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

        if (Vector2.Distance(thisGameObject.transform.position, currentTargetPos) > 0.1f && isArrivePoint == false)
        {
            movement.MoveToPointLerp(ref thisGameObject, currentTargetPos, targetMoveSpeed);
        }
        else if(isArrivePoint == false)
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
        currentTargetPos = new Vector2(1f, this.transform.position.y);
        targetMoveSpeed = GetMoveSpeed() / 2f * Time.deltaTime;
    }

    IEnumerator AttackEnemyBullet()
    {
        const float shootTime = 3f; // 3�� ���� �߻��ϴ� �ǹ��� ��
        const int shootCount = 10;
        float reAttackDelay = shootTime / shootCount; //���� �߻��� �߻� �ֱ�

        Repeat:
        yield return new WaitForSeconds(attackDelay);
        for(int i = 0; i < shootCount; i++)
        {
            GameObject instance = Instantiate(enemyProjectiles,transform.position,transform.rotation);
            projectileManage.SetProjectileData(ref instance, attackData.animCtrl, 10f, 1f, 5f, "Enemy");
            targetManage.DirectTargetObject(ref instance, ref targetPlayer);
            yield return new WaitForSeconds(reAttackDelay);

        }
        goto Repeat;
    }

    void SetTransTargetTransform()
    {
        currentTargetPos = new Vector2(transform.position.x, transform.position.y * -1);
        isArrivePoint = false;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
