using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    public GameObject CardPanel;
    //리스트 관련해서 공부하는 기회의 스크립트. 결국 이 스크립트로 최종적으로 구현할 예정이지만, 학습에 중점을 두고 있음
    /*
    1. 카드인포를 기반으로 리스트를 받는다.
    2. 그 리스트를 랜덤 카드, 나머지 일반 카드로 분류한다
    3. 첫 번째 인덱스는 랜덤카드 고정이고, 나머지 두 자리는 일반카드가 차지한다.
    4. 그러나, 랜덤카드는 소멸되지 않으며, 선택한 일반 카드는 다시 등장하지 않는다.
    5. 이후 일반 카드가 1장 이하 또는 더 이상 없다면, 빈 곳은 카드 자체가 나오지 않는다.

    작업 현황--
    2번까지 했음.
    
    --------------------
    현재 캐릭터 객체들은 스탯 매니저를 기반으로 구동하지 않고 있다.
    이에 따라 플레이어의 카드 기능을 구현 후 연결을 진행하고
    최종 테스트 후 버그 확인이 필요하다
     */

    [SerializeField]
    //현재 보여지는 카드 게임오브젝트 배열
    GameObject[] currentCards;
    CardInfo[] loadedCards;

    public List<CardInfo> allCards;             //인스펙터에 보이는 카드정보

    public List<CardData> cardData;             //카드 리스트 설정용

    List<CardData> dupliCardData;               //카드데이터 복제본이며, 실제로 운용되는 리스트
    //Dictionary<string, CardData> dicCardData;

    //전체 카드 정보 목록

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        CardListLoad();
        //ShowCards();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            ShowCards();
        }

    }

    //리소스 폴더에서 모든 카드 데이터를 로드하여 리스트에 저장
    void CardListLoad()
    {
        /*loadedCards = Resources.LoadAll<CardInfo>("Scriptable/Card/");
        allCards = loadedCards.ToList();*/

        dupliCardData = cardData;

    }


    List<CardData> CardDataLoad()
    {
        List<CardData> cards = new List<CardData>();
        //랜덤 카드 할당
        CardData randomCard = dupliCardData.Find(cardData => cardData.cardType == CardData.CardType.Random);
        cards.Add(randomCard);

        //일반 카드 할당
        cards.AddRange(dupliCardData.FindAll(cardData => cardData.cardType == CardData.CardType.Common)
            .OrderBy(cardData => /*UnityEngine.*/Random.value)
            .Take(2)
            .ToList()
            );

        return cards;
    }

    void SetCard(GameObject card, CardData data)
    {

        Debug.Log(card.transform.Find("CardFrame/Icon"));
        card.transform.Find("CardFrame/Icon").GetComponent<Image>().sprite = data.icon;
        card.transform.Find("CardFrame/Comment").GetComponent<TextMeshProUGUI>().text = data.description;
        card.transform.Find("CardFrame/CardName").GetComponent<TextMeshProUGUI>().text = data.showCardName;

        //이건 맞아. cardInfo에 기능을 넣고, 이후 버튼 온클릭에 넣어서 실행을 하든, 여기서 카드 이름을 기반으로 버튼에 넣던가

        card.transform.Find("CardFrame").GetComponent<Button>().onClick.RemoveAllListeners();
        
        card.transform.Find("CardFrame").GetComponent<Button>().onClick.AddListener(() =>
        {
            CardEvent(data.cardName);
            CloseCardSelect();
        });

        //todo카드 프레임은 추후 변동되지 않거나, 추가 구현 요구
    }

    public void ShowCards()
    {
        List<CardData> instanceCards = CardDataLoad();
        for(int i = 0; i < instanceCards.Count; i++)
        {
            currentCards[i].SetActive(true);
            SetCard(currentCards[i], instanceCards[i]);
        }
        CardPanel.SetActive(true);
        Time.timeScale = 0.0f;
    }


    /// <summary>
    /// 선택한 이후는, 모든 카드들이 비활성화가 되어 전투한다.
    /// 추후 여러 UI가 생긴다면, 카드 뿐만 아니라 상위의 오브젝트로 정의 후 그것 역시 비활성화 해야한다.
    /// </summary>
    void CloseCardSelect()
    {
        Debug.Log("카드 닫음");
        for(int i = 0; i < currentCards.Length; i++)
        {
            currentCards[i].SetActive(false);
        }
        CardPanel.SetActive(false);
        Time.timeScale = 1.0f;

    }


    void CardEvent(string cardName)
    {
        
        CardData instance = dupliCardData.Find(CardData => CardData.cardName == cardName);
        if(cardName != "Random")
        {
            //Debug.Log(instance.cardName + "삭제됨");
            dupliCardData.Remove(instance);
        }
        switch (cardName)
        {
            
            case "Random":
                RandomCard();
                break;
            case "Death":
                DeathAttack();
                break;
            case "Defense":
                DefenseUpgrade();
                break;
            case "Cri":
                CriticalOn();
                break;
            case "BigBullet":
                BigBullet();
                break;
            case "RandomSkill":
                RandomSkill();
                break;
            default:
                Debug.Log("오류 발생. 카드 이름 매치 안됐음");
                break;
        }
        
        
    }

    /// <summary>
    /// 순수 랜덤의 확률로 플레이어에게 효과 부여. 공격력, 공격속도, 이동속도 등 능력치 상승/하락 부여
    /// </summary>
    void RandomCard()
    {
        Debug.Log("Random");
        RandomEvent();
    }

    void RandomEvent()
    {
        int randIndex = Random.Range(0, 3);
        string stateCommant = "";
        switch (randIndex)
        {
            case 0:
                StatManager.instance.randomAddDamage += 0.2f;
                stateCommant = "공격력 상승";
                break;
            case 1:
                StatManager.instance.randomAddMoveSpeed += 0.1f;
                float playerMoveSpeed = StatManager.instance.playerData.GetObjectMoveSpeedMultify();
                StatManager.instance.playerData.SetObjectMoveSpeedMultify(playerMoveSpeed *  (1 + StatManager.instance.randomAddMoveSpeed));
                stateCommant = "이동속도 상승";
                break;
            case 2:
                float colscale = StatManager.instance.randomSizeMultifyValue;
                StatManager.instance.playerData.SetTransCollider(colscale);
                stateCommant = "크기 감소";
                break;
        }
        Debug.Log(stateCommant);
        StatManager.instance.playerData.ShowCardText(stateCommant);
    }


    /// <summary>
    /// 최대 목숨 기준 감소된 목숨만큼 피해량 증가 기능
    /// 작업은 끝났으며, 추후 버그 확인 필요
    /// </summary>
    void DeathAttack()
    {
        Debug.Log("Death");
        Player player = StatManager.instance.playerData;
        //플레이어의 최대체력의 값을 현재 체력으로 뺄셈을 하면, 감소된 수치가 나온다
        //이를 기반으로 카드 획득할 때 잃은 체력 비례 공격력 상승을 바로 적용할 수 있다
        int currentPlayerHp = (int)player.GetMaxHp() - (int)player.GetHp();

        //체력이 감소된 채로 카드를 획득했다면 적용
        
        
        StatManager.instance.p_damageFromCard += StatManager.instance.p_bulletDamageUpByLossHp * currentPlayerHp;
        player.OnDamage += PlayerHitCheckForCard;
        
    }

    /// <summary>
    /// 플레이어가 카드 획득 이후 피격시 처리하기 위해 액션 구독을 카드획득 할 때 하였음
    /// 바로 아래의 함수 반드시 인지할 것(아직 사용하지 않았음)
    /// </summary>
    void PlayerHitCheckForCard()
    {
        StatManager.instance.p_damageFromCard += StatManager.instance.p_bulletDamageUpByLossHp;
    }

    /// <summary>
    /// 플레이어가 체력을 회복했을 때, 처리해야하기에 현재는 회복이 없다. 이에 따라 아직 적용하지는 않았음
    /// todo
    /// </summary>
    void PlayerHealCheckForCard()
    {
        StatManager.instance.p_damageFromCard -= StatManager.instance.p_bulletDamageUpByLossHp;
    }

    /// <summary>
    /// 플레이어가 피격 시 피해 무시(즉사 제외) 및 무적 시간 5초 적용(해당 무적 한정)
    /// 구현 완료. 대신 스탯 관련해서 연결 필요하며, 보호막이 적용 중일 때는 UI상으로 노출이 되어야 함
    /// </summary>
    void DefenseUpgrade()
    {
        Debug.Log("Defense");
        //StatManager.instance.cardInvincibilityTime = 5;
        //기존의 보호막 시스템을 여기에다가 넣자.
        //또한 얼음 버프 효과가 발동이 되면, TakeDamage를 해지하고, 보호막 만을 위한 함수를 구독하여 유연하게 처리한다
        //쉴드로 피해를 상쇄할 때는 무적시간이 5초이다.
        StatManager.instance.playerData.SetShield(1);
        UI_Manager.instance.defIcon.SetActive(true);
    }

    /// <summary>
    /// 플레이어의 치명타 공격 활성화
    /// 구현완료. 대신 스탯 관련해서 연결 필요
    /// </summary>
    void CriticalOn()
    {
        Debug.Log("Cri");
        StatManager.instance.criticalPercent += 20f;
        StatManager.instance.isCritical = true;
    }

    /// <summary>
    /// 플레이어 이속 감소 및 일반 발사체 크기 증가
    /// 구현완료. 대신 스텟 관련해서 연결 필요
    /// </summary>
    void BigBullet()
    {
        Debug.Log("BigBullet");
        StatManager.instance.p_projectileSizeMultify = 2f;
        float playerMoveSpeed = StatManager.instance.playerData.GetObjectMoveSpeedMultify();
        float moveSpeedCalValue = StatManager.instance.p_moveSpeedTransValue;
        StatManager.instance.playerData.SetObjectMoveSpeedMultify(playerMoveSpeed * moveSpeedCalValue);
        
    }

    /// <summary>
    /// 충전스킬 랜덤화 및 스킬 자체 데미지 1.5배 증가
    /// 구현완료. 대신 스텟 관련해서 연결 필요
    /// </summary>
    void RandomSkill()
    {
        Debug.Log("RandomSkill");
        StatManager.instance.isRandomSkill = true;
        StatManager.instance.p_skillDamageMultify += 0.2f;
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

    void tresh2()
    {
/*
        /// <summary>
        /// 기본적으로 3장의 카드를 무작위로 가져온다.
        /// 첫 카드는 반드시 랜덤카드만 등장한다
        /// 나머지 두 곳은 미선택된 일반카드가 등장한다.
        /// 지속적으로 일반카드를 선택하여 일반카드가 2장 미만일 경우 1장만 반환할 수 있으며, 모두 획득 시 더 이상 등장하지 않는다.
        /// </summary>
        /// <param cardName="getCount">가져올 카드 개수. 기본값은 2장이다 </param>
        /// <returns>선택된 카드 리스트</returns>
        public List<CardInfo> GetCards(int getCount = 2)
        {
            *//*Dictionary<CardInfo, string> cardName;
            cardName = new Dictionary<CardInfo, string>();
            for(int i = 0; i < loadedCards.Length; i++)
            {
                cardName[loadedCards[i]] = loadedCards[i].cardName;
            }
            List<CardInfo> randCards = allCards.OrderBy( CardInfo => UnityEngine.Random.value).ToList();*//*

            List<CardInfo> randCards;
            randCards.FirstOrDefault(CardInfo => CardInfo.cardType == CardInfo.CardType.Random);
            //randCards = allCards.FirstOrDefault(allCards.cardType == CardInfo.CardType.Random);


            return randCards.Take(getCount).ToList();
        }

        void SetCard(GameObject card, CardInfo data)
        {

            Debug.Log(card.transform.Find("CardFrame/Icon"));
            card.transform.Find("CardFrame/Icon").GetComponent<Image>().sprite = data.icon;
            card.transform.Find("CardFrame/Comment").GetComponent<TextMeshProUGUI>().text = data.description;
            card.transform.Find("CardFrame/CardName").GetComponent<TextMeshProUGUI>().text = data.cardName;

            //이건 맞아. cardInfo에 기능을 넣고, 이후 버튼 온클릭에 넣어서 실행을 하든, 여기서 카드 이름을 기반으로 버튼에 넣던가

            card.transform.Find("CardFrame").GetComponent<Button>().onClick.RemoveAllListeners();
            //cardAction = CardEvent(data.cardName);
            //cardAction += SelectCard(data.cardName);
            card.transform.Find("CardFrame").GetComponent<Button>().onClick.AddListener(() =>
            {
                SelectCard(data.cardName);
                CardEvent(data.cardName);
            });

            //todo카드 프레임은 추후 변동되지 않거나, 추가 구현 요구
        }

        /// <summary>
        /// 현재는 기본 구현으로 만들고, 이후 기획안을 기반으로 구현할 것.
        /// </summary>
        public void ShowCards()
        {
            int loadCard;
            //카드 리스트가 2장 이상이면 정상 호출
            if (allCards.Count > 1)
            {
                loadCard = 2;
            }
            else
            {
                loadCard = allCards.Count;
            }
            List<CardInfo> instanceCards = GetCards(loadCard);

            for (int i = 0; i < loadCard; i++)
            {
                SetCard(currentCards[i + 1], instanceCards[i]);
            }
        }

        /// <summary>
        /// 카드 선택 이후 카드효과 적용 및 선택한 카드는 리스트에서 삭제해야한다. 또한 테스트용의 경우 카드가 랜덤으로 다시 돌려야 한다.
        /// </summary>
        void SelectCard(string cardName)
        {
            if (cardName == "Random")
                return;
            //리스트에서 제외시키는 기능구현. 하지만 랜덤 카드는 삭제되지 않는다
            //CardInfo removeCard = allCards.FirstOrDefault(CardInfo => CardInfo.cardName == cardName);

            CardInfo removeCard = null;

            foreach (CardInfo card in allCards)
            {
                if (card.cardName == cardName)
                {
                    removeCard = card;
                    break;
                }
            }

            if (removeCard != null)
                allCards.Remove(removeCard);
        }*/
    }
}