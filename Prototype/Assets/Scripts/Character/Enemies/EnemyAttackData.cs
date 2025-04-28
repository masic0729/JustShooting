using UnityEngine;

[System.Serializable]
public class EnemyAttackData
{
    public Sprite sprite;

    public RuntimeAnimatorController animCtrl;


    public const float damage = 1f;
    public float moveSpeed;
}
