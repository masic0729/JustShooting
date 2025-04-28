using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    protected TargetBulletManagement targetBulletManager;
    public float rotateValue = 1.0f;
    protected float rotateDefaultValue;
    protected float rotateAddValue = 0.5f;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

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
        //Debug.Log("Bullet");
        targetBulletManager = new TargetBulletManagement();
        rotateDefaultValue = rotateValue;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    public void InitRotateValue()
    {
        rotateValue = rotateDefaultValue;
    }
}
