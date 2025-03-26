using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

//[CreateAssetMenu(fileName = "AttackStats", menuName = "ScriptableObjects/PlayerPower", order = 2)]

public class PlayerPower : MonoBehaviour
{
    Player player;
    [Header("�÷��̾ ���������� �����ϴ� �Ŀ� ���� ������")]
    public Action powerEvents; //�Ŀ��� ���õ� ��� ����� �� �ʸ��� �����Ϸ��� �׼�
    private float power;
    public float playerPower { get { return power; } set { power = value;  } }
    public const float maxPower = 100; //�Ŀ� ��. 100 ���� �����̴�
    public float powerUpValue; //�Ŀ��� ������ ��ġ
    public bool isPowerMax; //�Ŀ��� ��� ȸ���ߴ� �� Ȯ���ϴ� ������
    bool isActivedSkill;

    void Start()
    {
        player = GetComponent<Player>();

    }

    /// <summary>
    /// power �ڿ�ȸ���� �����ϱ� ���� ���
    /// </summary>
    public IEnumerator DefaultPowerUp()
    {
        ReStart:

        yield return new WaitForSeconds(1.0f); // �� �ʸ��� ����

        //�Ŀ��� ��� ä������ �ʰų� ä�� �� �ִ� ��Ȳ�̶�� �Ŀ� ���
        if(isPowerMax == false && isActivedSkill == false)
        {
            PowerUp(powerUpValue);
        }
        goto ReStart; //�ݺ�
    }

    /// <summary>
    /// power ���� ����ϴ� ���. power�� �� �ʸ��� �ڿ� ȸ���ϰų�, ���� ������ �� �����(����μ�)
    /// </summary>
    public void PowerUp(float value)
    {
        //�Ŀ� 100�����ϸ� 100���� ����. �ƴ� �� �״�� ���
        if(power + value >= maxPower)
        {
            //power�� 100�� �Ǿ����Ƿ� max bool ������ ����
            power = 100;
            isPowerMax = true;
            player.PowerOn();
            Debug.Log("�Ŀ� �� ���� ��" + isPowerMax);
        }
        else
        {
            power += value;
            Debug.Log("power : " + power.ToString());
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