using System.Collections.Generic;
using UnityEngine;

//todo ���� �ҷ��� ���� Ȯ��. �������� ���� �� ����, ������ ������ �� ������ �ѹ� �� Ȯ���� ��. �ε����� ����ٴ� �̽�

public class TargetBulletManagement
{
    GameObject targetObject; // ���� Ž���� Ÿ�� ������Ʈ
    Vector3[] targetObjectsVec; // Ÿ�� �ĺ����� ��ġ ���� �迭

    //ĳ���Ͱ� ���ϴ� ����� �±׸� �ҷ��� ���� ĳ���͵��� Ž���س��� ���. �ش� �Լ��� ���� ���� ����Ѵ�.
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

        //���� ����ϴ� Ÿ���� �÷��̾ �����ϹǷ�, �Ʒ� �ڵ���� �ǹ̾��⿡ �ٷ� ����
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

            //�� ����� ���� ������� ���� ���� Ž�� ����
            if (wantBoss == true && targetCharacters[i].GetComponent<Enemy>().GetIsBoss() == true)
            {
                targetObject = targetCharacters[i];
                break;
            }
            //�Ѿ˰� ���� �Ÿ� �� �� ������Ʈ �� �Ÿ� ����. ���� ȭ�� ���� ���� ����� ���� ���� �������� Ž����.
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

    // �߻�ü ��ġ �������� Ÿ�� Ž�� (���� �÷��̾ ����ϴ� ���)
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

            //�� ����� ���� ������� ���� ���� Ž�� ����
            if (wantBoss == true && targetCharacters[i].GetComponent<Enemy>().GetIsBoss() == true && targetCharacters[i].GetComponent<Enemy>().GetCharacterColEnable() == true)
            {
                targetObject = targetCharacters[i];
                break;
            }
            // �Ÿ� �� �� ȭ�� �� ���� Ȯ�� �� Ÿ�� ����
            if (targetObjectDistance > targetDistance &&
                Mathf.Abs(targetObjectsVec[i].x) <= 11 &&
                Mathf.Abs(targetObjectsVec[i].y) <= 5 &&
                targetCharacters[i].GetComponent<Enemy>().GetIsBoss() == false)
            {
                targetObjectDistance = targetDistance;
                targetObject = targetCharacters[i];
            }
        }

        thisProjectile.GetComponent<Bullet>().InitRotateValue(); // ȸ���� �ʱ�ȭ
        return targetObject;
    }

    public void TunningObject(ref GameObject thisProjectile, ref GameObject targetObject, ref float rotateValue, float rotateAddValue)
    {
        Vector3 directionToTarget;

        // Ÿ���� ���� ��� Ÿ�� �������� ���
        if (targetObject != null)
        {
            directionToTarget = (targetObject.transform.position - thisProjectile.transform.position).normalized;
        }
        else
        {
            // Ÿ���� ������ ������ ������ ����
            directionToTarget = (new Vector3(11, 0, 0) - thisProjectile.transform.position).normalized;
        }

        rotateValue += rotateAddValue; // ȸ�� �ΰ��� ����

        Vector3 currentForward = thisProjectile.transform.up;
        float angle = Vector3.SignedAngle(currentForward, directionToTarget, Vector3.forward);

        // ȸ�� ����
        if (angle != 0)
        {
            // ȸ�� �ӵ��� ���� ���� źȯó�� ȸ��
            Quaternion rotation = Quaternion.Euler(0, 0, angle * rotateValue * Time.deltaTime);
            thisProjectile.transform.rotation = rotation * thisProjectile.transform.rotation;
        }
    }

    public void DirectTargetObject(ref GameObject thisProjectile, ref GameObject targetObject)
    {
        float angle;

        // Ÿ���� ���� ��� ������ ������ �ٶ�
        if (targetObject == null)
        {
            angle = Mathf.Atan2(0f, 11f - thisProjectile.transform.position.x) * Mathf.Rad2Deg;
            thisProjectile.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
        else
        {
            // Ÿ�� ��ġ�� �������� �߻�ü ������ ����
            angle = Mathf.Atan2(targetObject.transform.position.y - thisProjectile.transform.position.y,
                                 targetObject.transform.position.x - thisProjectile.transform.position.x) * Mathf.Rad2Deg;
            thisProjectile.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }
}
