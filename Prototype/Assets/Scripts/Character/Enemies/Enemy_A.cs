using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_A : Enemy
{
    public enum EnemyState
    {
        Spawn,
        Move,
        Action,
        Attack
    }
    public EnemyState enemyState;

    [SerializeField]
    float targetPosY, targetPosX;
    float targetMoveSpeed;
    Vector2 currentTargetPosY;
    Vector2 currentTargetPosX;

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

        // 목표 위치 설정
        currentTargetPosY = new Vector2(transform.position.x, targetPosY);
        currentTargetPosX = new Vector2(targetPosX, transform.position.y);

        // X와 Y 모두 목표 위치에 가까워질 때까지 이동
        if (Vector2.Distance(transform.position, currentTargetPosY) > 0.1f || Vector2.Distance(transform.position, currentTargetPosX) > 0.1f)
        {
            // X 방향으로 이동
            if (Vector2.Distance(transform.position, currentTargetPosX) > 0.1f)
                movement.MoveToPointLerp(ref thisGameObject, currentTargetPosX, targetMoveSpeed);

            // Y 방향으로 이동
            if (Vector2.Distance(transform.position, currentTargetPosY) > 0.1f)
                movement.MoveToPointLerp(ref thisGameObject, currentTargetPosY, targetMoveSpeed);
        }
        else if (isArriveTargetPos == false)
        {
            isArriveTargetPos = true;
            Invoke("SetTransTargetTransform", 0.2f);
        }
    }

    void SetTransTargetTransform()
    {
        // Y 목표 위치 반전
        targetPosY *= -1;

        // X 목표 위치도 반전
        targetPosX -= 1;

        isArriveTargetPos = false;
    }

    protected override void Init()
    {
        base.Init();
        targetMoveSpeed = GetMoveSpeed() / 2f * Time.deltaTime;
        targetPosX = transform.position.x;
        targetPosY = 4f;
        SetTransTargetTransform();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}