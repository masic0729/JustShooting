using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHitEvent : MonoBehaviour
{
    public bool SetHpByDamage(ref float hp , ref float damage)
    {
        hp -= damage; //ü�� ���� �� ������� Ȯ��

        if (hp <= 0)
            return true;
        else return false;
    }
}
