using UnityEditor.Animations;
using UnityEngine;

//[CreateAssetMenu(fileName = "AttackStats", menuName = "ScriptableObjects/AttackStats", order = 1)]
public class AttackStats : MonoBehaviour
{
    [Header("���� ��ü���� �߻�ü ������")]
    public AnimatorController animCtrl;             // �߻�ü�� �ִϸ��̼�
    public Sprite sprite;                           // �߻�ü�� ���ҽ�
    [HideInInspector]
    public float damage = 1f;                       // ���ط�
    public float moveSpeed = 5f;                    // �߻�ü �̵� �ӵ�
    public float attackDelayMultify = 1;            // ���� �ֱ�(���ݼӵ�)
    public float powerValue = 0;                    //�� �Ѿ��� �����ϴ� �Ŀ���
    public float damageMultiplier = 1;              // ���ظ� �� �� �ִ� ���ݷ� �� ���� ���
    
}