using UnityEditor.Animations;
using UnityEngine;

[System.Serializable]

public class EnemyAttackData
{
    public Sprite sprite;
    public AnimatorController animCtrl;
    public const float damage = 1f;
    public float moveSpeed;
}
