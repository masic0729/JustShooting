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
        /*isCanBounceX = true;
        isCanBounceY = true;*/
    }

    /// <summary>
    /// ƨ��� �Ѿ� Ŭ���� Ư�� �� ��ǥ���� ������� Ȯ�� �� ��ġ�� ��
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
