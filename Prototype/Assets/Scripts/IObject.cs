using UnityEngine;

public abstract class IObject : MonoBehaviour
{
    // [SerializeField] protected bool isCheckEscape; //화면 밖으로 나갈 때 보통 삭제되는데, 삭제해야되는 발사체인 지 확인하는 변수
    protected float maxMoveX, maxMoveY; //화면 기준 객체들이 존재하거나 이동할 수 있는 기준
    [Header("IObject")]

    [SerializeField]
    protected float moveSpeed = 0f; // 모든 오브젝트는 일반적으로 이동속도가 존재한다.
    [SerializeField]
    protected float objectMoveSpeedMultify; // 오브젝트의 이동 배율. 높을 수록 이동 속도가 빨라진다.

    

    // Start is called before the first frame update
    virtual protected void Start()
    {
        transform.parent = GameObject.Find("GameZone").transform;
        objectMoveSpeedMultify = 1;
    }

    // Update is called once per frame
    virtual protected void Update()
    {

    }


    protected abstract void Init();

    protected void ObjectMove(Vector3 vector)
    {
        //초기화 했는 지 확인하는 과정
        if(moveSpeed == 0)
            Debug.Log(this.gameObject.name + "is moveSpeed 0");

        //저장된 속도와 입력된 방향을 기반으로 이동
        transform.Translate(vector * moveSpeed * objectMoveSpeedMultify * Time.deltaTime);
    }

    //getset
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public void SetMoveSpeed(float Value)
    {
        moveSpeed = Value;
    }

    public void SetObjectMoveSpeedMultify(float value)
    {
        objectMoveSpeedMultify = value;
    }

    public float GetObjectMoveSpeedMultify()
    {
        return objectMoveSpeedMultify;
    }
}