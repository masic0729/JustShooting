using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("y최댓값")]
    [SerializeField]
    ObjectMovement movement;
    Shooter thisGameObject;
    // length 변수 선언: y축 최대 진폭 크기
    private float length = 0.2f;
    public float startShootY;
    // yVector 변수 선언: 위 또는 아래 방향 결정용 변수 (1 또는 -1)
    float yVector;
    [SerializeField]
    // yMoveSpeedMultify 변수 선언: y축 이동 속도 배율
    private float yMoveSpeedMultify = 1f;
    // runningTime 변수 선언: 시간 누적용, Sin 함수 계산에 사용
    private float runningTime = 0f;

    public bool isReverse = false;

    // 초기화 시 호출, 부모 Start 호출 후 Init 실행
    void Start()
    {
        Init();
        yVector = isReverse == true ? -1 : 1;
        movement = new ObjectMovement();
        thisGameObject = this.gameObject.GetComponent<Shooter>();
        startShootY = transform.position.y;
    }

    // 매 프레임 호출, 부모 Update 실행 후 좌측 이동 및 Sin 곡선 Y축 이동 수행
    void Update()
    {
        movement.MoveToSinY_ForShooter(ref thisGameObject, ref runningTime, length * yVector, 5f * yMoveSpeedMultify); // Sin 함수로 y축 움직임 처리
    }

    // 변수 초기화 및 위치 설정
    void Init()
    {

    }
}
