using UnityEditor.Animations;
using UnityEngine;

//���� ������ ����, �Ӹ��� �ȵ��ư��� �ñ⿡ ��¦��°ɷ�. ������ �߿��� ��ũ��Ʈ �� �ϳ���� ��
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
