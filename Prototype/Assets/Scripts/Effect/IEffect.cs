using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEffect : MonoBehaviour
{
    protected GameObject TargetObject;     // ����Ʈ�� ������� ��� ������Ʈ
    public float destroyTime = 10f;        // ���� �ð��� ������ �ڵ� ����
    protected string parentPath;           // �ڽ� ������Ʈ �� ����Ʈ�� ���� ��ġ �̸�

    // ���� �� ����Ǵ� �ʱ�ȭ �Լ�
    protected virtual void Start()
    {
        // ���� �ð��� ������ ������Ʈ�� �ı�
        Destroy(this.gameObject, destroyTime);

        // Ÿ�� ������Ʈ�� �����Ǿ� ������, �ش� Ÿ���� ���� Ư�� ��ġ�� ����Ʈ�� ����
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
    /// Ÿ�� ������Ʈ�� �ڽĵ� �� ������ �̸�(cardName) �Ǵ� �±�(tag)�� ���� ù ��° �ڽ��� ã�� ��� �Լ�
    /// </summary>
    /// <param cardName="parent">�θ� Ʈ������</param>
    /// <param cardName="name">ã���� �ϴ� �ڽ��� �̸�</param>
    /// <param cardName="tag">ã���� �ϴ� �ڽ��� �±�</param>
    /// <returns>�ش� ������ �����ϴ� ù ��° �ڽ� Transform</returns>
    protected Transform FindFirstChild(Transform parent, string name = null, string tag = null)
    {
        foreach (Transform child in parent)
        {
            // �̸� �Ǵ� �±װ� ��ġ�ϴ� �ڽ� �߰� �� ��ȯ
            if ((name != null && child.name == name) ||
                (tag != null && child.CompareTag(tag)))
            {
                return child;
            }

            // �ڽ��� �ڽı��� ��������� Ž��
            if (child.childCount > 0)
            {
                Transform result = FindFirstChild(child, name, tag);
                if (result != null)
                    return result;
            }
        }
        return null; // �� ã���� ��� null ��ȯ
    }
}
