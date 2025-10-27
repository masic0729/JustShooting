using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    public GameObject CardPanel;
    public GameObject CardNotClickPanel;
    public Animator CardViewAnim;
    public Animator[] cardAnims;
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
        if (instance == null)
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
        if (Input.GetKeyDown(KeyCode.LeftControl))
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

    void SetCard(GameObject card, CardData data, Animator anim)
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
            StartCoroutine(CardCloseAction(anim));
            CardNotClickPanel.SetActive(true);

        });

        //todo카드 프레임은 추후 변동되지 않거나, 추가 구현 요구
    }

    /// <summary>
    /// 카드 선택 후 TimeScale상태 관계 없이 반드시 1초 뒤
    /// 창 닫기 및 기능 적용 등 지연 실행을 한다.
    /// 이때 1초 동안 카드 선택 시 이펙트 및 사운드가 출력 되는 것을 목표로 한다.
    /// </summary>
    /// <returns></returns>
    IEnumerator CardCloseAction(Animator anim)
    {
        PlaySelectCard(anim);

        yield return new WaitForSecondsRealtime(1.0f);
        CloseCardSelect();
    }

    public void ShowCards()
    {
        List<CardData> instanceCards = CardDataLoad();
        for (int i = 0; i < instanceCards.Count; i++)
        {
            currentCards[i].SetActive(true);
            SetCard(currentCards[i], instanceCards[i], cardAnims[i]);
        }
        CardPanel.SetActive(true);
        CardNotClickPanel.SetActive(false);
        CardViewAnim.SetTrigger("ShowCard");
    }


    /// <summary>
    /// 선택한 이후는, 모든 카드들이 비활성화가 되어 전투한다.
    /// 추후 여러 UI가 생긴다면, 카드 뿐만 아니라 상위의 오브젝트로 정의 후 그것 역시 비활성화 해야한다.
    /// </summary>
    void CloseCardSelect()
    {
        Debug.Log("카드 닫음");
        for (int i = 0; i < currentCards.Length; i++)
        {
            currentCards[i].SetActive(false);
        }
        CardPanel.SetActive(false);
        Time.timeScale = 1.0f;

    }


    void CardEvent(string cardName)
    {

        CardData instance = dupliCardData.Find(CardData => CardData.cardName == cardName);
        if (cardName != "Random")
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
        UI_Manager.instance.UpdateCardSelectLog(instance.SelectIcon);

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
                StatManager.instance.playerData.SetObjectMoveSpeedMultify(playerMoveSpeed * (1 + StatManager.instance.randomAddMoveSpeed));
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

    public void PlaySelectCard(Animator anim)
    {
        /*var main = ps.main;
        main.useUnscaledTime = true;*/
        anim.SetTrigger("CardClick");
        Debug.Log("이펙트 실행됨" + anim.gameObject.name);
        CardViewAnim.SetTrigger("CloseCard");
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
        StatManager.instance.p_projectileSizeMultify = 1.5f;
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
        StatManager.instance.p_skillDamageMultify += 0.3f;
    }
}