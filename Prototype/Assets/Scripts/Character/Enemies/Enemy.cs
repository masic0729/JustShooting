using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public GameObject enemyProjectiles; //���߿� ��ųʸ� ������� ������ �ִ� �ڵ�� ���� ����

    [Header("Enemy�� ���� ������")]
    public EnemyAttackData attackData;

    protected ObjectMovement movement;
    
    protected GameObject thisGameObject;

    [SerializeField]
    bool isBoss = false; //���� ���� Ȯ��. �⺻���� ����

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
        movement = new ObjectMovement();
        thisGameObject = this.gameObject;
    }

    void CheckOverGameZone()
    {
        if(this.transform.position.x < -12f)
        {
            Destroy(this.gameObject);
        }
    }

    virtual protected void OnTriggerEnter2D(Collider2D collision)
    {
        const float damageValue = 1f;
        if(collision.transform.name == "Player")
        {
            //�÷��̾� �����͸� �ҷ��� ���ظ��ش�
            Character instancePlayer = collision.gameObject.GetComponent<Character>();
            if(instancePlayer != null && characterInteraction != null)
            {
                characterInteraction.SendDamage(ref instancePlayer, damageValue);
                Debug.Log(instancePlayer.GetHp());
            }
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
