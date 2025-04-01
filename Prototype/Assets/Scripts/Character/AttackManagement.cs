using UnityEngine;

public class AttackManagement
{
    public void ShootBulletRotate(ref GameObject projectile,
        Transform shootTransform, float rotateZ)
    {
        projectile.transform.position = shootTransform.position;        //�߻�ü ��ġ ����
        projectile.transform.rotation = shootTransform.rotation;        //�߻�ü ȸ�� �� ����
        projectile.transform.Rotate(0, 0, rotateZ);
    }
}
