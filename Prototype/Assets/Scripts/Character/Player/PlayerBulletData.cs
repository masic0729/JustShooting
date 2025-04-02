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
    public float lifeTime; //�� �Ӽ��� �߻�ü ����ð�
    public float attackDelayMultify; // ���� �ֱ� ����

    //���� �Ӽ��� ��Ʈ��ĵ ����̱⿡ �ش� ��ü�� 0�̴�
    public float moveSpeed; //����ü �ӵ�
    public float powerValue; // ��ų ����� ���� �Ŀ� ��
    public float attackMultify; //������ ����
}
