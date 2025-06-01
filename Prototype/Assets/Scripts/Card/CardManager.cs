using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<PlayerCardData> allCards;  // 전체 카드 목록
    public List<PlayerCardData> activeCards; // 현재 선택지 3개

    /*public List<PlayerCardData> GetAvailableCardChoices(int count)
    {
        // 1. 필터링
        List<PlayerCardData> availableSkillCards = allCards
            .Where(card => card.isSkill && card.currentLevel < card.maxLevel)
            .ToList();

        List<PlayerCardData> availableNormalCards = allCards
            .Where(card => !card.isSkill && !card.hasAppearedYet)
            .ToList();

        // 2. 조건 예외 체크
        if (availableSkillCards.Count == 0)
        {
            // 스킬 카드가 모두 만렙 → 조건 해제
            // 일반 카드만 등장 허용
            return availableNormalCards
                .OrderBy(_ => Random.value)
                .Take(Mathf.Min(count, availableNormalCards.Count))
                .ToList();
        }

        if (availableSkillCards.Count + availableNormalCards.Count <= count)
        {
            // 총 등장 가능 카드 수가 부족할 경우 → 전부 출력
            return availableSkillCards
                .Concat(availableNormalCards)
                .ToList();
        }

        // 3. 정상 케이스 (스킬 최소 1, 최대 2)
        int skillCardCount = Mathf.Min(availableSkillCards.Count, Random.Range(1, 3));
        int normalCardCount = count - skillCardCount;

        var selectedSkill = availableSkillCards
            .OrderBy(_ => Random.value)
            .Take(skillCardCount);

        var selectedNormal = availableNormalCards
            .OrderBy(_ => Random.value)
            .Take(normalCardCount);

        return selectedSkill.Concat(selectedNormal).ToList();
    }*/

   /* public List<PlayerCardData> GetCards()
    {
        
        return 
    }*/
}
