public class ObjectInteraction
{
    /// <summary>
    /// 충돌한 상대 편의 데이터를 불러와 체력을 감소하는 행위.
    /// 만약 대상의 체력이 0이하가 되면 죽음 액션이 실행된다.
    /// 또한 이 함수는 발사체, 캐릭터 클래스들이 사용한다.
    /// 이로 인해 스크립트 명은 오브젝트로 넓은 의미로 이름을 지었다.
    /// 해당 기능은 직접적으로 체력 업데이트와 피격에 따른 여러 연출을 분리하여 만든 것으로 볼 수 있다
    /// </summary>
    /// <param cardName="character">데미지를 받을 캐릭터 객체 (주로 적)</param>
    /// <param cardName="damage">입힐 데미지 양</param>
    public void SendDamage(ref Character character, float damage)
    {
        // 대상 캐릭터가 무적 상태이면 데미지 무시
        if (character.GetIsInvincibility() == false)
        {
            // 쉴드가 있을 경우 쉴드를 먼저 감소시킴
            if (character.GetShield() > 0)
            {
                if (character.GetShield() - damage < 0)
                {
                    character.SetShield(0);  // 쉴드가 0 아래로 내려가면 0으로 설정
                }
                else
                {
                    character.SetShield(character.GetShield() - damage);  // 쉴드 감소
                }
            }
            else
            {
                // 쉴드가 없으면 체력에서 직접 데미지 감소 처리
                float caluHp = character.GetHp() - damage;

                if (caluHp <= 0)
                {
                    character.SetHp(0);  // 체력이 0 이하가 되면 0으로 설정하고 죽음 처리 실행
                    character.OnCharacterDeath?.Invoke();
                }
                else
                {
                    character.SetHp(caluHp);  // 체력 감소
                }
            }

            // 데미지를 입은 상태 처리 (피격 이펙트 및 무적처리 등)
            
            character.OnDamage?.Invoke();
        }
    }
}
