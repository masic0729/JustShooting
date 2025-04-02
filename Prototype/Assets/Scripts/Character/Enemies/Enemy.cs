using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public GameObject enemyProjectiles; //���߿� ��ųʸ� ������� ������ �ִ� �ڵ�� ���� ����

    [Header("Enemy�� ���� ������")]
    public EnemyAttackData attackData;
    //������ �⺻������ �������� 1�����̴�
    ObjectInteration characterIteraction;
    protected EnemyMovement movement;
    
    protected GameObject thisGameObject;

    [SerializeField]
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
        CheckOverGameZone();
    }

    protected override void Init()
    {
        base.Init();
        characterIteraction = new ObjectInteration();
        movement = new EnemyMovement();
        thisGameObject = this.gameObject;
    }

    void CheckOverGameZone()
    {
        if(this.transform.position.x < -12f)
        {
            Destroy(this.gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.name == "Player")
        {
            //�÷��̾� �����͸� �ҷ��� ���ظ��ش�
            Character instancePlayer = collision.gameObject.GetComponent<Character>();
            characterIteraction.SendDamage(ref instancePlayer, 1f);
        }
    }

    public bool GetIsBoss()
    {
        return isBoss;
    }

    public void SetIsBoss(bool state)
    {
        isBoss = state;
    }
}
