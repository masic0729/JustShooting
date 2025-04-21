using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBullet : Bullet
{
    //각 좌표의 회전 여부. 추후 삭제될 수 있음
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
        /*isCanBounceX = true;
        isCanBounceY = true;*/
    }

    /// <summary>
    /// 튕기는 총알 클래스 특성 상 좌표값을 기반으로 확인 후 조치를 함
    /// </summary>
    void CheckBounce()
    {
        /*if(Mathf.Abs(this.gameObject.transform.position.x) > Mathf.Abs(maxMoveX) && isCanBounceX == true)
        {
            //isCanBounceX = false;
            Debug.Log(projectileMoveVector);
            projectileMoveVector = new Vector3(projectileMoveVector.x, projectileMoveVector.y * -1, projectileMoveVector.z);
            Debug.Log(projectileMoveVector);
            //Invoke("SetTransBounceX", 0.3f);
        }
        if (Mathf.Abs(this.gameObject.transform.position.y) > Mathf.Abs(maxMoveY) && isCanBounceY == true)
        {
            //isCanBounceY = false;
            projectileMoveVector = new Vector3(projectileMoveVector.x, projectileMoveVector.y * -1, projectileMoveVector.z);
            //Invoke("SetTransBounceY", 0.3f);
        }*/

        if ((Mathf.Abs(this.gameObject.transform.position.y) > Mathf.Abs(maxMoveY))  ||
            Mathf.Abs(this.gameObject.transform.position.x) > Mathf.Abs(maxMoveX))
        {
            projectileMoveVector = new Vector3(projectileMoveVector.x, projectileMoveVector.y * -1, projectileMoveVector.z);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    /// <summary>
    /// getset
    /// </summary>
    /*void SetTransBounceX()
    {
        isCanBounceX = true;
    }
    void SetTransBounceY()
    {
        isCanBounceY = true;
    }*/

    
}
