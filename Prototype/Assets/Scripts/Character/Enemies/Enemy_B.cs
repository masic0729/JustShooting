using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_B : Enemy
{
    [Header("몬스터의 y값 변경 범위값 설정")]
    [SerializeField]
    float maxY_Range;
    [SerializeField]
    float arrivePositionX;
    float targetPosY;
    float targetMoveSpeed;
    

    Vector2 currentTargetPos;
    Vector2 randTargetPosY;

    bool isArrivePoint = false;
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
        //아직 정지하려는 위치에 도달하지 않았으므로 앞으로 이동

        if (Vector2.Distance(thisGameObject.transform.position, currentTargetPos) > 0.1f && isArrivePoint == false)
        {
            //ObjectMove(Vector2.left);
            movement.MoveToPointNormal(ref thisGameObject, currentTargetPos, targetMoveSpeed);

        }
        else if(isArrivePoint == false)
        {
            isArrivePoint = true;
            SetTransTargetTransform();

        }

        if (isArrivePoint == true)
        {

            if (Vector2.Distance(transform.position, randTargetPosY) > 0.1f && isArriveTargetPos == false)
            {
                movement.MoveToPointLerp(ref thisGameObject, randTargetPosY, targetMoveSpeed);
            }
            else if (isArriveTargetPos == false)
            {
                isArriveTargetPos = true;
                Invoke("SetTransTargetTransform", 1f);
            }
        }
    }

    protected override void Init()
    {
        base.Init();
        attackDelay = 3f;
        targetMoveSpeed = GetMoveSpeed() / 2f * Time.deltaTime;
        targetPosY = this.transform.position.y;
        currentTargetPos = new Vector2(1f, targetPosY);
        StartCoroutine(AttackEnemyBullet());
    }

    void SetTransTargetTransform()
    {
        targetPosY = Random.Range(-maxY_Range, maxY_Range);
        randTargetPosY = new Vector2(transform.position.x, targetPosY);
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
            
            projectileManage.SetProjectileData(ref instance, attackData.animCtrl, 10f, 1f, 5f, "Enemy");
            
            //attackManage.ShootBulletRotate(ref instance, shootTransform["CommonBullet"], 360 / shootCount * i);
        }
        goto Repeat;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }    
}