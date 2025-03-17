using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWindSkill : MonoBehaviour
{
    int enemyBulletCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        //1.5�� �� �ڵ� ���� �� ����� �Ѿ� ���� �÷��̾�� ����
        Invoke("DestroyAndSenddata", 1.5f);
    }

    void DestroyAndAttack()
    {
        transform.parent.GetComponent<Player>();
        Destroy(this.gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //��, �߻�ü�̸鼭, �±� ���� ������ �з��Ǹ� �ش� ������Ʈ ����
        if(collision.transform.tag == "Enemy" && 
            collision.gameObject.GetComponent<Projectile>() == true)
        {
            //����� �ٷ� �����ϰ� ��������, �ٽ� �켱���� ������ ������ ��ȹ�� ��� ���� ����
            Destroy(collision.gameObject);
            enemyBulletCount++;
        }

    }
}