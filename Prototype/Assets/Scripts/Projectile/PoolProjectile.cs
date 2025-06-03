using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using System;

public class PoolProjectile : MonoBehaviour
{
    public IObjectPool<GameObject> pool; // 유니티의 Object Pool 시스템에서 사용하는 풀 참조

    // 풀링한 프로젝타입에 추가적으로 삽입된 스크립트 정보
    List<Type> scriptTypes; // 런타임에 AddComponent로 추가된 스크립트 타입들을 저장

    public enum projectileTag // 발사체의 출처
    {
        Player, // 플레이어가 생성한 투사체
        Enemy   // 적이 생성한 투사체
    }

    private void Awake()
    {
        scriptTypes = new List<Type>(); // 스크립트 타입 리스트 초기화
    }

    //public void ReleasePool()
    //{
    //    PRA_Pooling.instance.ReleasePool(this.gameObject); //특정 조건에 의해(대표적으로 충돌에 의해 삭제하거나, 씬이 종료될 때 등등)
    //}

    public void AddScript(Type scriptType) // 타입 방식을 이용하여 내가 원하는 스크립트를 타 스크립트에서 호출할 수 있다. 그러나 이러한 메서드가 풀링과 관련된 스크립트에서 존재하는 것은 부적합할 수 있다(아님 말고).
    {
        this.gameObject.AddComponent(scriptType); // 해당 타입의 스크립트를 런타임에 AddComponent로 추가
        scriptTypes.Add(scriptType); // 추가한 타입을 리스트에 저장
        Debug.Log(scriptTypes); // 디버깅 용도
    }

    public void RemoveAllAddedScripts() // 이하 동일
    {
        if (scriptTypes != null)
        {
            foreach (Type type in scriptTypes)
            {
                Destroy(this.gameObject.GetComponent(type)); // 추가했던 스크립트를 제거
            }
            scriptTypes.Clear(); // 리스트 비우기
        }
        //pool.Release(this.gameObject);
    }

    private void Start()
    {
        // 필요 시 초기화용 코드 작성 가능
    }

    private void OnEnable() // 풀링 시스템을 다루게 되면 OnEnable을 이용해 효율적으로 투사체들을 관리할 수 있다
    {
        //Invoke("RemoveAllAddedScripts", 5f); // 일정 시간 뒤 반환
    }
}
