using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcedBuffEffect : PlayerEffect
{
    //public float shieldValue = 1; // �÷��̾�� �ο��� �ǵ� ��

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // �÷��̾��� �ǰ� ��� ����
        player.SetIsBuffInvicibility(true);
        //player.SetShield(shieldValue);

        Invoke("DestroyBuff", 10f);                              //10�� �� �ڵ� ����
    }


    private void Update()
    {
        if(player.GetIsBuffInvicibility() == false)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// ���� ���� ��ȣ�� ���¿��� �ǰ� �� ���� �ǰ� ������ �״�� �θ�,
    /// ���� ���� ���� ����� �����Ѵ�
    /// </summary>
    /// <param cardName="damage">�������� ����ִ�.</param>
    void DestroyBuff(float damage)
    {
        player.SetIsBuffInvicibility(false);

    }
}
