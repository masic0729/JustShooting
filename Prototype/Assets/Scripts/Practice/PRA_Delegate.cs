using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRA_Delecate : MonoBehaviour
{
    //���� ����
    //1. �Ѿ��� �ʱ⼳��, ��� �ൿ�� ���� ������ ��������Ʈ�� �� �� �ִ�. ������ �̵�, ȸ�� ����?
    //2. �׷��� Ư�� ���(�ִϸ��̼� ���� FSM ���)�� ���ؼ��� ��������Ʈ�� ����ϴ°� �ǹ̰� �����ɱ�?�� ������ ���� �ʿ䰡 �ִ�.
    //3. ������ƮǮ�� �����ؼ� ��ǥ������ �Ѿ��� ���÷�, �پ��� Ÿ���� �Ѿ�(�÷��̾��, ���̵� ���� ������)�� ������Ʈ�� �������� ���ҽ�, ���, �±� ��� �پ��� ���� �� ��ɵ��� ��������Ʈ�� ���� �ʱ�ȭ �� ������ �� �ִٴ� �߿��� �����̴�.
    delegate void Calculator(int numA, int numB);
    Calculator calculator;
    int rotateSpeed;

    void Add(int numA, int numB)
    {
        Debug.Log(numA + numB);
    }
    void Sub(int numA, int numB)
    {
        Debug.Log(numA - numB);
    }
    void Mul(int numA, int numB)
    {
        Debug.Log(numA * numB);
    }
    void Div(int numA, int numB)
    {
        Debug.Log(numA / numB);
    }


    void Start()
    {
        calculator = Add;
        calculator += Sub; // �߰�
        calculator += Mul;
        calculator += Div;

        calculator -= Sub; // ����

        calculator = null; //�ʱ�ȭ
        gameObject.AddComponent<BoxCollider>();
        calculator(5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        Test();
    }

    void Test()
    {
        if (!GetComponent<BoxCollider>() && Input.GetKeyDown(KeyCode.A))
        {
            gameObject.AddComponent<BoxCollider>();
        }
        else if (GetComponent<BoxCollider>() && Input.GetKeyDown(KeyCode.A))
        {
            Destroy(this.GetComponent<BoxCollider>()); //�ݶ��̴� ����
        }
    }
}
