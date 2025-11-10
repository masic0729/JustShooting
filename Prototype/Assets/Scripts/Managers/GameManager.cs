using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤 인스턴스
    [SerializeField] Player player;
    [SerializeField] SpriteRenderer fadeScreen;
    [SerializeField] int stage = 0;
    [SerializeField] GameObject[] backgrounds;
    //public bool isGameEnd = false;                                 //승패를 떠나서, 어쨋든 끝났을 때 처리하는 스탠스

    public enum GameState
    {
        Ready, // 준비 상태 (카운트다운 중)
        Play,  // 게임 진행 중
        End    // 게임 종료됨
    }

    public GameState gameState; // 현재 게임 상태

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // 싱글톤 할당
        }
        else
        {
            Destroy(this.gameObject); // 중복 인스턴스 방지
        }
    }

    private void Start()
    {
        gameState = GameState.Ready; // 시작 시 게임 상태는 Ready

        // 카운트 이미지가 존재하면 카운트다운 시작
        if (UI_Manager.instance.CountImage != null)
        {
            StartCoroutine(UI_Manager.instance.CountingStart());
        }
        else
        {
            gameState = GameState.Play; // 카운트 없이 바로 시작
        }
    }

    void Update()
    {
        // 게임 중 ESC를 누르면 일시정지 화면 표시
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale != 0)
        {
            UI_Manager.instance.ShowScreen(UI_Manager.ScreenInfo.Pause);
        }
    }

    // 게임 종료 처리
    public void GameEnd(UI_Manager.ScreenInfo enumState)
    {

        if (gameState != GameState.End)
        {
            //isGameEnd = true;
            gameState = GameState.End;
            StartCoroutine(EndStart(enumState)); // 종료 화면 전환
        }
    }

    public void PlayerWinReAction()
    {
        player.anim.SetTrigger("Win");
    }

    public void StageUp()
    {
        //이곳에 배경 전환 및 화면 페이드 아웃/페이드 인
        StartCoroutine(FadeInOutAction());
    }

    public int GetStage()
    {
        return stage;
    }

    // 종료 연출 (1초 대기 후 화면 전환 + 배경음 변경)
    IEnumerator EndStart(UI_Manager.ScreenInfo enumState)
    {
        yield return new WaitForSeconds(2f);
        UI_Manager.instance.ShowScreen(enumState); // 종료 화면 출력
        //isGameEnd = true;
        gameState = GameState.End;
        AudioManager.Instance.PlayBGM("Soft1"); // 부드러운 배경음 전환
    }
    
    IEnumerator FadeInOutAction()
    {
        for (int i = 0; i <= 100; i++)
        {
            fadeScreen.color = new Vector4(0, 0, 0, i /100f);
            yield return new WaitForSeconds(Time.deltaTime);

        }/*
        fadeScreen.color = new Vector4(0, 0, 0, 1f);*/

        backgrounds[stage].SetActive(false);
        stage++;
        backgrounds[stage].SetActive(true);

/*
 *      //배경 전환. 추후 리소스 받을 시 적용하면 됨
        for (int i = 0; i < backgrounds.Length; i++)
        {
        }*/

        //이곳에스테이지 전환

        for (int i = 100; i >= 0; i--)
        {
            fadeScreen.color = new Vector4(0, 0, 0, i / 100f);
            yield return new WaitForSeconds(Time.deltaTime);

        }
    }
}
