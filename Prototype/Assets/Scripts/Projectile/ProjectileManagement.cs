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

        if (tag == "Enemy")
        {
            projectile.GetComponent<Projectile>().SetMoveSpeed(moveSpeed * StatManager.instance.e_projectileSpeedMultify); // �̵� �ӵ� ����

        }
    }

    // �ִϸ��̼� ��� �߻�ü ����
    public void SetProjectileData(ref GameObject projectile, RuntimeAnimatorController animCtrl,
        float moveSpeed, float damage, float lifeTime, string tag)
    {
        projectile.GetComponent<Animator>().runtimeAnimatorController = animCtrl; // �ִϸ��̼� ��Ʈ�ѷ� ����
        projectile.GetComponent<Projectile>().SetMoveSpeed(moveSpeed); // �̵� �ӵ� ����
        projectile.GetComponent<Projectile>().SetDamage(damage); // ������ ����
        projectile.GetComponent<Projectile>().SetLifeTime(lifeTime); // ���� �ð� ����
        projectile.transform.tag = tag; // �±� ����

        if(tag == "Enemy")
        {
            projectile.GetComponent<Projectile>().SetMoveSpeed(moveSpeed * StatManager.instance.e_projectileSpeedMultify); // �̵� �ӵ� ����

        }
    }
}
