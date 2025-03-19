using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "AttackStats", menuName = "ScriptableObjects/PlayerPower", order = 2)]

public class PlayerPower : MonoBehaviour
{
    Player player;
    [Header("�÷��̾ ���������� �����ϴ� �Ŀ� ���� ������")]
    public Action powerEvents; //�Ŀ��� ���õ� ��� ����� �� �ʸ��� �����Ϸ��� �׼�
    public float power;
    public const float maxPower = 100; //�Ŀ� ��. 100 ���� �����̴�
    public float powerUpValue; //�Ŀ��� ������ ��ġ
    public bool isPowerMax; //�Ŀ��� ��� ȸ���ߴ� �� Ȯ���ϴ� ������
    [Header("�÷��̾ �� �Ӽ��� ��ų�� ����ϱ� ���� ��ɵ��� ����")]
    public GameObject[] SkillObjects;

    void Start()
    {
        GetComponent<Player>().PowerOn();

    }

    /// <summary>
    /// power �ڿ�ȸ���� �����ϱ� ���� ���
    /// </summary>
    public IEnumerator DefaultPowerUp()
    {
        ReStart:

        yield return new WaitForSeconds(1.0f); // �� �ʸ��� ����

        //�Ŀ��� ��� ä������ ������ �Ŀ� ���
        if(isPowerMax == false)
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
        }
        else
        {
            power += value;
            Debug.Log("power : " + power.ToString());
        }
    }
}