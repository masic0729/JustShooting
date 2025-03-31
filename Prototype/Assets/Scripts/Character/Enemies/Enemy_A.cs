using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_A : Enemy
{
    [Header("몬스터의 이동과 관련된 데이터")]
    [SerializeField]
    float targetPosY;
    float targetMoveSpeed;
    Vector2 currentTargetPos;

    bool isArriveTargetPos = false;
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
        ObjectMove(Vector2.left);
        currentTargetPos = new Vector2(transform.position.x, targetPosY);
        if (Vector2.Distance(transform.position, currentTargetPos) > 0.1f && isArriveTargetPos == false)
        {
            //transform.position = Vector2.MoveTowards(transform.position, currentTargetPos, Time.deltaTime * GetMoveSpeed() * 5f);
            movement.MoveToPointLerp(ref thisGameObject, ref currentTargetPos, ref targetMoveSpeed);
        }
        else if(isArriveTargetPos == false)
        {
            isArriveTargetPos = true;
            Invoke("SetTransTargetTransform", 0.2f);
        }

    }

    void SetTransTargetTransform()
    {
        targetPosY *= -1;
        isArriveTargetPos = false;
    }

    protected override void Init()
    {
        base.Init();
        targetMoveSpeed = 0.01f;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
