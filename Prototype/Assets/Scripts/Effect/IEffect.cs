using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEffect : MonoBehaviour
{
    protected GameObject TargetObject;     // 이펙트가 따라붙을 대상 오브젝트
    public float destroyTime = 10f;        // 일정 시간이 지나면 자동 삭제
    protected string parentPath;           // 자식 오브젝트 중 이펙트를 붙일 위치 이름

    // 시작 시 실행되는 초기화 함수
    protected virtual void Start()
    {
        // 일정 시간이 지나면 오브젝트를 파괴
        Destroy(this.gameObject, destroyTime);

        // 타겟 오브젝트가 지정되어 있으면, 해당 타겟의 하위 특정 위치에 이펙트를 붙임
        if (TargetObject != null)
        {
            transform.parent = FindFirstChild(TargetObject.transform, parentPath);
            transform.position = transform.parent.transform.position;
        }
        else
        {
            Debug.Log(this.gameObject + "'s TargetObject is not found");
        }
    }

    /// <summary>
    /// 타겟 오브젝트의 자식들 중 지정한 이름(cardName) 또는 태그(tag)를 가진 첫 번째 자식을 찾는 재귀 함수
    /// </summary>
    /// <param cardName="parent">부모 트랜스폼</param>
    /// <param cardName="name">찾고자 하는 자식의 이름</param>
    /// <param cardName="tag">찾고자 하는 자식의 태그</param>
    /// <returns>해당 조건을 만족하는 첫 번째 자식 Transform</returns>
    protected Transform FindFirstChild(Transform parent, string name = null, string tag = null)
    {
        foreach (Transform child in parent)
        {
            // 이름 또는 태그가 일치하는 자식 발견 시 반환
            if ((name != null && child.name == name) ||
                (tag != null && child.CompareTag(tag)))
            {
                return child;
            }

            // 자식의 자식까지 재귀적으로 탐색
            if (child.childCount > 0)
            {
                Transform result = FindFirstChild(child, name, tag);
                if (result != null)
                    return result;
            }
        }
        return null; // 못 찾았을 경우 null 반환
    }
}
