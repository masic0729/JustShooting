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

        PoolInit(); // Ǯ �ʱ�ȭ
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

            // �� �߻�ü �̸��� Ű�� �Ͽ� �ش� ������Ʈ Ǯ ���� �� ���
            Pools[Projectiles[index].name] = new ObjectPool<GameObject>(
                () => CreateObject(index), // ��ü ����
                GetPool,                   // ������ �� ����
                ReleasePool,               // ��ȯ�� �� ����
                DestroyPool,               // ������ �� ����
                false,                     // �ڵ� ���� ���� (false)
                100,                       // �⺻ ����
                1000                       // �ִ� ����
            );
        }
    }

    /// <summary>
    /// �䱸�ϴ� ������Ʈ�� ������ �� �����ϸ�, �̸� ��ųʸ�ȭ�� ���� ȣ����� ��Ȯ�ϰ� �� �� �ִ�
    /// </summary>
    /// <param name="index">�迭�� ����� �߻�ü���� �ε�����</param>
    /// <returns>poolObjects ��ȯ</returns>
    GameObject CreateObject(int index)
    {
        GameObject poolInstance = Instantiate(Projectiles[index]); // �߻�ü ����
        poolInstance.GetComponent<PoolProjectile>().pool = this.Pools[Projectiles[index].name]; // �ڽ��� Ǯ ���� ����
        return poolInstance;
    }

    // ������Ʈ�� Ǯ���� ���� �� ȣ��Ǵ� �޼���
    public void GetPool(GameObject poolObject)
    {
        poolObject.SetActive(true);              // ������Ʈ Ȱ��ȭ
        poolObject.transform.parent = GameZone;  // GameZone�� ��ġ
    }

    // ������Ʈ�� Ǯ�� ��ȯ�� �� ȣ��Ǵ� �޼���
    public void ReleasePool(GameObject poolObject)
    {
        poolObject.transform.parent = this.transform; // PoolManager �Ʒ��� �̵�
        poolObject.SetActive(false);                  // ��Ȱ��ȭ
    }

    // ������Ʈ�� ������ ������ �� ȣ��Ǵ� �޼���
    public void DestroyPool(GameObject poolObject)
    {
        Destroy(poolObject);
    }
}
