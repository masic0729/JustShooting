using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBulletManagement : MonoBehaviour
{
    GameObject targetObject;
    Vector3[] targetObjectsVec;
    float rotationSpeed = 4f; //미사일이 회전한도 상승 값. 미사일일 특정 오브젝트를 추격하는 시간이 길어질 수록 값이 오르게 됨


    //캐릭터가 원하는 대상의 태그를 불러와 관련 캐릭터들을 탐지해내는 기능. 해당 함수는 보통 적이 사용한다.
    public GameObject TargetSearching(string targetTag)
    {
        float targetDistance, targetObjectDistance = 200f;
        GameObject[] instanceCharacters = GameObject.FindGameObjectsWithTag(targetTag);

        targetObjectsVec = new Vector3[instanceCharacters.Length];

        for (int i = 0; i < targetObjectsVec.Length; i++)
        {
            targetObjectsVec[i] = instanceCharacters[i].GetComponent<Character>().transform.position;
            targetDistance = (targetObjectsVec[i] - this.transform.position).magnitude;
            //총알과 적의 거리 비교 후 오브젝트 및 거리 갱신. 또한 화면 기준 아직 벗어나지 않은 적을 기준으로 탐지
            if (targetObjectDistance > targetDistance &&
                Mathf.Abs(targetObjectsVec[i].x) <= 11 &&
                Mathf.Abs(targetObjectsVec[i].y) <= 5)
            {
                targetObjectDistance = targetDistance;
                targetObject = instanceCharacters[i];
            }
        }
        return targetObject;
    }

    //미사일의 타겟 탐지를 오버로딩한 것. 보통 플레이어가 사용한다
    public GameObject TargetSearching(string targetTag, bool wantBoss)
    {
        float targetDistance, targetObjectDistance = 200f;
        GameObject[] instanceCharacters = GameObject.FindGameObjectsWithTag(targetTag);
        
        targetObjectsVec = new Vector3[instanceCharacters.Length];

        for (int i = 0; i < targetObjectsVec.Length; i++)
        {

            targetObjectsVec[i] = instanceCharacters[i].GetComponent<Character>().transform.position;
            targetDistance = (targetObjectsVec[i] - this.transform.position).magnitude;
            //총알과 적의 거리 비교 후 오브젝트 및 거리 갱신. 또한 화면 기준 아직 벗어나지 않은 적을 기준으로 탐지
            if (targetObjectDistance > targetDistance &&
                Mathf.Abs(targetObjectsVec[i].x) <= 11 &&
                Mathf.Abs(targetObjectsVec[i].y) <= 5)
            {
                //보스를 원하지 않거나, 그 대상의 보스라면 일단 최신화하는 부분
                if (wantBoss == false || instanceCharacters[i].GetComponent<Enemy>().GetIsBoss() == true)
                {
                    targetObjectDistance = targetDistance;
                    targetObject = instanceCharacters[i];
                }
                //둘 다 아니면 패스
                else
                    continue;
                
            }
        }
        return targetObject;
    }

    public void TunningObject(ref GameObject thisProjectile, ref GameObject targetObject, float rotationSpeedAddValue)
    {
        if(targetObject != null)
        {
            Vector3 directionToTarget = (targetObject.transform.position - transform.position).normalized;
            Vector3 currentForward = transform.up;
            float angle = Vector3.SignedAngle(currentForward, directionToTarget, Vector3.forward);

            // 회전 적용
            if (angle != 0)
            {
                // 회전각을 더하는 이유는 일정한 각도로 빠르게 설정하면 유도탄 답지 않은 연출이 되며, 낮게 주면 오브젝트를 맞추지 못하고 공전하는듯한 현상을 보인다.
                // 이로 인해 한번 타깃을 정한 오브젝트의 추적이 길어질 수록 회전 한계값을 늘려 결국 적중하게 만듦


                Quaternion rotation = Quaternion.Euler(0, 0, angle * rotationSpeed * Time.deltaTime);
                thisProjectile.transform.rotation = rotation * transform.rotation;

                rotationSpeed += rotationSpeedAddValue;
            }
        }
    }


}
