using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 엔드 보스 클래스.
/// Boss 클래스를 상속하며, 최종 보스 여부에 따른 게임 종료 이벤트 처리 포함.
/// </summary>
public class EndBoss : Boss
{
    [SerializeField]
    // 최종 보스 여부 플래그
    bool isFinalBoss;

    /// <summary>
    /// 초기화 및 부모 Start 호출
    /// </summary>
    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// 매 프레임 업데이트 호출
    /// </summary>
    protected override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// 오브젝트 파괴 시 동작 (현재 내용 없음)
    /// </summary>
    private void OnDestroy()
    {
    }

    /// <summary>
    /// 최종 보스 사망 시 게임 승리 처리
    /// </summary>
    protected void FinalEndBossDeath()
    {
        GameManager.instance.GameEnd(UI_Manager.ScreenInfo.Win);
    }

    /// <summary>
    /// 초기 설정 및 이벤트 구독
    /// </summary>
    protected override void Init()
    {
        base.Init();

        if (isFinalBoss == true)
        {
            OnCharacterDeath += FinalEndBossDeath; // 최종 보스일 경우 승리 이벤트 연결
        }
        else
        {
            OnCharacterDeath += StageClearAction; // 일반 보스 클리어 이벤트
            OnCharacterDeath += RestartWave;      // 웨이브 재시작 (추후 제거 예정)
        }
    }

    /// <summary>
    /// 스테이지 클리어 시 추가 동작 (맵 변경, 포탈 생성 등)
    /// </summary>
    void StageClearAction()
    {
        Debug.Log("스테이지 클리어. 클리어 이후 맵 변경, 몬스터 데이터, 포탈 생성 등 다양한 기능 추가 요구");
    }

    /// <summary>
    /// 충돌 처리, 부모 클래스 호출
    /// </summary>
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
