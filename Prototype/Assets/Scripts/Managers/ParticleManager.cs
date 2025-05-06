using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;

    [Header("이펙트 프리팹들 (name으로 구분됨)")]
    public GameObject[] particlePrefabs;
    public int poolSize = 10;

    // 프리팹 저장용
    private Dictionary<string, GameObject> prefabDict = new Dictionary<string, GameObject>();

    // 각 이펙트 이름별 풀
    private Dictionary<string, Queue<GameObject>> particlePools = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        Instance = this;

        // 프리팹 초기화 및 풀 생성
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
            Debug.LogWarning($"[ParticleManager] '{effectName}' 이펙트가 등록되지 않았습니다.");
            return;
        }

        GameObject effect;

        // 해당 이펙트 풀에서 꺼냄
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

        // 파티클 실행
        ParticleSystem ps = effect.GetComponent<ParticleSystem>();
        ps.Play();

        // 일정 시간 후 다시 반환
        StartCoroutine(ReturnToPool(effectName, effect, ps.main.duration));
    }

    private IEnumerator ReturnToPool(string effectName, GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
        particlePools[effectName].Enqueue(obj);
    }
}
