using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleBoss : Boss
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnDestroy()
    {
        
    }

    protected override void Init()
    {
        base.Init();
        OnCharacterDeath += RestartWave; //���� �� ���̺� �簳

    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
