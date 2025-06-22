using UnityEngine;

[System.Serializable]
public class PlayerBulletData
{

    // sprite 변수 선언
    public Sprite sprite;
    // animCtrl 변수 선언
    public RuntimeAnimatorController animCtrl;
    
    // weaponName 변수 선언
    public string weaponName; // 공격타입
    // weaponLevel 변수 선언
    public int weaponLevel = 1;
    // lifeTime 변수 선언
    public float lifeTime; //각 속성의 발사체 생명시간
    // attackDelayMultify 변수 선언
    public float attackDelayMultify; // 공격 주기 배율

    //번개 속성은 히트스캔 방식이기에 해당 객체는 0이다
    // moveSpeed 변수 선언
    public float moveSpeed; //투사체 속도
    // powerValue 변수 선언
    public float powerValue; // 스킬 사용을 위한 파워 값
    // attackMultify 변수 선언
    public float attackMultify; //데미지 배율

    /*
    Sprite sprite;                              //발사체의 리소스
    
    public float canMoveDistance = 100f;        //최대 이동거리는 100이나, 불 속성은 단거리로 조정해야함
    
    public float lifeTime;                      //각 속성의 발사체 생명시간

    public float attackSpeed;                   //공격 속도 배율. 빠르게 올릴 것이라면 값을 올려야 함
    
    public float 
     */
}