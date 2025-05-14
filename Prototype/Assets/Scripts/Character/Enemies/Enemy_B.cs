using System.Collections;
using UnityEngine;

public class Enemy_B : Enemy
{
    [Header("몬스터의 y값 변경 범위값 설정")]
    [SerializeField]
    float maxY_Range;
    [SerializeField]
    float targetPosY;
    [SerializeField]
    int shootCount = 10;
    bool isArrivePoint = false;


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
        if (Vector2.Distance(thisGameObject.transform.position, currentTargetPos) > distanceNeedValue && isArrivePoint == false)
        {
            //ObjectMove(Vector2.left);
            movement.MoveToPointLerp(ref thisGameObject, currentTargetPos, targetMoveSpeed);
        }
        else if (isArrivePoint == false)
        {
            isArrivePoint = true;
            Invoke("SetTransTargetTransform", 1f);
        }
    }

    protected override void Init()
    {
        base.Init();
        attackDelay = 3f;
        targetMoveSpeed = GetMoveSpeed() / 2f;
        targetPosY = this.transform.position.y;
        if (isSelfPosition == true)
        {
            //currentTargetPos = new Vector2(1f, this.transform.position.y);
            currentTargetPos = new Vector2(transform.position.x - 22f, targetPosY);
        }
        
        StartCoroutine(AttackEnemyBullet());

        //stateMachine.ChangeState(new BossSpawnState(this)); //여기를 기점으로 FSM 시작. 하지만 기능은 지금 없음. 그리고 상위 클래스에 둬도 괜찮은 지 추후 검수 예정

    }

    void SetTransTargetTransform()
    {
        targetPosY = Random.Range(-maxY_Range, maxY_Range);
        currentTargetPos = new Vector2(transform.position.x, targetPosY);
        isArrivePoint = false;
    }

    IEnumerator AttackEnemyBullet()
    {
        Repeat:
       
        yield return new WaitForSeconds(attackDelay);

        for (int i = 1; i <= 10; i++)
        {
            GameObject instance = Instantiate(enemyProjectile["EnemyBullet"]);
            
            projectileManage.SetProjectileData(ref instance, attackData.animCtrl, attackData.moveSpeed, 1f, 5f, "Enemy");
            attackManage.ShootBulletRotate(ref instance, shootTransform["CommonBullet"], 360 / shootCount * i);
        }
        goto Repeat;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }    
}