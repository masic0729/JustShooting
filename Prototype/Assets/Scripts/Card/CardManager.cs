using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CardManager : MonoBehaviour
{
    //리스트 관련해서 공부하는 기회의 스크립트. 결국 이 스크립트로 최종적으로 구현할 예정이지만, 학습에 중점을 두고 있음
    [SerializeField]
    //현재 보여지는 카드 게임오브젝트 배열
    GameObject[] currentCards;
    CardInfo[] loadedCards;

    //전체 카드 정보 목록
    public List<CardInfo> allCards;

    private void Start()
    {
        CardDataLoad();
        Test();
        ShowCards();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ShowCards();
        }

    }

    //리소스 폴더에서 모든 카드 데이터를 로드하여 리스트에 저장
    void CardDataLoad()
    {
        loadedCards = Resources.LoadAll<CardInfo>("Scriptable/Card/");
        allCards = loadedCards.ToList();
    }

    /// <summary>
    /// 기본적으로 3장의 카드를 무작위로 가져온다.
    /// 획득 제한 및 카드가 부족한 경우 3장 미만으로 반환할 수 있음.
    /// </summary>
    /// <param name="getCount">가져올 카드 개수</param>
    /// <returns>선택된 카드 리스트</returns>
    public List<CardInfo> GetCards(int getCount = 3)
    {
        List<CardInfo> dupliCards = allCards;

        List<CardInfo> randCards = allCards.OrderBy(_ => Random.value).ToList();

        return randCards.Take(getCount).ToList();
    }

    void SetCard(GameObject card, CardInfo info)
    {
        Debug.Log(card.transform.Find("CardFrame/Icon"));
        card.transform.Find("CardFrame/Icon").GetComponent<Image>().sprite = info.icon;
        card.transform.Find("CardFrame/Comment").GetComponent<TextMeshProUGUI>().text = info.description;
        card.transform.Find("CardFrame/CardName").GetComponent<TextMeshProUGUI>().text = info.cardName;
        //todo카드 프레임은 추후 변동되지 않거나, 추가 구현 요구
    }

    /// <summary>
    /// 현재는 기본 구현으로 만들고, 이후 기획안을 기반으로 구현할 것.
    /// </summary>
    public void ShowCards()
    {
        List<CardInfo> instanceCards = GetCards();
        Debug.Log(instanceCards[0].icon);
        for(int i = 0; i < GetCards().Count; i++)
        {
            SetCard(currentCards[i], instanceCards[i]);
        }
    }

    void Test()
    {
        //for(int i = 0; i < )
    }

    void tresh()
    {
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
    }

}