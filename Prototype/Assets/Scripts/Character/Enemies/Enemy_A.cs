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
        // ��ǥ ��ġ ����
        currentTargetPosY = new Vector2(transform.position.x, targetPosY);
        currentTargetPosX = new Vector2(-22f, transform.position.y);
        //|| Vector2.Distance(transform.position, currentTargetPosX> 0.1f) //todo

        // X�� Y ��� ��ǥ ��ġ�� ������� ������ �̵�
        if (Vector2.Distance(transform.position, currentTargetPosY) > 0.5f)
        {
            // X �������� �̵�
            //if (Vector2.Distance(transform.position, currentTargetPosX) > 0.1f)
                movement.MoveToPointNormal(ref thisGameObject, currentTargetPosX, targetMoveSpeed);

            // Y �������� �̵�
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
        // Y ��ǥ ��ġ ����
        targetPosY *= -1;

        // X ��ǥ ��ġ�� ����
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
    [Header("y�ִ�")]
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