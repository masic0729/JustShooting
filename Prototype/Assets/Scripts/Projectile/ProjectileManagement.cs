using UnityEditor.Animations;
using UnityEngine;

//지금 하지는 말자, 머리가 안돌아가는 시기에 깔짝대는걸로. 하지만 중요한 스크립트 중 하나라고 봄
public class ProjectileManagement
{
    public void SetProjectileData(ref GameObject projectile, Sprite sprite,
        float moveSpeed, float damage, float lifeTime, string tag)
    {
        projectile.GetComponent<SpriteRenderer>().sprite = sprite;
        projectile.GetComponent<Projectile>().SetMoveSpeed(moveSpeed);
        projectile.GetComponent<Projectile>().SetDamage(damage);
        projectile.GetComponent<Projectile>().SetLifeTime(lifeTime);
        projectile.transform.tag = tag;
    }

    public void SetProjectileData(ref GameObject projectile, AnimatorController animCtrl,
        float moveSpeed, float damage, float lifeTime, string tag)
    {
        projectile.GetComponent<Animator>().runtimeAnimatorController = animCtrl;
        projectile.GetComponent<Projectile>().SetMoveSpeed(moveSpeed);
        projectile.GetComponent<Projectile>().SetDamage(damage);
        projectile.GetComponent<Projectile>().SetLifeTime(lifeTime);
        projectile.transform.tag = tag;
    }

}
