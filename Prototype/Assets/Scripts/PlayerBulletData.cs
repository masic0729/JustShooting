using System.Collections;
using System.Collections.Generic;
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
    public int weaponLevel;         

    public float[] attackDelay; // 공격 주기
    public float[] attackDamage; // 피해량

    //번개 속성은 히트스캔 방식이기에 해당 객체는 0이다
    public float[] shootSpeed; //투사체 속도
}
