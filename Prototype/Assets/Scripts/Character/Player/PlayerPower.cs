using System;
using System.Collections;
using UnityEngine;

//[CreateAssetMenu(fileName = "AttackStats", menuName = "ScriptableObjects/PlayerPower", order = 2)]

public class PlayerPower : MonoBehaviour
{
    Player player;
    [Header("플레이어가 독단적으로 소지하는 파워 관련 데이터")]
    public Action powerEvents; //파워와 관련된 모든 기능을 매 초마다 실행하려는 액션
    private float power;
    public float playerPower { get { return power; } set { power = value;  } }
    public const float maxPower = 100; //파워 값. 100 값은 고정이다
    public float powerUpValue; //파워가 오르는 수치
    public bool isPowerMax; //파워가 모두 회복했는 지 확인하는 데이터
    bool isActivedSkill;

    void Start()
    {
        player = GetComponent<Player>();
    }

    /// <summary>
    /// power 자연회복을 실행하기 위한 기능
    /// </summary>
    public IEnumerator DefaultPowerUp()
    {
        ReStart:

        yield return new WaitForSeconds(1.0f); // 매 초마다 실행

        //파워가 모두 채워지지 않거나 채울 수 있는 상황이라면 파워 상승
        if(isPowerMax == false && isActivedSkill == false)
        {
            PowerUp(powerUpValue);
        }
        goto ReStart; //반복
    }

    /// <summary>
    /// power 값이 상승하는 기능. power는 매 초마다 자연 회복하거나, 적을 적중할 때 상승함(현재로선)
    /// </summary>
    public void PowerUp(float value)
    {
        //파워 100오버하면 100으로 조정. 아닐 시 그대로 상승
        if(power + value >= maxPower)
        {
            //power가 100이 되었으므로 max bool 참으로 변경
            power = 100;
            isPowerMax = true;
            player.PowerOn();
            Debug.Log("파워 다 차면 참" + isPowerMax);
        }
        else
        {
            power += value;
        }
    }

    public bool GetIsActivedSkill()
    {
        return isActivedSkill;
    }

    public void SetIsActivedSkill(bool state)
    {
        isActivedSkill = state;
    }
}