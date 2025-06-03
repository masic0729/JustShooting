using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 플레이어 카드 데이터를 저장하는 구조체.
/// 카드 유형, 시각적 요소, 레벨 정보, 설명 등을 포함.
/// </summary>
[System.Serializable]
public struct PlayerCardData
{
    /// <summary>
    /// 카드의 고유 타입 정의 (무기 강화, 스킬 강화)
    /// </summary>
    public enum CardType
    {
        Weapon,     // 플레이어 무기 강화
        Skill       // 플레이어 기술 부여 및 강화
    }

    // 카드 프레임 이미지
    public Sprite background;
    // 카드 아이콘 이미지
    public Sprite icon;
    // 카드 이름
    public string cardName;
    // 카드 설명 텍스트
    public string description;
    // 현재 카드 레벨
    public int currentLevel;
    // 카드 최대 레벨
    public int maxLevel;
    // 스킬 카드 여부 (true면 스킬 카드)
    public bool isSkill;
}
