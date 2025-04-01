using UnityEngine;
using UnityEditor.Animations;


[System.Serializable]
public class PlayerBulletData
{
    /// <summary>
    /// 
    /// </summary>
    public Sprite sprite;
    public AnimatorController animCtrl;
    
    public string weaponName; // 공격타입
    public int weaponLevel = 1;
    public float lifeTime; //각 속성의 발사체 생명시간
    public float attackDelayMultify; // 공격 주기 배율

    //번개 속성은 히트스캔 방식이기에 해당 객체는 0이다
    public float moveSpeed; //투사체 속도
    public float powerValue; // 스킬 사용을 위한 파워 값
    public float attackMultify; //데미지 배율
}
