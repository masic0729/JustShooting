using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todo ���� �ҷ��� ���� Ȯ��. �������� ���� �� ����, ������ ������ �� ������ �ѹ� �� Ȯ���� ��. �ε����� ����ٴ� �̽�

public class TargetBulletManagement
{
    GameObject targetObject;
    Vector3[] targetObjectsVec;
    float rotationSpeed = 4f; //�̻����� ȸ���ѵ� ��� ��. �̻����� Ư�� ������Ʈ�� �߰��ϴ� �ð��� ����� ���� ���� ������ ��


    //ĳ���Ͱ� ���ϴ� ����� �±׸� �ҷ��� ���� ĳ���͵��� Ž���س��� ���. �ش� �Լ��� ���� ���� ����Ѵ�.
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
        //���� ����ϴ� Ÿ���� �÷��̾ �����ϹǷ�, �Ʒ� �ڵ���� �ǹ̾��⿡ �ٷ� ����

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

            //�� ����� ���� ������� ���� ���� Ž�� ����
            if (wantBoss == true && targetCharacters[i].GetComponent<Enemy>().GetIsBoss() == true)
            {
                targetObject = targetCharacters[i];
                break;
            }
            //�Ѿ˰� ���� �Ÿ� �� �� ������Ʈ �� �Ÿ� ����. ���� ȭ�� ���� ���� ����� ���� ���� �������� Ž����.
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
        //�̻����� Ÿ�� Ž���� �����ε��� ��. ���� �÷��̾ ����Ѵ�
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
        }*/

    public void TunningObject(ref GameObject thisProjectile, ref GameObject targetObject, ref float rotateValue, float rotateAddValue)
    {
        /*Vector3 directionToTarget;
        if (targetObject != null)
        {
            directionToTarget = (targetObject.transform.position - transform.position).normalized;
            Vector3 currentForward = transform.up;
            float angle = Vector3.SignedAngle(currentForward, directionToTarget, Vector3.forward);

            // ȸ�� ����
            if (angle != 0)
            {
                // ȸ������ ���ϴ� ������ ������ ������ ������ �����ϸ� ����ź ���� ���� ������ �Ǹ�, ���� �ָ� ������Ʈ�� ������ ���ϰ� �����ϴµ��� ������ ���δ�.
                // �̷� ���� �ѹ� Ÿ���� ���� ������Ʈ�� ������ ����� ���� ȸ�� �Ѱ谪�� �÷� �ᱹ �����ϰ� ����
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

        //    // ȸ�� ����
        //    if (angle != 0)
        //    {
        //        // ȸ������ ���ϴ� ������ ������ ������ ������ �����ϸ� ����ź ���� ���� ������ �Ǹ�, ���� �ָ� ������Ʈ�� ������ ���ϰ� �����ϴµ��� ������ ���δ�.
        //        // �̷� ���� �ѹ� Ÿ���� ���� ������Ʈ�� ������ ����� ���� ȸ�� �Ѱ谪�� �÷� �ᱹ �����ϰ� ����
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
            //rotateValue = 0.3f; //��� �̰� ���� �ڵ�� �ƴ�. �ϴ� �ӽ� �������� �� ���̰�, �Լ��� ���� �� ���� �ؾ���
        }
        rotateValue += rotateAddValue; //��� �̰� ���� �ڵ�� �ƴ�. �ϴ� �ӽ� �������� �� ���̰�, �Լ��� ���� �� ���� �ؾ���

        Vector3 currentForward = thisProjectile.transform.up;
        float angle = Vector3.SignedAngle(currentForward, directionToTarget, Vector3.forward);

        // ȸ�� ����
        if (angle != 0)
        {
            // ȸ������ ���ϴ� ������ ������ ������ ������ �����ϸ� ����ź ���� ���� ������ �Ǹ�, ���� �ָ� ������Ʈ�� ������ ���ϰ� �����ϴµ��� ������ ���δ�.
            // �̷� ���� �ѹ� Ÿ���� ���� ������Ʈ�� ������ ����� ���� ȸ�� �Ѱ谪�� �÷� �ᱹ �����ϰ� ����
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

        // ȸ�� ����
        if (angle != 0)
        {
            // ȸ������ ���ϴ� ������ ������ ������ ������ �����ϸ� ����ź ���� ���� ������ �Ǹ�, ���� �ָ� ������Ʈ�� ������ ���ϰ� �����ϴµ��� ������ ���δ�.
            // �̷� ���� �ѹ� Ÿ���� ���� ������Ʈ�� ������ ����� ���� ȸ�� �Ѱ谪�� �÷� �ᱹ �����ϰ� ����


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
