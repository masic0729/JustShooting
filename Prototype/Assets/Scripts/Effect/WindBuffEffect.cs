using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBuffEffect : PlayerEffect
{
    [SerializeField]
    float moveMultiplierValue = 0.2f;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player.SetObjectMoveSpeedMultify(player.GetObjectMoveSpeedMultify() + moveMultiplierValue);
    }

    private void OnDestroy()
    {
        if (player != null)
        {
            player.SetObjectMoveSpeedMultify(player.GetObjectMoveSpeedMultify() - moveMultiplierValue);
        }
        else
        {
            Debug.Log("���� ���˹��� �÷��̾ ��� ȿ�� ����");
        }
    }
}
