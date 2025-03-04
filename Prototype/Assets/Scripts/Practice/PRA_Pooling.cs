using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class PRA_Pooling : MonoBehaviour
{
    public static PRA_Pooling instance;

    public GameObject bulletPrefab;
    public Transform GameZone;

    //���� ���� �Ѿ�, �Ϲ� ���� �� �������� ��ü ���� ���� ��ü���� ������� Ǯ���� ����� ����.
    //GameObject �� ���׸�ȭ ���ٴ� ������ ��ũ��Ʈ�� �������� ��� ���
    public IObjectPool<GameObject> bulletPool;


    int defaultCapacity = 10;
    int maxPoolSize = 130;


    private void Awake()
    {
        if(instance == null)
        {
            //�̱���ȭ
            instance = this;
        }
        Init();
    }

    void Init()
    {
        bulletPool = new ObjectPool<GameObject>(CreateObject, GetPool, ReleasePool,
            DestroyPool, false, defaultCapacity, maxPoolSize);

        //for (int i = 0; i < defaultCapacity; i++)
        //{
        //    PRA_Bullet bullet = CreateObject().GetComponent<PRA_Bullet>();
        //    bullet.transform.parent = this.gameObject.transform; //�ش� �ڵ�� ������ ������Ʈ�� �ڽ����� �̵��ϴ� �۾��̸�, ���� Ǯ���� ������� �ʴ� ��ü�� ������ �� �̷��� �ڵ�� �����ϹǷ� ��� ó���� �� ��� ����
        //    bullet.pool.Release(bullet.gameObject); //���� �������� �ؾ� ������, �׽�Ʈ�� ������� �ӽ� �ּ�(��Ȱ��ȭ)ó��
        //    Debug.Log(defaultCapacity);
        //}
    }

    GameObject CreateObject()
    {
        GameObject poolInstance = Instantiate(bulletPrefab);
        poolInstance.GetComponent<PRA_Bullet>().pool = this.bulletPool;
        poolInstance.GetComponent<PRA_Bullet>().AddScript(typeof(PRA_Test));
        return poolInstance;
    }

    public void GetPool(GameObject poolObject)
    {
        poolObject.SetActive(true);
        poolObject.transform.parent = GameZone;
    }

    public void ReleasePool(GameObject poolObject)
    {
        poolObject.SetActive(false);
        poolObject.transform.parent = this.transform;
    }

    public void DestroyPool(GameObject poolObject)
    {
        Destroy(poolObject);
    }
}
