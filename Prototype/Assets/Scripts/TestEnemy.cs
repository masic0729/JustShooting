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
        //�̷� ������ �浹 ó���� �̺�Ʈ�� ���� �����ϰ� �۾��� �� �ִ�.
        //�� ���� Ŭ���� ���� �̺�Ʈ ���� �Լ����� ���� �����ϰų�, ���� ���ȭ�Ͽ� �߰��ؾ� �ɰ����� �Ǵ�
        
        
    }
}
