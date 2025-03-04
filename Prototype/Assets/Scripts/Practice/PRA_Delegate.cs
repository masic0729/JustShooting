using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRA_Delecate : MonoBehaviour
{
    //영감 사항
    //1. 총알의 초기설정, 상시 행동에 대한 구조는 델리게이트로 할 수 있다. 앞으로 이동, 회전 정도?
    //2. 그러나 특정 기능(애니메이션 같이 FSM 요소)에 대해서는 델리게이트를 사용하는게 의미가 좋은걸까?란 질문을 던질 필요가 있다.
    //3. 오브젝트풀링 관련해서 대표적으로 총알을 예시로, 다양한 타입의 총알(플레이어든, 적이든 같은 종류든)의 오브젝트를 기준으로 리소스, 기능, 태그 등등 다양한 설정 및 기능들을 델리게이트를 통해 초기화 및 실행할 수 있다는 중요한 열쇠이다.
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
        calculator += Sub; // 추가
        calculator += Mul;
        calculator += Div;

        calculator -= Sub; // 삭제

        calculator = null; //초기화
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
            Destroy(this.GetComponent<BoxCollider>()); //콜라이더 삭제
        }
    }
}
