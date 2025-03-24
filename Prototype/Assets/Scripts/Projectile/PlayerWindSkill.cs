using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWindSkill : PlayerEffect
{
    int enemyBulletCount = 0;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        DestroyAndAttack();
    }

    void DestroyAndAttack()
    {
        transform.parent.GetComponent<Player>();
        Destroy(this.gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //즉, 발사체이면서, 태그 값이 적으로 분류되면 해당 오브젝트 삭제
        if(collision.transform.tag == "Enemy" && 
            collision.gameObject.GetComponent<Projectile>() == true)
        {
            //현재는 바로 삭제하고 끝나지만, 핵심 우선순위 개발이 끝나면 기획서 대로 구현 예정
            Destroy(collision.gameObject);
            enemyBulletCount++;
        }

    }
}