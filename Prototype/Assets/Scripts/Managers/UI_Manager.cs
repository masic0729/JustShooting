using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class UI_Manager : MonoBehaviour
{
    public enum ScreenInfo
    {
        Win,
        Lose,
        Pause
    }

    public static UI_Manager instance; // 싱글톤 인스턴스

    public GameObject CountImage; // 카운트다운 이미지 그룹
    [SerializeField]
    private GameObject[] GameScreens; // 승패/일시정지 등의 화면 패널들
    Dictionary<string, GameObject> GameScreenName; // 이름으로 각 패널에 접근하기 위한 딕셔너리

    [Header("PlayerWeapon")]
    public GameObject[] currentWeapon; // 현재 무기 UI 표시
    public GameObject[] nextWeapon;    // 다음 무기 UI 표시

    public GameObject[] hpIcons; // 플레이어 HP 아이콘들

    [Header("BossUI_Info")]
    public GameObject bossHp; // 보스 HP UI 패널
    //public GameObject lightningUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }



    void Init()
    {
        GameScreenName = new Dictionary<string, GameObject>();
        for (int i = 0; i < GameScreens.Length; i++)
        {
            GameScreenName[GameScreens[i].name] = GameScreens[i]; // 이름을 기반으로 딕셔너리화
        }
        bossHp.SetActive(false); // 보스 UI는 처음엔 비활성화
    }

    // 게임 종료 화면(Pause, Win, Lose) 출력
    public void ShowScreen(ScreenInfo state)
    {
        GameScreenName[state.ToString()].SetActive(true);
        Time.timeScale = 0.0f; // 게임 일시정지
    }

    // 무기 변경 시 UI 표시 갱신
    public void UpdateWeaponUI(string bulletType)
    {
        for (int i = 0; i < currentWeapon.Length; i++)
        {
            currentWeapon[i].SetActive(false);
            nextWeapon[i].SetActive(false);
        }

        switch (bulletType)
        {
            case "Wind":
                currentWeapon[0].SetActive(true);
                nextWeapon[1].SetActive(true);
                break;
            case "Iced":
                currentWeapon[1].SetActive(true);
                nextWeapon[2].SetActive(true);
                break;
            case "Fire":
                currentWeapon[2].SetActive(true);
                nextWeapon[0].SetActive(true);
                break;
                /*case "Lightning":
                    lightningUI.SetActive(true);
                    break;*/
        }
    }

    // 플레이어의 HP 수치에 따라 UI 아이콘 활성화
    public void UpdatePlayerHP(float hpValue)
    {
        for (int i = 0; i < hpIcons.Length; i++)
        {
            hpIcons[i].SetActive(false);
        }

        // 실제 HP 수만큼 아이콘을 다시 활성화
        for (int i = 0; i < Mathf.Clamp((int)hpValue, 0, hpIcons.Length); i++)
        {
            hpIcons[i].SetActive(true);
        }
    }

    // 시작 시 카운트다운 연출
    public IEnumerator CountingStart()
    {
        Time.timeScale = 0;
        CountImage.SetActive(true);
        GameObject currentImage;

        for (int i = 0; i < CountImage.transform.childCount; i++)
        {
            if (i < CountImage.transform.childCount - 1)
            {
                AudioManager.Instance.PlaySFX("CountSound");
            }
            else
            {
                AudioManager.Instance.PlaySFX("StartSound");
            }

            currentImage = CountImage.transform.GetChild(i).gameObject;

            currentImage.SetActive(true);
            yield return new WaitForSecondsRealtime(1f);
            currentImage.SetActive(false);
        }

        GameManager.instance.gameState = GameManager.GameState.Play; // 게임 시작
        Time.timeScale = 1;
    }

    // 보스 체력바 출력 및 이름 표시
    public void ShowBossHp(string bossName)
    {
        bossHp.SetActive(true);
        bossHp.GetComponentInChildren<TextMeshProUGUI>().text = bossName;
    }

    // 보스 체력바 숨기기
    public void HideBossHp()
    {
        bossHp.SetActive(false);
    }
}
