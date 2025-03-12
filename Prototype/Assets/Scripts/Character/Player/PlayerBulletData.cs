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
    
    public string weaponName; // ����Ÿ��
    public int weaponLevel = 1;         

    public float attackDelay; // ���� �ֱ�
    public float damage; // ���ط�

    //���� �Ӽ��� ��Ʈ��ĵ ����̱⿡ �ش� ��ü�� 0�̴�
    public float moveSpeed; //����ü �ӵ�
    public float powerValue; // ��ų ����� ���� �Ŀ� ��
}
