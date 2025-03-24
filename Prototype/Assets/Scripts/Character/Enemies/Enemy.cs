using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    //적군은 기본적으로 데미지가 1고정이다
    ObjectInteration characterIteraction;

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
            //플레이어 데이터를 불러와 피해를준다
            Character instancePlayer = collision.gameObject.GetComponent<Character>();
            characterIteraction.SendDamage(ref instancePlayer, 1f);
            Debug.Log(instancePlayer.GetHp());
        }
    }
}
