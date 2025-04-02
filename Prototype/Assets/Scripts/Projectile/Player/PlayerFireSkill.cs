using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFireSkill : PlayerEffect
{
    ObjectInteration interation;
    const float attackDelay = 0.5f;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        interation = new ObjectInteration();

        StartCoroutine(FireDamaging());
    }


    IEnumerator FireDamaging()
    {
        for(int i = 0; i < 5; i++)
        {
            float colliderRadius = GetComponent<CircleCollider2D>().radius;
            float objectScale = transform.localScale.x;
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, colliderRadius * objectScale);
            playerDamage = player.attackStats.damage;
            EnemyAttack(cols);
            yield return new WaitForSeconds(attackDelay);
            
        }
    }

    void EnemyAttack(Collider2D[] objects)
    {
        foreach(Collider2D col in objects)
        {
            //Ȯ���ϰ� �� �迭�� �� �ɷ��� ��, ��󿡰� ���ظ� �ش�.
            if(col.transform.tag == "Enemy" && col.TryGetComponent(out Character enemy))
            {
                interation.SendDamage(ref enemy, playerDamage);
                Debug.Log("�ҼӼ����� ������ " + playerDamage + "����");
            }

        }
    }
}
