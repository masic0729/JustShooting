using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using System;

public class PoolProjectile : MonoBehaviour
{
    public IObjectPool<GameObject> pool;

    //Ǯ���� ������Ÿ�Կ� �߰������� ���Ե� ��ũ��Ʈ ����
    List<Type> scriptTypes;
    public enum projectileTag //�߻�ü�� ��ó. 
    {
        Player,
        Enemy
    }

    private void Awake()
    {

        scriptTypes = new List<Type>();

    }

    //public void ReleasePool()
    //{
    //    PRA_Pooling.instance.ReleasePool(this.gameObject); //Ư�� ���ǿ� ����(��ǥ������ �浹�� ���� �����ϰų�, ���� ����� �� ���)
    //}

    public void AddScript(Type scriptType) //Ÿ�� ����� �̿��Ͽ� ���� ���ϴ� ��ũ��Ʈ�� Ÿ ��ũ��Ʈ���� ȣ���� �� �ִ�. �׷��� �̷��� �޼��尡 Ǯ���� ���õ� ��ũ��Ʈ���� �����ϴ� ���� �������� �� �ִ�(�ƴ� ����).
    {
        this.gameObject.AddComponent(scriptType); //�ش� ������Ʈ�� ���� ���ϴ� ��ũ��Ʈ ����
        scriptTypes.Add(scriptType); //������ ��ũ��Ʈ ���� ����Ʈ�� �߰�
        Debug.Log(scriptTypes);
    }

    public void RemoveAllAddedScripts() //���� ����
    {
        if (scriptTypes != null)
        {
            foreach (Type type in scriptTypes)
            {
                Destroy(this.gameObject.GetComponent(type));
            }
            scriptTypes.Clear();
        }
        pool.Release(this.gameObject);
    }

    private void Start()
    {

    }

    private void OnEnable() //Ǯ�� �ý����� �ٷ�� �Ǹ� OnEnable�� �̿��� ȿ�������� ����ü���� ������ �� �ִ�
    {
        Invoke("RemoveAllAddedScripts", 5f); // ���� �ð� �� ��ȯ
    }
}
