using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_D : Enemy
{
    GameObject targetPlayer;
    TargetBulletManagement targetManage;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Init()
    {
        base.Init();
        targetManage = new TargetBulletManagement();
        targetPlayer = GameObject.Find("Player");

    }



    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
