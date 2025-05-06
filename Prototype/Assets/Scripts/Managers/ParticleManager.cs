using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;

    [Header("����Ʈ �����յ� (name���� ���е�)")]
    public GameObject[] particlePrefabs;
    public int poolSize = 10;

    // ������ �����
    private Dictionary<string, GameObject> prefabDict = new Dictionary<string, GameObject>();

    // �� ����Ʈ �̸��� Ǯ
    private Dictionary<string, Queue<GameObject>> particlePools = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        Instance = this;

        // ������ �ʱ�ȭ �� Ǯ ����
        foreach (GameObject prefab in particlePrefabs)
        {
            string name = prefab.name;

            prefabDict[name] = prefab;

            Queue<GameObject> pool = new Queue<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                pool.Enqueue(obj);
            }

            particlePools[name] = pool;
        }
    }

    public void PlayEffect(string effectName, Vector3 position)
    {
        if (!particlePools.ContainsKey(effectName))
        {
            Debug.LogWarning($"[ParticleManager] '{effectName}' ����Ʈ�� ��ϵ��� �ʾҽ��ϴ�.");
            return;
        }

        GameObject effect;

        // �ش� ����Ʈ Ǯ���� ����
        var pool = particlePools[effectName];
        if (pool.Count > 0)
        {
            effect = pool.Dequeue();
        }
        else
        {
            effect = Instantiate(prefabDict[effectName]);
        }

        effect.transform.position = position;
        effect.SetActive(true);

        // ��ƼŬ ����
        ParticleSystem ps = effect.GetComponent<ParticleSystem>();
        ps.Play();

        // ���� �ð� �� �ٽ� ��ȯ
        StartCoroutine(ReturnToPool(effectName, effect, ps.main.duration));
    }

    private IEnumerator ReturnToPool(string effectName, GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
        particlePools[effectName].Enqueue(obj);
    }
}
