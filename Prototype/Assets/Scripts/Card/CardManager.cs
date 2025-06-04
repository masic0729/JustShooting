using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField]
    //현재 보여지는 카드 게임오브젝트 배열
    private GameObject[] currentCards;
    CardInfo[] loadedCards;
    //전체 카드 정보 목록
    public List<CardInfo> allCards;

    /*
    //특정 개수만큼 등장 가능한 카드 리스트를 필터링하여 반환 (주석 처리됨)
    public List<PlayerCardData> GetAvailableCardChoices(int count)
    {
        // 스킬 카드 중 현재 레벨이 최대 레벨 미만인 카드 필터링
        List<PlayerCardData> availableSkillCards = allCards
            .Where(card => card.isSkill && card.currentLevel < card.maxLevel)
            .ToList();

        // 일반 카드 중 아직 등장하지 않은 카드 필터링
        List<PlayerCardData> availableNormalCards = allCards
            .Where(card => !card.isSkill && !card.hasAppearedYet)
            .ToList();

        // 스킬 카드가 모두 만렙인 경우 일반 카드만 반환
        if (availableSkillCards.Count == 0)
        {
            return availableNormalCards
                .OrderBy(_ => Random.value)
                .Take(Mathf.Min(count, availableNormalCards.Count))
                .ToList();
        }

        // 등장 가능한 카드 수가 부족한 경우 모든 카드 반환
        if (availableSkillCards.Count + availableNormalCards.Count <= count)
        {
            return availableSkillCards
                .Concat(availableNormalCards)
                .ToList();
        }

        // 정상적인 카드 선택 (스킬 카드 1~2장 포함)
        int skillCardCount = Mathf.Min(availableSkillCards.Count, Random.Range(1, 3));
        int normalCardCount = count - skillCardCount;

        var selectedSkill = availableSkillCards
            .OrderBy(_ => Random.value)
            .Take(skillCardCount);

        var selectedNormal = availableNormalCards
            .OrderBy(_ => Random.value)
            .Take(normalCardCount);

        return selectedSkill.Concat(selectedNormal).ToList();
    }
    */

    //게임 오브젝트가 활성화될 때 초기화 호출
    private void Awake()
    {
        CardInit();
    }

    //리소스 폴더에서 모든 카드 데이터를 로드하여 리스트에 저장
    void CardInit()
    {
        loadedCards = Resources.LoadAll<CardInfo>("Scriptable/Card/");
        for(int i = 0; i < loadedCards.Length; i++)
        {
            loadedCards[i].cardData.icon = Resources.Load<Sprite>("Card/IconTest");
            Debug.Log(loadedCards[i].cardData.icon);
        }
        allCards = loadedCards.ToList();
    }

    /// <summary>
    /// 기본적으로 3장의 카드를 무작위로 가져온다.
    /// 획득 제한 및 카드가 부족한 경우 3장 미만으로 반환할 수 있음.
    /// </summary>
    /// <param name="getCount">가져올 카드 개수</param>
    /// <returns>선택된 카드 리스트</returns>
    public List<PlayerCardData> GetCards(int getCount = 3)
    {
        // 현재 구현되지 않음, null 반환
        return null;
    }
}