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
    /// 총알이 목표 지역까지 도달했는 지 확인하는 함수
    /// bool타입으로 해당되는 총알인 지 확인 후 거리 도달 시 삭제
    /// 해당 총알은 1보스 및 3보스 패턴에 사용되는 스크립트이다.
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
