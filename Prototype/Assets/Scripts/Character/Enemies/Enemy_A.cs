using UnityEngine;
using UnityEngine.UIElements;

// Enemy_A는 Sin 곡선 형태로 움직이는 적 캐릭터
public class Enemy_A : Enemy
{
    [Header("y최댓값")]
    [SerializeField]
    // length 변수 선언: y축 최대 진폭 크기
    private float length = 4f;
    // yVector 변수 선언: 위 또는 아래 방향 결정용 변수 (1 또는 -1)
    float yVector;
    [SerializeField]
    // yMoveSpeedMultify 변수 선언: y축 이동 속도 배율
    private float yMoveSpeedMultify;
    // runningTime 변수 선언: 시간 누적용, Sin 함수 계산에 사용
    private float runningTime = 0f;

    // 초기화 시 호출, 부모 Start 호출 후 Init 실행
    protected override void Start()
    {
        base.Start();
        Init();
    }

    // 매 프레임 호출, 부모 Update 실행 후 좌측 이동 및 Sin 곡선 Y축 이동 수행
    protected override void Update()
    {
        base.Update();
        ObjectMove(Vector2.left); // 기본적으로 왼쪽 이동
        movement.MoveToSinY(ref thisGameObject, ref runningTime, length * yVector, GetMoveSpeed() * yMoveSpeedMultify); // Sin 함수로 y축 움직임 처리
    }

    // 변수 초기화 및 위치 설정
    protected override void Init()
    {
        base.Init();
        // SpawnManager에서 적이 아래로 내려가는지 여부에 따라 y 방향 결정 (-1: 아래, 1: 위)
        yVector = SpawnManager.instance.isEnemyA_Down == true ? -1 : 1;
        // 초기 위치를 오른쪽 끝 (x=12)으로 설정
        transform.position = new Vector3(12, transform.position.y, 0);
    }

    // 충돌 처리, 부모 OnTriggerEnter2D 호출 유지
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    // 기존 임시 보존용 이동 관련 코드, 사용하지 않는 주석 처리된 코드 영역
    void Tresh()
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

        protected override void Update()
        {
            base.Update();

            currentTargetPosY = new Vector2(transform.position.x, targetPosY);
            currentTargetPosX = new Vector2(-22f, transform.position.y);

            if (Vector2.Distance(transform.position, currentTargetPosY) > 0.5f)
            {
                movement.MoveToPointNormal(ref thisGameObject, currentTargetPosX, targetMoveSpeed);
                movement.MoveToPointLerp(ref thisGameObject, currentTargetPosY, targetMoveSpeed);
            }
            else if (isArriveTargetPos == false)
            {
                isArriveTargetPos = true;
                SetTransTargetTransform();
            }
        }

        void SetTransTargetTransform()
        {
            targetPosY *= -1;
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
    }
}
