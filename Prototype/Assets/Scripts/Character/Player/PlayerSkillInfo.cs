using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillInfo : MonoBehaviour
{
    //플레이어 기준으로 발동하는 것들이므로 충돌 이벤트 처리는 오직 플레이어를 기준으로 할 것
    [Header("플레이어가 각 속성의 스킬을 사용하기 위한 기능들의 모임")]
    Player player;
    public GameObject WindPuller;

    private void Awake()
    {
        //플레이어 스크립트 불러오기
        player = this.gameObject.GetComponent<Player>();
    }

    public void WindSkill()
    {
        //주위의 적 총알을 흡수하고, 유도탄으로 발사한다.(흡수 기능 및 유도탄 발사)
        GameObject instance =  Instantiate(WindPuller, transform.position, transform.rotation);
        instance.transform.parent = player.transform; //플레이어에 고정
    }

    public void IceSkill()
    {
        //플레이어에게 보호막을 주는 것(시스템 상 10초간).
    }

    public void FireSkill()
    {
        //받피감 50퍼
    }

    /*
     * 전기 공격은 확정된 기능을 논의 중이므로 보류
    public void LightningSkill()
    {
        
    }
    */

    //void 
}
