using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 임시용 클래스
/// </summary>
public class EnemyBullet : Bullet
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Init();
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

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    
}
