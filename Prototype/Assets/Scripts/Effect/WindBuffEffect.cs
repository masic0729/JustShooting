using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBuffEffect : PlayerEffect
{
    [SerializeField]
    float moveMultiplierValue = 0.2f; // �̵� �ӵ� ���� ���

    // Start�� ������Ʈ�� Ȱ��ȭ�� �� ȣ��Ǹ�, ���� ���� ������ ����
    protected override void Start()
    {
        base.Start();
        // �÷��̾��� �̵� ����� �������� �̵� �ӵ� ��� ȿ���� �ο�
        player.SetObjectMoveSpeedMultify(player.GetObjectMoveSpeedMultify() + moveMultiplierValue);
    }

    // ���� ������Ʈ�� �ı��� ��, ȿ���� ���� ���ͽ�Ŵ
    private void OnDestroy()
    {
        if (player != null)
        {
            // �̵� �ӵ� ���� ����� �ٽ� ������� �ǵ���
            player.SetObjectMoveSpeedMultify(player.GetObjectMoveSpeedMultify() - moveMultiplierValue);
        }
        else
        {
            Debug.Log("���� ���˹��� �÷��̾ ��� ȿ�� ����"); // ����׿� �޽���
        }
    }
}
