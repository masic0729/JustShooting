using System.Collections;
using UnityEngine;
using static Stove.PCSDK.Base;
using static Stove.PCSDK.GameSupport;
using UnityEngine.UI;

public class StovePCSDK3Manager1 : MonoBehaviour
{
    public static StovePCSDK3Manager1 instance;

    private StovePCInitializeParam initParam;
    private OnModifyStatFinished onModifyStatFinished;
    private bool hasTriggeredTestAchievement = false;

    public GameObject baseResult;
    public GameObject gameResult;
    public Text logText;

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
            gameId = "GM-2783-6964EF6A_IND",
            applicationKey = "7efc3ef1c4b4b420fdbfd4e0e466590673dbeedfbdc255467b12104debad1db1"
        };
        logText.text = "준비중";

        Base_RestartAppIfNecessaryAsync(initParam, 30000, (CallbackResult callbackResult, bool restartAppIfNecessary) =>
        {
            if (restartAppIfNecessary)
            {
                logText.text = ("스토브 런처로 실행해야 합니다.");
                Application.Quit();
                return;
            }

            Base_Initialize(initParam, (CallbackResult callbackResult2) =>
            {
                if (!callbackResult2.result.IsSuccessful())
                {
                    logText.text = ($"Base SDK 초기화 실패: {callbackResult2.result}");
                    return;
                }

                logText.text = ("Base SDK 초기화 성공");
                baseResult.SetActive(true);
                InitializeGameSupportAndTriggerTest();
            });
        });
    }

    private void InitializeGameSupportAndTriggerTest()
    {
        var result = GameSupport_Initialize();

        // SDK 버전에 따라 result.IsSuccessful() 형태일 수도 있음
        if (!result.IsSuccessful())
        {
            logText.text = ($"GameSupport SDK 초기화 실패: {result}");
            return;
        }

        logText.text = ("GameSupport SDK 초기화 성공");
        gameResult.SetActive(true);

        onModifyStatFinished = OnModifyStatFinishedCallback;
        TriggerTestAchievementOnce();
    }

    private void TriggerTestAchievementOnce()
    {
        if (hasTriggeredTestAchievement)
            return;

        hasTriggeredTestAchievement = true;

        GameSupport_ModifyStat("TEST_0306", 1, onModifyStatFinished);
        logText.text = ("테스트 업적용 Stat 갱신 요청 보냄");
    }

    private void OnModifyStatFinishedCallback(CallbackResult callbackResult, StovePCModifyStatValue stat)
    {
        if (callbackResult.result.IsSuccessful())
        {
            logText.text = ("Stat 갱신 성공");
        }
        else
        {
            logText.text = ($"Stat 갱신 실패: {callbackResult.result}");
        }
    }
}