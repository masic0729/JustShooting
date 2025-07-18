using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Card Data", menuName = "Scriptable Object/Card Data")]
public class CardInfo : ScriptableObject
{
    // cardData 변수 선언: 카드의 상세 데이터가 저장됨
    //public PlayerCardData cardData;
    /// <summary>
    /// 카드의 고유 타입 정의 (무기 강화, 스킬 강화)
    /// </summary>
    public enum CardType
    {
        Random,     // 플레이어 무기 강화
        Common       // 플레이어 기술 부여 및 강화
    }
    public CardType cardType = CardType.Common;
    // 카드 프레임 이미지
    public Sprite background;
    // 카드 아이콘 이미지
    public Sprite icon;
    // 카드 이름
    public string cardName;
    public string showCardName;
    // 카드 설명 텍스트
    public string description;
    // 현재 카드 레벨
    //public int currentLevel;
    // 카드 최대 레벨
    //public int maxLevel;
    // 스킬 카드 여부 (true면 스킬 카드)
    //public bool isSkill;

    
}
