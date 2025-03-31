using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_B : Enemy
{
    [Header("������ y�� ���� ������ ����")]
    [SerializeField]
    float maxY_Range;
    [SerializeField]
    float arrivePositionX;
    float targetPosY;
    float targetMoveSpeed;
    

    Vector2 currentTargetPos;
    //Vector2 randTargetPosY;

    bool isArriveTargetPos = false;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Init();
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //���� �����Ϸ��� ��ġ�� �������� �ʾ����Ƿ� ������ �̵�
        if(transform.position.x - arrivePositionX > 0.1f)
        {
            //movement.MoveToPointNormal(ref thisGameObject, ref arrivePositionX, ref targetMoveSpeed);
            ObjectMove(Vector2.left);
        }
        else if(Vector2.Distance(transform.position, currentTargetPos) > 0.1f && isArriveTargetPos == false)
        {
            movement.MoveToPointLerp(ref thisGameObject, ref currentTargetPos, ref targetMoveSpeed);
        }
        else if(isArriveTargetPos == false)
        {
            isArriveTargetPos = true;
            Invoke("SetTransTargetTransform", 1f);
        }
    }

    protected override void Init()
    {
        base.Init();
        attackDelay = 3f;
        targetMoveSpeed = 0.01f;
        targetPosY = this.transform.position.y;
        SetTransTargetTransform();
        StartCoroutine(AttackEnemyBullet());
    }

    void SetTransTargetTransform()
    {
        targetPosY = Random.Range(-maxY_Range, maxY_Range);
        currentTargetPos = new Vector2(arrivePositionX, targetPosY);
        isArriveTargetPos = false;
    }

    IEnumerator AttackEnemyBullet()
    {
        Repeat:
        int shootCount = 10;
       
        yield return new WaitForSeconds(attackDelay);

        for (int i = 1; i <= 10; i++)
        {
            GameObject instance = Instantiate(enemyProjectiles);
            attackManage.ShootBulletRotate(ref instance, shootTransform["CommonBullet"], 360 / shootCount * i);

        }
        goto Repeat;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    

}
