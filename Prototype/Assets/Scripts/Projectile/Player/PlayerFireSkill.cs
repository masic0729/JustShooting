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
        AudioManager.Instance.PlaySFX("FireSkill");
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

    protected override void EnemyAttack(Collider2D[] objects)
    {
        bool isHit = false;
        foreach(Collider2D col in objects)
        {
            //Ȯ���ϰ� �� �迭�� �� �ɷ��� ��, ��󿡰� ���ظ� �ش�.
            if(col.transform.tag == "Enemy" && col.TryGetComponent(out Character enemy))
            {
                interation.SendDamage(ref enemy, playerDamage);
                Debug.Log("�ҼӼ����� ������ " + playerDamage + "����");

                GameObject instanceEffect = Resources.Load("Prefabs/PlayX4/EnemyHit", typeof(GameObject)) as GameObject;

                Vector2 hitPos = col.transform.position; // ��ų �߽� ���� ��ġ�� ����Ʈ ����
                Instantiate(instanceEffect, hitPos, transform.rotation);
                instanceEffect.transform.Rotate(0, 0, Random.Range(0f, 360f));
                //ParticleManager.Instance.PlayEffect("EnemyHit", hitPos);
                

                isHit = true;
            }
        }
        if(isHit == true)
        {
            AudioManager.Instance.PlaySFX("EnemyHit");
        }
    }
}
