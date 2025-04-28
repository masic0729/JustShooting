using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWindSkill : PlayerEffect
{
    public GameObject WindSkillBullet;
    int enemyBulletCount = 0;
    public bool isTest;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        
    }

    private void OnDestroy()
    {
        DestroyAndAttack();
    }

    void DestroyAndAttack()
    {
        //windBullet load
        if(isTest == true)
            enemyBulletCount += 20;

        player.WindSkill(WindSkillBullet, enemyBulletCount);

        Destroy(this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //즉, 발사체이면서, 태그 값이 적으로 분류되면 해당 오브젝트 삭제
        if (collision.transform.tag == "Enemy" &&
            collision.gameObject.GetComponent<Projectile>())
        {
            //현재는 바로 삭제하고 끝나지만, 핵심 우선순위 개발이 끝나면 기획서 대로 구현 예정
            Destroy(collision.gameObject);
            enemyBulletCount++;
        }
    }
}