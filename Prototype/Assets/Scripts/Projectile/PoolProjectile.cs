using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using System;

public class PoolProjectile : MonoBehaviour
{
    public IObjectPool<GameObject> pool; // ����Ƽ�� Object Pool �ý��ۿ��� ����ϴ� Ǯ ����

    // Ǯ���� ������Ÿ�Կ� �߰������� ���Ե� ��ũ��Ʈ ����
    List<Type> scriptTypes; // ��Ÿ�ӿ� AddComponent�� �߰��� ��ũ��Ʈ Ÿ�Ե��� ����

    public enum projectileTag // �߻�ü�� ��ó
    {
        Player, // �÷��̾ ������ ����ü
        Enemy   // ���� ������ ����ü
    }

    private void Awake()
    {
        scriptTypes = new List<Type>(); // ��ũ��Ʈ Ÿ�� ����Ʈ �ʱ�ȭ
    }

    //public void ReleasePool()
    //{
    //    PRA_Pooling.instance.ReleasePool(this.gameObject); //Ư�� ���ǿ� ����(��ǥ������ �浹�� ���� �����ϰų�, ���� ����� �� ���)
    //}

    public void AddScript(Type scriptType) // Ÿ�� ����� �̿��Ͽ� ���� ���ϴ� ��ũ��Ʈ�� Ÿ ��ũ��Ʈ���� ȣ���� �� �ִ�. �׷��� �̷��� �޼��尡 Ǯ���� ���õ� ��ũ��Ʈ���� �����ϴ� ���� �������� �� �ִ�(�ƴ� ����).
    {
        this.gameObject.AddComponent(scriptType); // �ش� Ÿ���� ��ũ��Ʈ�� ��Ÿ�ӿ� AddComponent�� �߰�
        scriptTypes.Add(scriptType); // �߰��� Ÿ���� ����Ʈ�� ����
        Debug.Log(scriptTypes); // ����� �뵵
    }

    public void RemoveAllAddedScripts() // ���� ����
    {
        if (scriptTypes != null)
        {
            foreach (Type type in scriptTypes)
            {
                Destroy(this.gameObject.GetComponent(type)); // �߰��ߴ� ��ũ��Ʈ�� ����
            }
            scriptTypes.Clear(); // ����Ʈ ����
        }
        //pool.Release(this.gameObject);
    }

    private void Start()
    {
        // �ʿ� �� �ʱ�ȭ�� �ڵ� �ۼ� ����
    }

    private void OnEnable() // Ǯ�� �ý����� �ٷ�� �Ǹ� OnEnable�� �̿��� ȿ�������� ����ü���� ������ �� �ִ�
    {
        //Invoke("RemoveAllAddedScripts", 5f); // ���� �ð� �� ��ȯ
    }
}
