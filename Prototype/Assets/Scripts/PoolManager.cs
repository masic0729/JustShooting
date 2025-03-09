using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    public GameObject[] Projectiles; //���۵� Ǯ�� �߻�ü��
    public static PoolManager Instance; //�̱���
    public Transform GameZone; //���� ���ӿ� �����ϴ� ��� ��ü�� �����ϴ� ������Ʈ

    public Dictionary<string, IObjectPool<GameObject>> Pools; //���ǵ� �߻�ü�� ����Ϸ��� ������

    //int defaultCapacity = 10; //�ּ� Ǯ�� ������Ʈ ����
    //int maxPoolSize = 130; //�ִ� Ǯ�� ������Ʈ ����
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        PoolInit();
    }

    void PoolInit()
    {
        // Projectiles ���̸�ŭ Ǯ�� ����
        Pools = new Dictionary<string, IObjectPool<GameObject>>();

        for (int i = 0; i < Projectiles.Length; i++)
        {
            int index = i; // Ŭ���� ������ ���ϱ� ���� index ���� ����
            //���ٽ��� ����Ͽ��µ�, ���� �ڵ� �帧���μ� ���ϴ� �������� �̾����
            //���ٽ��� �̿��� ������ƮǮ�� ������ ������� �����Ͽ� �پ��� ����ü���� ó���� �� �ְ� �ϴ� �ٰ��̴�
            Pools[Projectiles[index].name] = new ObjectPool<GameObject>(() => CreateObject(index),    // �� ������ �´� ��ü ����
                GetPool,                      // ��ü ��������
                ReleasePool,                  // ��ü ��ȯ�ϱ�
                DestroyPool,                  // ��ü �ı�
                false,                        // �ڵ� Ȯ�� ����
                10,
                1000
            );
        }
    }
    GameObject CreateObject(int index)
    {
        GameObject poolInstance = Instantiate(Projectiles[index]);
        poolInstance.GetComponent<PoolProjectile>().pool = this.Pools[Projectiles[index].name];
        //poolInstance.GetComponent<PRA_Bullet>().AddScript(typeof(PRA_Test));
        return poolInstance;
    }

    public void GetPool(GameObject poolObject)
    {
        poolObject.SetActive(true);
        poolObject.transform.parent = GameZone;
    }
    public void ReleasePool(GameObject poolObject)
    {
        poolObject.transform.parent = this.transform;
        
        poolObject.SetActive(false);
    }
    public void DestroyPool(GameObject poolObject)
    {
        Destroy(poolObject);
    }
}
