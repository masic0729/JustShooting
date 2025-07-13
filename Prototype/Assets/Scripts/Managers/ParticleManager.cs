using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance; // �̱��� �ν��Ͻ�

    [Header("����Ʈ �����յ� (name���� ���е�)")]
    public GameObject[] particlePrefabs; // ����Ʈ ������ �迭
    public int poolSize = 10; // �� ����Ʈ�� �ʱ� Ǯ ũ��

    // ������ �����
    private Dictionary<string, GameObject> prefabDict = new Dictionary<string, GameObject>(); // �̸� ��� ������ ����

    // �� ����Ʈ �̸��� Ǯ
    private Dictionary<string, Queue<GameObject>> particlePools = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this; // �̱��� �ʱ�ȭ
        }

        // ������ �ʱ�ȭ �� Ǯ ����
        foreach (GameObject prefab in particlePrefabs)
        {
            string name = prefab.name;

            prefabDict[name] = prefab; // �̸��� Ű�� ������ ����

            Queue<GameObject> pool = new Queue<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false); // �ʱ⿡�� ��Ȱ��ȭ
                pool.Enqueue(obj);    // Ǯ�� �߰�
            }

            particlePools[name] = pool; // �̸��� Ǯ ���
        }
    }

    // ����Ʈ ��� �Լ�
    public void PlayEffect(string effectName, Vector3 position)
    {
        if (!particlePools.ContainsKey(effectName))
        {
            Debug.LogWarning(effectName + "����Ʈ �̵��");
            return;
        }

        GameObject effect;

        // �ش� ����Ʈ Ǯ���� ����
        Queue<GameObject> pool = particlePools[effectName];
        if (pool.Count > 0)
        {
            effect = pool.Dequeue();
        }
        else
        {
            effect = Instantiate(prefabDict[effectName]); // ������ ���� ����
        }

        effect.transform.position = position; // ��ġ ����
        effect.SetActive(true);

        // ��ƼŬ ����
        ParticleSystem ps = effect.GetComponent<ParticleSystem>();
        ps.Play();

        // ���� �ð� �� �ٽ� ��ȯ
        if(Instance != null)
        {
            StartCoroutine(ReturnToPool(effectName, effect, ps.main.duration));
        }

    }

    // ������ �� ����Ʈ�� Ǯ�� ����
    private IEnumerator ReturnToPool(string effectName, GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
        particlePools[effectName].Enqueue(obj); // �ٽ� ť�� ����
    }
}
