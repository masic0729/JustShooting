using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEffect : MonoBehaviour
{
    protected GameObject TargetObject;
    public float destroyTime = 10f; //삭제 대기 시간

    protected virtual void Start()
    {
        Destroy(this.gameObject, destroyTime);
        if(TargetObject != null)
        {
            transform.position = TargetObject.transform.position;
            transform.parent = TargetObject.transform;
        }
        else
        {
            Debug.Log(this.gameObject + "'s TargetObject is not found");
        }
    }
}
