using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    public GameObject[] Projectiles; //제작된 풀링 발사체들
    public static PoolManager Instance; //싱글톤
    public Transform GameZone; //실제 게임에 존재하는 모든 객체를 저장하는 오브젝트

    public Dictionary<string, IObjectPool<GameObject>> Pools; //정의된 발사체를 등록하려는 데이터

    //int defaultCapacity = 10; //최소 풀링 오브젝트 개수
    //int maxPoolSize = 130; //최대 풀링 오브젝트 개수

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        PoolInit(); // 풀 초기화
    }

    void PoolInit()
    {
        // Projectiles 길이만큼 풀을 생성
        Pools = new Dictionary<string, IObjectPool<GameObject>>();

        for (int i = 0; i < Projectiles.Length; i++)
        {
            int index = i; // 클로저 문제를 피하기 위해 index 값을 저장

            //람다식을 사용하였는데, 현재 코드 흐름으로서 원하는 방향으로 이어갈려면
            //람다식을 이용해 오브젝트풀의 문법을 편법으로 접근하여 다양한 투사체들을 처리할 수 있게 하는 근거이다

            // 각 발사체 이름을 키로 하여 해당 오브젝트 풀 생성 및 등록
            Pools[Projectiles[index].name] = new ObjectPool<GameObject>(
                () => CreateObject(index), // 객체 생성
                GetPool,                   // 가져올 때 실행
                ReleasePool,               // 반환할 때 실행
                DestroyPool,               // 삭제할 때 실행
                false,                     // 자동 생성 여부 (false)
                100,                       // 기본 개수
                1000                       // 최대 개수
            );
        }
    }

    /// <summary>
    /// 요구하는 오브젝트를 생성할 때 실행하며, 이를 딕셔너리화를 통해 호출명을 명확하게 볼 수 있다
    /// </summary>
    /// <param name="index">배열로 저장된 발사체들의 인덱스값</param>
    /// <returns>poolObjects 반환</returns>
    GameObject CreateObject(int index)
    {
        GameObject poolInstance = Instantiate(Projectiles[index]); // 발사체 생성
        poolInstance.GetComponent<PoolProjectile>().pool = this.Pools[Projectiles[index].name]; // 자신의 풀 정보 연결
        return poolInstance;
    }

    // 오브젝트를 풀에서 꺼낼 때 호출되는 메서드
    public void GetPool(GameObject poolObject)
    {
        poolObject.SetActive(true);              // 오브젝트 활성화
        poolObject.transform.parent = GameZone;  // GameZone에 배치
    }

    // 오브젝트를 풀에 반환할 때 호출되는 메서드
    public void ReleasePool(GameObject poolObject)
    {
        poolObject.transform.parent = this.transform; // PoolManager 아래로 이동
        poolObject.SetActive(false);                  // 비활성화
    }

    // 오브젝트를 완전히 제거할 때 호출되는 메서드
    public void DestroyPool(GameObject poolObject)
    {
        Destroy(poolObject);
    }
}
