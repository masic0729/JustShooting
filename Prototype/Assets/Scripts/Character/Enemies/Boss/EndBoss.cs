using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBoss : Boss
{
    [SerializeField]
    bool isFinalBoss; //���� ���� �Ǵ� �׽�Ʈ������ ������ ���庸�� ����

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnDestroy()
    {
        
    }

    //������ �׾����Ƿ� ���� �¸�
    protected void FinalEndBossDeath()
    {
        GameManager.instance.GameEnd(UI_Manager.ScreenInfo.WinScreen);
    }

    protected override void Init()
    {
        base.Init();
        if(isFinalBoss == true)
        {
            OnCharacterDeath += FinalEndBossDeath;
        }
        else
        {
            OnCharacterDeath += StageClearAction;
        }
    }

    void StageClearAction()
    {
        Debug.Log("�������� Ŭ����. Ŭ���� ���� �� ����, ���� ������, ��Ż ���� �� �پ��� ��� �߰� �䱸");
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

    }
}
