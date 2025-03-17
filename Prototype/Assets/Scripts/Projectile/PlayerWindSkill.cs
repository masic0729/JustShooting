using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWindSkill : MonoBehaviour
{
    int enemyBulletCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        //1.5초 뒤 자동 삭제 및 흡수한 총알 개수 플레이어에게 전달
        Invoke("DestroyAndSenddata", 1.5f);
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