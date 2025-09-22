#if UNITY_EDITOR
using UnityEditor.Animations;
#endif
using UnityEngine;

//지금 하지는 말자, 머리가 안돌아가는 시기에 깔짝대는걸로. 하지만 중요한 스크립트 중 하나라고 봄
public class ProjectileManagement
{
    // 스프라이트 기반 발사체 세팅
    public void SetProjectileData(ref GameObject projectile, Sprite sprite,
        float moveSpeed, float damage, float lifeTime, string tag)
    {
        projectile.GetComponent<SpriteRenderer>().sprite = sprite; // 발사체 외형 설정
        projectile.GetComponent<Projectile>().SetMoveSpeed(moveSpeed); // 이동 속도 설정
        projectile.GetComponent<Projectile>().SetDamage(damage); // 데미지 설정
        projectile.GetComponent<Projectile>().SetLifeTime(lifeTime); // 생존 시간 설정
        projectile.transform.tag = tag; // 태그 설정 (Player / Enemy 등)

        if (tag == "Enemy")
        {
            projectile.GetComponent<Projectile>().SetMoveSpeed(moveSpeed * StatManager.instance.e_projectileSpeedMultify); // 이동 속도 설정

        }
    }

    // 애니메이션 기반 발사체 세팅
    public void SetProjectileData(ref GameObject projectile, RuntimeAnimatorController animCtrl,
        float moveSpeed, float damage, float lifeTime, string tag)
    {
        projectile.GetComponent<Animator>().runtimeAnimatorController = animCtrl; // 애니메이션 컨트롤러 설정
        projectile.GetComponent<Projectile>().SetMoveSpeed(moveSpeed); // 이동 속도 설정
        projectile.GetComponent<Projectile>().SetDamage(damage); // 데미지 설정
        projectile.GetComponent<Projectile>().SetLifeTime(lifeTime); // 생존 시간 설정
        projectile.transform.tag = tag; // 태그 설정

        if(tag == "Enemy")
        {
            projectile.GetComponent<Projectile>().SetMoveSpeed(moveSpeed * StatManager.instance.e_projectileSpeedMultify); // 이동 속도 설정

        }
    }
}
