using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<PlayerCardData> allCards;  // ��ü ī�� ���
    public List<PlayerCardData> activeCards; // ���� ������ 3��

    /*public List<PlayerCardData> GetAvailableCardChoices(int count)
    {
        // 1. ���͸�
        List<PlayerCardData> availableSkillCards = allCards
            .Where(card => card.isSkill && card.currentLevel < card.maxLevel)
            .ToList();

        List<PlayerCardData> availableNormalCards = allCards
            .Where(card => !card.isSkill && !card.hasAppearedYet)
            .ToList();

        // 2. ���� ���� üũ
        if (availableSkillCards.Count == 0)
        {
            // ��ų ī�尡 ��� ���� �� ���� ����
            // �Ϲ� ī�常 ���� ���
            return availableNormalCards
                .OrderBy(_ => Random.value)
                .Take(Mathf.Min(count, availableNormalCards.Count))
                .ToList();
        }

        if (availableSkillCards.Count + availableNormalCards.Count <= count)
        {
            // �� ���� ���� ī�� ���� ������ ��� �� ���� ���
            return availableSkillCards
                .Concat(availableNormalCards)
                .ToList();
        }

        // 3. ���� ���̽� (��ų �ּ� 1, �ִ� 2)
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
