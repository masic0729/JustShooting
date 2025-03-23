using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

//[CreateAssetMenu(fileName = "AttackStats", menuName = "ScriptableObjects/AttackStats", order = 1)]
public class AttackStats : MonoBehaviour
{
    [Header("���� ��ü���� �߻�ü ������")]
    public AnimatorController animCtrl;         // �߻�ü�� �ִϸ��̼�
    public Sprite sprite;                       // �߻�ü�� ���ҽ�
    public float damage;                        // ���ط�
    public float moveSpeed;                     // �߻�ü �̵� �ӵ�
    public float attackSpeed;                   // ���� �ֱ�(���ݼӵ�(
    public float powerValue;                    //�� �Ѿ��� �����ϴ� �Ŀ���
    //protected float damageMultiplier = 1;     // **����// ���ظ� �� �� �ִ� ���ݷ� �� ���� ���
    
}