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
        player.OnCharacterDamaged -= player.TakeDamage;
        player.OnCharacterDamaged += DestroyBuff;
        player.OnCharacterDamaged += DamagedAtBuff;
        //player.SetShield(shieldValue);

        Invoke("DestroyBuff", 10f);                              //10�� �� �ڵ� ����
    }

    
    void DamagedAtBuff(float damage)
    {
        player.OnInvincibility(player.GetCommonInvincibilityTime());
        
    }

    /// <summary>
    /// ���� ���� ��ȣ�� ���¿��� �ǰ� �� ���� �ǰ� ������ �״�� �θ�,
    /// ���� ���� ���� ����� �����Ѵ�
    /// </summary>
    /// <param cardName="damage">�������� ����ִ�.</param>
    void DestroyBuff(float damage)
    {
        player.OnCharacterDamaged += player.TakeDamage;
        player.OnCharacterDamaged -= DamagedAtBuff;
        player.OnCharacterDamaged -= DestroyBuff;

        Destroy(this.gameObject);

    }
}
