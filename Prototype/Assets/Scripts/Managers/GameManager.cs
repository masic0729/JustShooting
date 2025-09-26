using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤 인스턴스
    [SerializeField] int stage = 1;
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
        /*if(Input.GetKeyDown(KeyCode.Escape) && isGameEnd == false)
        {
            UI_Manager.instance.ShowScreen(UI_Manager.ScreenInfo.Pause);
        }*/

        // 게임 중 ESC를 누르면 일시정지 화면 표시
        if (Input.GetKeyDown(KeyCode.Escape) && gameState == GameState.Play)
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

    public void StageUp()
    {
        stage++;
    }

    // 종료 연출 (1초 대기 후 화면 전환 + 배경음 변경)
    IEnumerator EndStart(UI_Manager.ScreenInfo enumState)
    {
        yield return new WaitForSeconds(1f);
        UI_Manager.instance.ShowScreen(enumState); // 종료 화면 출력
        //isGameEnd = true;
        gameState = GameState.End;
        AudioManager.Instance.PlayBGM("Soft1"); // 부드러운 배경음 전환
    }
}
