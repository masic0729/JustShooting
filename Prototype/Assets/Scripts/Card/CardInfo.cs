using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Card Data", menuName = "Scriptable Object/Card Data")]
public class CardInfo : ScriptableObject
{
    public PlayerCardData cardData;
    

    /// <summary>
    /// �� ī���� ��ɹߵ�
    /// </summary>
    public void CardAction()
    {
        Debug.Log("������ ������!!");
    }
}
