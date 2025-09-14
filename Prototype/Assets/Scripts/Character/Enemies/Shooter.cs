using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("y�ִ�")]
    [SerializeField]
    ObjectMovement movement;
    Shooter thisGameObject;
    // length ���� ����: y�� �ִ� ���� ũ��
    private float length = 0.2f;
    public float startShootY;
    // yVector ���� ����: �� �Ǵ� �Ʒ� ���� ������ ���� (1 �Ǵ� -1)
    float yVector;
    [SerializeField]
    // yMoveSpeedMultify ���� ����: y�� �̵� �ӵ� ����
    private float yMoveSpeedMultify = 1f;
    // runningTime ���� ����: �ð� ������, Sin �Լ� ��꿡 ���
    private float runningTime = 0f;

    public bool isReverse = false;

    // �ʱ�ȭ �� ȣ��, �θ� Start ȣ�� �� Init ����
    void Start()
    {
        Init();
        yVector = isReverse == true ? -1 : 1;
        movement = new ObjectMovement();
        thisGameObject = this.gameObject.GetComponent<Shooter>();
        startShootY = transform.position.y;
    }

    // �� ������ ȣ��, �θ� Update ���� �� ���� �̵� �� Sin � Y�� �̵� ����
    void Update()
    {
        movement.MoveToSinY_ForShooter(ref thisGameObject, ref runningTime, length * yVector, 5f * yMoveSpeedMultify); // Sin �Լ��� y�� ������ ó��
    }

    // ���� �ʱ�ȭ �� ��ġ ����
    void Init()
    {

    }
}
