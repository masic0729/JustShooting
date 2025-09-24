using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyTargetBullet : Bullet
{
    [SerializeField] Vector2 target;
    [SerializeField] bool isArriveDestroy = false;
    [SerializeField] float currentDistance;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();
        Init();
    }

    protected override void Update()
    {
        base.Update();
        CheckArriveTarget();
    }

    protected override void Init()
    {
        base.Init();
        maxMoveX = 15f;
        maxMoveY = 15f;
        transform.Rotate(0, 0, 180);
    }



    /// <summary>
    /// �Ѿ��� ��ǥ �������� �����ߴ� �� Ȯ���ϴ� �Լ�
    /// boolŸ������ �ش�Ǵ� �Ѿ��� �� Ȯ�� �� �Ÿ� ���� �� ����
    /// �ش� �Ѿ��� 1���� �� 3���� ���Ͽ� ���Ǵ� ��ũ��Ʈ�̴�.
    /// </summary>
    void CheckArriveTarget()
    {
        if (isArriveDestroy == false)
            return;

        currentDistance = Vector2.Distance((Vector2)transform.position, target);
        if (currentDistance < 0.05f)
        {
            Destroy(this.gameObject);
        }
    }

    public Vector2 GetTargetVector2()
    {
        return target;
    }
}
