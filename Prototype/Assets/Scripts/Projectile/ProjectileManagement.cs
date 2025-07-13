#if UNITY_EDITOR
using UnityEditor.Animations;
#endif
using UnityEngine;

//���� ������ ����, �Ӹ��� �ȵ��ư��� �ñ⿡ ��¦��°ɷ�. ������ �߿��� ��ũ��Ʈ �� �ϳ���� ��
public class ProjectileManagement
{
    // ��������Ʈ ��� �߻�ü ����
    public void SetProjectileData(ref GameObject projectile, Sprite sprite,
        float moveSpeed, float damage, float lifeTime, string tag)
    {
        projectile.GetComponent<SpriteRenderer>().sprite = sprite; // �߻�ü ���� ����
        projectile.GetComponent<Projectile>().SetMoveSpeed(moveSpeed); // �̵� �ӵ� ����
        projectile.GetComponent<Projectile>().SetDamage(damage); // ������ ����
        projectile.GetComponent<Projectile>().SetLifeTime(lifeTime); // ���� �ð� ����
        projectile.transform.tag = tag; // �±� ���� (Player / Enemy ��)
    }

    // �ִϸ��̼� ��� �߻�ü ����
    public void SetProjectileData(ref GameObject projectile, RuntimeAnimatorController animCtrl,
        float moveSpeed, float damage, float lifeTime, string tag)
    {
        projectile.GetComponent<Animator>().runtimeAnimatorController = animCtrl; // �ִϸ��̼� ��Ʈ�ѷ� ����
        projectile.GetComponent<Projectile>().SetMoveSpeed(moveSpeed); // �̵� �ӵ� ����
        projectile.GetComponent<Projectile>().SetDamage(damage + StatManager.instance.p_damageFromCard); // ������ ����
        projectile.GetComponent<Projectile>().SetLifeTime(lifeTime); // ���� �ð� ����
        projectile.transform.tag = tag; // �±� ����
    }
}
