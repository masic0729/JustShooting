using System.Collections;
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
        PoolInit();
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
            Pools[Projectiles[index].name] = new ObjectPool<GameObject>(() => CreateObject(index),    // 각 종류에 맞는 객체 생성
                GetPool,                      // 객체 가져오기
                ReleasePool,                  // 객체 반환하기
                DestroyPool,                  // 객체 파괴
                false,                        // 자동 확장 여부
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
