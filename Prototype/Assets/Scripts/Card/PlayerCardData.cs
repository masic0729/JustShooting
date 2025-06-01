using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardData
{
    public string cardName;                 //카드 이름
    public Sprite background;               //카드 프레임
    public Sprite icon;                     //아이콘
    public string description;              //카드 설명
    public int currentLevel;                //카드의 레벨
    public int maxLevel;                    //최대 레벨
    public bool isSkill;                    //스킬카드 유무

}