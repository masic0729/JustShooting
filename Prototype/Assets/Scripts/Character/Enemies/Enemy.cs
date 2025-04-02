using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public GameObject enemyProjectiles; //나중에 딕셔너리 기반으로 가독성 있는 코드로 변형 예정

    [Header("Enemy의 공격 데이터")]
    public EnemyAttackData attackData;
    //적군은 기본적으로 데미지가 1고정이다
    ObjectInteration characterIteraction;
    protected EnemyMovement movement;
    
    protected GameObject thisGameObject;

    [SerializeField]
    bool isBoss = false; //보스 유무 확인. 기본값은 거짓

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
            //플레이어 데이터를 불러와 피해를준다
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
