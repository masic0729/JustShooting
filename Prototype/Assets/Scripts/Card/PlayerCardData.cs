using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct PlayerCardData
{
    //카드 고유의 타입
    public enum CardType
    {
        Weapon,             //플레이어 무기 강화
        Skill               //플레이어 기술 부여 및 강화
    }

    //카드 이름, 설명, 카드프레임, 카드 아이콘, 기능 부여
    public Sprite background;               //카드 프레임
    public Sprite icon;                     //아이콘
    public string cardName;                 //카드 이름
    public string description;              //카드 설명
    public int currentLevel;                //카드의 레벨
    public int maxLevel;                    //최대 레벨
    public bool isSkill;                    //스킬카드 유무

    
}