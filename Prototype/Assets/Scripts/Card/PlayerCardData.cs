using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct PlayerCardData
{
    //ī�� ������ Ÿ��
    public enum CardType
    {
        Weapon,             //�÷��̾� ���� ��ȭ
        Skill               //�÷��̾� ��� �ο� �� ��ȭ
    }

    //ī�� �̸�, ����, ī��������, ī�� ������, ��� �ο�
    public Sprite background;               //ī�� ������
    public Sprite icon;                     //������
    public string cardName;                 //ī�� �̸�
    public string description;              //ī�� ����
    public int currentLevel;                //ī���� ����
    public int maxLevel;                    //�ִ� ����
    public bool isSkill;                    //��ųī�� ����

    
}