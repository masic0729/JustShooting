public class ObjectInteration
{


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

        if (character.GetIsInvincibility() == false)
        {
            if (character.GetShield() > 0)
            {
                if (character.GetShield() - damage < 0)
                {
                    character.SetShield(0);
                }
                else
                {
                    character.SetShield(character.GetShield() - damage);
                }
            }
            else
            {
                float newHp = character.GetHp() - damage;

                if (newHp <= 0)
                {
                    character.SetHp(0);
                    character.OnCharacterDeath?.Invoke();
                }
                else
                {
                    character.SetHp(newHp);
                }
            }

            character.OnDamage();
        }

    }
}
