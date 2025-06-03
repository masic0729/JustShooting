using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card Data", menuName = "Scriptable Object/Card Data")]
public class CardInfo : ScriptableObject
{
    // cardData 변수 선언: 카드의 상세 데이터가 저장됨
    public PlayerCardData cardData;

    /// <summary>
    /// 카드가 발동할 때 실행되는 기능
    /// </summary>
    // CardAction 함수: 카드의 주요 동작을 수행하는 함수
    public void CardAction()
    {
        Debug.Log("굉장해 엄정나!!"); // 카드 액션 실행 시 콘솔 출력
    }
}
