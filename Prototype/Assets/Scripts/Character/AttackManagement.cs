using UnityEngine;

public class AttackManagement
{
    public void ShootBulletRotate(ref GameObject projectile,
        Transform shootTransform, float rotateZ)
    {
        projectile.transform.position = shootTransform.position;        //발사체 위치 조정
        projectile.transform.rotation = shootTransform.rotation;        //발사체 회전 값 조정
        projectile.transform.Rotate(0, 0, rotateZ);                     //발사체 회전 적용
    }

    /// <summary>
    /// 2D오브젝트가 목표 방향으로 바라보는(회전)기능 
    /// </summary>
    public void ShootBulletLookAt(ref GameObject bullet, Vector2 target)
    {
        /*Transform targetPos;
        targetPos.position = target;*/
        Vector2 dir = target - (Vector2)bullet.transform.position;

        // 방향 → 각도 (라디안 → 도)
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;

        // Z축 회전 적용 (2D에서는 z축 기준으로 회전)
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}