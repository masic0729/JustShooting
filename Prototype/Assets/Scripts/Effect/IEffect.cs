using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEffect : MonoBehaviour
{
    protected GameObject TargetObject;
    public float destroyTime = 10f; //삭제 대기 시간
    protected string parentPath;

    protected virtual void Start()
    {
        Destroy(this.gameObject, destroyTime);
        if(TargetObject != null)
        {
            transform.parent = FindFirstChild(TargetObject.transform, parentPath);
            transform.position = transform.parent.transform.position;
        }
        else
        {
            Debug.Log(this.gameObject + "'s TargetObject is not found");
        }
    }

    protected Transform FindFirstChild(Transform parent, string name = null, string tag = null)
    {
        foreach (Transform child in parent)
        {
            if ((name != null && child.name == name) ||
                (tag != null && child.CompareTag(tag)))
            {
                return child;
            }

            if (child.childCount > 0)
            {
                Transform result = FindFirstChild(child, name, tag);
                if (result != null)
                    return result;
            }
        }
        return null;
    }


}
