using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{
    protected override void Start()
    {
        base.Start();
        Init();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Init()
    {
        base.Init();
        OnGetDamageEvent += Test;
    }

    

    

    void Test()
    {
        //이런 식으로 충돌 처리를 이벤트를 통해 유연하게 작업할 수 있다.
        //각 하위 클래스 마다 이벤트 관련 함수들을 각각 제작하거나, 따로 모듈화하여 추가해야 될것으로 판단
        
        
    }
}
