using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class PRA_Pooling : MonoBehaviour
{
    public static PRA_Pooling instance;

    public GameObject bulletPrefab;
    public Transform GameZone;

    //가장 많은 총알, 일반 몬스터 등 절대적인 개체 수가 많은 객체들을 대상으로 풀링을 사용할 예정.
    //GameObject 및 제네릭화 보다는 정해진 스크립트를 기준으로 사용 고려
    public IObjectPool<GameObject> bulletPool;


    int defaultCapacity = 10;
    int maxPoolSize = 130;


    private void Awake()
    {
        if(instance == null)
        {
            //싱글톤화
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
        //    bullet.transform.parent = this.gameObject.transform; //해당 코드는 게임존 오브젝트의 자식으로 이동하는 작업이며, 추후 풀링을 사용하지 않는 객체를 삽입할 때 이러한 코드와 동일하므로 어떻게 처리할 지 고민 예정
        //    bullet.pool.Release(bullet.gameObject); //원래 릴리스를 해야 하지만, 테스트를 명분으로 임시 주석(비활성화)처리
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
