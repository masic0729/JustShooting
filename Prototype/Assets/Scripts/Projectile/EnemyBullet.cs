using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ӽÿ� Ŭ����
/// </summary>
public class EnemyBullet : Bullet
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start(); // Bullet�� Start ȣ��
        Init(); // �ʱ�ȭ
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update(); // Bullet�� Update ȣ��
    }

    // �� �Ѿ��� �ʱ� ����
    protected override void Init()
    {
        base.Init(); // Bullet�� Init ȣ��
    }

    // �浹 ó��
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision); // Bullet�� �浹 ó�� ����
    }
}
