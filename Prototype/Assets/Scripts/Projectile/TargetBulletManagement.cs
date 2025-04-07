using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBulletManagement : MonoBehaviour
{
    GameObject targetObject;
    Vector3[] targetObjectsVec;
    float rotationSpeed = 4f; //�̻����� ȸ���ѵ� ��� ��. �̻����� Ư�� ������Ʈ�� �߰��ϴ� �ð��� ����� ���� ���� ������ ��


    //ĳ���Ͱ� ���ϴ� ����� �±׸� �ҷ��� ���� ĳ���͵��� Ž���س��� ���. �ش� �Լ��� ���� ���� ����Ѵ�.
    public GameObject TargetSearching(string targetTag)
    {
        float targetDistance, targetObjectDistance = 200f;
        GameObject[] instanceCharacters = GameObject.FindGameObjectsWithTag(targetTag);

        targetObjectsVec = new Vector3[instanceCharacters.Length];

        for (int i = 0; i < targetObjectsVec.Length; i++)
        {
            targetObjectsVec[i] = instanceCharacters[i].GetComponent<Character>().transform.position;
            targetDistance = (targetObjectsVec[i] - this.transform.position).magnitude;
            //�Ѿ˰� ���� �Ÿ� �� �� ������Ʈ �� �Ÿ� ����. ���� ȭ�� ���� ���� ����� ���� ���� �������� Ž��
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

    //�̻����� Ÿ�� Ž���� �����ε��� ��. ���� �÷��̾ ����Ѵ�
    public GameObject TargetSearching(string targetTag, bool wantBoss)
    {
        float targetDistance, targetObjectDistance = 200f;
        GameObject[] instanceCharacters = GameObject.FindGameObjectsWithTag(targetTag);
        
        targetObjectsVec = new Vector3[instanceCharacters.Length];

        for (int i = 0; i < targetObjectsVec.Length; i++)
        {

            targetObjectsVec[i] = instanceCharacters[i].GetComponent<Character>().transform.position;
            targetDistance = (targetObjectsVec[i] - this.transform.position).magnitude;
            //�Ѿ˰� ���� �Ÿ� �� �� ������Ʈ �� �Ÿ� ����. ���� ȭ�� ���� ���� ����� ���� ���� �������� Ž��
            if (targetObjectDistance > targetDistance &&
                Mathf.Abs(targetObjectsVec[i].x) <= 11 &&
                Mathf.Abs(targetObjectsVec[i].y) <= 5)
            {
                //������ ������ �ʰų�, �� ����� ������� �ϴ� �ֽ�ȭ�ϴ� �κ�
                if (wantBoss == false || instanceCharacters[i].GetComponent<Enemy>().GetIsBoss() == true)
                {
                    targetObjectDistance = targetDistance;
                    targetObject = instanceCharacters[i];
                }
                //�� �� �ƴϸ� �н�
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

            // ȸ�� ����
            if (angle != 0)
            {
                // ȸ������ ���ϴ� ������ ������ ������ ������ �����ϸ� ����ź ���� ���� ������ �Ǹ�, ���� �ָ� ������Ʈ�� ������ ���ϰ� �����ϴµ��� ������ ���δ�.
                // �̷� ���� �ѹ� Ÿ���� ���� ������Ʈ�� ������ ����� ���� ȸ�� �Ѱ谪�� �÷� �ᱹ �����ϰ� ����


                Quaternion rotation = Quaternion.Euler(0, 0, angle * rotationSpeed * Time.deltaTime);
                thisProjectile.transform.rotation = rotation * transform.rotation;

                rotationSpeed += rotationSpeedAddValue;
            }
        }
    }


}
