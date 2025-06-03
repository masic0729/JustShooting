using UnityEngine;

// ��� �̵��� ������Ʈ�� ���� ����� ������ �߻� Ŭ����
public abstract class IObject : MonoBehaviour
{
    // ��ü�� �̵� ������ �ִ� ȭ�� ����
    protected float maxMoveX, maxMoveY;

    [Header("IObject")]

    [SerializeField]
    protected float moveSpeed = 0f; // �⺻ �̵� �ӵ�

    [SerializeField]
    protected float objectMoveSpeedMultify; // �̵� �ӵ��� �������� ����. 1���� ũ�� �� ������ �̵�

    // ������Ʈ�� ������ �� �����
    virtual protected void Start()
    {
        // �� ������Ʈ�� GameZone ������Ʈ�� �ڽ����� ����
        transform.parent = GameObject.Find("GameZone").transform;

        // �̵� ���� �ʱ�ȭ
        objectMoveSpeedMultify = 1;
    }

    // �� �����Ӹ��� ����� (���⼭�� ���� ������� ����)
    virtual protected void Update()
    {

    }

    // �ڽ� Ŭ�������� �����ؾ� �ϴ� �ʱ�ȭ �Լ�
    protected abstract void Init();

    // ���� ���͸� �޾Ƽ� ������Ʈ�� �̵���Ű�� �Լ�
    protected void ObjectMove(Vector3 vector)
    {
        // �̵� �ӵ��� 0�̸� ����� �޽��� ��� (�̵��� �Ұ������� �˸�)
        if (moveSpeed == 0)
            Debug.Log(this.gameObject.name + " is moveSpeed 0");

        // ���� ���Ϳ� �ӵ�, ������ ���ؼ� �̵� ó��
        transform.Translate(vector * moveSpeed * objectMoveSpeedMultify * Time.deltaTime);
    }

    // �̵� �ӵ� ���� ��ȯ
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    // �̵� �ӵ��� ����
    public void SetMoveSpeed(float Value)
    {
        moveSpeed = Value;
    }

    // �̵� ������ ����
    public void SetObjectMoveSpeedMultify(float value)
    {
        objectMoveSpeedMultify = value;
    }

    // �̵� ���� ���� ��ȯ
    public float GetObjectMoveSpeedMultify()
    {
        return objectMoveSpeedMultify;
    }
}
