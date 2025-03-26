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
    public float damage = 1;                    // ���ط�
    public float moveSpeed;                     // �߻�ü �̵� �ӵ�
    public float attackDelayMultify;            // ���� �ֱ�(���ݼӵ�)
    public float powerValue;                    //�� �Ѿ��� �����ϴ� �Ŀ���
    public float damageMultiplier = 1;          // ���ظ� �� �� �ִ� ���ݷ� �� ���� ���
    
}