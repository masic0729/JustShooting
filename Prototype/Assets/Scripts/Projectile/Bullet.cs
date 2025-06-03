using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �⺻ ����ü Projectile�� ��ӹ��� �Ϲ� �Ѿ� Ŭ����
public class Bullet : Projectile
{
    protected TargetBulletManagement targetBulletManager; // Ÿ���� �����ϴ� Ŭ���� (Ÿ�� ������)

    public float rotateValue = 1.0f; // ���� ȸ�� ��
    protected float rotateDefaultValue; // �ʱ� ȸ�� �� �����
    protected float rotateAddValue = 1.5f; // ȸ�� �ӵ� ������

    protected override void OnEnable()
    {
        base.OnEnable(); // ������Ʈ Ǯ�� Ȱ��ȭ �� ȣ��
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start(); // Projectile�� �ʱ�ȭ ȣ��
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update(); // Projectile�� ������Ʈ ȣ��
    }

    // �Ѿ��� �ʱ� ���� ó��
    protected override void Init()
    {
        base.Init();
        //Debug.Log("Bullet");
        targetBulletManager = new TargetBulletManagement(); // Ÿ�� ���� Ŭ���� �ν��Ͻ� ����
        rotateDefaultValue = rotateValue; // �ʱ� ȸ�� ���� ����
    }

    // �浹 ó��
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision); // Projectile�� �浹 ó�� ����
    }

    // ȸ�� ���� �ʱⰪ���� �ǵ����� �Լ�
    public void InitRotateValue()
    {
        rotateValue = rotateDefaultValue;
    }
}
