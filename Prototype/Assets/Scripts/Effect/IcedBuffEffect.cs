using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcedBuffEffect : PlayerEffect
{
    public float shieldValue = 1; // �÷��̾�� �ο��� �ǵ� ��

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // �÷��̾�� �ǵ� �ο�
        player.SetShield(shieldValue);
    }

    void Update()
    {
        // �ǵ尡 ��� �����Ǿ�����, �� ���� ������Ʈ�� ����
        if (player.GetShield() <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        // �ǵ尡 �������� ���, ���� ���� �� �ǵ嵵 ����
        if (player.GetShield() > 0)
            player.SetShield(0);
    }
}
