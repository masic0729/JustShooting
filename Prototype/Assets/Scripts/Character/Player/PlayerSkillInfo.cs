using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillInfo : MonoBehaviour
{
    //�÷��̾� �������� �ߵ��ϴ� �͵��̹Ƿ� �浹 �̺�Ʈ ó���� ���� �÷��̾ �������� �� ��
    [Header("�÷��̾ �� �Ӽ��� ��ų�� ����ϱ� ���� ��ɵ��� ����")]
    Player player;
    public GameObject WindPuller;

    private void Awake()
    {
        //�÷��̾� ��ũ��Ʈ �ҷ�����
        player = this.gameObject.GetComponent<Player>();
    }

    public void WindSkill()
    {
        //������ �� �Ѿ��� ����ϰ�, ����ź���� �߻��Ѵ�.(��� ��� �� ����ź �߻�)
        GameObject instance =  Instantiate(WindPuller, transform.position, transform.rotation);
        instance.transform.parent = player.transform; //�÷��̾ ����
    }

    public void IceSkill()
    {
        //�÷��̾�� ��ȣ���� �ִ� ��(�ý��� �� 10�ʰ�).
    }

    public void FireSkill()
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
