using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillInfo : MonoBehaviour
{
    //�÷��̾� �������� �ߵ��ϴ� �͵��̹Ƿ� �浹 �̺�Ʈ ó���� ���� �÷��̾ �������� �� ��
    [Header("�÷��̾ �� �Ӽ��� ��ų�� ����ϱ� ���� ��ɵ��� ����")]
    Player player;

    private void Awake()
    {
        //�÷��̾� ��ũ��Ʈ �ҷ�����
        player = this.gameObject.GetComponent<Player>();
    }

    public void ActiveWind()
    {
        
    }

    public void ActiveIced()
    {
        //�÷��̾�� ��ȣ���� �ִ� ��(�ý��� �� 10�ʰ�).

    }

    public void ActiveFire()
    {
        //���ǰ� 50��
    }

    /*
     * ���� ������ Ȯ���� ����� ���� ���̹Ƿ� ����
    public void LightningSkill()
    {
        
    }
    */

    //void 
}
