using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (character.GetShield() > 0)
        {
            //보호막이 있으니까 보호막 값 감소
            if (character.GetShield() - damage <= 0)
            {
                //보호막 0이 되니까 보호막 0으로 만들기
                character.SetShield(0);
            }
            else
            {
                //피격 돼도 보호막이 남으므로 단순 감소
                character.SetShield(character.GetShield() - damage);
            }
        }
        else
        {
            //보호막이 없으니까 체력 감소
            if (character.GetHp() - damage <= 0)
            {
                //체력이 0이므로 죽음처리
                character.SetHp(0);
                character.OnCharacterDeath?.Invoke();
            }
            else
            {
                //죽지 않으므로 체력감소 처리
                character.SetHp(character.GetHp() - damage);
            }
        }
    }
}
