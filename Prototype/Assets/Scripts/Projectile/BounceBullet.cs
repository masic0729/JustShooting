using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �ݻ�Ǵ� �Ѿ� ����� ���� Ŭ���� (Bullet ���)
public class BounceBullet : Bullet
{
    // �� ��ǥ�� ȸ�� ����. ���� ������ �� ����
    /*bool isCanBounceX;
    bool isCanBounceY;*/

    // ������Ʈ ���� �� �ʱ�ȭ
    protected override void Start()
    {
        base.Start();
        Init();
    }

    // �� �����Ӹ��� �����
    protected override void Update()
    {
        base.Update();
        CheckBounce(); // ��ġ�� Ȯ���Ͽ� �ݻ� ���� �˻�
    }

    // �ʱ� ����
    protected override void Init()
    {
        base.Init();
        maxMoveX = 10.5f; // �����ص� �� (X�� ����� ���� ���ǿ�)
    }

    // ȭ�� ��踦 �������� �ݻ� ������ üũ��
    void CheckBounce()
    {
        Vector3 pos = transform.position;

        // Y�� ��� �� �ݻ�
        if (pos.y > maxMoveY - 0.1f)
        {
            ApplyBounceRotation(Vector3.down); // �Ʒ� �������� �ݻ�
        }
        // Y�� �ϴ� �� �ݻ�
        else if (pos.y < -maxMoveY + 0.1f)
        {
            ApplyBounceRotation(Vector3.up); // �� �������� �ݻ�
        }

        // X�� �ʰ� �� ���� (ƨ�� ����)
        if (Mathf.Abs(pos.x) > maxMoveX)
        {
            Destroy(this.gameObject); // ȭ�� ����� ������Ʈ �ı�
        }
    }

    // �ݻ� ���� ó��
    void ApplyBounceRotation(Vector3 normal)
    {
        // ���� Sprite�� ���� (�Ѿ��� �ٶ󺸴� ���� ����)
        Vector3 currentDir = transform.up;

        // �ݻ� ���� ��� (�Ի簢 = �ݻ簢)
        Vector3 reflected = Vector3.Reflect(currentDir, normal);

        // ȸ�� ���� ��� (Z�� �������� �󸶳� ȸ���ؾ� �ϴ���)
        float angle = Vector3.SignedAngle(currentDir, reflected, Vector3.forward);

        // Sprite ȸ���� ���� (�Ѿ��� �̵� ���ʹ� �״�� ����)
        transform.Rotate(0f, 0f, angle);
    }

    // �浹 ó�� (�⺻ Bullet ���� ����)
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
