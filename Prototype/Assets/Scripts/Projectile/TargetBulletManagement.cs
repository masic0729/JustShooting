using System.Collections.Generic;
using UnityEngine;

//todo 윈드 불렛에 버그 확인. 얼음에도 있을 것 같고, 얼음은 없었던 것 같지만 한번 더 확인할 것. 인덱스를 벗어났다는 이슈

public class TargetBulletManagement
{
    GameObject targetObject; // 현재 탐지된 타겟 오브젝트
    Vector3[] targetObjectsVec; // 타겟 후보들의 위치 벡터 배열

    //캐릭터가 원하는 대상의 태그를 불러와 관련 캐릭터들을 탐지해내는 기능. 해당 함수는 보통 적이 사용한다.
    public GameObject TargetSearching(string targetTag, bool wantBoss)
    {
        GameObject[] instance = GameObject.FindGameObjectsWithTag(targetTag);

        List<GameObject> targetCharacters = new List<GameObject>();
        for (int i = 0; i < instance.Length; i++)
        {
            if (instance[i].GetComponent<Character>() != null)
            {
                targetCharacters.Add(instance[i]);
            }
        }

        //적이 사용하는 타겟은 플레이어가 유일하므로, 아래 코드들은 의미없기에 바로 리턴
        if (targetCharacters[0].transform.tag == "Player")
        {
            targetObject = targetCharacters[0];
            return targetObject;
        }

        if (targetCharacters == null)
        {
            return null;
        }

        float targetDistance, targetObjectDistance = 200f;
        targetObjectsVec = new Vector3[targetCharacters.Count];

        for (int i = 0; i < targetObjectsVec.Length; i++)
        {
            targetObjectsVec[i] = targetCharacters[i].transform.position;
            targetDistance = (targetObjectsVec[i] - instance[0].transform.position).magnitude; //todo

            //그 대상이 만약 보스라면 관계 없이 탐지 종료
            if (wantBoss == true && targetCharacters[i].GetComponent<Enemy>().GetIsBoss() == true)
            {
                targetObject = targetCharacters[i];
                break;
            }
            //총알과 적의 거리 비교 후 오브젝트 및 거리 갱신. 또한 화면 기준 아직 벗어나지 않은 적을 기준으로 탐지함.
            else if (targetObjectDistance > targetDistance &&
                Mathf.Abs(targetObjectsVec[i].x) <= 11 &&
                Mathf.Abs(targetObjectsVec[i].y) <= 5)
            {
                targetObjectDistance = targetDistance;
                targetObject = targetCharacters[i];
            }
        }
        return targetObject;
    }

    // 발사체 위치 기준으로 타겟 탐색 (보통 플레이어가 사용하는 방식)
    public GameObject TargetSearching(ref GameObject thisProjectile, string targetTag, bool wantBoss = false)
    {
        if(targetTag == "Player")
        {
            GameObject targetPlayer = GameObject.Find(targetTag);
            return targetPlayer;
        }
        GameObject[] instance = GameObject.FindGameObjectsWithTag(targetTag);
        

        List<GameObject> targetCharacters = new List<GameObject>();

        for (int i = 0; i < instance.Length; i++)
        {
            if (instance != null && instance[i].GetComponent<Character>() != null)
            {
                targetCharacters.Add(instance[i]);
            }
        }

        if (targetCharacters == null)
        {
            return null;
        }

        float targetDistance, targetObjectDistance = 200f;
        targetObjectsVec = new Vector3[targetCharacters.Count];

        for (int i = 0; i < targetObjectsVec.Length; i++)
        {
            targetObjectsVec[i] = targetCharacters[i].transform.position;
            targetDistance = (targetObjectsVec[i] - thisProjectile.transform.position).magnitude;

            //그 대상이 만약 보스라면 관계 없이 탐지 종료
            if (wantBoss == true && targetCharacters[i].GetComponent<Enemy>().GetIsBoss() == true && targetCharacters[i].GetComponent<Enemy>().GetCharacterColEnable() == true)
            {
                targetObject = targetCharacters[i];
                break;
            }
            // 거리 비교 및 화면 내 여부 확인 후 타겟 결정
            if (targetObjectDistance > targetDistance &&
                Mathf.Abs(targetObjectsVec[i].x) <= 11 &&
                Mathf.Abs(targetObjectsVec[i].y) <= 5 &&
                targetCharacters[i].GetComponent<Enemy>().GetIsBoss() == false)
            {
                targetObjectDistance = targetDistance;
                targetObject = targetCharacters[i];
            }
        }

        thisProjectile.GetComponent<Bullet>().InitRotateValue(); // 회전값 초기화
        return targetObject;
    }

    public void TunningObject(ref GameObject thisProjectile, ref GameObject targetObject, ref float rotateValue, float rotateAddValue)
    {
        Vector3 directionToTarget;

        // 타겟이 있을 경우 타겟 방향으로 계산
        if (targetObject != null)
        {
            directionToTarget = (targetObject.transform.position - thisProjectile.transform.position).normalized;
        }
        else
        {
            // 타겟이 없으면 오른쪽 방향을 향함
            directionToTarget = (new Vector3(11, 0, 0) - thisProjectile.transform.position).normalized;
        }

        rotateValue += rotateAddValue; // 회전 민감도 증가

        Vector3 currentForward = thisProjectile.transform.up;
        float angle = Vector3.SignedAngle(currentForward, directionToTarget, Vector3.forward);

        // 회전 적용
        if (angle != 0)
        {
            // 회전 속도에 따라 유도 탄환처럼 회전
            Quaternion rotation = Quaternion.Euler(0, 0, angle * rotateValue * Time.deltaTime);
            thisProjectile.transform.rotation = rotation * thisProjectile.transform.rotation;
        }
    }

    public void DirectTargetObject(ref GameObject thisProjectile, ref GameObject targetObject)
    {
        float angle;

        // 타겟이 없을 경우 오른쪽 방향을 바라봄
        if (targetObject == null)
        {
            angle = Mathf.Atan2(0f, 11f - thisProjectile.transform.position.x) * Mathf.Rad2Deg;
            thisProjectile.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
        else
        {
            // 타겟 위치를 기준으로 발사체 방향을 설정
            angle = Mathf.Atan2(targetObject.transform.position.y - thisProjectile.transform.position.y,
                                 targetObject.transform.position.x - thisProjectile.transform.position.x) * Mathf.Rad2Deg;
            thisProjectile.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }
}
