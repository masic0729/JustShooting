public class ObjectInteration
{


    /// <summary>
    /// 충돌한 상대 편의 데이터를 불러와 체력을 감소하는 행위.
    /// 만약 대상의 체력이 0이하가 되면 죽음 액션이 실행된다
    /// 또한 이 함수는 발사체, 캐릭터 클래스들이 사용한다.
    /// 이로 인해 스크립트 명은 오브젝트로 넓은 의미로 이름을 지었다
    /// </summary>
    /// <param name="character"> 발사체 기준 적으로 간주되는 객체</param>
    public void SendDamage(ref Character character, float damage)
    {
        //해당 캐릭터가 무적이라면, 데미지 자체 효과는 발생하지 않는다

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
