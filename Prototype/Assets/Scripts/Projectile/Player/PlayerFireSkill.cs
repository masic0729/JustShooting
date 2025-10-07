using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFireSkill : PlayerEffect
{
    ObjectInteraction interation;
    const float attackDelay = 0.5f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        interation = new ObjectInteraction();
        //transform.Rotate(90, 0, 0);
        playerDamage *= player.attackStats.damage;

        StartCoroutine(FireDamaging());
        AudioManager.Instance.PlaySFX("FireSkill");

    }

    IEnumerator FireDamaging()
    {
        for(int i = 0; i < 40; i++)
        {
            float colliderRadius = GetComponent<CircleCollider2D>().radius;
            float objectScale = transform.localScale.x;
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, colliderRadius * objectScale);
            EnemyAttack(cols);
            yield return new WaitForSeconds(attackDelay / 2f);
            
        }
    }

    protected override void EnemyAttack(Collider2D[] objects)
    {
        bool isHit = false;
        foreach(Collider2D col in objects)
        {
            //Ȯ���ϰ� �� �迭�� �� �ɷ��� ��, ��󿡰� ���ظ� �ش�.
            if(col.transform.tag == "Enemy" && col.TryGetComponent(out Character enemy))
            {
                interation.SendDamage(ref enemy, playerDamage / 2f);
                Debug.Log("�ҼӼ����� ������ " + playerDamage + "����");
                Vector2 randPos = new Vector2(Random.Range(-0.7f, 0.5f), Random.Range(-1f, 0.0f));
                Vector2 hitPos = enemy.transform.position; // ��ų �߽� ���� ��ġ�� ����Ʈ ����

                hitPos += randPos;
                ParticleManager.Instance.PlayEffect("Fire", hitPos);

                hitPos = enemy.transform.position; // ��ų �߽� ���� ��ġ�� ����Ʈ ����
                randPos = new Vector2(Random.Range(-0.7f, 0.5f), Random.Range(0f, 1f));
                hitPos += randPos;

                ParticleManager.Instance.PlayEffect("Fire", hitPos);
                

                isHit = true;
            }
        }
        if(isHit == true)
        {
            AudioManager.Instance.PlaySFX("EnemyHit");
        }
    }
}
