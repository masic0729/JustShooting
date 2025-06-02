using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Card Data", menuName = "Scriptable Object/Card Data")]
public class CardInfo : ScriptableObject
{
    public PlayerCardData cardData;
    

    /// <summary>
    /// 각 카드의 기능발동
    /// </summary>
    public void CardAction()
    {
        Debug.Log("굉장해 엄정나!!");
    }
}
