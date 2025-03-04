using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using System;

public class PoolProjectile : MonoBehaviour
{
    public IObjectPool<GameObject> pool;

    //풀링한 프로젝타입에 추가적으로 삽입된 스크립트 정보
    List<Type> scriptTypes;
    public enum projectileTag //발사체의 출처. 
    {
        Player,
        Enemy
    }

    private void Awake()
    {

        scriptTypes = new List<Type>();

    }

    //public void ReleasePool()
    //{
    //    PRA_Pooling.instance.ReleasePool(this.gameObject); //특정 조건에 의해(대표적으로 충돌에 의해 삭제하거나, 씬이 종료될 때 등등)
    //}

    public void AddScript(Type scriptType) //타입 방식을 이용하여 내가 원하는 스크립트를 타 스크립트에서 호출할 수 있다. 그러나 이러한 메서드가 풀링과 관련된 스크립트에서 존재하는 것은 부적합할 수 있다(아님 말고).
    {
        this.gameObject.AddComponent(scriptType); //해당 오브젝트에 내가 원하는 스크립트 삽입
        scriptTypes.Add(scriptType); //삽입한 스크립트 명을 리스트에 추가
        Debug.Log(scriptTypes);
    }

    public void RemoveAllAddedScripts() //이하 동일
    {
        if (scriptTypes != null)
        {
            foreach (Type type in scriptTypes)
            {
                Destroy(this.gameObject.GetComponent(type));
            }
            scriptTypes.Clear();
        }
        pool.Release(this.gameObject);
    }

    private void Start()
    {

    }

    private void OnEnable() //풀링 시스템을 다루게 되면 OnEnable을 이용해 효율적으로 투사체들을 관리할 수 있다
    {
        Invoke("RemoveAllAddedScripts", 5f); // 일정 시간 뒤 반환
    }
}
