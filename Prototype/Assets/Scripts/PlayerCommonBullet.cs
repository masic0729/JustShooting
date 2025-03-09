using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommonBullet : Bullet
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
        transform.Translate(Vector2.right * Time.deltaTime * 10);
    }

    protected override void Init()
    {
        base.Init();
        //Debug.Log("PlayerBullet");
    }
}
