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
        //��, �߻�ü�̸鼭, �±� ���� ������ �з��Ǹ� �ش� ������Ʈ ����
        if (collision.transform.tag == "Enemy" &&
            collision.gameObject.GetComponent<Projectile>())
        {
            //����� �ٷ� �����ϰ� ��������, �ٽ� �켱���� ������ ������ ��ȹ�� ��� ���� ����
            Destroy(collision.gameObject);
            enemyBulletCount++;
        }
    }
}