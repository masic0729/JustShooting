using System.Collections;
using UnityEngine;
using static Stove.PCSDK.Base;
using static Stove.PCSDK.GameSupport;

public class BaseSDK : MonoBehaviour
{
    public static BaseSDK instance;

    private StovePCInitializeParam initParam;

    // GameSupport 콜백 보관
    private OnModifyStatFinished onModifyStatFinished;

    // 테스트 중복 방지
    private bool hasTriggeredTestAchievement = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        initParam = new StovePCInitializeParam
        {
            environment = "LIVE",
            gameId = "여기에_실제_GAME_ID",
            applicationKey = "여기에_실제_APP_KEY"
        };

        Base_RestartAppIfNecessaryAsync(initParam, 60000, (CallbackResult callbackResult, bool restartAppIfNecessary) =>
        {
            if (restartAppIfNecessary)
            {
                Debug.Log("스토브 런처로 실행해야 합니다.");
                Application.Quit();
                return;
            }

            Base_Initialize(initParam, (CallbackResult callbackResult2) =>
            {
                if (!callbackResult2.result.IsSuccessful())
                {
                    Debug.LogError("Base SDK 초기화 실패");
                    return;
                }

                Debug.Log("Base SDK 초기화 성공");

                InitializeGameSupportAndTriggerTest();
            });
        });
    }

    private void InitializeGameSupportAndTriggerTest()
    {
        var result = GameSupport_Initialize();

        if (!result.IsSuccessful())
        {
            Debug.LogError("GameSupport SDK 초기화 실패");
            return;
        }

        Debug.Log("GameSupport SDK 초기화 성공");

        // 콜백 등록
        onModifyStatFinished = OnModifyStatFinishedCallback;

        // 게임 시작하자마자 테스트 업적 발동
        TriggerTestAchievementOnce();
    }

    private void TriggerTestAchievementOnce()
    {
        if (hasTriggeredTestAchievement)
            return;

        hasTriggeredTestAchievement = true;

        // TEST_0306 스탯을 1 증가
        GameSupport_ModifyStat("TEST_0306", 1, onModifyStatFinished);

        Debug.Log("테스트 업적용 Stat 갱신 요청 보냄");
    }

    private void OnModifyStatFinishedCallback(CallbackResult callbackResult, StovePCModifyStatValue stat)
    {
        if (callbackResult.result.IsSuccessful())
        {
            Debug.Log($"Stat 갱신 성공. 현재 값: {stat.currentValue}");
        }
        else
        {
            Debug.LogError($"Stat 갱신 실패: {callbackResult.result}");
        }
    }
}