using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CardData
{
    public enum CardType
    {
        Common = 0,
        Random
    }

    public CardType cardType = CardType.Common;             //카드 타입
    public Sprite cardFrame;                                //카드 프레임
    public Sprite icon;                                     //카드 아이콘
    public Sprite SelectIcon;                               //카드 선택 확인 아이콘       
    public string cardName;                                 //카드 이름
    public string showCardName;                             //카드 이름
    public string description;                              //카드 설명

}
