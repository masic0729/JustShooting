using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBullet : Bullet
{
    //�� ��ǥ�� ȸ�� ����. ���� ������ �� ����
    /*bool isCanBounceX;
    bool isCanBounceY;*/
    protected override void Start()
    {
        base.Start();
        Init();
    }
    protected override void Update()
    {
        base.Update();
        CheckBounce();
    }

    protected override void Init()
    {
        base.Init();
        maxMoveX = 10.5f; // �����ص� ��
    }

    void CheckBounce()
    {
        Vector3 pos = transform.position;

        // Y�� ��� �� �ݻ�
        if (pos.y > maxMoveY - 0.1f)
        {
            ApplyBounceRotation(Vector3.down);
        }
        // Y�� �ϴ� �� �ݻ�
        else if (pos.y < -maxMoveY + 0.1f)
        {
            ApplyBounceRotation(Vector3.up);
        }

        // X�� �ʰ� �� ���� (ƨ�� ����)
        if (Mathf.Abs(pos.x) > maxMoveX)
        {
            Destroy(this.gameObject);
        }
    }

    void ApplyBounceRotation(Vector3 normal)
    {
        // ���� Sprite�� ���� (�Ѿ��� �ٶ󺸴� ���� ����)
        Vector3 currentDir = transform.up;

        // �ݻ� ���� ��� (�Ի簢 = �ݻ簢)
        Vector3 reflected = Vector3.Reflect(currentDir, normal);

        // ȸ�� ���� ��� (Z�� ����)
        float angle = Vector3.SignedAngle(currentDir, reflected, Vector3.forward);

        // Sprite ȸ���� ���� (projectileMoveVector�� �״��)
        transform.Rotate(0f, 0f, angle);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
