using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteration
{

    const float invincibilityTime = 2.0f;

    /// <summary>
    /// �浹�� ��� ���� �����͸� �ҷ��� ü���� �����ϴ� ����.
    /// ���� ����� ü���� 0���ϰ� �Ǹ� ���� �׼��� ����ȴ�
    /// ���� �� �Լ��� �߻�ü, ĳ���� Ŭ�������� ����Ѵ�.
    /// �̷� ���� ��ũ��Ʈ ���� ������Ʈ�� ���� �ǹ̷� �̸��� ������
    /// </summary>
    /// <param name="character"> �߻�ü ���� ������ ���ֵǴ� ��ü</param>
    public void SendDamage(ref Character character, float damage)
    {
        //�ش� ĳ���Ͱ� �����̶��, ������ ��ü ȿ���� �߻����� �ʴ´�
        if(character.GetIsInvincibility() == false)
        {
            if (character.GetShield() > 0)
            {
                //��ȣ���� �����ϱ� ��ȣ�� �� ����
                if (character.GetShield() - damage <= 0)
                {
                    //��ȣ�� 0�� �Ǵϱ� ��ȣ�� 0���� �����
                    character.SetShield(0);
                }
                else
                {
                    //�ǰ� �ŵ� ��ȣ���� �����Ƿ� �ܼ� ����
                    character.SetShield(character.GetShield() - damage);
                }
            }
            else
            {
                //��ȣ���� �����ϱ� ü�� ����
                if (character.GetHp() - damage <= 0)
                {
                    //ü���� 0�̹Ƿ� ����ó��
                    character.SetHp(0);
                    character.OnCharacterDeath?.Invoke();
                }
                else
                {
                    //���� �����Ƿ� ü�°��� ó��
                    character.SetHp(character.GetHp() - damage);
                }

            }
            if(character.transform.name == "Player")
            {
                character.SetIsInvincibility(true);
                character.Invoke("TransIsInvincibilityFalse", invincibilityTime);
            }
        }
    }
}
