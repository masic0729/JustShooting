using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    //������ �⺻������ �������� 1�����̴�
    ObjectInteration characterIteraction;

    bool isBoss = false; //���� ���� Ȯ��. �⺻���� ����

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void Init()
    {
        base.Init();
        characterIteraction = new ObjectInteration();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.name == "Player")
        {
            //�÷��̾� �����͸� �ҷ��� ���ظ��ش�
            Character instancePlayer = collision.gameObject.GetComponent<Character>();
            characterIteraction.SendDamage(ref instancePlayer, 1f);
            Debug.Log(instancePlayer.GetHp());
        }
    }
}
