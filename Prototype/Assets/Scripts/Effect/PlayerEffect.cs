using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : IEffect
{
    protected Player player;             // �÷��̾� ��ũ��Ʈ ������ ����
    protected float playerDamage;        // �÷��̾��� ���ݷ� �� ����Ʈ�� �ʿ��� ��ġ �����

    // Start�� ������Ʈ Ȱ��ȭ �� ���� ���� ����Ǵ� �Լ�
    protected override void Start()
    {
        TargetObject = GameObject.Find("Player");   // Ÿ�� ������Ʈ�� Player�� ����
        parentPath = "Skill";                       // ����Ʈ�� ���� �ڽ� ������Ʈ �̸� ����
        base.Start();                               // �θ� Ŭ����(IEffect)�� Start ����
        player = TargetObject.GetComponent<Player>(); // �÷��̾� ������Ʈ ĳ��
        playerDamage = StatManager.instance.p_skillDamageMultify;
    }

    /// <summary>
    /// �ڽ� Ŭ�������� �������� �� �ִ� �� ���� ó�� �Լ�
    /// </summary>
    /// <param cardName="objects">�浹�� �� ��ü �迭</param>
    protected virtual void EnemyAttack(Collider2D[] objects)
    {
        // ����Ʈ �߻� �� ������ �������� �ִ� ó�� ���� (���� ������)
        //ParticleManager.Instance.PlayEffect("")
    }
}
