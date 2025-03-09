using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHitEvent : MonoBehaviour
{
    public bool SetHpByDamage(ref float hp , ref float damage)
    {
        hp -= damage; //체력 감소 후 사망인지 확인

        if (hp <= 0)
            return true;
        else return false;
    }
}
