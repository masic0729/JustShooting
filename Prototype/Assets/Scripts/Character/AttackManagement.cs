using UnityEngine;

public class AttackManagement
{
    public void ShootBulletRotate(ref GameObject projectile,
        Transform shootTransform, float rotateZ)
    {
        projectile.transform.position = shootTransform.position;        //발사체 위치 조정
        projectile.transform.rotation = shootTransform.rotation;        //발사체 회전 값 조정
        projectile.transform.Rotate(0, 0, rotateZ);
    }
}
