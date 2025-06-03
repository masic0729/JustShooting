using UnityEngine;

// 모든 이동형 오브젝트의 공통 기능을 정의한 추상 클래스
public abstract class IObject : MonoBehaviour
{
    // 객체가 이동 가능한 최대 화면 범위
    protected float maxMoveX, maxMoveY;

    [Header("IObject")]

    [SerializeField]
    protected float moveSpeed = 0f; // 기본 이동 속도

    [SerializeField]
    protected float objectMoveSpeedMultify; // 이동 속도에 곱해지는 배율. 1보다 크면 더 빠르게 이동

    // 오브젝트가 생성될 때 실행됨
    virtual protected void Start()
    {
        // 이 오브젝트를 GameZone 오브젝트의 자식으로 설정
        transform.parent = GameObject.Find("GameZone").transform;

        // 이동 배율 초기화
        objectMoveSpeedMultify = 1;
    }

    // 매 프레임마다 실행됨 (여기서는 따로 사용하지 않음)
    virtual protected void Update()
    {

    }

    // 자식 클래스에서 구현해야 하는 초기화 함수
    protected abstract void Init();

    // 방향 벡터를 받아서 오브젝트를 이동시키는 함수
    protected void ObjectMove(Vector3 vector)
    {
        // 이동 속도가 0이면 디버그 메시지 출력 (이동이 불가능함을 알림)
        if (moveSpeed == 0)
            Debug.Log(this.gameObject.name + " is moveSpeed 0");

        // 방향 벡터와 속도, 배율을 곱해서 이동 처리
        transform.Translate(vector * moveSpeed * objectMoveSpeedMultify * Time.deltaTime);
    }

    // 이동 속도 값을 반환
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    // 이동 속도를 설정
    public void SetMoveSpeed(float Value)
    {
        moveSpeed = Value;
    }

    // 이동 배율을 설정
    public void SetObjectMoveSpeedMultify(float value)
    {
        objectMoveSpeedMultify = value;
    }

    // 이동 배율 값을 반환
    public float GetObjectMoveSpeedMultify()
    {
        return objectMoveSpeedMultify;
    }
}
