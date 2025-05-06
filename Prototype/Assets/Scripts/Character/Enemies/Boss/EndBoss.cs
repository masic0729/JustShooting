using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBoss : Boss
{
    [SerializeField]
    bool isFinalBoss; //현재 개발 또는 테스트버전의 마지막 엔드보스 여부

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

    //보스가 죽었으므로 게임 승리
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
        Debug.Log("스테이지 클리어. 클리어 이후 맵 변경, 몬스터 데이터, 포탈 생성 등 다양한 기능 추가 요구");
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

    }
}
