using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_A : Enemy
{
    /*
    [SerializeField]
    float targetPosY, targetPosX;
    float targetMoveSpeed;
    Vector2 currentTargetPosY;
    Vector2 currentTargetPosX;

    bool isArriveTargetPos = false;

    protected override void Start()
    {
        base.Start();
        Init();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        /*
        // 목표 위치 설정
        currentTargetPosY = new Vector2(transform.position.x, targetPosY);
        currentTargetPosX = new Vector2(-22f, transform.position.y);
        //|| Vector2.Distance(transform.position, currentTargetPosX> 0.1f) //todo

        // X와 Y 모두 목표 위치에 가까워질 때까지 이동
        if (Vector2.Distance(transform.position, currentTargetPosY) > 0.5f)
        {
            // X 방향으로 이동
            //if (Vector2.Distance(transform.position, currentTargetPosX) > 0.1f)
                movement.MoveToPointNormal(ref thisGameObject, currentTargetPosX, targetMoveSpeed);

            // Y 방향으로 이동
            //if (Vector2.Distance(transform.position, currentTargetPosY) > 0.1f)
                movement.MoveToPointLerp(ref thisGameObject, currentTargetPosY, targetMoveSpeed);
        }
        else if (isArriveTargetPos == false)
        {
            isArriveTargetPos = true;
            //Invoke("SetTransTargetTransform", 0.01f);
            SetTransTargetTransform();
        }
        


    }

    void SetTransTargetTransform()
    {
        // Y 목표 위치 반전
        targetPosY *= -1;

        // X 목표 위치도 반전
        //targetPosX -= 1;

        isArriveTargetPos = false;
    }

    protected override void Init()
    {
        base.Init();
        targetMoveSpeed = GetMoveSpeed() * Time.deltaTime;
        targetPosX = transform.position.x;
        targetPosY = 4f;
        SetTransTargetTransform();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
    */
    [Header("y최댓값")]
    [SerializeField]
    private float length = 4.5f;

    private float runningTime = 0f;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        Init();
    }

    protected override void Update()
    {
        base.Update();
        ObjectMove(Vector2.left);
        movement.MoveToSinY(ref thisGameObject, ref runningTime, length, GetMoveSpeed());
    }

    protected override void Init()
    {
        base.Init();
    }
}