using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance; // 싱글톤 인스턴스

    [Header("이펙트 프리팹들 (name으로 구분됨)")]
    public GameObject[] particlePrefabs; // 이펙트 프리팹 배열
    public int poolSize = 10; // 각 이펙트당 초기 풀 크기

    // 프리팹 저장용
    private Dictionary<string, GameObject> prefabDict = new Dictionary<string, GameObject>(); // 이름 기반 프리팹 참조

    // 각 이펙트 이름별 풀
    private Dictionary<string, Queue<GameObject>> particlePools = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this; // 싱글톤 초기화
        }

        // 프리팹 초기화 및 풀 생성
        foreach (GameObject prefab in particlePrefabs)
        {
            string name = prefab.name;

            prefabDict[name] = prefab; // 이름을 키로 프리팹 저장

            Queue<GameObject> pool = new Queue<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false); // 초기에는 비활성화
                pool.Enqueue(obj);    // 풀에 추가
            }

            particlePools[name] = pool; // 이름별 풀 등록
        }
    }

    // 이펙트 재생 함수
    public void PlayEffect(string effectName, Vector3 position)
    {
        if (!particlePools.ContainsKey(effectName))
        {
            Debug.LogWarning(effectName + "이펙트 미등록");
            return;
        }

        GameObject effect;

        // 해당 이펙트 풀에서 꺼냄
        Queue<GameObject> pool = particlePools[effectName];
        if (pool.Count > 0)
        {
            effect = pool.Dequeue();
        }
        else
        {
            effect = Instantiate(prefabDict[effectName]); // 없으면 새로 생성
        }

        effect.transform.position = position; // 위치 설정
        effect.SetActive(true);

        // 파티클 실행
        ParticleSystem ps = effect.GetComponent<ParticleSystem>();
        ps.Play();

        // 일정 시간 후 다시 반환
        if(Instance != null)
        {
            StartCoroutine(ReturnToPool(effectName, effect, ps.main.duration));
        }

    }

    // 딜레이 후 이펙트를 풀로 복귀
    private IEnumerator ReturnToPool(string effectName, GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
        particlePools[effectName].Enqueue(obj); // 다시 큐에 삽입
    }
}
