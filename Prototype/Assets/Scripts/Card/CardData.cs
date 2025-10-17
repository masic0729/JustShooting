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

    public CardType cardType = CardType.Common;             //ī�� Ÿ��
    public Sprite cardFrame;                                //ī�� ������
    public Sprite icon;                                     //ī�� ������
    public Sprite SelectIcon;                               //ī�� ���� Ȯ�� ������       
    public string cardName;                                 //ī�� �̸�
    public string showCardName;                             //ī�� �̸�
    public string description;                              //ī�� ����

}
