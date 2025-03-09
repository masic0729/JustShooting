using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;


[System.Serializable]
public class PlayerBulletData
{
    //public GameObject weapon;
    public Sprite sprite;
    public AnimatorController animCtrl;
    
    public string weaponName; // ����Ÿ��

    public float[] attackDelay; // ���� �ֱ�
    public float[] attackDamage; // ���ط�

    //���� �Ӽ��� ��Ʈ��ĵ ����̱⿡ �ش� ��ü�� 0�̴�
    public float[] shootSpeed; //����ü �ӵ�
}
