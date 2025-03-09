using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    

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
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "PlayerAttack")
        {
            Debug.Log("나 충돌했어");
            OnGetDamageEvent();

        }
    }
}
