using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todo 윈드 불렛에 버그 확인. 얼음에도 있을 것 같고, 얼음은 없었던 것 같지만 한번 더 확인할 것. 인덱스를 벗어났다는 이슈

public class TargetBulletManagement
{
    GameObject targetObject;
    Vector3[] targetObjectsVec;
    float rotationSpeed = 4f; //미사일이 회전한도 상승 값. 미사일일 특정 오브젝트를 추격하는 시간이 길어질 수록 값이 오르게 됨


    //캐릭터가 원하는 대상의 태그를 불러와 관련 캐릭터들을 탐지해내는 기능. 해당 함수는 보통 적이 사용한다.
    public GameObject TargetSearching(string targetTag, bool wantBoss)
    {
        GameObject[] instance = GameObject.FindGameObjectsWithTag(targetTag);

        List<GameObject> targetCharacters = new List<GameObject>();
        for(int i = 0; i < instance.Length; i++)
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

        if(targetCharacters == null)
        {
            return null;
        }

        float targetDistance, targetObjectDistance = 200f;
        targetObjectsVec = new Vector3[targetCharacters.Count];

        for (int i = 0; i < targetObjectsVec.Length; i++)
        {

            //targetObjectsVec[i] = instance[i].GetComponent<Character>().transform.position;
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

    public GameObject TargetSearching(ref GameObject thisProjectile, string targetTag, bool wantBoss)
    {
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

            //targetObjectsVec[i] = instance[i].GetComponent<Character>().transform.position;
            targetObjectsVec[i] = targetCharacters[i].transform.position;
            targetDistance = (targetObjectsVec[i] - thisProjectile.transform.position).magnitude;

            //그 대상이 만약 보스라면 관계 없이 탐지 종료
            if (wantBoss == true && targetCharacters[i].GetComponent<Enemy>().GetIsBoss() == true)
            {
                targetObject = targetCharacters[i];
                break;
            }
            //총알과 적의 거리 비교 후 오브젝트 및 거리 갱신. 또한 화면 기준 아직 벗어나지 않은 적을 기준으로 탐지함.
            if (targetObjectDistance > targetDistance &&
                Mathf.Abs(targetObjectsVec[i].x) <= 11 &&
                Mathf.Abs(targetObjectsVec[i].y) <= 5)
            {
                targetObjectDistance = targetDistance;
                targetObject = targetCharacters[i];
            }
        }
        thisProjectile.GetComponent<Bullet>().InitRotateValue();
        return targetObject;
    }
    /*
        //미사일의 타겟 탐지를 오버로딩한 것. 보통 플레이어가 사용한다
        public GameObject TargetSearching(string targetTag, bool wantBoss)
        {
            bool isBossFind = false;
            float targetDistance, targetObjectDistance = 200f;
            GameObject[] instanceCharacters = GameObject.FindGameObjectsWithTag(targetTag);

            targetObjectsVec = new Vector3[instanceCharacters.Length];

            for (int i = 0; i < targetObjectsVec.Length; i++)
            {

                //targetObjectsVec[i] = instance[i].GetComponent<Character>().transform.position;
                targetObjectsVec[i] = instanceCharacters[i].transform.position;
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
        }*/

    public void TunningObject(ref GameObject thisProjectile, ref GameObject targetObject, ref float rotateValue, float rotateAddValue)
    {
        /*Vector3 directionToTarget;
        if (targetObject != null)
        {
            directionToTarget = (targetObject.transform.position - transform.position).normalized;
            Vector3 currentForward = transform.up;
            float angle = Vector3.SignedAngle(currentForward, directionToTarget, Vector3.forward);

            // 회전 적용
            if (angle != 0)
            {
                // 회전각을 더하는 이유는 일정한 각도로 빠르게 설정하면 유도탄 답지 않은 연출이 되며, 낮게 주면 오브젝트를 맞추지 못하고 공전하는듯한 현상을 보인다.
                // 이로 인해 한번 타깃을 정한 오브젝트의 추적이 길어질 수록 회전 한계값을 늘려 결국 적중하게 만듦
                Quaternion rotation = Quaternion.Euler(0, 0, angle * rotateValue * Time.deltaTime);
                thisProjectile.transform.rotation = rotation * transform.rotation;
            }
        }*/

        //Vector3 directionToTarget;
        //if (targetObject != null)
        //{
        //    directionToTarget = (targetObject.transform.position - thisProjectile.transform.position).normalized;
        //    Vector3 currentForward = thisProjectile.transform.up;
        //    float angle = Vector3.SignedAngle(currentForward, directionToTarget, Vector3.forward);

        //    // 회전 적용
        //    if (angle != 0)
        //    {
        //        // 회전각을 더하는 이유는 일정한 각도로 빠르게 설정하면 유도탄 답지 않은 연출이 되며, 낮게 주면 오브젝트를 맞추지 못하고 공전하는듯한 현상을 보인다.
        //        // 이로 인해 한번 타깃을 정한 오브젝트의 추적이 길어질 수록 회전 한계값을 늘려 결국 적중하게 만듦
        //        Quaternion rotation = Quaternion.Euler(0, 0, angle * rotateValue * Time.deltaTime);
        //        thisProjectile.transform.rotation = rotation * thisProjectile.transform.rotation;
        //    }
        //}

        Vector3 directionToTarget;
        if (targetObject != null)
        {
            directionToTarget = (targetObject.transform.position - thisProjectile.transform.position).normalized;
            //void
        }
        else
        {
            directionToTarget = (new Vector3(11, 0, 0) - thisProjectile.transform.position).normalized;
            //rotateValue = 0.3f; //사실 이거 좋은 코드는 아님. 일단 임시 방편으로 한 것이고, 함수를 따로 더 만들어서 해야함
        }
        rotateValue += rotateAddValue; //사실 이거 좋은 코드는 아님. 일단 임시 방편으로 한 것이고, 함수를 따로 더 만들어서 해야함

        Vector3 currentForward = thisProjectile.transform.up;
        float angle = Vector3.SignedAngle(currentForward, directionToTarget, Vector3.forward);

        // 회전 적용
        if (angle != 0)
        {
            // 회전각을 더하는 이유는 일정한 각도로 빠르게 설정하면 유도탄 답지 않은 연출이 되며, 낮게 주면 오브젝트를 맞추지 못하고 공전하는듯한 현상을 보인다.
            // 이로 인해 한번 타깃을 정한 오브젝트의 추적이 길어질 수록 회전 한계값을 늘려 결국 적중하게 만듦
            Quaternion rotation = Quaternion.Euler(0, 0, angle * rotateValue * Time.deltaTime);
            thisProjectile.transform.rotation = rotation * thisProjectile.transform.rotation;
        }

        /*
        if(targetObject != null)
        {
            directionToTarget = (targetObject.transform.position - transform.position).normalized;

        }
        else
        {
            directionToTarget = (new Vector3(11, 0, 0) - transform.position).normalized;
        }
        Vector3 currentForward = transform.up;
        float angle = Vector3.SignedAngle(currentForward, directionToTarget, Vector3.forward);

        // 회전 적용
        if (angle != 0)
        {
            // 회전각을 더하는 이유는 일정한 각도로 빠르게 설정하면 유도탄 답지 않은 연출이 되며, 낮게 주면 오브젝트를 맞추지 못하고 공전하는듯한 현상을 보인다.
            // 이로 인해 한번 타깃을 정한 오브젝트의 추적이 길어질 수록 회전 한계값을 늘려 결국 적중하게 만듦


            Quaternion rotation = Quaternion.Euler(0, 0, angle * rotationSpeed * Time.deltaTime);
            thisProjectile.transform.rotation = rotation * transform.rotation;

            rotationSpeed += rotateValue;
        }*/
    }

    public void DirectTargetObject(ref GameObject thisProjectile, ref GameObject targetObject)
    {
        /*float angle = Mathf.Atan2(targetObject.transform.position.y - thisProjectile.transform.position.y, targetObject.transform.position.x - thisProjectile.transform.position.x) * Mathf.Rad2Deg;
        thisProjectile.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);*/
        float angle;
        if (targetObject == null)
        {
            angle = Mathf.Atan2(0f, 11f - thisProjectile.transform.position.x) * Mathf.Rad2Deg;
            thisProjectile.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
        else
        {
            angle = Mathf.Atan2(targetObject.transform.position.y - thisProjectile.transform.position.y, targetObject.transform.position.x - thisProjectile.transform.position.x) * Mathf.Rad2Deg;
            thisProjectile.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }
}
