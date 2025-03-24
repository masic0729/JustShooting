using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IObject : MonoBehaviour
{
    // [SerializeField] protected bool isCheckEscape; //ȭ�� ������ ���� �� ���� �����Ǵµ�, �����ؾߵǴ� �߻�ü�� �� Ȯ���ϴ� ����

    protected float maxMoveX, maxMoveY; //ȭ�� ���� ��ü���� �����ϰų� �̵��� �� �ִ� ����
    [Header("IObject")]

    [SerializeField]
    protected float moveSpeed = 0f; // ��� ������Ʈ�� �Ϲ������� �̵��ӵ��� �����Ѵ�.

    virtual protected void Awake()
    {
        
    }

    // Start is called before the first frame update
    virtual protected void Start()
    {
        transform.parent = GameObject.Find("GameZone").transform;
    }

    // Update is called once per frame
    virtual protected void Update()
    {

    }


    protected abstract void Init();

    protected void ObjectMove(Vector3 vector)
    {
        //�ʱ�ȭ �ߴ� �� Ȯ���ϴ� ����
        if(moveSpeed == 0)
            Debug.Log(this.gameObject.name + "is moveSpeed 0");

        //����� �ӵ��� �Էµ� ������ ������� �̵�
        transform.Translate(vector * moveSpeed * Time.deltaTime);
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
}